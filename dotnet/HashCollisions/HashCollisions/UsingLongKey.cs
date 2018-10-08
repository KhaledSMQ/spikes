using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HashCollisions
{
    public class UsingLongKey
    {
        private static readonly Random Rnd = new Random();
        private const string KeysFile = "keys20k.txt";
        private const int IdCount = 1000;
        private const int Combinations = 20000;

        public void Run()
        {
            var keyList = GetKeys(Encode);

            var results = new Dictionary<string, int>();
            
            var keys = new HashSet<string>();

            var sw = new Stopwatch();
            sw.Start();

            ProcessKeys(keyList, results, keys);

            sw.Stop();
            Console.WriteLine($"Processed {results.Count} combinations in {sw.ElapsedMilliseconds} ms.");

            sw.Reset();
            sw.Start();

            foreach (var key in keys)
            {
                var v = results[key];
                //Console.WriteLine($"{key.Substring(0, 10)} = {v}");
            }

            sw.Stop();

            Console.WriteLine($"Retrieved {keys.Count} results in {sw.ElapsedMilliseconds} ms.");
            Console.WriteLine($"Average key length is {keys.Average(k => k.Length)}.");
        }

        private static void ProcessKeys(IList<string> keyList, IDictionary<string, int> dict, HashSet<string> hashset)
        {
            foreach (var c in Enumerable.Range(0, keyList.Count))
            {
                var key = keyList[c];
                if (dict.ContainsKey(key))
                {
                    Console.WriteLine($"Key {key.Substring(0, 10)} for c = {c} already exists.");
                }
                dict[key] = c;
                hashset.Add(key);
                if (c % 500 == 0)
                {
                    Console.WriteLine($"Processed {hashset.Count} keys.");
                }
            }
        }

        private static string Encode(string orig)
        {
            var bytes = Encoding.UTF8.GetBytes(orig);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            var base64 = Convert.ToBase64String(hash);
            return base64;
        }

        private static IList<string> GenerateKeys(Func<string, string> func)
        {
            var list = new List<string>();
            foreach (var c in Enumerable.Range(0, Combinations))
            {
                var key = GetLongKey(IdCount, func);
                list.Add(key);
                if (c % 500 == 0)
                {
                    Console.WriteLine($"Generated {list.Count} keys.");
                }
            }
            return list;
        }

        private static void WriteKeysToFile(IEnumerable<string> keys)
        {
            File.AppendAllLines(KeysFile, keys);
        }

        private static IList<string> ReadKeysFromFile()
        {
            var lines = File.ReadAllLines(KeysFile);
            return lines;
        }

        private static IList<string> GetKeys(Func<string, string> func)
        {
            var sw = new Stopwatch();
            sw.Start();

            IList<string> keys;
            if (File.Exists(KeysFile))
            {
                keys = ReadKeysFromFile();
                sw.Stop();
                Console.WriteLine($"Read {keys.Count} keys from file in {sw.ElapsedMilliseconds} ms.");
            }
            else
            {
                keys = GenerateKeys(func);
                WriteKeysToFile(keys);
                sw.Stop();
                Console.WriteLine($"Generated {keys.Count} keys and wrote them to a file in {sw.ElapsedMilliseconds} ms.");
            }

            return keys;
        }

        private static string GetLongKey(int idCount, Func<string, string> func)
        {
            var ids = Shuffle(Enumerable.Range(0, idCount).ToArray(), 10);
            var builder = new StringBuilder();

            foreach (var id in ids)
            {
                builder.Append(id).Append("_");
            }

            var orig = builder.ToString();
            var key = func(orig);
            return key;
        }

        private static IEnumerable<int> Shuffle(IList<int> array, int count)
        {
            foreach(var idx in Enumerable.Range(0, count))
            {
                var n = Rnd.Next(array.Count);
                var k = Rnd.Next(array.Count);
                var old = array[n];
                array[n] = array[k];
                array[k] = old;
            }

            return array;
        }

        private static int[] ShuffleAll(int[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                var k = Rnd.Next(n--);
                var old = array[n];
                array[n] = array[k];
                array[k] = old;
            }

            return array;
        }
    }
}
