namespace Common.Extensions.Net.Http
{
    public  sealed class HttpClientFactory
    {
        /// <summary>
        /// 请求头参数集合
        /// </summary>
        protected IDictionary<string, string> HeaderParams { get; }
        /// <summary>
        /// Cookie参数集合
        /// </summary>
        protected IDictionary<string, string> Cookies { get; }

        protected IHttpClientFactory _httpClient;

        protected string _name;

        public HttpClientFactory(IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
        }
        public HttpClientFactory(string name,IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
            _name = name;
        }


        public HttpClient TryGetInstance(string name="")
        {
            string temp = _name ?? name;
            return _httpClient.CreateClient(temp);
        }

        /// <summary>
        /// 发送josn格式以body
        /// </summary>
        /// <param name="josn"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HttpRequestMessage CreatRequest(string josn, HttpMethod method,Uri uri)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = method;
            httpRequest.Content=new StringContent(josn);
            httpRequest.Headers.Add("Accept", "application/json");
            httpRequest.RequestUri = uri;
            httpRequest.Content.Headers.ContentType=new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return httpRequest;
        }
        public static HttpRequestMessage CreatRequest(Uri uri, HttpMethod method)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = method;
            httpRequest.RequestUri = uri;
            return httpRequest;
        }

        /// <summary>
        /// heat.get
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var client = TryGetInstance();
            var res=  await client.GetAsync(uri);
            return res;
        }
        /// <summary>
        /// header.get
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="josn"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string uri, string josn)
        {
            var client = TryGetInstance();
            var request = CreatRequest(josn, HttpMethod.Get, new Uri(uri));
            var res = await client.SendAsync(request);
            return res;
        }
        /// <summary>
        /// header.post
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async  Task<HttpResponseMessage> PostAsync(string uri)
        {
            var client = TryGetInstance();
            var request = CreatRequest(new Uri(uri), HttpMethod.Post);
            var res= await client.SendAsync(request);
            return res;
        }

        /// <summary>
        /// body.post
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="josn"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string uri,string josn)
        {
            var client = TryGetInstance();
            var request = CreatRequest(josn, HttpMethod.Post,new Uri(uri));
            var res = await client.SendAsync(request);
            return res;
        }
    }
}
