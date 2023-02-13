using System;
using System.IO;
using System.Text.RegularExpressions;

namespace IDNumberReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "idNumbers.txt";
            
            try 
            {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found: " + filePath);
            }

            string[] idNumbers = File.ReadAllLines(filePath);
            
            string pattern = @"^\d{13}$";
            Regex regex = new Regex(pattern);

            foreach (string idNumber in idNumbers)
            {
                if (!regex.IsMatch(idNumber))
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

                DateTime dateOfBirth = new DateTime(year, month, day);
                Console.WriteLine("Date of birth: " + dateOfBirth.ToString("dd/MM/yyyy"));
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
        }
    }

