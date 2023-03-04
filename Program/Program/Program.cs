using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            // define and read each line in .txt file
            string filePath = @"C: \Users\sdd\Desktop\infint.txt";
            string[] lines = File.ReadAllLines(filePath);

            //pattern is multiple of 3 
            for (int i = 0; i < lines.Length; i += 3)
            {
                InfInt A = new InfInt(lines[i]);
                InfInt B = new InfInt(lines[i + 1]);
                string qua = lines[i + 2];
                Console.WriteLine(lines[i] + " " + lines[i + 2] + " " + lines[i + 1] + " = ");
                A.answer(B, qua);
                Console.WriteLine();
            }

            Console.ReadLine();

        }
    }
}