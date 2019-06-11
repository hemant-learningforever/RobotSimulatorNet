using RobotSimulator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator.Implementation
{
    public class Move : IMove
    {
        private int xAxisCoordinate = -1;

        private int yAxisCoordinate = -1;

        private Facing direction;

        public IMoveValidator moveValidator { get; set; }

        public Move(IMoveValidator moveValidator)
        {
            if (moveValidator == null) throw new ArgumentNullException("MoveValidator missing");
            this.moveValidator = moveValidator;
        }

        public override string ToString()
        {
            return string.Format("X: {0},  Y: {1},  Facing: {2}", xAxisCoordinate, yAxisCoordinate, direction);
        }

        public void Place(int x, int y, Facing facing)
        {
            if (moveValidator.Validate(x, y))
            {
                this.xAxisCoordinate = x;
                this.yAxisCoordinate = y;
                this.direction = facing;
            }
        }

        public void ChangePosition()
        {
            switch (this.direction)
            {
                case Facing.EAST:
                    if (moveValidator.Validate(this.xAxisCoordinate + 1, this.yAxisCoordinate)) this.xAxisCoordinate++;
                    break;
                case Facing.NORTH:
                    if (moveValidator.Validate(this.xAxisCoordinate, this.yAxisCoordinate + 1)) this.yAxisCoordinate++;
                    break;
                case Facing.SOUTH:
                    if (moveValidator.Validate(this.xAxisCoordinate, this.yAxisCoordinate - 1)) this.yAxisCoordinate--;
                    break;
                case Facing.WEST:
                    if (moveValidator.Validate(this.xAxisCoordinate - 1, this.yAxisCoordinate)) this.xAxisCoordinate--;
                    break;
                default: break;
            }
        }

        public void Left()
        {
            if (moveValidator.Validate(this.xAxisCoordinate, this.yAxisCoordinate))
            {
                switch (this.direction)
                {
                    case Facing.EAST:
                        this.direction = Facing.NORTH;
                        break;
                    case Facing.NORTH:
                        this.direction = Facing.WEST;
                        break;
                    case Facing.SOUTH:
                        this.direction = Facing.EAST;
                        break;
                    case Facing.WEST:
                        this.direction = Facing.SOUTH;
                        break;
                    default: break;
                }
            }

        }

        public void Right()
        {
            if (moveValidator.Validate(this.xAxisCoordinate, this.yAxisCoordinate))
            {
                switch (this.direction)
                {
                    case Facing.EAST:
                        this.direction = Facing.SOUTH;
                        break;
                    case Facing.NORTH:
                        this.direction = Facing.EAST;
                        break;
                    case Facing.SOUTH:
                        this.direction = Facing.WEST;
                        break;
                    case Facing.WEST:
                        this.direction = Facing.NORTH;
                        break;
                    default: break;
                }
            }

        }

        public string Report()
        {
            if (moveValidator.Validate(this.xAxisCoordinate, this.yAxisCoordinate))
            {
                return ToString();
            }
            else
            {
                return string.Empty;
            }

        }

        public bool CanAction(ActionType actionType)
        {

            switch (actionType)
            {

                case ActionType.PLACE:

                    return true;

                default:

                    return (this.xAxisCoordinate > -1 && this.yAxisCoordinate > -1);

            }

        }

    }
}
