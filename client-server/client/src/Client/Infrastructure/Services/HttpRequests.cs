using Domain.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class HttpRequests : IHttpRequests
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequests(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<T> Get<T>(string path)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                path
            );

            httpRequestMessage.Headers.Authorization = SetAuthorizationHeader(token: await CreateAccessToken());

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentStream);
        }

        private async Task<string> CreateAccessToken() => await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        private AuthenticationHeaderValue SetAuthorizationHeader(string token) => new AuthenticationHeaderValue("Bearer", token);
    }
}
