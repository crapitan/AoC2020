﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2020 day 1");
            var data = new List<int> {
1645
,1995
,1658
,1062
,1472
,1710
,1424
,1823
,1518
,1656
,1811
,1511
,1320
,1521
,1395
,1996
,1724
,1666
,1637
,1504
,1766
,534
,1738
,1791
,1372
,1225
,1690
,1949
,1495
,1436
,1166
,1686
,1861
,1889
,1887
,997
,1202
,1478
,833
,1497
,1459
,1717
,1272
,1047
,1751
,1549
,1204
,1230
,1260
,1611
,1506
,1648
,1354
,1415
,1615
,1327
,1622
,1592
,1807
,1601
,1026
,1757
,1376
,1707
,1514
,1905
,1660
,1578
,1963
,1292
,390
,1898
,1019
,1580
,1499
,1830
,1801
,1881
,1764
,1442
,1838
,1088
,1087
,1040
,1349
,1644
,1908
,1697
,1115
,1178
,1224
,1810
,1445
,1594
,1894
,1287
,1676
,1435
,1294
,1796
,1350
,1685
,1118
,1488
,1726
,1696
,1190
,1538
,1780
,1806
,1207
,1346
,1705
,983
,1249
,1455
,2002
,1466
,1723
,1227
,1390
,1281
,1715
,1603
,1862
,1744
,1774
,1385
,1312
,1654
,1872
,1142
,1273
,1508
,1639
,1827
,1461
,1795
,1533
,1304
,1417
,1984
,28
,1693
,1951
,1391
,1931
,1179
,1278
,1400
,1361
,1369
,1343
,1416
,1426
,314
,1510
,1933
,1239
,1218
,1918
,1797
,1255
,1399
,1229
,723
,1992
,1595
,1191
,1916
,1525
,1605
,1524
,1869
,1652
,1874
,1756
,1246
,1310
,1219
,1482
,1429
,1244
,1554
,1575
,1123
,1194
,1408
,1917
,1613
,1773
,1809
,1987
,1733
,1844
,1423
,1718
,1714
,1923
,1503
            };

            var sortedList = data.OrderBy(o => o).ToArray();
            int start = 0, end = sortedList.Count() - 1, numberToFind = 2020;
            bool found = false;
            "".Substring("".IndexOf("-"))
            while (!found)
            {
                if (sortedList[start] + sortedList[end] == numberToFind)
                {
                    found = true;
                }
                else
                {
                    if (sortedList[start] + sortedList[end] > numberToFind)
                    {
                        end--;
                    }
                    else
                    {
                        start++;
                    }
                }
            }

            System.Console.WriteLine($"The numbers are {sortedList[start]} + {sortedList[end]} = {sortedList[start] + sortedList[end]} and multiplied it is {sortedList[start] * sortedList[end]} ");

            System.Console.WriteLine("Part 2 (three numbers)");

            found = false;
            start = 0;
            end = sortedList.Count() - 1;
            int extra = start + 1;

            numberToFind = 2020;

            while (!found)
            {
                if (end == start)
                {
                    extra++;
                    start = 0;
                    end = sortedList.Count() - 1;

                    if (extra > end)
                        throw new Exception("Coded wrong");
                }

                if (sortedList[start] + sortedList[end] + sortedList[extra]  == numberToFind)
                {
                    found = true;
                }
                else
                {
                    if (sortedList[start] + sortedList[end] + sortedList[extra]  > numberToFind)
                    {
                        end--;
                    }
                    else
                    {
                        start++;
                    }
                }
            }
            
            System.Console.WriteLine($"The numbers are {sortedList[start]} + {sortedList[end]} + {+ sortedList[extra]} = {sortedList[start] + sortedList[end] + sortedList[extra]} and multiplied it is {sortedList[start] * sortedList[end] * sortedList[extra]} ");

        }
    }
}
