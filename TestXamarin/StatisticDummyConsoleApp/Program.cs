using StatisticMobileDatabaseLibrary.Context;
using System;

namespace StatisticDummyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StatisticDatabaseContext statisticDatabaseContext = new StatisticDatabaseContext("FileName=Dummy.db");
            Console.WriteLine("Hello World!");
        }
    }
}
