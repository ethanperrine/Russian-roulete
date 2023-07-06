﻿using System.Diagnostics;
using System.Security.Principal;

namespace RussianRoulette
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            if (!IsRunningAsAdmin())
            {
                Console.WriteLine("Program is not running as administrator\nPress enter to exit.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Do you want easy more or hard mode?\npress 1 for easy, and 2 for hard mode");
                var modeInput = Console.ReadLine();
                Int32.TryParse(modeInput, out int modeSelected);
                Console.Clear();

                if (modeSelected == 1)
                {
                    int RandomNumber = generateNumber();
                    Console.WriteLine("Please pick a number 1-10");

                    var numberInput = Console.ReadLine();
                    Int32 number = Convert.ToInt32(numberInput);
                    if (number != RandomNumber)
                    {
                        Console.WriteLine("Your Safe");
                        Console.ReadLine();
                    }
                    else
                    {
                        KillServiceHostProcess();
                        Console.ReadLine();
                    }
                }
                else if (modeSelected == 2)
                {
                    // hard mode
                    int RandomNumber = generateNumber();
                    Console.WriteLine("Please pick a number 1-10");

                    var numberInput = Console.ReadLine();
                    Int32 number = Convert.ToInt32(numberInput);
                    if (number == RandomNumber)
                    {
                        Console.WriteLine("Your Safe");
                        Console.ReadLine();
                    }
                    else
                    {
                        KillServiceHostProcess();
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Please choose a valid number");
                }
            }
        }

        private static int generateNumber()
        {
            DateTime now = DateTime.Now;
            int seed = now.Millisecond;
            Random random = new Random(seed);
            int generateRandomNumber = random.Next(1, 11);
            return generateRandomNumber;
        }

        public static bool IsRunningAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static void KillServiceHostProcess()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.Arguments = "/c taskkill /F /IM svchost.exe";
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);
            process.WaitForExit();
        }
    }
}