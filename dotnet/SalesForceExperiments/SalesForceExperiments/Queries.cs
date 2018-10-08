namespace SalesForceExperiments
{
    public static class Queries
    {
        public const string Contacts = "select id, name, accountid, email from Contact";
        public const string Accounts = "select id, name from Account";
        public const string Products = "select id, name from Product2";
        public const string Assets = "select id, name, installdate, usageenddate, status, accountid, product2id from Asset";
        public const string QueryPrefix = "/services/data/v31.0/query?q=";
    }
}