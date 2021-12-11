using System;
using System.Collections.Generic;

namespace PascalTriangle
{
    internal class Program
    {
        /*
        https://leetcode.com/problems/pascals-triangle/
        */

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PascalTriangle pascalTriangle = new PascalTriangle(15);
            Console.WriteLine($"{pascalTriangle.GetRowsData().Count}"); 
        }

        public IList<IList<int>> Generate(int numRows)
        {
            PascalTriangle pascalTriangle = new PascalTriangle(numRows);
            return pascalTriangle.GetRowsData();
        }

        public class PascalTriangle
        {
            
            List<PascalTriangelRow> rows = null;

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
