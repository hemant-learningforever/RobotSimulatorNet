using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator.Contracts
{
    public interface IMove
    {
        void Place(int x, int y, Facing facing);

        void ChangePosition();

        void Left();

        void Right();

        string Report();

        bool CanAction(ActionType actionType);
    }
}
