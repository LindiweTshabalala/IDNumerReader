using System;
namespace TxtFileReader
{
    class Program
    {
        static void Main()
        {
            string filePath = "idNumbers.txt";
            string[] idNumbers;

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: " + filePath);
                return;
            }

            idNumbers = File.ReadAllLines(filePath);
            int count = 0;

            foreach (string idNumber in idNumbers)
            {
                if (idNumber.Length != 13)
                {
                    Console.WriteLine("Invalid ID number: " + idNumber);
                    continue;
                }

                int yearPrefix = int.Parse(idNumber.Substring(0, 2));
                int year = yearPrefix < 22 ? 2000 + yearPrefix : 1900 + yearPrefix;
                if (year > 2010)
                {
                    count++;
                }
                int month = int.Parse(idNumber.Substring(2, 2));
                int day = int.Parse(idNumber.Substring(4, 2));

                if (month < 1 || month > 12 || day < 1 || day > 31)
                {
                    Console.WriteLine("Invalid date of birth: " + idNumber);
                    continue;
                }

                DateTime dateOfBirth = new DateTime(year, month, day);
                Console.WriteLine("Date of birth: " + dateOfBirth.ToString("dd/MM/yyyy"));

                string outputFilePath = "peopleBornAfter2010.txt";
                File.WriteAllText(outputFilePath, count.ToString());
            }
        }
    }
}
