using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stromming.Streamers {

    /// <summary>
    /// Sample URL; https://e38fd90mob-dsn.algolia.net/1/indexes/*/queries?x-algolia-agent=Algolia%20for%20vanilla%20JavaScript%20(lite)%203.21.1%3Binstantsearch.js%201.11.7%3BJS%20Helper%202.19.0&x-algolia-application-id=E38FD90MOB&x-algolia-api-key=3f56a452156f1a76c8939af1798a2335
    /// </summary>
    public class SfAnytimeStreamer : IStreamer {

        private const string uri = "https://e38fd90mob-dsn.algolia.net/1/indexes/*/queries?x-algolia-agent=Algolia%20for%20vanilla%20JavaScript%20(lite)%203.21.1%3Binstantsearch.js%201.11.7%3BJS%20Helper%202.19.0&x-algolia-application-id=E38FD90MOB&x-algolia-api-key=3f56a452156f1a76c8939af1798a2335";

        private UriBuilder uriBuilder = new UriBuilder(uri);

        public string Name => "SF Anytime";

        public long Count { get; private set; }

        public void Search(string term) {
            string data = "{\"requests\":[{\"indexName\":\"prod_sfanytime_movies\",\"params\":\"query=" + Uri.EscapeDataString(term) + "&numericFilters=adult%3D0%2C%20available_in_se%3D1&hitsPerPage=60&maxValuesPerFacet=3&page=0&attributesToRetrieve=mediaid%2Cproducttype%2Cproducttypeid%2Ctitle%2Ctitle_sv%2Ctitle_no%2Ctitle_da%2Ctitle_fi%2Ccover_id%2Ccover_no%2Ccover_sv%2Ccover_da%2Ccover_fi&distinct=true&facets=%5B%5D&tagFilters=\"}]}";
            string content = Utils.PostContent(this.uriBuilder.ToString(), data);
            var json = JsonConvert.DeserializeObject<JObject>(content);
            this.Count = json["results"].Value<JArray>().FirstOrDefault()["nbHits"].Value<long>();
        }

    }

}
