using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorChallenge
{
    public class Elevator
    {
        #region Constructors
        public Elevator()
        {
            destinationFloors = new List<int>();
        }
        public Elevator(string name) { }

        #endregion

        #region Properties
        public int maxFloors
        { get; set; }

        public int currentFloor
        { get; set; }

        public List<int> destinationFloors
        { get; set; }

        public int undergoundFloors
        { get; set; }

        public int maxOccupants
        { get; set; }
        public int currentOccupants
        { get; set; }

        #endregion

        #region Public Methods

        //Calls the nearest elevator
        public void callElevator(int elevatorNo, int userFloor)
        {
            Console.WriteLine(string.Format("Elevator {0} Is coming", elevatorNo));
            if (currentFloor < userFloor)
                for (int i = currentFloor; i <= userFloor; i++)
                {
                    Console.WriteLine(i + "\n");
                    Thread.Sleep(1000);
                }

            else
                for (int i = currentFloor; i >= userFloor; i--)
                {
                    Console.WriteLine(i + "\n");
                    Thread.Sleep(1000);
                }
            Console.WriteLine("Opening Doors");
        }

        //Simulates downward movement of the elevator
        public void moveDown()
        {
            for (int i = currentFloor; i >= destinationFloors[0]; i--)
            {
                Console.WriteLine(i + "\n");
                Thread.Sleep(1000);
            }
            currentFloor = destinationFloors[0];
            //destinationFloors.RemoveAt(0);
            //Console.WriteLine("Opening Doors");
        }

        //Simulates Upward movment of the elevator
        public void moveUp()
        {
            for (int i = currentFloor; i <= destinationFloors[0]; i++)
            {
                Console.WriteLine(i + "\n");
                Thread.Sleep(1000);
            }
            currentFloor = destinationFloors[0];
            //destinationFloors.RemoveAt(0);
            //Console.WriteLine("Opening Doors");
        }

        //checks if maximum occupancy is exceeded
        public bool maxOcupencyExceeded(int newOccupants)
        {
            if ((newOccupants + currentOccupants) <= maxOccupants)
                return false;
            else
                return true;
        }

        #endregion
    }
}
