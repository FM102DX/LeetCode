using System;
using System.Diagnostics;

namespace NthMagicalNumber
{
    internal class Program
    {

        /*
         * 
         * https://leetcode.com/problems/nth-magical-number/
         * 
         * */

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //TestRun();
            
            //RunOnce(1000000000, 40000, 40000);
            
            RunOnce(1000000000, 39999, 40000);



            // 5,2,4

            //TestRun();
        }

        public static void TestRun(int tries = 20)
        {

            Stopwatch sw = new Stopwatch();

            for (int i = 1; i <= tries; i++)
            {
                sw.Reset();
                sw.Start();
                Random random = new Random();
                int n = random.Next(1, 1000 - 000 - 000);
                int a = random.Next(2, 40000);
                int b = random.Next(2, 40000);
                RunOnce(n, a, b,null, sw);
            }
        }

        public static void RunOnce(int n, int a, int b, NthMagicalNumberFinder finder = null, Stopwatch sw=null)
        {
                if (sw==null) sw= new Stopwatch();
                if (finder==null) finder = new NthMagicalNumberFinder(n, a, b);
                sw.Reset();
                sw.Start();
                Int64 x = finder.Find();
                sw.Stop();
                Console.WriteLine($"n={n} a={a} b={b} результат={x} время={sw.ElapsedMilliseconds}");
        }


        public int NthMagicalNumber(int n, int a, int b)
        {
            NthMagicalNumberFinder nthMagicalNumberFinder = new NthMagicalNumberFinder(n, a, b);
            return nthMagicalNumberFinder.Find();
        }

        public class NthMagicalNumberFinder
        {
            Int64 _n;
            Int64 _a;
            Int64 _b;
            bool bIsEnough;
            bool aIsEnough;
            bool isFlatTask;
            Int64 flatDevider;
            Int64 M = 1000000007;

            public NthMagicalNumberFinder(int n, int a, int b)
            {
                _n = Convert.ToInt64(n);
                _a = Convert.ToInt64(a); 
                _b = Convert.ToInt64(b);
                bIsEnough = (_a >= _b) & (_a % _b == 0);
                aIsEnough = (_b > _a) & (_b % _a == 0);
                isFlatTask = bIsEnough | aIsEnough;
                if (isFlatTask)
                {
                    flatDevider = aIsEnough ? _a : _b;
                }
            }

            public int Find()
            {

                Int64 rez;
                if (isFlatTask)
                {
                    rez= FindIFlat();
                }
                else
                {
                    rez =FindComplicated();
                }
                return Convert.ToInt32(rez % M);
            }


            private Int64 FindIFlat()
            {
                return flatDevider * _n;

                /*
                 * int i = 1;
                int rezCounter = 0;
                bool r;

                if (aIsEnough)
                {
                    while (true)
                    {
                        r = (i % flatDevider == 0);
                        if (r) rezCounter++;
                        if (rezCounter == _n) return i;
                        i++;
                    }
                }
                return -1;
                */
            }

            private Int64 FindComplicated()
            {
                int i = 1;
                int rezCounter = 0;
                bool ra,rb;

                while (true)
                {
                    ra = (i % _a == 0);
                    rb = (i % _b == 0);
                    if (ra | rb) rezCounter++;
                    if (rezCounter == _n) return i;
                    i++;
                }
            }



        }

    }
}
