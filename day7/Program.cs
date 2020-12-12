using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7");
            var data = File.ReadAllText(@"D:\Crap\adventofcode2020\day7\data.txt");
            var bags = data.Split(".", StringSplitOptions.RemoveEmptyEntries);


            var colors = (from bag in (bags
                .Select(bag => bag.Replace(" bags contain no other bags.", Environment.NewLine))
                .Select(bag => Regex.Replace(bag, "bag[a-z] ? contain [0-9] ", Environment.NewLine))
                .Select(bag => Regex.Replace(bag, "( bags?, [0-9] )", Environment.NewLine).Trim()))
                          from color in bag.Split(Environment.NewLine)
                          select color.Trim()
                 ).Distinct().OrderByDescending(o => o);

            System.Console.WriteLine($"Number of colors {colors.Count()}");

            var myBags = colors.Distinct().Select(color => new Bag { Color = color }).ToArray();
            System.Console.WriteLine($"Number of bags {myBags.Count()}");

            for (var p = 0; p < bags.Count(); p++)
            {
                var bag = bags[p];

                var color = bag.FirstColor();
                var colorbags = (bag.GetColorsAndAmount() ?? new List<Bag>()).ToArray();

                var myBag = myBags.SingleOrDefault(b => b.Color == color.Trim());

                if (myBag == null)
                    continue;

                for (var j = 0; j < colorbags.Count(); j++)
                {
                    var cBag = colorbags[j];
                    var coloredBag = myBags.SingleOrDefault(b => b.Color == cBag.Color);

                    if (coloredBag is null)
                        continue;

                    for (var i = 0; i < cBag.Amount; i++)
                    {
                        myBag.Bags.Add(coloredBag);
                    }
                }
            }


            int found = 0;

            foreach (var myBag in myBags.OrderBy(c => c.Name))
            {
                if (myBag.Find("shiny gold"))
                {
                    found++;
                    System.Console.Write(found);
                    myBag.PathToGold("shiny gold");
                }
            }
           
            System.Console.WriteLine($"Found bags {found}");
            System.Console.WriteLine("Part 2");
            var goldBag = myBags.Single(c => c.Color == "shiny gold");

            var countBags = goldBag.CountBags() -1;
            System.Console.WriteLine("Number of bags inside gold " + countBags);
        }
    }

    public static class ColorExtensions
    {
        public static string FirstColor(this string input)
        {
            return Regex.Replace(input, "bag(s|.)(,|.)", Environment.NewLine)
                .Replace("bag.", Environment.NewLine)
                .Replace("bags", Environment.NewLine)
                .Replace("Contain", string.Empty)
                .Trim()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).First();
        }

        public static IEnumerable<Bag> GetColorsAndAmount(this string input)
        {
            var list = Regex.Replace(input, "bag(s|.)(,|.)", Environment.NewLine)
                .Replace("contain", string.Empty)
                .Replace("bag.", Environment.NewLine)
                .Replace("no other bags.", string.Empty)
                .Replace("bags", string.Empty)
                .Replace("bag", string.Empty)
                .Trim()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);


            foreach (string item in list.Where(i => Regex.IsMatch(i, "[0-9] *")))
            {
                var color = Regex.Replace(item, "[0-9]", string.Empty).Trim();
                var amount = int.Parse(Regex.Replace(item, "[a-z]", string.Empty).Trim());
                yield return new Bag(amount, color);
            }
        }
    }

    public class Bag
    {


        public Bag()
        {

        }

        public Bag(int amount, string color)
        {
            this.Amount = amount;
            this.Color = color;
        }
        public int Amount { get; set; }
        public string Color { get; set; }
        public bool EndNode { get; set; }

        public string Name { get; set; }

        public bool? ContainsColor { get; set; }

        public bool Find(string color, string coloredVisited = "")
        {
            string path = $"{coloredVisited}.[{this.Color}]";

            if (this.ContainsColor.HasValue)
                return this.ContainsColor.Value;

            if (this.Color == color)
            {
                this.ContainsColor = true;
                System.Console.WriteLine(path);
            }

            foreach (var item in Bags)
            {
                if (item.Find(color, path))
                {
                    this.ContainsColor = true;
                    break;
                }
            }

            if (!this.ContainsColor.HasValue)
                this.ContainsColor = false;

            return ContainsColor.Value;
        }

        public bool PathToGold(string color, string path = "")
        {
            var subPath = $"{path}.[{this.Color}]";

            if (this.Color == color)
             {
               System.Console.WriteLine(subPath);
               return true;
             }   
            
            foreach (var bag in Bags)
            {
               if (bag.PathToGold(color, subPath))
               {
                   return true;
               }
            }

            return false;
        }

        public List<Bag> Bags { get; set; } = new List<Bag>();

        public int CountBags()
        {
            var sum = 0;

            foreach (var items in this.Bags)
            {
                sum += items.CountBags();
            }

            return sum + 1;
        }


        public int CountColor(int i, string color, string colorVisited = "")
        {
            if (this.Color == color)
            {
                i++;
                System.Console.WriteLine(colorVisited + "." + color);
            }

            if (colorVisited.Contains(this.Color))
                return i;

            string path = colorVisited + "." + this.Color;

            this.EndNode = ((!Bags.Any() || Bags.All(b => b.EndNode)) && this.Color != color);

            foreach (var bag in Bags)
            {
                bag.CountColor(i, color, path);
            }

            return i;
        }

        public string ToString(int layer, string colorVisited = "", string highLigt = "")
        {
            if (EndNode)
                return "";

            if (colorVisited.Contains(this.Color))
            {
                return "Loop";
            }

            string path = colorVisited + "." + this.Color;

            int myLayer = layer + 1;
            string outPut = "";

            for (int i = 0; i < layer; i++)
            {
                outPut += "  ";
            }

            if (this.Color == highLigt)
            {
                var color = System.Console.BackgroundColor;
                System.Console.BackgroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine(outPut + "- " + Color + "<----------------------------------------------------------------");
                System.Console.BackgroundColor = color;
            }
            else
            {
                System.Console.WriteLine(outPut + "- " + Color);
            }


            this.EndNode = (!Bags.Any() || Bags.All(b => b.EndNode));

            foreach (var item in Bags)
            {
                item.ToString(myLayer, path);
            }

            return "";
        }
    }
}
