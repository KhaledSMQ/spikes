using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Publisher;
using Shared;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sender starting...");
            var p = new Program();
            Console.WriteLine("Preparing...");
            var t = p.Prepare();
            t.Wait();
            Console.WriteLine("Prepared.");

            Console.WriteLine("Sending...");

            var publisher = new MessagePublisher(ConfigurationManager.AppSettings["Send.Path"],
                ConfigurationManager.AppSettings["Send.ConnectionString"]);
            var colors = new[] {"Blue", "Red", "Green"};
            foreach (var i in Enumerable.Range(1, 20))
            {
                var color = colors[i % colors.Length];
                var message = $"Hello there! This is message {i} and it has color {color}";
                Console.WriteLine("Sending message " + i + "...");
                publisher.SendMessage(new Message(message, DateTime.UtcNow, color));
                Console.WriteLine("Done.");
            }
            Console.WriteLine("All sent.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private async Task Prepare()
        {
            Console.WriteLine("Manager starting...");
            var manageCs = ConfigurationManager.AppSettings["Manage.ConnectionString"];
            var manager = NamespaceManager.CreateFromConnectionString(manageCs);
            Console.WriteLine("Manager started.");
            Console.WriteLine("Connected to address " + manager.Address);

            var topicName = "forwardingtest.t";
            var s1 = await manager.GetSubscriptionAsync(topicName, "forwardingtest1.s");
            s1.ForwardTo = "forwardingtest1.q";
            s1 = await manager.UpdateSubscriptionAsync(s1);

            var s1rules = await manager.GetRulesAsync(topicName, s1.Name);
            var s1client = MessagingFactory.CreateFromConnectionString(manageCs)
                .CreateSubscriptionClient(topicName, s1.Name);
            foreach (var rule in s1rules)
                await s1client.RemoveRuleAsync(rule.Name);
            await s1client.AddRuleAsync("BlueMessages", new SqlFilter("Color = 'Blue'"));
            await s1client.AddRuleAsync("RedMessages", new SqlFilter("Color = 'Red'"));

            var s2 = await manager.GetSubscriptionAsync(topicName, "forwardingtest2.s");
            s2.ForwardTo = "forwardingtest2.q";
            s2 = await manager.UpdateSubscriptionAsync(s2);

            var s2rules = await manager.GetRulesAsync(topicName, s2.Name);
            var s2client = MessagingFactory.CreateFromConnectionString(manageCs)
                .CreateSubscriptionClient(topicName, s2.Name);
            foreach (var rule in s2rules)
                await s2client.RemoveRuleAsync(rule.Name);
            await s2client.AddRuleAsync("RedMessages", new SqlFilter("Color = 'Red'"));
            await s2client.AddRuleAsync("GreenMessages", new SqlFilter("Color = 'Green'"));
        }
    }
}
