using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NeilRamsbottom.Zaptec.Charger.Api.Models;

namespace NeilRamsbottom.Zaptec.Charger.Api
{
    public class ZaptecApiClient
    {
        private readonly HttpClient _http;
        private readonly string _username;
        private readonly string _password;
        private ZaptecApiToken _authenticationToken = null;

        public bool IsAuthenticated { get { return _authenticationToken != null && _authenticationToken.ExpiresDate > DateTime.UtcNow; } }

        public ZaptecApiClient(Uri baseUrl, string username, string password)
        {
            _username = username;
            _password = password;
            _http = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }

        public async Task<ZaptecApiToken> Authenticate()
        {
            var authenticationParameters = new Dictionary<string, string>()
            {
                { "grant_type", "password" },
                { "username", _username },
                { "password", _password },
            };

            var request = new HttpRequestMessage(HttpMethod.Get, "/oauth/token")
            {
                Content = new FormUrlEncodedContent(authenticationParameters)
            };

            var response = await _http.SendAsync(request);
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new RFC1123DateTimeConverter());

            if (response.IsSuccessStatusCode)
            {
                using (var responseBodyStream = await response.Content.ReadAsStreamAsync())
                {
                    _authenticationToken = await JsonSerializer.DeserializeAsync<ZaptecApiToken>(responseBodyStream, null);
                }
            }
            return _authenticationToken;
        }

        public async Task<ZaptecChargerState[]> GetChargerState(string chargerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/chargers/{chargerId}/state");
            AppendAuthorizationHeader(request);
            var response = await _http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseBodyStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<ZaptecChargerState[]>(responseBodyStream);
                }
            }
            else
            {
                throw new ZaptecApiException("Failed fetching charger state.");
            }
        }

        public async Task<ZaptecPagedData<ZaptecSessionListModel>> GetZaptecChargingSessionsAsync(string chargerId, DateTime? fromDate = null)
        {
            var url = $"/api/chargehistory?options.chargerId={chargerId}";

            if (fromDate != null)
            {
                url += $"&options.from={fromDate.Value.ToString("s")}";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AppendAuthorizationHeader(request);
            var response = await _http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseBodyStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<ZaptecPagedData<ZaptecSessionListModel>>(responseBodyStream);
                }
            }
            else
            {
                throw new ZaptecApiException("Failed charging session data.");
            }

        }

        private void AppendAuthorizationHeader(HttpRequestMessage request)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                _authenticationToken.AccessToken);
        }
    }
}
