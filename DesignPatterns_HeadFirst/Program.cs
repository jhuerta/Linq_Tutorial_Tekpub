using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DesignPatterns_HeadFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User();


            Lesson_5_1s();
            //Lesson_5Bis();
            //Lesson_5();
            //Lesson_4();
            //Lesson_3();
            //Lesson_2();
            //Lesson_1();
        }


        private static  void Lesson_5_1s()
        {
            // Distinct
            // Contains
            // Intersect/Exceptions/

            // UNIQUE locations COMBINING 2 groups
            // CheckUnions();

            // Locations in one group NOT present in other group
            // CheckExceptions();

            // Locations in one group ALSO present in other group
            // CheckIntersections();

            // Playing with Except/Intersect/Union
            //CheckNotPresentLocations();

            // SelectMany
            SelectMany();
        }


        private static void SelectMany()
        {
            var users = LoadUsers();

            var usersIn30 = users.Where(u => u.Age > 29 && u.Age < 31).Take(3);
            var usersIn40 = users.Where(u => u.Age > 39 && u.Age < 41).Take(4);
            var usersIn50 = users.Where(u => u.Age > 49 && u.Age < 51).Take(5);
            var usersIn60 = users.Where(u => u.Age > 59 && u.Age < 61).Take(6);

            var usersIn30and40 = new List<IEnumerable<User>>()
                                     {
                                         usersIn30,
                                         usersIn40,
                                         usersIn50,
                                         usersIn60
                                     };


            var manyUsers_2ndOverload = usersIn30and40.SelectMany(
                u => u,
                (userList, uu) => userList.Count().ToString() + " " + uu.Age + " " + uu.DisplayName
                );

            var manyUsers_3rdOverload_WithIndex = usersIn30and40.SelectMany(
                (u, index) => u.Select(su => index + " " + su.DisplayName));

            var manyUsers_simpleOverload = usersIn30and40.SelectMany(
                u => u);


            var manyUsers_usingSimpleOverload_ToIterateThroughObjectsAndGenerateInfo = users.Take(3).SelectMany(
                u => Enumerable.Range(1,u.Age)
                ); // Generating list of numbers. One list for each user. Each list different length of list.

            Console.WriteLine("In 30: {0}\nIn 40: {1}\nIn 50: {2}\nIn 60: {3}\nIn Total: {4}\nIn Total Computed: {5}\nSize Group: {6}",
                usersIn30.Count(),usersIn40.Count(), usersIn50.Count(), usersIn60.Count(),
                usersIn30.Count()+usersIn40.Count()+usersIn50.Count()+usersIn60.Count(),
                manyUsers_simpleOverload.Count(),usersIn30and40.Count());

            Console.WriteLine("\nOverload: Returning results deep from the list\n");
            foreach (var eachUser in manyUsers_2ndOverload)
            {
                Console.WriteLine(eachUser);
            }

            Console.WriteLine("\nSimple Overload\n");
            foreach (var eachUser in manyUsers_simpleOverload)
            {
                Console.WriteLine(eachUser);
            }

            Console.WriteLine("\nOverload With Index\n");
            foreach (var eachUser in manyUsers_3rdOverload_WithIndex)
            {
                Console.WriteLine(eachUser);
            }

            Console.WriteLine("\nSimple use of select many\n");
            foreach (var each in manyUsers_usingSimpleOverload_ToIterateThroughObjectsAndGenerateInfo)
            {
                Console.WriteLine(each);
            }

            Console.Read();

        }

        private static void CheckNotPresentLocations()
        {
            var users = LoadUsers();
            var locations_20_30 = users.Where(u => u.Age > 20 && u.Age < 22).Select(u => u.Location).Distinct();
            var locations_30_40 = users.Where(u => u.Age > 30 && u.Age < 32).Select(u => u.Location).Distinct();
            var uniqueLocationsIn20_30 = locations_20_30.Except(locations_30_40).Count();
            var uniqueLocationsIn30_40 = locations_30_40.Except(locations_20_30).Count();
            var uniqueLocations20_30_40 = uniqueLocationsIn20_30 + uniqueLocationsIn30_40;

            var uniqueLocations = locations_20_30.Union(locations_30_40)
                .Except(locations_20_30.Intersect(locations_30_40))
                .Except(locations_30_40.Intersect(locations_20_30))
                .Count();

            Console.WriteLine("Unique locations calculated separately: {0}", uniqueLocations20_30_40);
            Console.WriteLine("Unique locations calculated in one shot: {0}", uniqueLocations);
            Console.Read();

        }

        private static void CheckExceptions()
        {
            var users = LoadUsers();

            var locations_20_30 = users.Where(u => u.Age > 20 && u.Age < 22).Select(u => u.Location).Distinct();
            var locations_30_40 = users.Where(u => u.Age > 30 && u.Age < 32).Select(u => u.Location).Distinct();
            var mixed_locations = locations_20_30.Except(locations_30_40);
            Console.WriteLine("Locations in 20-30: {0}", locations_20_30.Count());
            Console.WriteLine("Locations in 20-30: {0}", locations_30_40.Count());
            Console.WriteLine("Locations in 20-30 not present in 30-40: {0}", mixed_locations.Count());

            foreach (var location in mixed_locations)
            {
                Console.WriteLine(location);
            }
            Console.Read();
        }



        private static void CheckUnions()
        {
            var users = LoadUsers();

            var locations_20_30 = users.Where(u => u.Age > 20 && u.Age < 22).Select(u => u.Location).Distinct();
            var locations_30_40 = users.Where(u => u.Age > 30 && u.Age < 32).Select(u => u.Location).Distinct();

            var intersectedLocations = locations_20_30.Intersect(locations_30_40);

            var mixed_locations = locations_20_30.Union(locations_30_40);
            Console.WriteLine("Locations in 20-30: {0}", locations_20_30.Count());
            Console.WriteLine("Locations in 20-30: {0}", locations_30_40.Count());
            Console.WriteLine("Union Locations: {0}", mixed_locations.Count());
            Console.WriteLine("Intersected Locations: {0}", intersectedLocations.Count());

            Console.Read();
        }

        private static void CheckIntersections()
        {
            var users = LoadUsers();

            var locations_20_30 = users.Where(u => u.Age > 20 && u.Age < 22).Select (u => u.Location).Distinct();
            var locations_30_40 = users.Where(u => u.Age > 30 && u.Age < 32).Select(u => u.Location).Distinct();
            var mixed_locations = locations_20_30.Intersect(locations_30_40);
            Console.WriteLine("Locations in 20-30: {0}",locations_20_30.Count());
            Console.WriteLine("Locations in 20-30: {0}",locations_30_40.Count());
            Console.WriteLine("Mixed Locations: {0}", mixed_locations.Count());

            foreach (var location in mixed_locations)
            {
                Console.WriteLine(location);
            }
            Console.Read();
        }

        private static void Lesson_5Bis()
        {
            var users = LoadUsers();

            // LINQ ORDERING OPERATORS
            // OrderBy / OrderByDescending
            // OrderBy with custom IComparer
            // ThenBy / ThenByDescending
            // Reverse

            // LINQ PARTIONING OPERATORS
            // Take / TakeWhile
            // Skip / SkipWhile -> Useful for Pagination! :D

            // LINQ AGGREGATORS OPERATORS
            // Count() / Count({condition} / CountLong
            // Min() / Max() (on int or adding condition). You need to implement IComparable
            // Sum / Average (a field)
            // Aggregate method: 3 overloads!

            // 1st Overload of Aggregator
            var result_with_custom_aggregate_1st = users
                .Where((u, i) => u.Location == "Spain")
                .Aggregate(0, (a, u) => u.UpVotes + a);

            var result_with_custom_aggregate_sum = users
                .Where((u, i) => u.Location == "Spain").Sum(u => u.UpVotes);


            Console.WriteLine("Custom Sum: {0}", result_with_custom_aggregate_1st);
            Console.WriteLine("Real Sum: {0}", result_with_custom_aggregate_sum);

            // 2nd Overload of Aggregator
            var result_with_custom_aggregate_2nd = users
                .Where((u, i) => u.Location == "Spain")
                .Take(10)
                .Aggregate(
                    new {Counter = 0, Sum = 0},
                    (a, u) => new
                                  {
                                      Counter = a.Counter + 1,
                                      Sum = a.Sum + u.UpVotes
                                  }, a => a.Sum/a.Counter);


            var result_with_custom_aggregate_average = users
                .Where((u, i) => u.Location == "Spain")
                .Take(10)
                .Average(u => u.UpVotes);

            Console.WriteLine("Custom Average: {0}", result_with_custom_aggregate_2nd);
            Console.WriteLine("Real Average: {0}", result_with_custom_aggregate_average);

            // 3rd Overload of Aggregator
            var result_with_custom_aggregate_3rd = users
                .Where((u, i) => u.Location == "Spain")
                .Select(u => u.UpVotes).Take(1)
                .Aggregate((upVote1, upVote2) => 1);
            Console.WriteLine("Sum with  first overlaad: {0}", result_with_custom_aggregate_3rd);
            
            var result_with_ordering = users
                .Where((u, i) => u.Location == "Spain")
                .OrderBy(u => u.DisplayName, StringComparer.CurrentCulture)
                .ThenBy(u => u.Age);

            var result_with_take = users
                .Where((u, i) => u.Location == "Spain")
                .Take(10)
                .Select((u, i) => new {OwnId = i, Age = u.Age}).SkipWhile(u => u.Age > 30);

            var result_with_aggregators = users
                .Where((u, i) => u.Location == "Spain")
                .Take(10)
                .Max();



                //.Select((u) => new
                //{
                //    About = u.AboutMe,
                //    DVotes = u.DownVotes,
                //    UVotes = u.UpVotes
                //}
                //);

            var resultCollection = result_with_aggregators;
            Console.WriteLine("The Result: {0}", resultCollection);
            //foreach (var result in resultCollection)
            //{
            //    Console.WriteLine("\n\tId: {0}\tAge: {1}", result.OwnId, result.Age);
            //    //Console.WriteLine(result);
            //}

            Console.Read();
        }

        private static void Lesson_3()
        {

            
            var users = LoadUsers();


            var resultCollection_with_object_initializers = users
                .Where((u, i) => u.Location == "Spain" && i < 2000)
                .Select((u) => new QueryResult() 
                                   {
                                       DisplayName = u.DisplayName,
                                       Age = u.Age
                                   }
                );


            var result_with_logic_in_select = users
                .Where((u, i) => u.Location == "Spain" && i < 2000)
                .Select((u) =>
                            {
                                var queryResult = new QueryResult();
                                queryResult.DisplayName = u.DisplayName;
                                queryResult.Age = u.Age;
                                return queryResult;
                            }
                );

            var result_with_Tuples = users
                .Where((u, i) => u.Location == "Spain" && i < 2000)
                .Select((u) => new QueryTuple<string,int>(u.DisplayName,u.Age)
                );

            var result_with_TupleBuilder = users
                .Where((u, i) => u.Location == "Spain" && i < 2000)
                .Select((u) => QueryTupleBuilder.Build(u.DisplayName, u.Age)
                );

            var result_with_AnonymousTypes = users
                .Where((u, i) => u.Location == "Spain")
                .Select((u) => new
                                   {
                                       About = u.AboutMe,
                                       DVotes = u.DownVotes,
                                       UVotes = u.UpVotes
                                   }
                );


            var resultCollection = result_with_AnonymousTypes;

            foreach (var result in resultCollection)
            {
                //Console.WriteLine("\n\tAbout: {0}\n\tDown Votes: {1}\n\tUp Votes: {2}", result.About, result.DVotes, result.UVotes);
            } 

            Console.WriteLine("\n\n\t Number of Results: {0}", resultCollection.Count());

            PlayingWithAnonymousTyes();

            object anonymousType = GiveMeAnAnoymousType("first_value", 123);

            Console.WriteLine("\t\nAnoymous Object -> First Field: {0} \t\nSecond Object: {1}",
                              anonymousType.ToString(),
                              anonymousType.ToString()
                );

            Console.Read();

        }

        private static void Lesson_5()
        {
            // .OrderBy
            
            var users = LoadUsers();

            //var results_using_skip_take_and_so_on = users
            //    .Where((u, i) => u.Age > 30 && u.Age < 45)
            //    .OrderBy(u => u.Age).ThenBy(u => u.DisplayName).SkipWhile(u => u.Age < 42);


            // 1st Aggregate Overload
            var implementing_sum_v3 = users
                //.Where((u, i) => u.Age > 30 && u.Age < 45)
                .Take(5)
                .Aggregate("*",(a, u) => a + "-" + u.UpVotes.ToString());
            Console.WriteLine("Aggregating String: " + implementing_sum_v3);


            // 1st Aggregate Overload
            var implementing_sum_v2 = users
                //.Where((u, i) => u.Age > 30 && u.Age < 45)
                .Select(u => u.UpVotes)
                .Take(2).Skip(1)
                .Aggregate( (i1, i2) => i1 + i2);

            Console.WriteLine("Funky Sum: " + implementing_sum_v2);

            // 2nd Overload
            var implementing_sum = users
                //.Where((u, i) => u.Age > 30 && u.Age < 45)
                //.Select(u => u.UpVotes)
                .Take(50)
                .Aggregate(
                    0, (a, u) => u.UpVotes + a);

            var using_sum = users
                //.Where((u, i) => u.Age > 30 && u.Age < 45)
                //.Select(u => u.UpVotes)
                .Take(50)
                .Sum(u => u.UpVotes);

            
            // 3rd Overload
            var implementing_average = users.Take(50)
                .Aggregate(
                    new { Count = 0, UpVotes = 0},
                    (accomulator, currentUser) => new {Count = accomulator.Count+1, UpVotes = accomulator.UpVotes + currentUser.UpVotes},
                    total => total.UpVotes/(float) total.Count
                );
            var real_average = users.Take(50)
                .Average(u => u.UpVotes);

            Console.WriteLine(implementing_sum);
            Console.WriteLine(using_sum);
            Console.WriteLine(implementing_average);
            Console.WriteLine(real_average);
            
            //foreach (var result in results)
            //{
            //    //Console.WriteLine("\n\tAbout: {0}\n\tDown Votes: {1}\n\tUp Votes: {2}",
            //    //                  result.About,
            //    //                  result.DVotes, result.UVotes
            //    //    );
            // Console.WriteLine(result);

            //    //Console.WriteLine("{0}_{1}_{2}",result.Age,count,result.DisplayName);
            //}

            Console.Read();
        }

        private static void Lesson_4()
        {
            var users = LoadUsers();

            var result_with_AnonymousTypes = users
                .Where((u, i) => u.Location == "Spain")
                .Select((u) => new
                                   {
                                       About = u.AboutMe,
                                       DVotes = u.DownVotes,
                                       UVotes = u.UpVotes
                                   }
                );


            var resultCollection = result_with_AnonymousTypes;

            foreach (var result in resultCollection)
            {
                Console.WriteLine("\n\tAbout: {0}\n\tDown Votes: {1}\n\tUp Votes: {2}",
                                  result.About,
                                  result.DVotes, result.UVotes
                    );
            }

            Console.Read();
        }
        private static object GiveMeAnAnoymousType(string text, int number)
        {
            var theObject_noVariableNames = new
                                                {
                                                    text,
                                                    number
                                                };
            var theObject_WithVariableNames = new
                                                  {
                                                      Text = text,
                                                      Number = number
                                                  };

            var representation_NoVarNames = String.Format(
                "Name: {0} - Number: {1}",
                theObject_noVariableNames.text,
                theObject_noVariableNames.number
                );

            var representation_VarNames = String.Format(
                "Name: {0} - Number: {1}",
                theObject_WithVariableNames.Text,
                theObject_WithVariableNames.Number
                );

            return theObject_WithVariableNames;
        }

        private static void PlayingWithAnonymousTyes()
        {
            var anony_1 = new {name = "a", age = 2};
            var anony_2 = new { name = "a", age = 2 };
            Console.Write(
                "Are they equal?: {0}",
                anony_1.GetType() == anony_2.GetType()
                );
        }


        private static IEnumerable<User> LoadUsers()
        {
            var xdoc = XDocument.Load(@"../../Files/users.xml");
            var userMapper = new UserMapper();
            return userMapper.Map(xdoc.Descendants("row"));
        }

        private static void Lesson_1()
        {
            LearningWhere_UsingLetters();

            LearningWhere_UsingIntegers();
        }

        private static void Lesson_2()
        {
            var users = LoadUsers();
            //var result = users.Where(u => u.Age < 10).Where(u => u.Age > 0);
            //var result = users.Where(u => u.Age < 10 && u.Age > 0 || u.DisplayName == "Justin Etheredge");
            var result = users.Select((u, i) => i);//.Where((u,i) => u.Location == "Spain" && i < 2000).Select( (u,i) => i);

            //var result = from u in users where u.Age > 80 && u.Age < 85 select u;

            foreach (var user in result)
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("\n\n\t Number of Results: {0}", result.Count());

            Console.Read();


        }

        public static void LearningWhere_UsingLetters()
        {

            var letters = new[] { "a", "b", "s", "s", "d", "e", "s", };

            Func<string, bool> theLetterFilter = (s => s == "s");

            var lettersFiltered = letters.FilterWithYield<string>(s => s == "s");

            var lettersFilteredWithDelegate = letters.StringFilter(theLetterFilter);

            foreach (var i in lettersFiltered)
            {
                Console.WriteLine("Main Loop: {0}", i);
            }

            Console.Read();

        }

        public static void LearningWhere_UsingIntegers()
        {

            var numbers = Enumerable.Range(1, 20); ;

            var usingYieldAndThenYield = numbers.FilterWithYield(n => n % 2 == 0).FilterWithYield(n => n % 13 == 0);
            //var usingClassicAndThenClassic = numbers.FilterClassicFull(n => n % 2 == 0).FilterClassicFull(n => n % 3 == 0);
            //var usingYieldThenClassic =  numbers.FilterWithYield(n => n % 2 == 0).FilterClassicFull(n => n % 3 == 0);
            //var usingClassicAndThenYield = numbers.FilterClassicFull(n => n % 2 == 0).FilterWithYield(n => n % 3 == 0);
            //var usingYieldAndThenClassic = numbers.FilterClassicFull(n => n % 2 == 0).FilterWithYield(n => n % 3 == 0);


            var numbersFiltered = usingYieldAndThenYield;

            foreach (var i in numbersFiltered)
            {
                Console.WriteLine("Main Loop: {0}", i);
            }

            Console.Read();

        }

        private static bool IsAnS(string letter)
        {
            return letter == "s";
        }
        private static bool IsOddNumber(int number)
        {
            return number % 2 == 0;
        }
        private static bool IsEvenNumber(int number)
        {
            return number % 2 != 0;
        }



        public class QueryTuple<T1, T2>
        {
            public T1 FirstItem { get; private set; }
            public T2 SecondItem { get; private set; }

            public QueryTuple(T1 firstItem, T2 secondItem)
            {
                FirstItem = firstItem;
                SecondItem = secondItem;
            }
        }

        class QueryTupleBuilder
        {
            public static QueryTuple<T1, T2> Build<T1,T2>(T1 item1, T2 item2)
        {
            return new QueryTuple<T1, T2>(item1, item2);
        }
        }
    }

    public class UserComparer : IComparer<User>
    {
        public int Compare(User x, User y)
        {
            if (x.Age == y.Age)
            {
                return string.Compare(x.DisplayName,y.DisplayName);
            }

            return (x.Age > y.Age) ? 1 : -1;  
        }
    }


    public class QueryResult  
    {
        public QueryResult()
        {
        }

        public QueryResult(string displayName, int age)
        {
            DisplayName = displayName;
            Age = age;
        }

        public string DisplayName { get; set; }
        public int Age { get; set; }
    }


    public class UserMapper
    {
        public IEnumerable<User> Map(IEnumerable<XElement> descendants)
        {
            var userCollection = new List<User>();
            foreach (var descendant in descendants)
            {
                userCollection.Add(new User()
                                       {
                                           DisplayName = (string)descendant.Attribute("DisplayName"),
                                           Age = descendant.Attribute("Age") == null ? 0 : (int) descendant.Attribute("Age"),
                                           CreationDate = (DateTime)descendant.Attribute("CreationDate"),
                                           DownVotes = (int)descendant.Attribute("DownVotes"),
                                           Id = (int)descendant.Attribute("Id"),
                                           LastAccessDate = (DateTime)descendant.Attribute("LastAccessDate"),
                                           Reputation = (int)descendant.Attribute("Reputation"),
                                           UpVotes = (int)descendant.Attribute("UpVotes"),
                                           Views = (int)descendant.Attribute("Views"),
                                           WebsiteUrl = (string)descendant.Attribute("WebsiteUrl"),
                                           AboutMe = (string)descendant.Attribute("AboutMe"),
                                           Location = (string)descendant.Attribute("Location"),

                                       });

            }

            return userCollection;
        }
    }

    public class User : IComparable
    {

        public User()
        {
            DisplayName = "";
            WebsiteUrl = "";
            DisplayName = "";
            AboutMe = "";
        }

        public int Id { get; set; }
        public int Reputation { get; set; }
        public int Views { get; set; }
        public int Age { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }

        public string DisplayName { get; set; }
        public string WebsiteUrl { get; set; }
        public string Location { get; set; }
        public string AboutMe { get; set; }

        public DateTime LastAccessDate { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    "\n\tName: {0}\n\tReputation: {1}\n\tWebsite: {2}\n\tAge: {3}\n\tLocation: {4}\n\tUpVotes: {5}\n\tDownVotes: {6}",
                    DisplayName,
                    Reputation,
                    WebsiteUrl,
                    Age,
                    Location,
                    UpVotes,
                    DownVotes);
        }

        public int CompareTo(object obj)
        {
            var newUser = (User) obj;
            if (this.Age == newUser.Age)
            {
                return string.Compare(this.DisplayName, newUser.DisplayName);
            }

            return (this.Age > newUser.Age) ? -1 : 1;  

        }
    }

    public static class MyExtenstions
    {
        public static void WriteToConsole(this string content)
        {
            Console.WriteLine(content);
        }

        public static IEnumerable<T> FilterWithYield<T>(this IEnumerable<T> numbers, Func<T, bool> Filter)
        {
            var result = new List<T>();
            foreach (var number in numbers)
            {
                if (Filter(number))
                {
                    Console.WriteLine("Yield FILTER Loop: {0}", number);
                    yield return number;
                }
            }
            //return result;
        }

        public static IEnumerable<T> FilterClassicFull<T>(this IEnumerable<T> numbers, Func<T, bool> Filter)
        {
            var result = new List<T>();
            foreach (var number in numbers)
            {
                if (Filter(number))
                {
                    //Console.WriteLine("FILTER Classic Loop: {0}", number);
                    result.Add(number);
                }
            }
            return result;
        }

        public static IEnumerable<string> StringFilter(this IEnumerable<string> collection, Func<string, bool> FilterToApply)
        {
            var result = new List<string>();
            foreach (string each in collection)
            {
                if (FilterToApply(each))
                {
                    result.Add(each);
                }

            }
            return result;

        }

    }
}



