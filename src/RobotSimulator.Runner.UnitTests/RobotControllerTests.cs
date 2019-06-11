using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RobotSimulator.Contracts;
using RobotSimulator.Implementation;

namespace RobotSimulator.Runner.UnitTests
{
    [TestClass]
    public class RobotControllerTests
    {
        Mock<IMove> move;
        Mock<IMoveValidator> moveValidator;

        [TestInitialize]
        public void Initialize()
        {
            move = new Mock<IMove>();
            moveValidator = new Mock<IMoveValidator>();
        }

        [TestMethod]
        public void ValidArguments_RobotController_ObjectCreated()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            Assert.IsNotNull(robot, "Object created successfully");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullMove_RobotController_ThrowsArgumentNullException()
        {
            try
            {
                RobotController robot = new RobotController(null, moveValidator.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: RobotMove missing", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullMoveValidator_RobotController_ThrowsArgumentNullException()
        {
            try
            {
                RobotController robot = new RobotController(move.Object, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: MoveValidator missing", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void NoCoordinatesAndDirection_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList  lst = robot.GetCoordinatesAndDirection(out isValid, "place");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void DirectionAvailableCoordinatesNot_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place north");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void CoordinatesAvailableNoDirection_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place 1,1");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void InvalidCoordinatesAndValidDirection_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place 5,5,NORTH");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void ValidCoordinatesAndInValidDirection_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place 1,1,NORTHEAST");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void InValidXValidYValidDirection_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place 9,1,NORTH");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void ValidXInValidYValidDirection_GetCoordinatesAndDirection_InvalidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place 1,9,NORTH");
            Assert.IsFalse(isValid);
            Assert.AreEqual("bad command", lst[0].ToString());
        }

        [TestMethod]
        public void ValidCoordinatesAndValidDirection_GetCoordinatesAndDirection_ValidPlace()
        {
            RobotController robot = new RobotController(move.Object, moveValidator.Object);
            moveValidator.Setup(m => m.Validate(1, 1)).Returns(true);
            bool isValid = false;
            ArrayList lst = robot.GetCoordinatesAndDirection(out isValid, "place 1,1,NORTH");
            Assert.IsTrue(isValid);
            Assert.AreEqual("place", lst[0].ToString());
            Assert.AreEqual("1", lst[1].ToString());
            Assert.AreEqual("1", lst[2].ToString());
            Assert.AreEqual("NORTH", lst[3].ToString());
        }

        [TestMethod]
        public void ValidPlaceArguments_PlaceRobot_ActionPassed()
        {
            PrivateObject obj = new PrivateObject(new RobotController(move.Object,moveValidator.Object));
            move.Setup(m => m.CanAction(ActionType.PLACE)).Returns(true);
            object[] args = new object[3] { 1, 2, Facing.NORTH };
            obj.Invoke("PlaceRobot",args);
            move.Verify(m => m.Place(1, 2, Facing.NORTH));
        }
    }
}
