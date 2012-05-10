using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using System.Xml.Linq;
namespace DesignPatterns_HeadFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Lesson_8();
            //Lesson_7();

            //Lesson_6();
            //Lesson_5_1s();
            //Lesson_5Bis();
            //Lesson_5();
            //Lesson_4();
            //Lesson_3(); 
            //Lesson_2();
            //Lesson_1();
        }


        





        private static void Lesson_8()
        {
            var id = "Id";
            var firstName = "firstName";
            var lastName = "lastName";

            var xmlPeople = new XElement("people",
                                         new XElement(
                                             "person",
                                             new XAttribute(id, 0),
                                             new XElement(firstName, "Michael"),
                                             new XElement(lastName, "Jordan")
                                             ),
                                         new XElement(
                                             "person",
                                             new XAttribute(id, 1),
                                             new XElement(firstName, "Scott"),
                                             new XElement(lastName, "Pipper")
                                             ),
                                         new XElement(
                                             "person",
                                             new XAttribute(id, 2),
                                             new XElement(firstName, "Larry"),
                                             new XElement(lastName, "Bird")
                                             )
                );
            Console.Write(xmlPeople);

            var peopleList = new List<Person>
                                {
                                    new Person(0, "Michael", "Jordan"),
                                    new Person(1, "Scott", "Pipper"),
                                    new Person(2, "Larry", "Bird")
                                };

            var xmlPeople_2 = new XElement("people");
            foreach (var person in peopleList)
            {
                xmlPeople_2.Add(new XElement(
                                    "person",
                                    new XAttribute(id, person.Id),
                                    new XElement(firstName, person.firstName),
                                    new XElement(lastName, person.lastName)
                                    )
                    );

            }


            var xmlPeople_3 = new XElement("people",
                                           peopleList.Select(person => new XElement(
                                                                           "person",
                                                                           new XAttribute(id, person.Id),
                                                                           new XElement(firstName, person.firstName),
                                                                           new XElement(lastName, person.lastName)
                                                                           )));

            Console.WriteLine(xmlPeople);
            Console.WriteLine("--------------------");
            Console .WriteLine(xmlPeople_2);
            Console.WriteLine("--------------------");
            Console.WriteLine(xmlPeople_3);
            Console.WriteLine("--------------------");




            const string peopleXML = @"<people>
                              <person Id=""0"">
                                <firstName>Michael</firstName>
                                <lastName>Jordan</lastName>
                              </person>
                              <person Id=""1"">
                                <firstName>Scott</firstName>
                                <lastName>Pipper</lastName>
                              </person>
                              <person Id=""2"">
                                <firstName>Larry</firstName>
                                <lastName>Bird</lastName>
                              </person>
                            </people>";
            var parsedXML = XElement.Parse(peopleXML);

            Console.WriteLine(parsedXML);


            var larry = parsedXML
                .Descendants("person")
                .Where(x => x.Attribute("Id").Value == "2")
                .Single();

            var larryByElements = parsedXML
                .Elements("person")
                .Where(x => x.Attribute("Id").Value == "2")
                .Single();
            var larryByElementValue = parsedXML
                .Elements("person")
                .Where(x => x.Element("firstName").Value == "Larry")
                .Single();
            var larryByOperation = parsedXML
    .Elements("person")
    .Where(x => x.Element("firstName").Value.StartsWith("Mic"))
    .Single();

            var larryByOperationReturnList = parsedXML
                .Elements("person")
                .Where(x => x.Element("lastName").Value.Contains("i"));


            var creatingXElement = new XElement("newPeople",
                                                parsedXML
                                                    .Elements("person")
                                                    .Where(x => x.Element("lastName").Value.Contains("i"))
                );



            const string new_peopleXML = @"<people>
                              <person Id=""0"" address_id=""1"">
                                <firstName>Michael</firstName>
                                <lastName>Jordan</lastName>
                              </person>
                              <person Id=""1"" address_id=""2"">
                                <firstName>Scott</firstName>
                                <lastName>Pipper</lastName>
                              </person>
                              <person Id=""2"" address_id=""3"">
                                <firstName>Larry</firstName>
                                <lastName>Bird</lastName>
                              </person>
                            </people>";

            const string addresses =
                @"<addresses>
                    <address id=""1"">
                        <street>This is the street 1</street>
                        <city>City 1</city>
                    </address>
                    <address id=""2"">
                        <street>This is the street 2</street>
                        <city>City 2</city>
                    </address>
                    <address id=""3"">
                        <street>This is the street 3</street>
                        <city>City 3</city>
                    </address>
                </addresses>";

            var addressesXML = XElement.Parse(addresses);
            var peopleWithAddressXML = XElement.Parse(new_peopleXML);
            var result = new XElement("people",
                                      peopleWithAddressXML.Elements("person")
                                          .Join(addressesXML.Elements("address"),
                                                p => p.Attribute("address_id").Value,
                                                a => a.Attribute("id").Value,
                                                (qq, aa) => new XElement(
                                                                "person",
                                                                qq.Element("firstName"),
                                                                qq.Element("lastName"),
                                                                aa.Element("street"),
                                                                aa.Element("city"),
                                                                new XElement("AddId", aa.Attribute("id").Value)
                                                                )
                                          )
                );


            Console.WriteLine("+++++++++++++++++++++++++++++");
            Console.WriteLine(larry);
            Console.WriteLine("+++++++++++++++++++++++++++++");
            Console.WriteLine(larryByElements);
            Console.WriteLine("+++++++++++++++++++++++++++++");
            Console.WriteLine(larryByElementValue);
            Console.WriteLine("+++++++++++++++++++++++++++++");
            Console.WriteLine(larryByOperation);
            Console.WriteLine("__________________________");
            Console.WriteLine(larryByOperationReturnList);
            Console.WriteLine("+++++++++++++++++++++++++++++");
            Console.WriteLine(creatingXElement);
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(result);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine(result);
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");


            


            if (Xml_people_equals_xml_people2(xmlPeople, xmlPeople_2, xmlPeople_3, parsedXML))
            {
                Console.WriteLine("Equal!!!!!!!!!!!!!!!");
            }









            Console.Read();
        }

        private static bool Xml_people_equals_xml_people2(XElement p1, XElement p2, XElement p3, XElement p4)
        {
            //return xmlPeople.ToString().CompareTo(xmlPeople_2.ToString()) ==0;
            return (p1.Value == p2.Value) 
                && (p2.Value == p3.Value)
                && (p1.Value == p3.Value)
                && (p1.Value == p4.Value)
                && (p2.Value == p4.Value)
                && (p3.Value == p4.Value);
        }

        public class Person
        {
            public int Id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }

            public Person(int id, string firstName, string lastName)
            {
                Id = id;
                this.firstName = firstName;
                this.lastName = lastName;
            }

            public Person()
            {

            }
        }






        private static void Lesson_7()
        {
            AddCategoriesProducts();

            var category1 = new Category
                                {
                                    Id = 0,
                                    Name = "Hats" 
                                };
            var category2 = new Category
                                {
                                    Id = 1,
                                    Name = "Shoes"
                                };
            using (var context = new TekPubDataContext())
            {
                var products = context.Products.Where(p => p.Name.StartsWith("C")).First();
                
                Console.Write(products.Name);
                //Console.Read();

                //var deleteCategory = new Category
                //{
                //    Id = 4,
                //    Name = "New Category"
                //};
                //context.Categories.InsertOnSubmit(deleteCategory);

                var deleteCategory = context.Categories.Where(c => c.Id == 4).Single();

                context.Categories.DeleteOnSubmit(deleteCategory);
                context.SubmitChanges();

                var hatCategory = context.Categories.Where(c => c.Name == "Hats").First();
                hatCategory.Name = "Hats";
                context.SubmitChanges();

            }

            

        }

        private static void AddCategoriesProducts()
        {
            using (var context = new TekPubDataContext())
            {

                var category1 = new Category
                                    {
                                        Id = 0,
                                        Name = "Hats"
                                    };
                var category2 = new Category
                                    {
                                        Id = 1,
                                        Name = "Shoes"
                                    };

                var hatsCategories = context.Categories.Single(u => u.Name == "Hats");
                var shoesCategories = context.Categories.Single(u => u.Name == "Shoes");

                var product1 = CreateProduct(hatsCategories, "Top Hat", 14);
                var product2 = CreateProduct(hatsCategories, "Floppy Hat", 15);
                var product3 = CreateProduct(hatsCategories, "Cowboy Hat", 16);
                var product4 = CreateProduct(hatsCategories, "Fedora", 17);
                var product5 = CreateProduct(hatsCategories, "Sneaker", 18);
                var product6 = CreateProduct(hatsCategories, "High Heel", 19);
                var product7 = CreateProduct(hatsCategories, "Dress Shoe", 20);


                context.Products.InsertOnSubmit(product1);
                context.Products.InsertOnSubmit(product2);
                context.Products.InsertOnSubmit(product3);
                context.Products.InsertOnSubmit(product4);
                context.Products.InsertOnSubmit(product5);
                context.Products.InsertOnSubmit(product6);
                context.Products.InsertOnSubmit(product7);


                context.SubmitChanges();
            }

        }

        private static Product CreateProduct(Category hatsCategories, string name, int id)
        {
            return new Product()
            {
                Id = id,
                Name = name,
                Category = hatsCategories,
                CategoryId = 0
            };
            
            return new Product()
                       {
                           Id = id,
                           Name = name,
                           Category = hatsCategories
                       };
        }


        private static void Lesson_6()
        {

            // ToList // ToArray // ToDictionary 

            // ToLookUp: Grouping! Powerfull! Grouping elements by a key, or properties of the elements by anykey.
            // ToLookUp: WATCH OUT! Forces Execution! Go for GroupBy instead, which is delayed execution!

            // GroupBy: Faster (do not force execution) and more flexible: Allows operations. Amazing to group and perform ops in the groups

            // Join: As expected in SQL

            // GroupJoin: Similar to Join but with Group capabilities - Instead of returning a table, where on the left side we repeat the values, and on the
            // right side, the unique values, here we are grouping. So, every object is composed of a name, and a list ob elements. In the Join, the 
            // is a list of name and unique element. Allows Right/Left join --> REQUIRES DETAILED STUDY :D

            // Zip: 4.0


            // Cast


             
            var users = LoadUsers();
            var userList = users.Select(u => u).ToList();
            var userArray = users.Select(u => u).ToArray();
            var userDictionary_v1 = users.Select(u => u).ToDictionary(u => u.Id);
            var userDictionary_v2 = users.Select(u => u).ToDictionary(u => u.Id, u => u.Location);
            var userLookUp = users.Select(u => u).ToLookup(u => u.Location, u => u.Id).Where(u => u.Count() == 3);
            var userGroupsByAge = users.ToLookup(u => u.Age).OrderBy(u => u.Key);

            var userGroupsUsers_By_Age = users.GroupBy(u => u.Age).OrderBy(u => u.Key);
            var userGroupsNames_By_Age = users.GroupBy(u => u.Age,u => u.DisplayName).OrderBy(u => u.Key);
            var userGroupsNames_By_Age_Return_Operation = users.GroupBy(
                u => u.Age,
                u => u,
                (age, uList) => new
                                       {
                                           Age = age,
                                           UpVoteAverage = uList.Select(u => u.UpVotes).Average(),
                                           DwnVoteAverage = uList.Select(u => u.DownVotes).Average()
                                       }
                ).OrderBy( u=> u.Age);

            var userGroupsNames_By_Age_VoteAveage = users.GroupBy(
                u => u.Age,
                u => u.UpVotes,
                (age, upVotes) => new
                                      {
                                          Age = age,
                                          UpVoteAverage = upVotes.Average()
                                      }
                ).OrderBy(u => u.Age);

            //var first10Users = users.Take(10);
            //var second10Users = users.Take(20).Skip(10);
            //var zippedUsers = first10Users.Zip(second10Users, (firsts, seconds) => firsts + " " + seconds);


            // CONVERSATION OPERATIONS
            // Cast and OfType

            // Concat

            var list = new ArrayList();
            list.Add(1);
            list.Add("2");
            list.Add(3);
            list.Add(4);
            list.Add(5);

            // Exception, it can't cast "2" to Integer
            var strongTypeList = list.Cast<int>().Select(U => U);

            // It filters the "2"
            var intStrongTypeList = list.OfType<int>().Select(U => U);

            // It filters all the numbers
            var stringStrongTypeList = list.OfType<string>().Select(U => U);

            //var list1 = new[] {1,2,3};
            var list1 = new[] { "4","5","6"};
            var list2 = new[] { "4", "5", "6" };
            var list3 = list1.Concat(list2);

            //foreach(var element in userDictionary_v2)
            //{
            //    Console.WriteLine("{0} - {1}",element.Key, element.Value);
            //}
            Console.WriteLine("\n _______ \n");
            foreach (var group in userLookUp)
            {

                Console.Write("Loc: {0} \t",group.Key );
                foreach (var id in group)
                {
                    //Console.Write(id + ",");
                }
                Console.Write("\n");
            }

            Console.WriteLine("\n _______ \n");
            foreach (var ageGroup in userGroupsByAge)
            {
                //Console.WriteLine("Age: {0} \t {1}", ageGroup.Key,ageGroup.Count());
            }

            Console.WriteLine("\n _______ \n");
            foreach (var ageGroup in userGroupsNames_By_Age)
            {
                //Console.WriteLine("Age: {0} \t {1}", ageGroup.Key, ageGroup.Count());
            }


            Console.WriteLine("\n _______ \n");
            foreach (var ageVoteAverage in userGroupsNames_By_Age_Return_Operation)
            {
                Console.WriteLine("Age: {0} \t Uvt: {1} \t Dvt: {2}", ageVoteAverage.Age, ageVoteAverage.UpVoteAverage,ageVoteAverage.DwnVoteAverage);
            }

            Console.Read();

        }

        private static  void Lesson_5_1s()
        {
            // Quantifying Operators
            //QuantifyingOperators();

            // OrderOperators
            //OrderOperators();

            // SelectOperators: ElementAt/Contains/Distinct/Intersect/Except/Union
            SelectOperators();

            // Playing with Except/Intersect/Union
            //CheckNotPresentLocations();

            // SelectMany
            //SelectMany(): 
        }


        private static void SelectOperators()
        {
            var users = LoadUsers();
            
            var distinct = users.Take(500).Select(u => u.Location).Distinct();
            Console.Write("Distinct Countries: {0}", distinct.Count());

            const string country = "Spain";
            var contains = users.Take(556).Select(u => u.Location).Contains(country);
            var firstSpanishUser = users.ElementAt(555);
            Console.Write(firstSpanishUser);

            Console.Write("\n------\n");

            var first50SpanishUserAges = users.Where(u => u.Location == "Spain").Select(u => u.Age).Take(50);
            var first50FrenchUserAges = users.Where(u => u.Location == "France").Select(u => u.Age).Take(50);

            var agesInSpainNotInFrance = first50SpanishUserAges.Except(first50FrenchUserAges);
            var agesInBoth = first50SpanishUserAges.Intersect(first50FrenchUserAges);

            var unitingAges = agesInSpainNotInFrance.Union(agesInBoth);


            Console.Write("\n------\n");
            Console.Write("\nOnly in Spain\n");
            foreach (var age in agesInSpainNotInFrance)
            {
                Console.WriteLine(age);
            }


            Console.Write("\n------\n");
            Console.Write("\nAges in Both\n");
            foreach (var age in agesInBoth)
            {
                Console.WriteLine(age);
            }

            Console.Write("\nAges in France\n");
            Console.Write("\n------\n");
            foreach (var user in first50FrenchUserAges)
            {
                Console.WriteLine(user);
            }

            var firstSpanishUserBetterWay = users.First(u => u.Location == "Spain");
            Console.Write("\n------\n");
            Console.Write("\nFirst Spanish User In a Better way\n");

            Console.Write(firstSpanishUserBetterWay);

            Console.Write("\nContains {0}?: {1}", country, contains);

            //var result = defaultIfEmpty.First();

            var ownUsers = new[]{
                                   new
                                   {
                                           u = 1,
                                           numbers = new[] {1, 2, 3, 4}
                                       },
                                   new
                                   {
                                           u = 2,
                                           numbers = new[] {5, 6, 7, 8}
                                       },
                                   new
                                   {
                                           u = 3,
                                           numbers = new[] {9, 10, 12, 13}
                                       }
                               };

            Console.Write("\n---SELECT MANY ---\n");
            var numbersFromUser1stOverload = ownUsers.SelectMany(u => u.numbers);
            var numbersFromUser2ndOverload = ownUsers.SelectMany(u => u.numbers, (u,b) => "Num: " + u.u + " NumbList: " + b);
            var numbersFromUserd = ownUsers.SelectMany(u =>
                                                           {
                                                               return new[] {u.u}; // Converting the number into a list
                                                           });

            var usingTheDataInsie = ownUsers.SelectMany( u => u.numbers.Select( a => "The Number: " + a.ToString() ) );
            Console.Write("\nAS LONG AS YOU SELECT A LIST, YOU ARE FINE! :\n");
            foreach (var number in usingTheDataInsie)
            {
                Console.WriteLine(number);
            }
            Console.Write("\nEND--SELECT MANY --END\n");

            foreach (var number in numbersFromUser1stOverload)
            {
                Console.WriteLine(number);
            }
            Console.Write("\nEND--SELECT MANY --END\n");

            foreach (var number in numbersFromUser2ndOverload)
            {
                Console.WriteLine(number);
            }

            Console.Write("\nEND--SELECT MANY --END\n");
            foreach (var number in numbersFromUserd)
            {
                Console.WriteLine(number);
            }




            //Console.Write(result);
            Console.Read();
        }

        private static void OrderOperators()
        {
            var users = LoadUsers();
            var single = users.Take(500).Where(u => u.Id == 5).Single();
            var singleOrDefault = users.Take(500).Where(u => u.Id == -5).SingleOrDefault();
            var first = users.Take(500).Where(u => u.Id > 5).First();
            var firstOrDefault = users.Take(500).Where(u => u.Id < -5).FirstOrDefault();

            var last = users.Take(500).Where(u => u.Id < 5).Last();
            var lastOrDefault = users.Take(500).Where(u => u.Id < -5).LastOrDefault();

            var elementAt = users.ElementAt(0);
            var elementAtOrDefault = users.ElementAtOrDefault(-1);

            var defaultIfEmpty = users.Where(u => u.Id < -5).DefaultIfEmpty();


            var result = defaultIfEmpty.First();


            Console.Write(result);
            Console.Read();
        }

        private static void QuantifyingOperators()
        {
            var users = LoadUsers();
            var isAny = users.Take(500).Any(u => u.Location == "Francia");

            var all = users.Take(10).All(u => u.Id < 50);

            Console.WriteLine("Any: " + isAny);
            Console.WriteLine("All: " + all);

            Console.Read();

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
            var xdoc = XDocument.Load(@"../../../../Files/users.xml");
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



