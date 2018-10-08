using System;
using System.Data.Services.Client;
using System.Linq;

namespace GraphApiExperiments
{
    public static class DataServiceResponseExtensions
    {
        private static readonly Func<OperationResponse, bool> SuccessStatusCodePredicate =
            r => r.StatusCode >= 200 && r.StatusCode <= 299;

        public static bool IsSuccessStatusCode(this DataServiceResponse response)
        {
            var success = response.All(SuccessStatusCodePredicate);
            return success;
        }

        public static void EnsureSuccessStatusCode(this DataServiceResponse response)
        {
            if (!response.IsSuccessStatusCode())
            {
                var successfulOps = response.Where(SuccessStatusCodePredicate);
                var exceptions = response.Except(successfulOps).Select(r => r.Error);
                throw new AggregateException(exceptions);
            }
        }
    }
}
