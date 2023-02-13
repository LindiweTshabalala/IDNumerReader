using System;
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
                IDReader idReader = new IDReader(filePath);
                idReader.ProcessIDNumbers();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    
    class IDReader
    {
        private string[] idNumbers;
        private Regex regex = new Regex(@"^\d{13}$");

        public IDReader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found: " + filePath);
            }

            idNumbers = File.ReadAllLines(filePath);
        }

        public void ProcessIDNumbers()
        {

            int before2010Count = 0;
            int after2010Count = 0;
            string before2010IDNumbers = "";
            string after2010IDNumbers = "";
            
            foreach (string idNumber in idNumbers)
            {
                if (!regex.IsMatch(idNumber))
                {
                    Console.WriteLine("Invalid ID number: " + idNumber);
                    continue;
                }

                IDInfo idInfo = new IDInfo(idNumber);
                if (!idInfo.IsValid())
                {
                    Console.WriteLine("Invalid date of birth: " + idNumber);
                    continue;
                }

                int year = idInfo.GetYear();
                if (year < 2010) 
                {
                    before2010Count++;
                    before2010IDNumbers += idNumber + Environment.NewLine;
                }
                else 
                {
                    after2010Count++;
                    after2010IDNumbers += idNumber + Environment.NewLine;
                }

                WriteToFile("before2010.txt", before2010Count, before2010IDNumbers);
                WriteToFile("after2010.txt", after2010Count, after2010IDNumbers);

                Console.WriteLine("Date of birth: " + idInfo.GetDateOfBirth());
            }
        }

        private void WriteToFile(string fileName, int count, string idNumbers)
        {
            string fileContent = "Number of people: " + count + Environment.NewLine;
            fileContent += "ID Numbers:" + Environment.NewLine;
            fileContent += idNumbers;
            File.WriteAllText(fileName, fileContent);
        }
    }

    class IDInfo
    {
        private int yearPrefix;
        private int year;
        private int month;
        private int day;

        public IDInfo(string idNumber)
        {
            yearPrefix = int.Parse(idNumber.Substring(0, 2));
            year = yearPrefix < 22 ? 2000 + yearPrefix : 1900 + yearPrefix;
            month = int.Parse(idNumber.Substring(2, 2));
            day = int.Parse(idNumber.Substring(4, 2));
        }

        public bool IsValid()
        {
            bool isValidMonth = (month >= 1 && month <= 12);
            bool isValidDay = (day >= 1 && day <= 31);
            bool isValidDate = (isValidMonth && isValidDay);

            return isValidDate;
        }

        public string GetDateOfBirth()
        {
            DateTime dateOfBirth = new DateTime(year, month, day);
            return dateOfBirth.ToString("dd/MM/yyyy");
        }

        public int GetYear()
        {
            return year;
        }
    }
}
