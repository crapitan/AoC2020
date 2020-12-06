using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace day5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var data = File.ReadAllText(@"D:\Crap\adventofcode2020\day5\data.txt");

            // Test
            var seating = new Seating("FBFBBFFRLR");
            System.Console.WriteLine($" Row {seating.Row} Seating {seating.Seat} : Id {seating.Id}");

            var seats = data.Split(Environment.NewLine).Select(l => new Seating(l)).OrderBy(b => b.Id).ToArray();

            var maxSeatId = seats.Max(s => s.Id);
            System.Console.WriteLine($"Max seat Id is {maxSeatId}");

            for (var i = 1; i <= seats.Length-1; i++)
            {
                 if (seats[i-1].Id < seats[i].Id -1)
                     System.Console.WriteLine($"{seats[i].Id} is your seat");
             }
        }
    }

    public class Seating
    {
        public Seating(string input)
        {
            Row = CalculateRow(input, 0, 127);
            Seat = CalculateSeat(input, 0, 7);
            Id = Row * 8 + Seat;
        }

        public int Id { get; set; }
        public int Row { get; set; }

        public int Seat { get; set; }

        private int CalculateRow(string intput, int from, int to)
        {
            if (from == to) return from;

            string firstLetter = intput.First() + "";

            intput = intput.Remove(0, 1);

            if (firstLetter == "L" || firstLetter == "R") return from;

            if (firstLetter == "F")
            {
                int newRowSet = (int)Math.Floor( to - ((to - from) / 2d));
                System.Console.WriteLine($"Row is between {from} and {newRowSet}");
                return CalculateRow(intput, from, newRowSet);
            }
            else
            {
                 int newRowSet = (int)Math.Ceiling(from + ( (to - from) / 2d));
                 System.Console.WriteLine($"Row is between {newRowSet} to {to}");
                return CalculateRow(intput, newRowSet, to);
            }
        }

         private int CalculateSeat(string intput, int from, int to)
        {
            if (from == to) return from;

            string firstLetter = intput.First() + "";
            intput = intput.Remove(0, 1);

            if (firstLetter == "F" || firstLetter == "B") return CalculateSeat(intput, from, to);

            if (firstLetter == "L")
            {
                int newRowSet = (int)Math.Floor( to - ((to - from) / 2d));
                System.Console.WriteLine($"Seat is between {from} and {newRowSet}");
                return CalculateSeat(intput, from, newRowSet);
            }
            else
            {
                 int newRowSet = (int)Math.Ceiling(from + ( (to - from) / 2d));
                 System.Console.WriteLine($"Seat is between {newRowSet} to {to}");
                return CalculateSeat(intput, newRowSet, to);
            }
        }
    }




}