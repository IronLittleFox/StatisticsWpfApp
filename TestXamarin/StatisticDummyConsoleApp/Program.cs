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
    //https://learn.microsoft.com/en-us/xamarin/android/user-interface/splash-screen
    //https://learn.microsoft.com/pl-pl/xamarin/ios/app-fundamentals/images-icons/launch-screens?tabs=windows
}
