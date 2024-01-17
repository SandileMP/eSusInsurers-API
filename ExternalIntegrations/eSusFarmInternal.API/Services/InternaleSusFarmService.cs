using eSusFarmInternal.API.Interfaces;
using eSusFarmInternal.API.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eSusFarmInternal.API.Services
{
    public class InternaleSusFarmService : IInternaleSusFarmService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheExpiryOptions;
        #endregion

        #region Constructor
        public InternaleSusFarmService(IHttpClientFactory httpClientFactory
                                     , IConfiguration configuration
                                     , IMemoryCache memoryCache)
        {
            _httpClient = httpClientFactory.CreateClient("InternaleSusFarmService");
            _configuration = configuration;
            _cache = memoryCache;
            _cacheExpiryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(_configuration.GetValue<double>("InternaleSusFarmServiceSettings:CacheExpirationInMinutes"))
            };
        }
        #endregion

        public async Task<List<CitiesDto>?> GetCitiesSummary(int provinceId
                                                           , CancellationToken cancellationToken)
        {
            var url = $"manageFarmer/GetCitiesList?provinces={provinceId}";

            var locationSummary = GetValueFromCache(provinceId);

            if (locationSummary.Item1 && !string.IsNullOrEmpty(locationSummary.Item2))
            {
                return JsonConvert.DeserializeObject<List<CitiesDto>>(locationSummary.Item2);
            }

            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new List<CitiesDto>();
            }

            var content = await response.Content.ReadAsStringAsync(CancellationToken.None);

            _cache.Set(provinceId, content, _cacheExpiryOptions);

            var data = JsonConvert.DeserializeObject<List<CitiesDto>>(content);

            return data;
        }

        public async Task<List<ProvincesDto>?> GetProvinceSummary(CancellationToken cancellationToken)
        {
            var url = $"manageFarmer/GetProvinceList";

            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new List<ProvincesDto>();
            }

            var content = await response.Content.ReadAsStringAsync(CancellationToken.None);

            var data = JsonConvert.DeserializeObject<ProvincesSummary>(content);

            return data.data;
        }

        private Tuple<bool, string?> GetValueFromCache(int provinceId)
        {
            var citySummaryDetails = string.Empty;

            var citySummary = _cache.TryGetValue(provinceId, out citySummaryDetails);

            return new Tuple<bool, string?>(citySummary, citySummaryDetails);
        }
    }
}
