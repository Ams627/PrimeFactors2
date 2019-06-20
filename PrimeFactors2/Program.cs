using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactors2
{
    class Program
    {
        public static List<int> sorting(List<int> numbers_a)
        {
            // create a "sieve" (of Eratosthenes) with either lowest prime factor of two for even numbers (this is a given)
            // or the number itself for odd numbers - we will determine the lowest prime for odd numbers 
            // after this.
            var _sieve = Enumerable.Range(0, 100001).Select(x => x % 2 == 0 ? 2 : x).ToArray();

            // take every number in the array in turn up to the square root of N:
            for (int i = 3; i * i < _sieve.Length; i++)
            {
                // if i is prime (i.e. its lowest prime factor is itself):
                if (_sieve[i] == i)
                {
                    // mark all multiples of i as having i as a prime factor
                    for (int j = 2 * i; j < _sieve.Length; j += i)
                    {
                        _sieve[j] = i;
                    }
                }
            }

            // divide down each number to count its prime factors:
            var primeFactors = new int[numbers_a.Count];
            for (int i = 0; i < numbers_a.Count; i++)
            {
                var n = numbers_a[i];
                while (n != 1)
                {
                    primeFactors[i]++;
                    n /= _sieve[n];
                }
            }

            // return a list sorted by prime factors then the number itself:
            // probably not efficient and if you can't read linq it's probably not easy to understand :)
            return numbers_a
                .Select((number, idx) => new { number, idx, pf = primeFactors[idx] })
                .OrderBy(b => b.pf)
                .ThenBy(c => c.number)
                .Select(d => d.number).ToList();
        }

        private static void Main(string[] args)
        {
            try
            {
                var testlist = new List<int> { 1, 2, 3, 4, 5, 6, 7, 9, 21, 4095, 4096, 8191 };
                var testlist2 = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3 };
                var testlist3 = new List<int> { 378, 7623, 1050, 2450, 9438, 980, 5733, 280, 6630, 5780, 1540, 468, 2058, 3380, 4732, 4420, 3250, 2860, 5236, 3850, 3042, 2079, 1575, 48, 1716, 9945, 5049, 3332, 1768, 990, 1638, 5070, 2420, 4004, 1386, 200, 6370, 1170, 264, 4550, 2310, 1452, 8575, 2380, 2178, 8470, 612, 882, 2205, 4250, 4290, 312, 5610, 396, 728, 1300, 168, 4095, 120, 3570, 5775, 500, 2244, 3388, 4125, 1530, 8085, 7546, 4998, 252, 630, 3675, 5967, 2925, 2142, 3978, 780, 1755, 5355, 1750, 1485, 8670, 3740, 6435, 2028, 616, 1323, 1428, 300, 6188, 6375, 2156, 3630, 2730, 1470, 8330, 270, 6006, 392, 2548, 6050, 660, 450, 6825, 180, 9625, 8125, 968, 2295, 8925, 9009, 7854, 3366, 3468, 750, 520, 3234, 3430, 1950, 3861, 1092, 7098, 5082, 8415, 8092, 5390, 9555, 1650, 1020, 2625, 588, 80, 1820, 1700, 6292, 7150, 3822, 4875, 8450, 1372, 9075, 2475, 2750, 8918, 3465, 9724, 7497, 420, 9282, 2652, 7605, 2574, 8228, 1100, 5950, 4851, 108, 1496, 2550, 700 };
                var testlist4 = new List<int> { 10000 };

                var endResult2 = sorting(testlist4);
                endResult2.ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
