using Common.Extensions.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;


namespace Common.Extensions.Net.Http
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHttpClient(this IServiceCollection services,HttpOptions options)
        {
            services.AddHttpClient();

         var httpClientBuilder=services.AddHttpClient(options.Name, client =>
            {
                client.BaseAddress = new Uri(options.BaseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "text/plain");
                client.Timeout = TimeSpan.FromMilliseconds(options.Timeout); 
                if (options.GetAuthorization != null)
                {
                   var httpResponse= client.Send(options.GetAuthorization);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string str = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + str);
                    }
                }
            });
            
            // 是否启动重试
            if (options.EnablePolicy == true)
            {
               var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                // Handle HTTP 5xx errors or HTTP 408 requests  状态码这几个重试
                 .WaitAndRetryAsync(
                 retryCount: options.RetryCount, // 重试次数
                 sleepDurationProvider: attempt => TimeSpan.FromSeconds(options.SleepDuration), // 重试间隔时间
                 onRetry: (outcome, timespan, retryAttempt, context) =>
                 {
                     options.policCallback?.Invoke(outcome.Result);
                 });
                httpClientBuilder.AddPolicyHandler(retryPolicy);
            }

            services.AddSingleton<HttpClientFactory>(sp =>
            {
                IHttpClientFactory basehttpFactory = sp.GetService<IHttpClientFactory>();
                HttpClientFactory clientFactory = new HttpClientFactory(options.Name,basehttpFactory);
                return clientFactory;
            });
            return services;
        }
    }
}
