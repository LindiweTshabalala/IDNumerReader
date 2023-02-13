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

            foreach (string idNumber in idNumbers)
            {
                if (idNumber.Length != 13)
                {
                    Console.WriteLine("Invalid ID number: " + idNumber);
                    continue;
                }

                int year = int.Parse(idNumber.Substring(0, 2));
                int month = int.Parse(idNumber.Substring(2, 2));
                int day = int.Parse(idNumber.Substring(4, 2));

                if (month < 1 || month > 12 || day < 1 || day > 31)
                {
                    Console.WriteLine("Invalid date of birth: " + idNumber);
                    continue;
                }

                DateTime dateOfBirth = new DateTime(year, month, day);
                Console.WriteLine("Date of birth: " + dateOfBirth.ToString("yy/MM/dd"));
            }
        }
    }
}
