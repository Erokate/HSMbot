using HSMbot.Entities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HSMbot.Entities
{
    public class NekoApi
    {
        public class NekoApiClient
        {
            private readonly HttpClient _httpClient;
            private const string _baseUrl = "https://nekobot.xyz/api/";

            public NekoApiClient(HttpClient httpClient)
            {
                this._httpClient = httpClient;
            }

            public NekoApiClient()
            {
                this._httpClient = new HttpClient();
            }

            public async Task<NekoApiImage> GetNekoApiImageAsync(ImageType imageType)
            {
                var fullUrl = _baseUrl + "imagegen?type=" + imageType.ToString().ToLower();
                var json = await _httpClient.GetStringAsync(fullUrl);
                return JsonConvert.DeserializeObject<NekoApiImage>(json);
            }
        }

        public enum ImageType
        {
            HAss,
            HMidRiff,
            PGif,
            FourK,
            Hentai,
            Holo,
            HNeko,
            Neko,
            HKitsune,
            Kemonomimi,
            Anal,
            GoneWild,
            Kanna,
            Ass,
            Pussy,
            Thigh,
            HThigh,
            Gah,
            Coffee,
            Food,
            Paizuri,
            Tentacle,
            Boobs,
            HBoobs,
            Yaoi,
            Cosplay,
            Swimsuit,
            Pantsu,
            Nakadashi
        }

    }
}
