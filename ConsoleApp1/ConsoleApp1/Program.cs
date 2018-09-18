using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
    } 
    class Team
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
    class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Phone> phones = new List<Phone>
            {
                new Phone{ Name = "Lumia 430", Company = "Microsoft"},
                new Phone{ Name = "Mi 5", Company = "Xiaomi"},
                new Phone{ Name = "LG G 5", Company = "LG"},
                new Phone{ Name = "iPhone 5s", Company = "Apple"},
                new Phone{ Name = "Lumia 630", Company = "Microsoft"},
                new Phone{ Name = "iPhone 6s", Company = "Apple"},
                new Phone{ Name = "Galaxy gio s3", Company = "Samsung"},
                new Phone{ Name = "Galaxy s9 Plus", Company = "Samsung"},
            };
            List<Player> players = new List<Player> {
                new Player{ Name="Messi", Team ="Барселона"},
                new Player{ Name="Countinio", Team ="Барселона"},
                new Player{ Name="Suarez", Team ="Барселона"},
                new Player{ Name="Роббен", Team ="Баварии"},
            };
            List<Team> teams = new List<Team>
            {
                new Team{ Name = "Баварии", Country ="Германия"},
                new Team{ Name = "Барселона", Country ="Испания"}
            };
            string[] teamsTop = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "Барселонна" };

            
            GroupingExample(phones);
            JoinExample(players, teams);
            LazyLoadExample(teamsTop);
            Console.ReadKey();
        }

        private static void LazyLoadExample(string[] teams)
        {
            Console.WriteLine("#####################################");
            var selectedTeams = (from team in teams
                                where team.ToUpper().StartsWith("Б")
                                orderby team
                                select team).ToList();
            
            teams[1] = "Зоря Луганск";
            //выполнение запроса
            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
            
        }

        private static void JoinExample(List<Player> players, List<Team> teams)
        {
            Console.WriteLine("########################");
            var result = from player in players
                         join team in teams on player.Team equals team.Name
                         select new { Name = player.Name, Team = player.Team, Country = team.Country };
            foreach (var playerInfo in result)
            {
                Console.WriteLine($"{playerInfo.Name}, {playerInfo.Team}, {playerInfo.Country}");
            }
        }
        private static void GroupingExample(List<Phone> phones)
        {
            //запросы
            var groupPhones = from phone in phones
                              group phone by phone.Company;
            //методы
            groupPhones = phones.GroupBy(g => g.Company);

            foreach (var group in groupPhones)
            {
                Console.WriteLine("----------------");
                Console.WriteLine(group.Key);
                foreach (Phone g in group)
                {
                    Console.WriteLine(g.Name);
                }
            }

            Console.WriteLine("##########################");
            var groupPhones2 = from phone in phones
                               group phone by phone.Company into g
                               select new { Name = g.Key, Count = g.Count() };

            groupPhones2 = phones.GroupBy(g => g.Company)
                .Select(g => new { Name = g.Key, Count = g.Count() });

            foreach (var item in groupPhones2)
            {
                Console.WriteLine($"{item.Name}: {item.Count}");
            }



            var groupPhones3 = from phone in phones
                               group phone by phone.Company
                               into g
                               select new
                               {
                                   Name = g.Key,
                                   Count = g.Count(),
                                   Phones = from i in g select i
                               };

            groupPhones3 = phones.GroupBy(g => g.Company)
                .Select(v => new {
                    Name = v.Key,
                    Count = v.Count(),
                    Phones = v.Select(p => p)
                });


            Console.WriteLine("#########################");
            foreach (var group in groupPhones3)
            {
                Console.WriteLine($"{group.Name}: {group.Count}");
                foreach (var item in group.Phones)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine();    
            }
        }
    }
}
