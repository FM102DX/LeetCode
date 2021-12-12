using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

            //RunOnce(1000000000, 39999, 40000);

            //RunOnce(3, 6, 4);

            RunOnce(3, 8, 9);
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
            
            Int64 nIsEvenMultiplier;
            Int64 nIsOddMultiplier;
            Int64 actualNMultiplier;

            Int64 bigger;
            Int64 smaller;
            bool nIsEven;

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
                bigger = (_a > _b) ? _a : _b;
                smaller = (_a > _b) ? _b : _a;
                nIsEven = n % 2 == 0;
                nIsEvenMultiplier = (n / 2);
                nIsOddMultiplier = (n / 2)+1;
                actualNMultiplier = nIsEven ? nIsEvenMultiplier : nIsOddMultiplier;

                //Console.WriteLine($"n={n} nIsEvenMultiplier={nIsEvenMultiplier} nIsOddMultiplier={nIsOddMultiplier}");
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
            }

            private long lessOf (long a, long b)
            {
                if (a >= b) return b; else return a;
            }
            private long biggerOf(long a, long b)
            {
                if (b >= a) return b; else return a;
            }

            private Int64 FindComplicated()
            {
                //long[] smallerValArray = new long[_n];
                // long[] biggerValArray = new long[_n];

              //  List<long> smallerValList = new List<long>();
              //  List<long> biggerValList = new List<long>();

                List<long> targetList = new List<long>();

                for (long i = 1; i <=_n;i++)
                {
                    targetList.Add(smaller * i);
                    targetList.Add(bigger * i);
                }
                targetList.Distinct().ToList().Sort();
                return targetList.ToArray()[_n];
            }

            private Int64 FindComplicated2()
            {

                long xMin = smaller * _n; //12

                //Считаем, сколько раз входило большее, и надо уникальные вхождения, т.е чтобы оно с ним не совпадало

                long biggerIntroNonUniqueCount = xMin / bigger; // всего вхождений

                if (biggerIntroNonUniqueCount * bigger == xMin) biggerIntroNonUniqueCount--;

                bool nonEncountered = false;

                if (biggerIntroNonUniqueCount>0)
                {
                    long biggerIntroUniqueCount = 0;

                    for (int i = 1; i <= biggerIntroNonUniqueCount; i++)
                    {
                        long f = bigger * i;

                        nonEncountered = (xMin >= f) & (f >= xMin - smaller);

                        if ((f % smaller != 0) & (!nonEncountered)) biggerIntroUniqueCount++;
                    }

                    long newN = _n - biggerIntroUniqueCount;

                    long newXMin = smaller * newN;

                    long newXmin4Bigger = bigger * (biggerIntroNonUniqueCount - 1*(nonEncountered ? 1 :0 )) ;

                    //return lessOf(newXMin, newXmin4Bigger);
                    return biggerOf(newXMin, newXmin4Bigger);

                }
                else
                {
                    return smaller*_n;
                }


            }
        }

    }
}
