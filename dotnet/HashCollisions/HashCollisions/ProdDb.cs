using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCollisions
{
    public class ProdDb
    {
        public void Run()
        {
            var data = GetData();

            var subsByUser = new Dictionary<int, List<int>>();
            foreach (var item in data)
            {
                if (!subsByUser.TryGetValue(item.UserId, out var list))
                {
                    list = new List<int>();
                    subsByUser[item.UserId] = list;
                }

                list.Add(item.SkuId);
            }

            var subsByUserStr = new Dictionary<int, string>();
            foreach (var userId in subsByUser.Keys)
            {
                var list = subsByUser[userId];
                var str = string.Join(",", list);
                subsByUserStr[userId] = str;
            }

            var usersByStr = new Dictionary<string, List<int>>();
            foreach (var userId in subsByUserStr.Keys)
            {
                var str = subsByUserStr[userId];
                if (!usersByStr.TryGetValue(str, out var list))
                {
                    list = new List<int>();
                    usersByStr[str] = list;
                }

                list.Add(userId);
            }

            Console.WriteLine($"Found {usersByStr.Count} profiles:");

            foreach (var str in usersByStr.Keys)
            {
                var users = usersByStr[str];
                Console.WriteLine($"{str} {string.Join(" ", users)}");
            }
        }

        private IEnumerable<Subscription> GetData()
        {
            var list = new List<Subscription>();

            var cs = "Data Source=genpowerrtdb.spikesco.com;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=entitlement;";
            var sql = "select UserId, SkuId from Subscriptions order by UserId, SkuId";

            using (var conn = new SqlConnection(cs))
            {
                var command = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var s = new Subscription { UserId = reader.GetInt32(0), SkuId = reader.GetInt32(1) };
                        list.Add(s);
                        //Console.WriteLine($"{reader[0]} {reader[1]}");
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return list;
        }

        private class Subscription
        {
            public int SkuId { get; set; }
            public int UserId { get; set; }
        }
    }
}
