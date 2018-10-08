using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HashCollisions
{
    //
    public class UsingBitArray
    {
        private static readonly Random Rnd = new Random((int) DateTime.UtcNow.Ticks);
        private const string KeysFile = "keys_bits.txt";
        private const int IdCount = 10000;
        private const int CombinationSize = 30;
        private const int Combinations = 1000000;

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
            var encoded = Convert.ToBase64String(hash);
            return encoded;
        }

        private static IList<string> GenerateKeys(Func<string, string> func)
        {
            if (func == null)
                func = s => s;

            var hashset = new HashSet<string>();
            foreach (var c in Enumerable.Range(0, Combinations))
            {
                var bits = GetRandomBitArray(IdCount);
                var ints = GetIntArray(bits);
                var raw = GetLongKey(ints);
                var key = func(raw);
                hashset.Add(key);

                if (c % 500 == 0)
                {
                    Console.WriteLine($"Generated {hashset.Count} keys.");
                }
            }

            return hashset.ToList();
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

        private static string GetLongKey(IEnumerable<int> ints)
        {
            var key = string.Join("_", ints);
            return key;
        }

        private static IEnumerable<int> GetIntArray(BitArray bits)
        {
            var intArray = new int[bits.Length / 32 + 1];
            bits.CopyTo(intArray, 0);
            return intArray;
        }

        private static BitArray GetRandomBitArray(int idCount)
        {
            var ids = new List<int>();
            var numOfIds = 1 + Rnd.Next(CombinationSize);
            foreach (var idx in Enumerable.Range(0, numOfIds))
            {
                var id = Rnd.Next(idCount);
                ids.Add(id);
            }

            var max = ids.Max();
            var bits = new BitArray(max + 1);
            foreach (var id in ids)
            {
                bits.Set(id, true);
            }

            return bits;
        }
    }
}
