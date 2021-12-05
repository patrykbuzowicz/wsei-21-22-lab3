using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wsei.Lab3.Services
{
    public class MetricsCollector : IMetricsCollector
    {
        private readonly List<CollectedRequestModel> _collected = new List<CollectedRequestModel>();

        public void Collect(string httpMethod, string path, int responseCode)
        {
            var collectedRequest = new CollectedRequestModel
            {
                HttpMethod = httpMethod,
                RequestUrl = path,
                ResponseCode = responseCode
            };

            _collected.Add(collectedRequest);
        }

        public IEnumerable<EndpointStats> GetEndpointStats()
        {
            var requestsGrouped = _collected.GroupBy(x => new { x.HttpMethod, x.RequestUrl });
            var requestsCounted = requestsGrouped.Select(x => new EndpointStats
            {
                HttpMethod = x.Key.HttpMethod,
                RequestUrl = x.Key.RequestUrl,
                RequestsCount = x.Count()
            });

            return requestsCounted;
        }
    }

    public class CollectedRequestModel
    {
        public string HttpMethod { get; set; }

        public string RequestUrl { get; set; }

        public int ResponseCode { get; set; }
    }

    public class EndpointStats
    {
        public string HttpMethod { get; set; }

        public string RequestUrl { get; set; }

        public int RequestsCount { get; set; }
    }
}
