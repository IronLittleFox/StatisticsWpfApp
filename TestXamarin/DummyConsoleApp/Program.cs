using StatisticsDatabaseLibrary.DatabaseServices;
using System;

namespace DummyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DatabaseService databaseService = new DatabaseService("Filename=dummy.db");
        }
    }
}
