using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns_HeadFirst
{
    class Program
    {
        static void Main(string[] args)
        {

            LearningWhere();

            return;
            var duck = new MallardDuck();
            duck.Display();
            duck.performFly();
            duck.performQuack();

            Console.Read();

            // C# 3.0 

            // Var Keyword
            var myInteger = 2;
            var myString = "string";

            // Collection Initializers
            var myarrayofInts = new[] {1,2,3};
            var myListofInts = new List<int> { 1, 2, 3 };
            var myCoolDictionary = new Dictionary<int, string>
                                       {
                                           {1, "uno"},
                                           {2, "dos"}
                                       };

            // Extension Methods
            var myFirstLink = new[] { 1, 2, 3 };
            "my string".WriteToConsole();

            var nums = new[] { 1, 2, 3, 4,5 };
            //nums.Where();
            


        }


        public static void LearningWhere()
        {

            var numbers = new[] {1, 2, 3, 4, 5, 6};



            var numbersFiltered = numbers.Filter(n =>
                                                      {
                                                          n++;
                                                          return n % 2 == 0;
                                                      }
                );
            foreach (var i in numbersFiltered)
            {
                Console.WriteLine(i);
            }

            var letters = new[] { "a", "b", "s", "s", "d", "e", "s", };

            Func<string,bool> theLetterFilter = delegate (string s)
                                                    {
                                                        return s == "s";
                                                    };

            var lettersFiltered = letters.Filter(s => s == "s");
            foreach (var i in lettersFiltered)
            {
                Console.WriteLine(i);
            }

            Console.Read();

        }

 

        private static bool SLetters(string letter)
        {
            return letter == "s";
        }
        private static bool OddNumber(int number)
        {
            return number%2 == 0;
        }

        private static bool EvenNumber(int number)
        {
            return number % 2 != 0;
        }
    }

    public static class MyExtenstions
    {
        public static void WriteToConsole(this string content)
        {
            Console.WriteLine(content);
        }

        public static List<T> Filter<T>(this IEnumerable<T> numbers, Func<T, bool> Filter)
        {
            var result = new List<T>();
            foreach (var number in numbers)
            {
                if (Filter(number))
                {
                    result.Add(number);
                }
            }
            return result;
        }
    }


}
