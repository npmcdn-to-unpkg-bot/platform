namespace Allors.Workspace.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Data;

    public class Database
    {
        public Database(string baseAddress)
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        ~Database()
        {
            this.Client.Dispose();
        }

        public HttpClient Client { get; set; }

        public async Task<PullResponse> pull(string name, Dictionary<string, string> values) {

            var content = new FormUrlEncodedContent(values);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(name + "/pull", UriKind.Relative),
                Method = HttpMethod.Post,
                Content = content
            };
            request.Headers.ExpectContinue = false;

            var response = await this.Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var pullResponse = await response.Content.ReadAsAsync<PullResponse>();
            return pullResponse;
        }

        public async Task<SyncResponse> sync(SyncRequest syncRequest)
        {

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("Database/Sync", UriKind.Relative),
                Method = HttpMethod.Post
            };
            request.Headers.ExpectContinue = false;

            var response = await this.Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var syncResponse = await response.Content.ReadAsAsync<SyncResponse>();
            return syncResponse;
        }

        public async Task<PushResponse> push(PushRequest pushRequest)
        {
            var uri = new Uri("Database/Push", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, pushRequest);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var pushResponse = await response.Content.ReadAsAsync<PushResponse>();
            return pushResponse;
        }

        public async Task<InvokeResponse> invoke(Method method)
        {
            var invokeRequest = new InvokeRequest
                                    {
                                        i = method.Object.id.ToString(),
                                        v = method.Object.version.ToString(),
                                        m = method.Name,
                                    };

            var uri = new Uri("Database/Invoke", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, invokeRequest);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var invokeResponse = await response.Content.ReadAsAsync<InvokeResponse>();
            return invokeResponse;
        }

        public async Task<InvokeResponse> invoke(string service, Dictionary<string, string> values)
        {
            var content = new FormUrlEncodedContent(values);
            var uri = new Uri(service, UriKind.Relative);
            var response = await this.Client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var invokeResponse = await response.Content.ReadAsAsync<InvokeResponse>();
            return invokeResponse;
        }
    }
}