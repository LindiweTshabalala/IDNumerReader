using System;
namespace TxtFileReader
{
    class Program
    {
        static void Main()
        {
            int bornBefore2010 = 0;
            int bornAfter2010 = 0;

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

                int yearPrefix = int.Parse(idNumber.Substring(0, 2));
                int year = yearPrefix < 22 ? 2000 + yearPrefix : 1900 + yearPrefix;
                int month = int.Parse(idNumber.Substring(2, 2));
                int day = int.Parse(idNumber.Substring(4, 2));
                

                if (month < 1 || month > 12 || day < 1 || day > 31)
                {
                    Console.WriteLine("Invalid date of birth: " + idNumber);
                    continue;
                }
                    if (year < 2010)
                {
                    bornBefore2010++;
                }
                else if (year > 2010) {
                    bornAfter2010++;
                }

                DateTime dateOfBirth = new DateTime(year, month, day);
                Console.WriteLine("Date of birth: " + dateOfBirth.ToString("dd/MM/yyyy"));
                bornBeforeAfter2010(bornBefore2010, bornAfter2010);

            }
        }

        static void bornBeforeAfter2010(int bornBefore2010, int bornAfter2010) {
            string filePath = "bornBeforeAndAfter2010.txt";
            string[] check2010 = {$"Born before 2010: {bornBefore2010}, Born after 2010: {bornAfter2010}"};
            File.WriteAllLines(filePath, check2010);
        }
    }
}
