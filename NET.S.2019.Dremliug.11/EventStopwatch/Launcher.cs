using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2019.Dremliug._11
{
    public class Launcher
    {
        public static void Main()
        {
            int seconds = 5;
            Console.WriteLine("Start of demo.\n");

            Console.WriteLine($"Timer started at: {DateTime.Now} for {seconds} seconds.");

            new TimerAfter(seconds, (sender, e) => Console.WriteLine($"Timer stopped at {e.EndTime}. \n\nEnd of demo.")).Start();

            Console.WriteLine("Wait...");
            Console.ReadLine();
        }
    }
}
