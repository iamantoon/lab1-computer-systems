using System;
using System.IO;

namespace base64
{
    internal class Program
    {
        static void Main()
        {
            string path = "C:\\ComputerSystems\\lab1\\3.txt";
            string base64Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            string result = "";
            byte[] fileBytes = File.ReadAllBytes(path);

            for (int i = 0; i < fileBytes.Length; i += 3)
            {
                int addition1 = fileBytes[i] << 16;
                int addition2 = (i + 1 < fileBytes.Length ? fileBytes[i + 1] : 0) << 8;
                int addition3 = i + 2 < fileBytes.Length ? fileBytes[i + 2] : 0;

                int value = addition1 + addition2 + addition3;

                for (int j = 0; j < 4; j++)
                {
                    int index = (value >> (6 * (3 - j))) & 0x3F;
                    result += base64Characters[index];
                }
            }

            int padding = fileBytes.Length % 3;
            if (padding > 0)
            {
                result = result.Substring(0, result.Length - padding);
                result += new string('=', padding);
            }

            Console.WriteLine(result);
        }
    }
}
