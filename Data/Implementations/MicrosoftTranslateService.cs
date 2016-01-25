using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net;

namespace Data.Implementations
{
    public class MicrosoftTranslateService : Data.Interfaces.ITranslationService
    {
        public MicrosoftTranslateService()
        {

        }

        public string Translate(string textToTranslate, Language targetLang, Language sourceLang)
        {
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + textToTranslate + "&from=" + getLanguageCode(sourceLang) + "&to=" + getLanguageCode(targetLang);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            AdmAuthentication auth = new AdmAuthentication("UCL-Cities-Unlocked-Test", "sn9Jqb+5CB7vTjyqDI9dT2gR+ipwxeoJZdwDlhi7ZDk=");
            var authToken = auth.GetAccessToken();
            httpWebRequest.Headers["Authorization"] = "Bearer " + authToken.access_token;
            //httpWebRequest.ContentType = "text/xml";
            //httpWebRequest.Method = "POST";
            //var asyncResult = httpWebRequest.GetRequestStreamAsync();
            //asyncResult.Wait();
            //using (System.IO.Stream stream =  asyncResult.Result)
            //{
            //    var requestBody = GenerateTranslateOptionsRequestBody("general", "text/plain", "", "", "", "");
            //    byte[] arrBytes = System.Text.Encoding.UTF8.GetBytes(requestBody);
            //    stream.Write(arrBytes, 0, arrBytes.Length);
            //}
            System.Net.WebResponse response = null;
            try
            {
                var aResult = httpWebRequest.GetResponseAsync();
                aResult.Wait();
                response = aResult.Result;
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    return  (string)dcs.ReadObject(stream);

                }
            }
            catch
            {
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
            switch (lang) {
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
