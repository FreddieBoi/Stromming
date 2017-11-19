using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Stromming.Streamers {

    /// <summary>
    /// Sample URL; https://www.svtplay.se/api/search?q=mysearchterm
    /// </summary>
    public class SvtPlayStreamer : IStreamer {

        private const string uri = "https://www.svtplay.se/api/search";

        private UriBuilder uriBuilder = new UriBuilder(uri);

        public string Name => "SVT Play";

        public long Count { get; private set; }

        public void Search(string term) {
            this.uriBuilder.Query = $"q={Uri.EscapeDataString(term)}";
            string content = Utils.GetContent(this.uriBuilder.ToString());
            var json = JsonConvert.DeserializeObject<JObject>(content);
            this.Count = json?["totalResults"]?.Value<long>() ?? 0;
        }

    }

}
