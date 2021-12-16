using System;
using System.Diagnostics;

namespace Partition_Equal_Subset_Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        public bool CanPartition(int[] nums)
        {
            return new PartitionAbilityDefiner(nums).Define();
        }

        public class PartitionAbilityDefiner
        {
            int[] _nums;


            public PartitionAbilityDefiner(int[] nums)
            {
                _nums = nums;
            }

            public bool Define()
            {
                for(int i=1; i<=_nums.Length; i++)
                {

                }
            }

        }



        public static void RunOnce()
        {

            Random rnd = new Random();

            Stopwatch sw = new Stopwatch();

            int[] arr = new int[rnd.Next(1, 200)];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(1, 100);
            }

            sw.Start();

            PartitionAbilityDefiner def = new PartitionAbilityDefiner(arr);

            bool rez = def.Define();

            sw.Stop();

            Console.WriteLine($"Define splitability of array of {arr.Length} elements, rezult is {rez}, took {sw.ElapsedMilliseconds}ms");
        }

    }
}
