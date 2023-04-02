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
        public void callElevator(int elevatorNo)
        {
            Console.WriteLine(string.Format("Elevator {0} Is coming", elevatorNo));
            for (int i = maxFloors; i >= currentFloor; i--)
            {
                Console.WriteLine(i + "\n");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Opening Doors");
        }


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
