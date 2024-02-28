using System;
using System.Collections.Generic;
using System.IO;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> paths = new List<string> 
                { "C:\\ComputerSystems\\lab1\\1.txt",
                  "C:\\ComputerSystems\\lab1\\2.txt",
                  "C:\\ComputerSystems\\lab1\\3.txt",
                  "C:\\ComputerSystems\\lab1\\textsInBase64\\1.txt",
                  "C:\\ComputerSystems\\lab1\\textsInBase64\\2.txt",
                  "C:\\ComputerSystems\\lab1\\textsInBase64\\3.txt",
                  "C:\\ComputerSystems\\lab1\\bzInBase64\\1.txt",
                  "C:\\ComputerSystems\\lab1\\bzInBase64\\2.txt",
                  "C:\\ComputerSystems\\lab1\\bzInBase64\\3.txt",
                  "C:\\ComputerSystems\\lab1\\bzip2\\1.bz2",
                  "C:\\ComputerSystems\\lab1\\bzip2\\2.bz2",
                  "C:\\ComputerSystems\\lab1\\bzip2\\3.bz2"
                };

            Console.WriteLine("Оберіть один з файлів для аналізу: ");
            
            Console.WriteLine("1 - уривок з підручника всесвітньої історії про передумови Першої світової війни");
            Console.WriteLine("2 - уривок з підручника алгебри про числа");
            Console.WriteLine("3 - початок роману Панаса Мирного 'Хіба ревуть воли, як ясла повні?'");
            Console.WriteLine("4 - base64: уривок з підручника всесвітньої історії про передумови Першої світової війни");
            Console.WriteLine("5 - base64: уривок з підручника алгебри про числа");
            Console.WriteLine("6 - base64: початок роману Панаса Мирного 'Хіба ревуть воли, як ясла повні?'");
            Console.WriteLine("7 - стиснений base64: уривок з підручника всесвітньої історії про передумови Першої світової війни");
            Console.WriteLine("8 - стиснений base64: уривок з підручника алгебри про числа");
            Console.WriteLine("9 - стиснений base64: початок роману Панаса Мирного 'Хіба ревуть воли, як ясла повні?'");
            Console.WriteLine("10 - стиснений: уривок з підручника всесвітньої історії про передумови Першої світової війни");
            Console.WriteLine("11 - стиснений: уривок з підручника алгебри про числа");
            Console.WriteLine("12 - стиснений: початок роману Панаса Мирного 'Хіба ревуть воли, як ясла повні?'");
            Console.WriteLine();

            int pathIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            string path = paths[pathIndex];
            string text = File.ReadAllText(path);
            int textLength = text.Length;

            Dictionary<char, Character> charCounts = new Dictionary<char, Character>();

            foreach (char c in text)
            {
                if (charCounts.ContainsKey(c)) charCounts[c].Count++;
                else charCounts.Add(c, new Character(1, 0));
            }

            foreach (var keyValuePair in charCounts)
            {
                keyValuePair.Value.Frequency = (float)keyValuePair.Value.Count / textLength;
            }

            foreach (var keyValuePair in charCounts)
            {
                Console.WriteLine($"\"{keyValuePair.Key}\" - {keyValuePair.Value.Count} - {keyValuePair.Value.Frequency}");
            }

            Console.WriteLine();

            float totalEntropy = 0.0f;
            foreach (var keyValuePair in charCounts)
            {
                float entropy = (float)(keyValuePair.Value.Frequency * Math.Log(keyValuePair.Value.Frequency, 2));
                totalEntropy += entropy;
            }
            totalEntropy = -totalEntropy;

            Console.WriteLine($"Cередня ентропія алфавіту для даного тексту: {totalEntropy} бітів/символ");

            Console.WriteLine();

            double totalInformation = totalEntropy * textLength / 8;
            Console.WriteLine($"Кількість інформації: {totalInformation} байт");
            
            long fileSizeBytes = new FileInfo(path).Length;
            long fileSizeBits = fileSizeBytes * 8;

            Console.WriteLine($"Розмір файлу {path}: {fileSizeBytes} байт або {fileSizeBits} бітів");

            if (totalInformation > fileSizeBits)
            {
                Console.WriteLine("Отже, значення кількості інформації тексту є більшим, ніж розмір файлу");
            }
            else if (totalInformation < fileSizeBits)
            {
                Console.WriteLine("Отже, значення кількості інформації тексту даного файлу є меншим, ніж розмір файлу");
            }
            else
            {
                Console.WriteLine("Обсяг інформації в тексті дорівнює розміру файлу");
            }
        }
    }

    class Character
    {
        public int Count { get; set; }
        public float Frequency { get; set; }

        public Character(int count, float frequency)
        {
            Count = count;
            Frequency = frequency;
        }
    }
}
