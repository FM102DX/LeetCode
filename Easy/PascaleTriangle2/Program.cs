using System;
using System.Collections.Generic;

namespace PascaleTriangle2
{
    internal class Program
    {

        /*
         * https://leetcode.com/problems/pascals-triangle-ii/
         * 
         */
        
        static void Main(string[] args)
        {
            int rowIndex = 15;

            PascalTriangle pascalTriangle = new PascalTriangle(rowIndex + 1);
            Console.WriteLine($"{pascalTriangle.GetDistinctRow(rowIndex).Items.Count}");
        }

        public IList<int> GetRow(int rowIndex)
        {
            PascalTriangle pascalTriangle = new PascalTriangle(rowIndex+1);   
            return pascalTriangle.GetDistinctRow(rowIndex).Items;
        }

        public class PascalTriangle
        {

            List<PascalTriangelRow> rows = null;

            public PascalTriangelRow GetDistinctRow(int zeroBasedRowIndex)
            {
                if (zeroBasedRowIndex + 1 > rows.Count)
                {
                    return null;
                }
                return  rows[zeroBasedRowIndex];
            }

            public PascalTriangle()
            {

            }
            public PascalTriangle(int numRows)
            {
                Generate(numRows);
            }

            public void Generate(int numRows)
            {
                rows = new List<PascalTriangelRow>();

                //List<List<int>> rowData = new List<List<int>>();

                PascalTriangelRow previousRow = null;
                PascalTriangelRow thisRow = null;
                int previousRowLength = 0;

                for (int i = 0; i < numRows; i++)
                {

                    thisRow = new PascalTriangelRow();

                    if (previousRow == null)
                    {
                        thisRow.Items.Add(1);
                    }
                    else
                    {
                        previousRowLength = previousRow.Items.Count;

                        for (int j = 0; j < previousRowLength; j++)
                        {
                            if (j == 0)
                            {
                                thisRow.Items.Add(previousRow.Items[0]); // это всегда 1 
                            }
                            else
                            {
                                thisRow.Items.Add(previousRow.Items[j] + previousRow.Items[j - 1]);
                            }

                        }
                        thisRow.Items.Add(1);
                    }
                    rows.Add(thisRow);

                    previousRow = thisRow;
                }
            }
            public void MakeRowsDump()
            {
                rows.ForEach(row =>
                {
                    Console.WriteLine(row.ToString());
                });
            }

            public IList<IList<int>> GetRowsData()
            {
                IList<IList<int>> rezTotal = new List<IList<int>>();
                foreach (PascalTriangelRow row in rows)
                {
                    List<int> rezRow = new List<int>();
                    foreach (int rowItem in row.Items)
                    {
                        rezRow.Add(rowItem);
                    }
                    rezTotal.Add(rezRow);
                }
                return rezTotal;
            }

            public class PascalTriangelRow
            {
                public List<int> Items = new List<int>();
                public override string ToString()
                {
                    return String.Join("  ", Items);
                }


            }
        }
    }
}
