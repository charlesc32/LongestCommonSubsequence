using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var input = new Dictionary<int, string>();
        var count = 1;
        using (StreamReader reader = File.OpenText(args[0]))
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (null == line)
                continue;
        
            if (line.Contains(';')) input.Add(count,line);
            count++;
        }

        var sortedOutput = MainDriver(input);
        foreach (var outputLine in sortedOutput)
        {
            Console.WriteLine(outputLine);
        }
    }

    internal static List<string> MainDriver(Dictionary<int, string> input)
    {
        var output = new ConcurrentDictionary<int, string>();
        Parallel.ForEach(input, individualLine =>
        {
            output.TryAdd(individualLine.Key, FindMostCommonSubSequence(individualLine.Value.Split(';')[0], individualLine.Value.Split(';')[1]));
        }
        );

        var ordered =  output.OrderBy(a => a.Key);
        var ret = new List<string>();
        foreach (var kvp in ordered)
        {
            ret.Add(kvp.Value);
        }
        return ret;
    }

    internal static string FindMostCommonSubSequence(string firstString, string secondString)
    {
        string output = string.Empty;
        //key is the substring found, value is the last position of substring found
        var foundSubStrings = new Dictionary<string, int>();
        for (int i = 0; i < firstString.Length; i++)
        {
            var charToFind = firstString[i];
            var foundIndexes = GetAllIndexesOf(secondString, charToFind);
            
            if (foundIndexes.Count > 0)
            {
                var partOfExistingString = -1;
                var newFoundSubStrings = new Dictionary<string, int>();
                var greaterIndex = 0;
                foreach (var foundString in foundSubStrings)
                {
                    greaterIndex = foundIndexes.Where(a => a > foundString.Value).FirstOrDefault();
                    if (greaterIndex != 0)
                    {
                        partOfExistingString = foundString.Value;
                        var existingString = foundString.Key;
                        var newKey = existingString += secondString[greaterIndex];
                        if (!newFoundSubStrings.ContainsKey(newKey)) newFoundSubStrings.Add(newKey, greaterIndex);
                    }
                }

                var key = secondString[greaterIndex].ToString();
                if (partOfExistingString == -1 && !foundSubStrings.ContainsKey(key)) foundSubStrings.Add(key, greaterIndex);

                if (newFoundSubStrings.Count > 0)
                {
                    foreach (var newString in newFoundSubStrings)
                    {
                        if (!foundSubStrings.ContainsKey(newString.Key)) foundSubStrings.Add(newString.Key, newString.Value);  
                    } 
                }
            }
        }

        return foundSubStrings.OrderByDescending(a => a.Key.Length).First().Key.Trim();
    }

    internal static List<int> GetAllIndexesOf(string stringToSearch, char charToFind)
    {
        var foundIndexes = new List<int>();
        var index = 0;

        while (index < stringToSearch.Length)
        {
            index = stringToSearch.IndexOf(charToFind, index);

            if (index > -1) foundIndexes.Add(index);
            else break;
            index++;  
        } 
        
        return foundIndexes;
    }
}