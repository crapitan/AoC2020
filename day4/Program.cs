using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText(@"D:\Crap\adventofcode2020\day4\data.txt");
            var rows = data.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var passPorts = rows.Select(r => new Passport(r.Replace(Environment.NewLine, " ")));

            var validPassPorts = passPorts.Where(p => p.IsValid).Count();

            System.Console.WriteLine($"Of {passPorts.Count()} passports {validPassPorts} is valid");
        }
    }

    public class Passport
    {
        public Passport(string input)
        {
            var rows = input.Split(' ');

            foreach (var item in rows)
            {
                if (!string.IsNullOrEmpty(item))
                {


                    var type = item.Substring(0, 3);
                    switch (type)
                    {
                        case "byr":
                            this.BirthYear = item.Substring(4);
                            break;
                        case "iyr":
                            this.IssuYear = item.Substring(4);
                            break;
                        case "eyr":
                            this.ExpirationYear = item.Substring(4);
                            break;
                        case "hgt":
                            this.Height = item.Substring(4);
                            break;
                        case "hcl":
                            this.HairColor = item.Substring(4);
                            break;
                        case "ecl":
                            this.EyeColor = item.Substring(4);
                            break;
                        case "pid":
                            this.PassportId = item.Substring(4);
                            break;
                        case "cid":
                            this.CountryId = item.Substring(4);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public string BirthYear { get; set; }
        public string IssuYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        public bool IsValid
        {
            get
            {

                if (string.IsNullOrEmpty(BirthYear) ||
                    string.IsNullOrEmpty(IssuYear) ||
                    string.IsNullOrEmpty(Height) ||
                    string.IsNullOrEmpty(HairColor) ||
                    string.IsNullOrEmpty(EyeColor) ||
                    string.IsNullOrEmpty(PassportId) ||
                    // string.IsNullOrEmpty(PassCountryIdportId) ||
                    string.IsNullOrEmpty(ExpirationYear))
                {
                  //  System.Console.WriteLine("Not all fields where pressent!");
                    return false;
                }

                if (!(this.BirthYear.Length == 4 && int.TryParse(this.BirthYear, out int year) && year >= 1920 && year <= 2002))
                {
                    System.Console.WriteLine($"The birthyear of {BirthYear} is not between 1920 and 2002");
                    return false;
                }
                else
                {
                    System.Console.WriteLine($"The birthyear of {BirthYear} is between 1920 and 2002");
                }

                if (!(this.IssuYear.Length == 4 && int.TryParse(this.IssuYear, out int issueyear) && issueyear >= 2010 && issueyear <= 2020))
                    return false;

                if (!(this.ExpirationYear.Length == 4 && int.TryParse(this.ExpirationYear, out int expyear) && expyear >= 2020 && expyear <= 2030))
                    return false;

                if (this.Height.Contains("cm"))
                {
                    if (!(int.TryParse(this.Height.Replace("cm", string.Empty), out int heightcm) && heightcm >= 150 && heightcm <= 193))
                        return false;
                }

                if (this.Height.Contains("in"))
                {
                    if (!(int.TryParse(this.Height.Replace("in", string.Empty), out int heightin) && heightin >= 59 && heightin <= 76))
                        return false;
                }

                if (this.HairColor.Length != 7)
                {
                    return false;
                }

                if (!Regex.IsMatch(this.HairColor, "^#[0-9a-f]{6}$"))
                {
                    return false;
                }

                if (!(EyeColor == "amb" ||
                        EyeColor == "blu" ||
                        EyeColor == "brn" ||
                        EyeColor == "gry" ||
                        EyeColor == "grn" ||
                        EyeColor == "hzl" ||
                        EyeColor == "oth"))
                {
                    return false;
                }

                if (!Regex.IsMatch(this.PassportId, "^[0-9]{9}$"))
                {
                    return false;
                }

                return true;
                ;

            }
        }
    }
}
