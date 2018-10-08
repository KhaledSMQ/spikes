using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureDataCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
            Console.ReadLine();
        }

        public void Run()
        {
            var token = new AuthToDataCatalog().Authenticate().Result;
            Console.WriteLine($"token = {token}");
            var dc = new DataCatalogHelpers("catalog", token);

            // get all views
            var views = dc.Search(new[]{ "objectType=view" });
            Output(views);

            // get all databases
            var databases = dc.Search(new[]{ "objectType=database" });
            Output(databases);

            // get all tables
            var tables = dc.Search(new[]{ "objectType=table" });
            Output(tables);

            // get all objects in a particular server
            var objectsInServer = dc.Search(new[]{ "dsl.address.server:genentitlements-dev.database.windows.net" });
            Output(objectsInServer);

            // get all SQL Server instances
            var sqlServers = dc.Search(new[]{ "sourceType=\"sql server\"" });
            Output(sqlServers);

            // get all objects with a specific name
            var objectsByName = dc.Search(new[]{ "name:Sku" });
            Output(objectsByName);

            // get all objects with a specific tag
            var objectsWithTag = dc.Search(new[]{ "tags:tag2" });
            Output(objectsWithTag);

            // generic search with multiple terms (OR)
            var byTerms = dc.Search(new[]{ "userguidid", "salesforceid" });
            Output(byTerms);

            // get all objects in a server and of a particular type
            var tablesInServer = dc.Search(new[]{ "objectType=table dsl.address.server:genentitlements-dev.database.windows.net" });
            Output(tablesInServer);

            // generic search with all
            var all = dc.Search(new[]{ " " });
            Output(all);

            // faceted search by source type with all
            var facetedBySourceType = dc.Search(new[]{ " " }, new[]{ "sourceType" });
            Output(facetedBySourceType);

            // faceted search by object type with all
            var facetedByObjectType = dc.Search(new[]{ " " }, new[]{ "objectType" });
            Output(facetedByObjectType);

            // faceted search by tag with all
            var facetedByTag = dc.Search(new[]{ " " }, new[]{ "tags" });
            Output(facetedByTag);

            // get relationships ...

            // Note: These ids should be retrieved dynamically from the search calls above
            // Hardcoded for illustration purposes only
            const string subscriptionsTableId =
                "https://ded8da95-eaff-4085-9417-04177825bb00-catalog.api.datacatalog.azure.com/catalogs/catalog/views/tables/eadf9e2b-c584-4b84-b8be-eb2ad764ba63";
            const string usersTableId =
                "https://ded8da95-eaff-4085-9417-04177825bb00-catalog.api.datacatalog.azure.com/catalogs/catalog/views/tables/b1cb4107-8859-4d4a-99cc-1589df93eab5";
            const string skusTableId =
                "https://ded8da95-eaff-4085-9417-04177825bb00-catalog.api.datacatalog.azure.com/catalogs/catalog/views/tables/8d672246-8078-46ed-8c22-a9451fa13f5e";
            const string batchReplicationAuditTableId =
                "https://ded8da95-eaff-4085-9417-04177825bb00-catalog.api.datacatalog.azure.com/catalogs/catalog/views/tables/fa1c03f9-8e4e-4a67-85e4-6934c63a99e2";

            var relationshipsFromUsers = dc.Relationships(usersTableId);
            Output(relationshipsFromUsers);

            var relationshipsFromSubscriptions = dc.Relationships(subscriptionsTableId);
            Output(relationshipsFromSubscriptions);

            var relationshipsToSubscriptions = dc.Relationships(toAssetId: subscriptionsTableId);
            Output(relationshipsToSubscriptions);

            var relationshipsFromSkus = dc.Relationships(skusTableId);
            Output(relationshipsFromSkus);

            var relationshipsFromBatchReplicationAudit = dc.Relationships(batchReplicationAuditTableId);
            Output(relationshipsFromBatchReplicationAudit);

            //File.AppendAllText("output.json", output);
        }

        private static dynamic ToDynamic(string json)
        {
            if (json == null)
                return JToken.Parse("{}");

            var result = JToken.Parse(json);

            return result;
        }

        private static string Output(string json)
        {
            var objects = ToDynamic(json);
            Console.WriteLine($"Search terms: {objects.query?.searchTerms ?? "<none>"}");
            Console.WriteLine($"Count: {objects.totalResults}");

            var output = objects.ToString(Formatting.Indented);
            
            Console.WriteLine(output);
            return output;
        }
    }
}
