using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText(@"D:\Crap\adventofcode2020\day3\data.txt");
            var rows = data.Split(Environment.NewLine);
           
            int startPosition = 0;

            for (var startRow = 0; startRow < rows.Length; startRow++)
            {
                var replaceWith = rows[startRow][startPosition] == '.' ? "O" : "X";
            
                 rows[startRow] = rows[startRow]
                    .Remove(startPosition, 1)
                    .Insert(startPosition, replaceWith);
                startPosition = startPosition +3;
               
                if (startPosition > rows[startRow].Length - 1)
                {
                  startPosition =  startPosition - (rows[startRow].Length);
                }     
            }

            foreach (var row in rows)
                System.Console.WriteLine(row);

            var trees = Convert.ToUInt64 ((from row in rows
                        from pos in row
                        where pos == 'X' select pos).Count());

            System.Console.WriteLine($"Part 1: Number or trees {trees}");
            var first =  BigInteger.Parse(PartII(1, 1).ToString());
            var second = BigInteger.Parse(PartII(3, 1).ToString());
            var thired = BigInteger.Parse(PartII(5, 1).ToString());
            var fourth = BigInteger.Parse(PartII(7,1).ToString());
            var fifth =  BigInteger.Parse(PartII(1, 2).ToString());

            var total = BigInteger.Multiply(first, second);
            total = BigInteger.Multiply(total, thired);
            total = BigInteger.Multiply(total, fourth);
            total = BigInteger.Multiply(total, fifth);

            System.Console.WriteLine($"Multipllied {first} x {second} x {thired} x {fourth} x {fifth} = {total}");
        }

        public static int PartII(int right, int down)
        {
            var data = File.ReadAllText(@"D:\Crap\adventofcode2020\day3\data.txt");
            var rows = data.Split(Environment.NewLine);

            int startPosition = 0;

            for (var startRow = 0; startRow < rows.Length; startRow = startRow + down)
            {
                var replaceWith = rows[startRow][startPosition] == '.' ? "O" : "X";

                 rows[startRow] = rows[startRow]
                    .Remove(startPosition, 1)
                    .Insert(startPosition, replaceWith);
                startPosition = startPosition + right;

                if (startPosition > rows[startRow].Length - 1)
                {
                  startPosition =  startPosition - (rows[startRow].Length);
                }
            }

            foreach (var row in rows)
                System.Console.WriteLine(row);

            var trees = (from row in rows
                        from pos in row
                        where pos == 'X' select pos).Count();

            System.Console.WriteLine($"Number or trees {trees}");

            return trees;
        }
        
    }

    public class Data 
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        public string Value5 { get; set; }
        public string Value6 { get; set; }
    }
}
