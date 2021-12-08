using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program {
  public static void Main (string[] args) {
    List<char>[] bitLists = new List<char>[12];
    for (var i = 0; i < bitLists.Length; i++)
        bitLists[i] = new List<char>(1000);

    var inputs = File.ReadAllLines("Input.txt").ToList();

    foreach (var input in inputs)
        for (var pos = 0; pos < input.Length; pos++)
            bitLists[pos].Add(input[pos]);


    
    var oxygen = GetNumber(FindOxygen(inputs));
    var scrubber = GetNumber(FindScrubber(inputs));


    Console.WriteLine($"Oxygen: {oxygen}, Scrubber: {scrubber}, Res: {oxygen * scrubber}");
  }

  static char MostCommon(List<char> chars)
  {
      var oneCount = chars.Count(c => c == '1');
      var count = chars.Count();

      return oneCount * 2 > count ? '1' : '0';
  }

  static char LeastCommon(char mostCommon) => mostCommon == '0' ? '1' : '0';

  static int GetNumber(IEnumerable<char> chars)
  {
      var s = string.Concat(chars);
      return Convert.ToInt32(s, 2);
  }

    static string FindOxygen(IEnumerable<string> inputs)
    {
        IEnumerable<string> items = inputs.ToList();

        for (var i = 0; i < items.First().Length; i++)
        {
            var (zeroes, ones) = ZeroOneCount(Horizontalize(items, i));

            items = FilterInputs(items, ones >= zeroes ? '1': '0', i);

            if (items.Count() == 1)
                return items.First();
        }

        throw new Exception("Find oxygen failed.");
    }

    static IEnumerable<string> FilterInputs(IEnumerable<string> inputs, char match, int pos)
    {
        return inputs.Where(input => input[pos] == match);
    }

    static (int zeroes, int ones) ZeroOneCount(List<char> zeroesAndOnes)
    {
        var zeroes = 0;
        var ones = 0;

        foreach (var c in zeroesAndOnes)
        {
            if (c == '0')
                zeroes++;
            else
                ones++;
        }

        return (zeroes, ones);
    }

    static string FindScrubber(IEnumerable<string> inputs)
    {
        IEnumerable<string> items = inputs.ToList();

        for (var i = 0; i < items.First().Length; i++)
        {
            var (zeroes, ones) = ZeroOneCount(Horizontalize(items, i));

            items = FilterInputs(items, zeroes <= ones ? '0': '1', i);

            if (items.Count() == 1)
                return items.First();
        }

        throw new Exception("Find scrubber failed.");
    }

    static List<char> Horizontalize(IEnumerable<string> items, int pos)
    {
        var result = new List<char>();
        foreach (var item in items)
            result.Add(item[pos]);

        return result;
    }
}