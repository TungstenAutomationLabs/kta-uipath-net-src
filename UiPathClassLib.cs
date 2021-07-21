using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UiPathClassLib
{
    public class UiPathClient
    {
        //public string ExtAppId { get; set; }
        //public string ExtAppSecret { get; set; }
        //public string UserKey { get; set; }
        //public string ClientId { get; set; }

        private const string LOGIN_ENDPOINT = "api/Account/Authenticate";
        private const string LOGIN_URL = "https://account.uipath.com/oauth/token";
        private const string EXTAPP_LOGIN_URL = "https://cloud.uipath.com/identity_/connect/token";

        static UiPathClient()
        {
            // UiPath requires TLS 1.1 or 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }

        public string GetExtAppAuthToken(string extAppId, string extAppSecret, string apiScopes)
        {
            var client = new HttpClient();

            var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", extAppId },
                    {"client_secret", extAppSecret },
                    {"scopes", apiScopes }
                }
            );

            var response = client.PostAsync(EXTAPP_LOGIN_URL, request).Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
            string authToken = values["access_token"];
            return authToken;
        }

        public string GetAuthToken(string clientId, string userKey)
        {
            var client = new HttpClient();
            var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},
                    {"client_id", clientId },
                    {"refresh_token", userKey }
                }
            );

            request.Headers.Add("X-PrettyPrint", "1");

            var response = client.PostAsync(LOGIN_URL, request).Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            //Console.WriteLine($"Response: {jsonResponse}");
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
            string authToken = values["access_token"];
            return authToken;
        }

        
        /*
        public string GetRobotId(string authToken, string instanceUrl, string robotUrl)
        {
            //robot url = odata/Robots?$filter=Name
            //robot url = robot url + robit ID + "'&$top=1"
            string url = instanceUrl + robotUrl;
            var client = new HttpClient();

            var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"Authorization", authToken}
                }
            );

            request.Headers.Add("X-PrettyPrint", "1");
            var response = client.PostAsync(url, request).Result;

            JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            string jobId = (string)body["value"][0]["Id"];
            return jobId;
        }

        /*

        public string GetReleaseKey(string authToken, string processUrl)
        {
            string url = Url + processUrl;
            var client = new HttpClient();

            var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"Authorization", authToken}
                }
            );

            request.Headers.Add("X-PrettyPrint", "1");
            var response = client.PostAsync(url, request).Result;

            JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            string jobId = (string)body["value"][0]["Key"];
            return jobId;
        }

        public string[] GetUiPathJobDetails(string authToken, string jobsData)
        {
            string url = Url + jobsData;
            var client = new HttpClient();

            var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"Authorization", authToken}
                }
            );

            request.Headers.Add("X-PrettyPrint", "1");
            var response = client.PostAsync(url, request).Result;

            //string jsonResponse = response.Content.ReadAsStringAsync().Result;
            JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            string jobState = (string)body["value"][0]["State"];
            string jobInfo = (string)body["value"][0]["Info"];
            string[] jobDetails = { jobState, jobInfo };
            return jobDetails;
        }
        */
    }
}
