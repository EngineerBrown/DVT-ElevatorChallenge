using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit;
using Ele

namespace ElevatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        private Elevator elevator;

        [SetUp]
        public void Setup()
        {
            elevator = new Elevator(10, 1000);
        }

        [TestMethod]
        public void TestAddDestination()
        {
            elevator.AddDestination(5, 200);
            Assert.AreEqual(1, elevator.GetDestinations()[4]);
        }

        [TestMethod]
        public void TestAddDestinationExceedsWeightLimit()
        {
            elevator.AddDestination(5, 1100);
            Assert.AreEqual(0, elevator.GetDestinations()[4]);
        }

        [TestMethod]
        public void TestMove()
        {
            elevator.AddDestination(5, 200);
            elevator.Move();
            Assert.AreEqual(5, elevator.GetCurrentFloor());
        }

        [TestMethod]
        public void TestFindNextFloor()
        {
            elevator.AddDestination(5, 200);
            elevator.AddDestination(8, 300);
            int nextFloor = elevator.FindNextFloor();
            Assert.AreEqual(5, nextFloor);
        }
    }

}
}
