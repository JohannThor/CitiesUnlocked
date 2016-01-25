using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Data.Implementations
{
    [DataContract]
    public class AdmAccessToken
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string expires_in { get; set; }
        [DataMember]
        public string scope { get; set; }
    }
    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string clientId;
        private string clientSecret;
        private string request;
        private AdmAccessToken token;
        private Common.Timer accessTokenRenewer;
        //Access token expires every 10 minutes. Renew it every 9 minutes only.
        private const int RefreshTokenDuration = 9;
        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", Uri.EscapeDataString(clientId), Uri.EscapeDataString(clientSecret));
            var asyncResult = HttpPost(DatamarketAccessUri, this.request);
            asyncResult.Wait();
            this.token = asyncResult.Result;
            //renew the token every specfied minutes
            accessTokenRenewer = new Common.Timer(new Common.TimerCallback(OnTokenExpiredCallback), this, RefreshTokenDuration, -1);
        }
        public AdmAccessToken GetAccessToken()
        {
            return this.token;
        }
        private void RenewAccessToken()
        {
            var asyncResult = HttpPost(DatamarketAccessUri, this.request);
            asyncResult.Wait(); // TODO: decide how far we want async to propogate
            AdmAccessToken newAccessToken = asyncResult.Result;
            //swap the new token with old one
            //Note: the swap is thread unsafe
            this.token = newAccessToken;
        }
        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (System.Net.WebException ex)
            {
            }
            finally
            {
                try
                {
                    accessTokenRenewer = new Common.Timer(new Common.TimerCallback(OnTokenExpiredCallback), this, RefreshTokenDuration, -1);
                }
                catch (Exception ex)
                {
                }
            }
        }
        private async Task<AdmAccessToken> HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.UTF8.GetBytes(requestDetails);
            // webRequest.ContentLength = bytes.Length;
            using (System.IO.Stream outputStream = await webRequest.GetRequestStreamAsync())
            {
                outputStream.Write(bytes, 0, bytes.Length);
                outputStream.Flush();
            }
            using (System.Net.WebResponse webResponse = await webRequest.GetResponseAsync())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }
    }
}
