using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator.Contracts
{
    public enum Facing
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public enum ActionType
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }
}
