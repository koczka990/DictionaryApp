using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DictionaryApp.Core.Models;
using Newtonsoft.Json;

namespace DictionaryApp.Core.Services
{
    // This class provides a wrapper around common functionality for HTTP actions.
    // Learn more at https://docs.microsoft.com/windows/uwp/networking/httpclient
    public class HttpDataService
    {
        private readonly Dictionary<string, object> responseCache;
        private HttpClient client;
        const string API_KEY = "dict.1.1.20220505T150713Z.6f6ae4bb7c0ea354.0731fdcbff1249c030cd7ea0e953956bd23ea0c3";

        public HttpDataService(string defaultBaseUrl = "")
        {
            client = new HttpClient();

            if (!string.IsNullOrEmpty(defaultBaseUrl))
            {
                client.BaseAddress = new Uri($"{defaultBaseUrl}/");
            }

            responseCache = new Dictionary<string, object>();
        }

        public async Task<T> GetAsync<T>(string uri, string accessToken = null, bool forceRefresh = false)
        {
            T result = default;

            // The responseCache is a simple store of past responses to avoid unnecessary requests for the same resource.
            // Feel free to remove it or extend this request logic as appropraite for your app.
            if (forceRefresh || !responseCache.ContainsKey(uri))
            {
                AddAuthorizationHeader(accessToken);
                var json = await client.GetStringAsync(uri);
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(json));

                if (responseCache.ContainsKey(uri))
                {
                    responseCache[uri] = result;
                }
                else
                {
                    responseCache.Add(uri, result);
                }
            }
            else
            {
                result = (T)responseCache[uri];
            }

            return result;
        }

        //private readonly string serverUrl = "https://dictionary.yandex.net/api/v1/dicservice.json";

        public async Task<List<string>> GetListOfLanguages()
        {
            return await GetAsync<List<string>>("getLangs?key=" + API_KEY);
        }

        public async Task<LookUp> LookupTranslation(string lang, string text)
        {
            return await GetAsync<LookUp>($"lookup?key={API_KEY}&lang={lang}&text={text}");
        }

        // Add this to all public methods
        private void AddAuthorizationHeader(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = null;
                return;
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
