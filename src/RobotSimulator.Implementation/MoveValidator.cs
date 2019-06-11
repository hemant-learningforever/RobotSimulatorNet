using RobotSimulator.Contracts;
using System;


namespace RobotSimulator.Implementation
{
    public class MoveValidator : IMoveValidator
    {
        const int MIN_X = 0;
        const int MAX_X = 4;
        const int MIN_Y = 0;
        const int MAX_Y = 4;
        public bool Validate(int x, int y)
        {
            return x >= MIN_X && x <= MAX_X && y >= MIN_Y && y <= MAX_Y;
        }
    }
}
