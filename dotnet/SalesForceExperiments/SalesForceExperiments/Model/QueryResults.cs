using System.Collections.Generic;

namespace SalesForceExperiments.Model
{
    public class QueryResults<TRecord>
    {
        public int TotalSize { get; set; }
        public bool Done { get; set; }
        public string NextRecordsUrl { get; set; }
        public List<TRecord> Records { get; set; }
    }
}
