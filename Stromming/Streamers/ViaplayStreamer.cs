using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Stromming.Streamers {

    /// <summary>
    /// Sample URL; https://content.viaplay.se/pcdash-se/search?query=mysearchterm
    /// </summary>
    public class ViaplayStreamer : IStreamer {

        private const string uri = "https://content.viaplay.se/pcdash-se/search";

        private UriBuilder uriBuilder = new UriBuilder(uri);

        public string Name => "Viaplay";

        public long Count { get; private set; }

        public void Search(string term) {
            this.uriBuilder.Query = $"query={Uri.EscapeDataString(term)}";
            string content = Utils.GetContent(this.uriBuilder.ToString());
            var json = JsonConvert.DeserializeObject<JObject>(content);
            this.Count = json?["_embedded"]?.Value<JObject>()?["viaplay:blocks"]?.Value<JArray>()?.FirstOrDefault()?["totalProductCount"]?.Value<long>() ?? 0;
        }

    }

}
