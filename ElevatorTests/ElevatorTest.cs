
using System;
using NUnit.Framework;
using ElevatorChallenge;
namespace ElevatorTests
{
    public class ElevatorTest
    {
        private Elevator elevator;

        [SetUp]
        public void Setup()
        {
            elevator = new Elevator();
        }


        [Test]
        public void TestCallElevator()
        {
            elevator.callElevator(5, 2);
        }

        [Test]
        public void TestMoveUpDestination()
        {
            elevator.currentFloor = 2;
            elevator.destinationFloors.Add(5);
            elevator.moveUp();
            Assert.AreEqual(5, elevator.destinationFloors[0]);
        }

        [Test]
        public void TestMoveDownDestination()
        {
            elevator.currentFloor = 9;
            elevator.destinationFloors.Add(2);
            elevator.moveDown();
            Assert.AreEqual(2, elevator.destinationFloors[0]);
        }

        [Test]
        public void TestMaximumOccupencyNotExeeded()
        {
            elevator.maxOccupants = 10;
            elevator.currentOccupants = 3;
            ;
            Assert.AreEqual(false, elevator.maxOcupencyExceeded(6));

        }


    }
}
