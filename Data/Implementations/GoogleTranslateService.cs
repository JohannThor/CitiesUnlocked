using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net;
using Google.Apis.Translate.v2;
using Google.Apis.Services;
using Translate.WP.TranslateText.Common;

namespace Data.Implementations
{
    public class GoogleTranslateService : Data.Interfaces.ITranslationService
    {

        public string DetectLanguage(string textToDetect)
        {
            return HttpCall("https://www.googleapis.com/language/translate/v2/detect?key=AIzaSyCBLqHVIvFEV6L7qilVXSrk9vs5gu1H4WM",
                new Dictionary<string, string> { { "text", textToDetect } },
                null);
        }

        public string Translate(string textToTranslate, Language targetLang, Language sourceLang)
        {
            return HttpCall("https://www.googleapis.com/language/translate/v2?key=AIzaSyCBLqHVIvFEV6L7qilVXSrk9vs5gu1H4WM",
                        new Dictionary<string, string> {
                            { "text", textToTranslate },
                            { "from", getLanguageCode(sourceLang) },
                            { "to", getLanguageCode(targetLang) } },
                        null
                        );
        }

        private string HttpCall(string baseUri, Dictionary<string, string> queryParameters, Dictionary<string, string> headers)
        {
            string uri = baseUri;
            if (queryParameters != null && queryParameters.Count > 0)
            {
                uri += "&q=";

                int formatCounter = 0;
                foreach (var key in queryParameters.Keys)
                {
                    if (formatCounter == 1)
                    {
                        uri += "&source=";
                    }
                    else if (formatCounter == 2)
                    {
                        uri += "&target=";
                    }

                    uri += String.Format("{0}", queryParameters[key]);

                    formatCounter++;
                }
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

            System.Net.WebResponse response = null;
            try
            {
                var aResult = httpWebRequest.GetResponseAsync();
                aResult.Wait();
                response = aResult.Result;
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    return (string)dcs.ReadObject(stream);
                }
            }
            catch
            {
                //TODO: Logging
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                    response = null;
                }
            }
        }

        private static string GenerateTranslateOptionsRequestBody(string category, string contentType, string ReservedFlags, string State, string Uri, string user)
        {
            string body = "<TranslateOptions xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\">" +
    "  <Category>{0}</Category>" +
    "  <ContentType>{1}</ContentType>" +
    "  <ReservedFlags>{2}</ReservedFlags>" +
    "  <State>{3}</State>" +
    "  <Uri>{4}</Uri>" +
    "  <User>{5}</User>" +
    "</TranslateOptions>";
            return string.Format(body, category, contentType, ReservedFlags, State, Uri, user);
        }

        private string getLanguageCode(Language lang)
        {
            switch (lang)
            {
                case Language.AmericanEnglish:
                case Language.BritishEnglish:
                    return "en";
                case Language.Chinese:
                    return "ch";
                case Language.Spanish:
                    return "es";
            }
            throw new Exception("Language not supported");
        }
    }
}
