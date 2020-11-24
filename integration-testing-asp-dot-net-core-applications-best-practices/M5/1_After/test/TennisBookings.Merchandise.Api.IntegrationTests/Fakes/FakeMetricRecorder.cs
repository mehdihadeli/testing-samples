using System.Collections.Generic;
using TennisBookings.Merchandise.Api.Diagnostics;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Fakes
{
    public class FakeMetricRecorder : IMetricRecorder
    {
        public List<RecordedMetric> Metrics = new List<RecordedMetric>();

        public void RecordMetric(string name, int increment = 1, string[] tags = null)
        {
            Metrics.Add(new RecordedMetric(name, increment, tags));
        }
    }

    public class RecordedMetric
    {
        public RecordedMetric(string name, int increment, string[] tags)
        {
            Name = name;
            Increment = increment;
            Tags = tags;
        }

        public string Name { get; }
        public int Increment { get; }
        public string[] Tags { get; }
    }
}
