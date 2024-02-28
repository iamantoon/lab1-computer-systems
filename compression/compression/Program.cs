using System.IO;
using ICSharpCode.SharpZipLib.BZip2;

namespace compression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = "C:\\ComputerSystems\\lab1\\3.txt";
            string destinationFile = "C:\\ComputerSystems\\lab1\\3.bz2";

            using (FileStream inputStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (FileStream outputStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write))
            using (BZip2OutputStream bzip2Stream = new BZip2OutputStream(outputStream))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    bzip2Stream.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
