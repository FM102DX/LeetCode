using System;
using System.Diagnostics;

namespace TwoNumbers
{
    internal class Program
    {
        /*
         * https://leetcode.com/problems/two-sum/
         * 
         * */

        static void Main(string[] args)
        {
            int numberOfTries = 20;

            TwoSumOperator twoSumOperator;

            int target = 0;

            Stopwatch sw = new Stopwatch();
            
            for (int i = 0; i < numberOfTries; i++)
            {
                sw.Reset();
                sw.Start();
                int[] arr = TwoSumOperator.GenerateRndIntArray();
                target = TwoSumOperator.GetRndTarget();
                twoSumOperator = new TwoSumOperator(arr, target);
                int[] rez = twoSumOperator.GetResult();
                sw.Stop();

                Console.WriteLine($"Array length={arr.Length} target={target} rez=[{rez[0]},{rez[1]}] that is [{arr[rez[0]]},{arr[rez[1]]}]  took {sw.ElapsedMilliseconds}ms");
            }
        }

        public int[] TwoSum(int[] nums, int target)
        {
            TwoSumOperator twoSumOperator = new TwoSumOperator(nums, target);
            return twoSumOperator.GetResult();
        }

        public class TwoSumOperator
        {
            int[] _nums;
            int _target;
            int _length;
            Random random = new Random();

            public TwoSumOperator(int[] nums, int target)
            {
                _nums = nums;
                _target = target;
                _length = nums.Length;
            }

            public static int GetRndTarget()
            {
                Random random = new Random();
                return random.Next(-1000-000-000, 1000 - 000 - 000);
            }

            public int[] GetResult()
            {
                for (int i = 0; i < _nums.Length - 1; i++)
                {
                    for (int j = i + 1; j < _nums.Length; j++)
                    {
                        if (_nums[i] + _nums[j] == _target)
                        {
                            return new int[2] { i, j };
                        }
                    }

                }
                return new int[2] { -1, -1 };
            }

            public static int[] GenerateRndIntArray()
            {
                Random random = new Random();

                int[] rezArr = new int[random.Next(2, 10000)];

                for (int i = 0; i < rezArr.Length; i++)
                {
                    rezArr[i] = random.Next(-1000 - 000 - 000, 1000 - 000 - 000);
                }

                return rezArr;
            }



        }
    }
}
