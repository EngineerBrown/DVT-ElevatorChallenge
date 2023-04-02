using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorChallenge
{
    internal class Program
    {
        static string welcomMsg = "Welcome To DVT Elevator Challenge ";
        static string promptMsg1 = "Elevator {0} is Currently on {1} floor. ";
        static string promptMsg2 = "Please press U if you are going up \n Please press D if you are going down";

        static void Main(string[] args)
        {

            List<Elevator> elevator = new List<Elevator>();
            configureElevator(elevator);
            Console.WriteLine(welcomMsg);
        repeat:
            //Report Current Positions of Elevators
            for (int i = 0; i < elevator.Count; i++)
                Console.WriteLine(string.Format(promptMsg1, (i + 1).ToString(), elevator[i].currentFloor.ToString()));

            Console.WriteLine(promptMsg2);

            var input = string.Empty;
            input = Console.ReadLine();
            if (input.ToUpper().Equals("U") || input.ToUpper().Equals("D"))
            {
                int nearest = getNearestElevator(elevator);
                elevator[nearest].callElevator(nearest + 1);
            addDestinations:
                Console.WriteLine("Enter your destination floor");
                if (int.TryParse(Console.ReadLine(), out int newDestination))
                    elevator[nearest].destinationFloors.Add(newDestination);
                else
                {
                    Console.WriteLine("Invalid Destination");
                    goto addDestinations;
                }
                Console.WriteLine("Add another destination floor Y/N?");
                var ans = Console.ReadLine().ToUpper();
                if (ans.Equals("Y"))
                    goto addDestinations;
                else if (ans.Equals("N"))
                {
                removeOccupants:
                    Console.WriteLine("How many occupants exited?:");
                    if (int.TryParse(Console.ReadLine(), out int oldOccupants))
                    {
                        elevator[nearest].currentOccupants -= oldOccupants;
                        if (elevator[nearest].currentOccupants < 0)
                        {
                            Console.WriteLine("Occupants cannot be negative, Elevator is now empty");
                            elevator[nearest].currentOccupants = 0;
                        }
                    }

                addOccupants:
                    Console.WriteLine("How many new occupants entered:");
                    if (int.TryParse(Console.ReadLine(), out int newOccupants))
                    {
                        if (!elevator[nearest].maxOcupencyExceeded(newOccupants))
                        {
                            elevator[nearest].currentOccupants += newOccupants;
                            Console.WriteLine("Closing Doors...");
                            if (elevator[nearest].destinationFloors[0] > elevator[nearest].currentFloor)
                            {
                                elevator[nearest].moveUp();
                            }
                            else if (elevator[nearest].destinationFloors[0] == elevator[nearest].currentFloor)
                            {
                                Console.WriteLine("Looks like you are already at your destination");
                            }
                            else if (elevator[nearest].destinationFloors[0] < elevator[nearest].currentFloor)
                            {
                                elevator[nearest].moveDown();
                            }
                            else
                            {
                                Console.WriteLine("Invalid request");
                                goto repeat;
                            }
                            elevator[nearest].destinationFloors.RemoveAt(0);
                            Console.WriteLine("Opening Doors...");
                            if (elevator[nearest].destinationFloors.Count > 0)
                            {
                                Console.WriteLine("Close doors and continue to next destination, Y/N?");
                                var ans1 = Console.ReadLine();
                                if (ans1.ToUpper().Equals("Y"))
                                {
                                    goto removeOccupants;
                                }
                                else goto addDestinations;
                            }
                            else
                            {
                                goto addDestinations;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Maximum occupency exceeded");
                            goto addOccupants;
                        }
                    }
                    else { Console.WriteLine("Invalid Number of Occupants"); goto addOccupants; }
                }
                else
                {
                    Console.WriteLine("Invalid Selection");
                    goto repeat;
                }
            }

            else
            {
                Console.WriteLine("Invalid input, restarting process...");
                Thread.Sleep(1000);
                goto repeat;
            }
        }

        private static void configureElevator(List<Elevator> elevator)
        {
            elevator.Clear();
            Console.WriteLine("Use Default Settings? Y/N?");
            var ans = Console.ReadLine();
            if (ans.ToUpper().Equals("Y"))
            {
                //Add 2 elevators
                //Add 2 elevators
                for (int i = 0; i < 2; i++)
                {
                    elevator.Add(new Elevator());
                    elevator[i].maxFloors = 10;
                    elevator[i].currentFloor = new Random().Next(1, 10);
                    elevator[i].undergoundFloors = 0;
                    elevator[i].maxOccupants = 8;
                }

            }
            else if (ans.ToUpper().Equals("N"))
            {
                Console.WriteLine("How Many Elevators does your building have?");
                var elevators = Console.ReadLine();
                Console.WriteLine("How Many Floors does your building have?");
                var floors = Console.ReadLine();
                //Console.WriteLine("How Many Underground Floors does your building have?");
                //var ugFloors = Console.ReadLine();
                Console.WriteLine("Maximum number of occupants?");
                var maxOccupants = Console.ReadLine();
                for (int i = 0; i < int.Parse(elevators); i++)
                {
                    elevator.Add(new Elevator());
                    elevator[i].maxFloors = int.Parse(floors);

                    Random r = new Random();
                    int rInt = r.Next(1, elevator[i].maxFloors);
                    elevator[i].currentFloor = rInt;
                    elevator[i].undergoundFloors = 0;
                    elevator[i].maxOccupants = int.Parse(maxOccupants);
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
                configureElevator(elevator);

            }
        }

        public static int getNearestElevator(List<Elevator> elevator)
        {
            List<int> currFloors = new List<int>();
            elevator.ToList().ForEach(x => currFloors.Add(x.currentFloor));
            int closest = currFloors.Aggregate((x, y) => Math.Abs(x - 0) < Math.Abs(y - 0) ? x : y);
            return elevator.FindIndex(p => p.currentFloor.Equals(closest));
        }
    }
}
