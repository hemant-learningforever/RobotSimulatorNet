using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotSimulator.Contracts;

namespace RobotSimulator.Implementation.UnitTests
{
    [TestClass]
    public class MoveTests
    {
        private IMove robotMove;

        [TestInitialize]
        public void Initialize()
        {
            this.robotMove = new Move(new MoveValidator());
        }

        [TestMethod]
        public void ValidValidator_Move_ObjectCreated()
        {
            Move obj = new Move(new MoveValidator());
            Assert.IsNotNull(obj.moveValidator, "Object created successfully.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullValidator_Move_ThrowsArgumentNullException()
        {
            Move obj = null;
            try
            {
                obj = new Move(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: MoveValidator missing", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void ToString_GetData()
        {
            string result = this.robotMove.ToString();
            Assert.AreEqual("X: -1,  Y: -1,  Facing: NORTH", result, "Test Robot.ToString()");
        }

        [TestMethod]
        public void InvalidXYCoordinates_Place_ActionFailed()
        {
            var originalState = robotMove.ToString();
            robotMove.Place(5, 5, Facing.EAST);
            var newState = robotMove.ToString();
            Assert.AreEqual(originalState, newState, "Place action failed - outside table.");
        }

        [TestMethod]
        public void ValidXYCoordinates_Place_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.EAST);
            var newState = robotMove.ToString();
            Assert.AreEqual("X: 0,  Y: 0,  Facing: EAST", newState, "Robot placed successfully.");
        }

        [TestMethod]
        public void ChangePosition_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            robotMove.ChangePosition();
            var newState = robotMove.ToString();
            Assert.AreEqual("X: 0,  Y: 1,  Facing: NORTH", newState, "Robot placed successfully.");
        }

        [TestMethod]
        public void RobotNotPlaced_ChangePosition_ActionFailed()
        {
            var originalState = robotMove.ToString();
            robotMove.ChangePosition();
            var newState = robotMove.ToString();

            Assert.AreEqual(originalState, newState, "Move action failed - First place robot on table.");
        }

        [TestMethod]
        public void ZeroZeroWest_ChangePosition_ActionFailed()
        {
            robotMove.Place(0, 0, Facing.WEST);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void ZeroZeroSouth_ChangePosition_ActionFailed()
        {
            robotMove.Place(0, 0, Facing.SOUTH);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void ZeroFourWest_ChangePosition_ActionFailed()
        {
            robotMove.Place(0, 4, Facing.WEST);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void ZeroFourNorth_ChangePosition_ActionFailed()
        {
            robotMove.Place(0, 4, Facing.NORTH);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void FourZeroSouth_ChangePosition_ActionFailed()
        {
            robotMove.Place(4, 0, Facing.SOUTH);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void FourZeroEast_ChangePosition_ActionFailed()
        {
            robotMove.Place(4, 0, Facing.EAST);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void FourFourEast_ChangePosition_ActionFailed()
        {
            robotMove.Place(4, 4, Facing.EAST);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void FourFourNorth_ChangePosition_ActionFailed()
        {
            robotMove.Place(4, 4, Facing.NORTH);
            var expectedState = robotMove.ToString();
            robotMove.ChangePosition();
            var result = robotMove.ToString();
            Assert.AreEqual(expectedState, result, "Move failed.");
        }

        [TestMethod]
        public void RobotNotPlaced_Left_ActionFailed()
        {
            var originalState = robotMove.ToString();
            robotMove.Left();
            var newState = robotMove.ToString();

            Assert.AreEqual(originalState, newState, "Left action failed - First place robot on table.");
        }

        [TestMethod]
        public void ZeroZeroEast_Left_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.EAST);
            robotMove.Left();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: NORTH", newState, "Left action passed - from east to north.");
        }

        [TestMethod]
        public void ZeroZeroNorth_Left_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            robotMove.Left();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: WEST", newState, "Left action passed - from north to west.");
        }

        [TestMethod]
        public void ZeroZeroWest_Left_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.WEST);
            robotMove.Left();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: SOUTH", newState, "Left action passed - from west to south.");
        }

        [TestMethod]
        public void ZeroZeroSouth_Left_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.SOUTH);
            robotMove.Left();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: EAST", newState, "Left action passed - from south to east.");
        }

        [TestMethod]
        public void RobotNotPlaced_Right_ActionFailed()
        {
            var originalState = robotMove.ToString();
            robotMove.Right ();
            var newState = robotMove.ToString();

            Assert.AreEqual(originalState, newState, "Right action failed - First place robot on table.");
        }

        [TestMethod]
        public void ZeroZeroEast_Right_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.EAST);
            robotMove.Right();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: SOUTH", newState, "Right action passed - from east to south.");
        }

        [TestMethod]
        public void ZeroZeroSouth_Right_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.SOUTH);
            robotMove.Right();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: WEST", newState, "Right action passed - from south to west.");
        }

        [TestMethod]
        public void ZeroZeroWest_Right_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.WEST);
            robotMove.Right();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: NORTH", newState, "Right action passed - from west to north.");
        }

        [TestMethod]
        public void ZeroZeroNorth_Right_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            robotMove.Right();
            var newState = robotMove.ToString();

            Assert.AreEqual("X: 0,  Y: 0,  Facing: EAST", newState, "Right action passed - from north to east.");
        }

        public void Place_CanAction_ActionPassed()
        {
            Assert.IsTrue(robotMove.CanAction(ActionType.PLACE), "Place action passed.");
        }

        public void MoveBeforePlace_CanAction_ActionFailed()
        {
            Assert.IsFalse(robotMove.CanAction(ActionType.MOVE), "Move action can not be taken before robot is placed on the table.");
        }
        public void LeftBeforePlace_CanAction_ActionFailed()
        {
            Assert.IsFalse(robotMove.CanAction(ActionType.LEFT), "Left action can not be taken before robot is placed on the table.");
        }

        public void RightBeforePlace_CanAction_ActionFailed()
        {
            Assert.IsFalse(robotMove.CanAction(ActionType.RIGHT), "Right action can not be taken before robot is placed on the table.");
        }

        public void ReportBeforePlace_CanAction_ActionFailed()
        {
            Assert.IsFalse(robotMove.CanAction(ActionType.REPORT), "Report action can not be taken before robot is placed on the table.");
        }

        public void MoveAfterPlace_CanAction_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            Assert.IsTrue(robotMove.CanAction(ActionType.MOVE), "Move action passed");
        }

        public void LeftAfterPlace_CanAction_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            Assert.IsTrue(robotMove.CanAction(ActionType.LEFT), "Left action passed");
        }

        public void RightAfterPlace_CanAction_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            Assert.IsTrue(robotMove.CanAction(ActionType.RIGHT), "Right action passed");
        }

        public void ReportAfterPlace_CanAction_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            Assert.IsTrue(robotMove.CanAction(ActionType.REPORT), "Right action passed");
        }

        public void PlaceAfterPlace_CanAction_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.NORTH);
            Assert.IsTrue(robotMove.CanAction(ActionType.PLACE), "Place action passed");
        }

        [TestMethod]
        public void BeforePlace_Report_ActionFailed()
        {
            var report = robotMove.Report();
            Assert.AreEqual(string.Empty, report, "Report action can not be taken before robot is placed on the table");

        }

        [TestMethod]
        public void AfterPlace_Report_ActionPassed()
        {
            robotMove.Place(0, 0, Facing.EAST);
            var report = robotMove.Report();
            Assert.AreEqual("X: 0,  Y: 0,  Facing: EAST", report, "Report action passed");

        }
    }
}
