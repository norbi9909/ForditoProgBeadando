using System;   
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fordito
{
    public static class Program
    {
        public static string[,] rulesMatrix = new string[12, 7];
        public static string[] helperArray = new string[7];
        public static Stack stack;

        public static void initRules()
        {
            StreamReader sr = new StreamReader("rule.txt");
            string helper = "";
            int i = 0;
            while (!sr.EndOfStream)
            {
                helper += sr.ReadLine();
                for (int k = 0; k < helperArray.Length; k++)
                {
                    helperArray[k] = helper.Split('|')[k];
                }
                for (int j = 0; j < helperArray.Length; j++)
                {
                    rulesMatrix[i, j] = helperArray[j];

                }
                helper = "";
                i++;
            }
        }

        public static bool checkInput(string input, string[,] matrix)
        {
            string characters = "";
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                characters += matrix[0, j];
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (!characters.Contains(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static string simple(string st)
        {
            return Regex.Replace(st, @"([0-9]+)", "i");
        }

        public static bool reform(string eMatrix)
        {
            if (eMatrix.Length == 0)
            {
                Console.WriteLine("Nem megálló állapot!");
                boolHelper = true;
                return true;
            }

            if(eMatrix.Trim() == "elfogad")
            {
                Console.WriteLine("Elfogadva"); 
                boolHelper = true;
                return true;
            }

            if(eMatrix.Trim() == "pop")
            {
                actualInputElement = actualInputElement.Substring(1); 
                return false;
            }

            if (eMatrix.Contains('('))
            {
                string subHelper = eMatrix.Substring(1).Split(',')[0];
                
                for (int j = subHelper.Length-1; j >= 0; j--)
                {
                    if (subHelper[j].Equals('e'))
                    {
                        continue;
                    }
                    stack.Push(subHelper[j].ToString());
                }
            }

            if (eMatrix.Contains(')'))
            {
                ruleNo += eMatrix.Substring(0, eMatrix.Length - 1).Split(',')[1];
            }
            string stackHelper = "";
            foreach (string e in stack)
            {
                stackHelper += e;
            }

            Console.WriteLine("{0}, {1}, {2}", actualInputElement, stackHelper, ruleNo);

            return false;
        }

        public static StringBuilder sb = new StringBuilder();
        public static string ruleNo = "";
        public static string actualInputElement = "(203+304)*55#";
        public static bool boolHelper = false;
        public static string helper;
        static void Main(string[] args)
        {
            initRules();
            string[] elements = new string[2];
            actualInputElement = simple(actualInputElement);
            stack = new Stack();
            stack.Push("#");
            stack.Push("E");

            Console.WriteLine("Rules: ");


            Console.WriteLine();

            for (int i = 0; i < rulesMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < rulesMatrix.GetLength(1); j++)
                {
                    Console.Write(rulesMatrix[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            if (checkInput(actualInputElement, rulesMatrix))
            {
                do
                {
                    for (int j = 0; j < rulesMatrix.GetLength(1); j++)
                    {
                        if (actualInputElement[0].ToString() == rulesMatrix[0, j])
                        {
                            helper = stack.Pop().ToString();
                            for (int t = 0; t < rulesMatrix.GetLength(0); t++)
                            {
                                if (helper == rulesMatrix[t, 0])
                                {
                                    reform(rulesMatrix[t, j]);
                                }
                            }
                        }
                    }
                } while (!boolHelper);
            }
            else
            {
                Console.WriteLine("Nem megfelelő input!");
            }

            Console.WriteLine("RuleNo: {0}", ruleNo);
            Console.ReadLine();
        }
    }
}
