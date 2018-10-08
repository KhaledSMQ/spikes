using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace HashCollisions
{
    class Program
    {
        static void Main(string[] args)
        {
            //var lk = new UsingBitArray();
            var lk = new ProdDb();
            lk.Run();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
