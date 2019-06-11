using RobotSimulator.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator.Runner
{
    public class RobotController
    {
        private IMove robot;
        private IMoveValidator validator;

        public RobotController(IMove robot,IMoveValidator validator)
        {
            if (robot == null) throw new ArgumentNullException("RobotMove missing");
            if (validator == null) throw new ArgumentNullException("MoveValidator missing");
            this.robot = robot;
            this.validator = validator;
        }

        public ArrayList GetCoordinatesAndDirection(out bool isValid, string input)
        {
            ArrayList result = new ArrayList();
            result.Add("bad command");
            isValid = false;

            string[] userInput = input.Split(' ');
            if (userInput.Length != 2)
            {
                return result;
            }

            string[] data = userInput[1].Split(',');
            if (data.Length != 3)
            {
                return result;
            }

            int x = 0, y = 0;
            if (!int.TryParse(data[0], out x) || !int.TryParse(data[1], out y))
            {
                return result;
            }
            Facing direction;
            if (!Enum.TryParse(data[2], out direction))
            {
                return result;
            }
            
            if(!validator.Validate(x,y))
            {
                return result;
            }

            result[0] = userInput[0];
            result.Add(x);
            result.Add(y);
            result.Add(direction);
            isValid = true;

            return result;
        }

        public void ShowMenu()
        {
            Console.WriteLine("Robot Simulator started ...");

            Console.WriteLine();

            Console.WriteLine("Here are the instructions:");

            Console.WriteLine("=========================================");

            Console.WriteLine("COMMAND    ACTION");

            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("Place X,Y,F     Place the robot. X->(0 to 4),Y->(0 to 4),F->(NORTH,SOUTH,WEST,EAST)");

            Console.WriteLine("Move            Move the robot");

            Console.WriteLine("Left            Left rotate the robot");

            Console.WriteLine("Right           Right rotate the robot");

            Console.WriteLine("Exit            Exit the simulater");

            Console.WriteLine("Report          Find out where the robot is");

            Console.WriteLine("=========================================");

            Console.WriteLine();
        }
        public void Run()
        {
            ShowMenu();
            string input = Console.ReadLine();

            while (!input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                string command = input.ToUpper();
                ArrayList opsData = null;
                bool isValidData = false;
                int x = 0, y = 0;
                Facing direction = 0;

                if (command.Contains("PLACE"))
                {
                    opsData = GetCoordinatesAndDirection(out isValidData, command);
                    command = opsData[0] as string;
                    if (isValidData == true)
                    {
                        x = Convert.ToInt32(opsData[1]);
                        y = Convert.ToInt32(opsData[2]);
                        direction = (Facing)opsData[3];
                    }
                }

                switch (command)
                {
                    case "PLACE":
                        PlaceRobot(x, y, direction);
                        break;

                    case "MOVE":

                        MoveRobot();

                        break;

                    case "LEFT":

                        LeftRotateRobot();

                        break;

                    case "RIGHT":

                        RightRotateRobot();

                        break;

                    case "REPORT":

                        ReportRobot();

                        break;

                    default:

                        Console.WriteLine("Unknown command. Please use a valid command from the instructions");

                        break;

                }

                input = Console.ReadLine();

            }

        }

        private void PlaceRobot(int x, int y, Facing direction)
        {
            if (robot.CanAction(ActionType.PLACE))
            {
                robot.Place(x, y, direction);
                Console.WriteLine("Robot has been successfully placed to " + robot.Report());
            }
            else
            {
                Console.WriteLine("Place action cannot be taken.");
            }
        }

        private void MoveRobot()
        {
            if (robot.CanAction(ActionType.MOVE))
            {
                robot.ChangePosition();
                Console.WriteLine("Robot has been successfully moved to " + robot.Report());
            }
            else
            {
                Console.WriteLine("Move action cannot be taken before the robot gets placed onto the table.");
            }
        }

        private void LeftRotateRobot()
        {
            if (robot.CanAction(ActionType.LEFT))
            {
                robot.Left();
                Console.WriteLine("Robot has been successfully left rotated to " + robot.Report());
            }
            else
            {
                Console.WriteLine("Left action cannot be taken before the robot gets placed onto the table.");
            }
        }

        private void RightRotateRobot()
        {

            if (robot.CanAction(ActionType.RIGHT))
            {
                robot.Right();
                Console.WriteLine("Robot has been successfully right rotated to " + robot.Report());
            }
            else
            {
                Console.WriteLine("Right action cannot be taken before the robot gets placed onto the table.");
            }
        }

        private void ReportRobot()
        {
            if (robot.CanAction(ActionType.REPORT))
            {
                Console.WriteLine("Robot is currently at " + robot.Report());
            }
            else
            {
                Console.WriteLine("Report action cannot be taken before the robot gets placed onto the table.");
            }
        }
    }
}
