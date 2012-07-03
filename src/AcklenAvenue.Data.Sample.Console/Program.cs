using System;
using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Data.NHibernate;
using AcklenAvenue.Data.Sample.Domain;
using NHibernate.Linq;
using StructureMap;

namespace AcklenAvenue.Data.Sample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            new ConsoleAppSampleBootstrapper(container).Run();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(
                "Ok, now that the database is populated, we're going to pull the data back out and print some names to the screen:");

            //second, query the database
            QueryTheDatabase(container.GetInstance<ISessionContainer>());

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        static void QueryTheDatabase(ISessionContainer sessionContainer)
        {
            //you've got to open the session before we can use it
            sessionContainer.OpenSession();

            //using the opened session from the sessionContainer, we can query the database
            List<Account> accounts = sessionContainer.Session.Query<Account>().ToList();
            accounts.ForEach(x => Console.WriteLine(x.Name));

            //afterwards, you need to close the session
            sessionContainer.CloseSession();
        }
    }
}