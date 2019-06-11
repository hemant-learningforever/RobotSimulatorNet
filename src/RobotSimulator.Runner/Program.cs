using RobotSimulator.Configuration;
using System;

namespace RobotSimulator.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            RobotController robotController = new Startup().Resolve<RobotController>();
            if (robotController != null)
            {
                try
                {
                    robotController.Run();
                }
                catch (Exception ex)
                {
                    ReportError("Oops, error occurred: " + ex.Message + "/nPress enter to exit the application");
                }

            }
            else
            {
                ReportError("Oops, robot found. Press enter to exit the application");
            }
        }

        private static void ReportError(string message)
        {
            Console.WriteLine(message);
            Console.Read();
        }
    }
}
