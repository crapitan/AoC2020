using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText(@"D:\Crap\adventofcode2020\day6\data.txt");
            var groups = data.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var groupsAnsersSum = groups.Select(d => d.Replace(Environment.NewLine, string.Empty).Distinct().Count()).Sum();

            System.Console.WriteLine($"Number of question {groupsAnsersSum}");
            
            int result = 0;
            foreach (var group in groups)
            {
                var letters = group.Replace(Environment.NewLine, string.Empty).Distinct();

                foreach (var letter in letters)
                {
                   if (group.Split(Environment.NewLine).All(d => d.Contains(letter)))
                   {
                       result++;
                   }
                }
            }

            System.Console.WriteLine($"Result {result}");
        }
    }
}
