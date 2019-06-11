using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotSimulator.Implementation.UnitTests
{
    [TestClass]
    public class MoveValidatorTests
    {
        private MoveValidator validator = null;

        [TestInitialize]
        public void Initialize()
        {
            this.validator = new MoveValidator();
        }

        [TestMethod]
        public void ValidMinXMinYCoordinate_Validate_ReturnsTrue()
        {
            bool result = this.validator.Validate(0, 0);
            Assert.IsTrue(result, "valid x and y coordinates.");
        }

        [TestMethod]
        public void ValidMaxXMaxYCoordinate_Validate_ReturnsTrue()
        {
            bool result = this.validator.Validate(4, 4);
            Assert.IsTrue(result, "valid x and y coordinates.");
        }

        [TestMethod]
        public void ValidXInvalidYCoordinate_Validate_ReturnsFalse()
        {
            bool result = this.validator.Validate(0, 5);
            Assert.IsFalse(result, "y coordinate is out of range.");
        }

        [TestMethod]
        public void InvalidXValidYCoordinate_Validate_ReturnsFalse()
        {
            bool result = this.validator.Validate(5, 0);
            Assert.IsFalse(result, "x coordinate is out of range.");
        }

        [TestMethod]
        public void InvalidXInvalidYCoordinate_Validate_ReturnsFalse()
        {
            bool result = this.validator.Validate(5, 5);
            Assert.IsFalse(result, "both x and y coordinates are out of range.");
        }

        [TestMethod]
        public void NegativeXValidYCoordinate_Validate_ReturnsFalse()
        {
            bool result = this.validator.Validate(-1, 2);
            Assert.IsFalse(result, "x coordinate is negative value.");
        }

        [TestMethod]
        public void NegativeYValidXCoordinate_Validate_ReturnsFalse()
        {
            bool result = this.validator.Validate(1, -5);
            Assert.IsFalse(result, "y coordinate is negative value.");
        }
    }
}
