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
        static string welcomMsg = "Welcome To DVT Elevator Challenge : Please press X at any point during this simulation to start a new elevator session and summon the nearest elevator to a specific floor";
        static string promptMsg1 = "Elevator {0} is Currently on {1} floor. It is currently carrying {2} occupants ";
        static string promptMsg2 = " Please press U if you are going up \n Please press D if you are going down";

        static void Main(string[] args)
        {

            List<Elevator> elevator = new List<Elevator>();
            configureElevator(elevator);
            Console.WriteLine(welcomMsg);
        repeat:
            //Report Current Positions of Elevators
            getElevatorStats(elevator);

            var input = string.Empty;
            input = Console.ReadLine();
            if (input.ToUpper().Equals("U") || input.ToUpper().Equals("D"))
            {
                Console.WriteLine("Which Floor are you currently on? ");
                input = Console.ReadLine();
                if (int.TryParse(input, out int userFloor))
                {
                    int nearest = getNearestElevator(elevator, userFloor);
                    elevator[nearest].callElevator(nearest + 1, userFloor);
                addDestinations:
                    Console.WriteLine("Enter your destination floor");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int newDestination))
                        elevator[nearest].destinationFloors.Add(newDestination);
                    else
                    {
                        if (input.ToUpper().Equals("X"))
                            goto repeat;

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
                        else
                        {
                            if (ans.ToUpper().Equals("X"))
                                goto repeat;

                            Console.WriteLine("Invalid number entered");
                            goto removeOccupants;
                        }

                    addOccupants:
                        Console.WriteLine("How many new occupants entered:");
                        input = Console.ReadLine();
                        if (int.TryParse(input, out int newOccupants))
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
                                    else
                                    {
                                        if (ans1.ToUpper().Equals("X"))
                                            goto repeat;

                                        goto addDestinations;
                                    }
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
                        else
                        {
                            if (input.ToUpper().Equals("X"))
                                goto repeat;
                            Console.WriteLine("Invalid Number of Occupants");
                            goto addOccupants;
                        }
                    }
                    else
                    {
                        if (ans.ToUpper().Equals("X"))
                            goto repeat;

                        Console.WriteLine("Invalid Selection");
                        goto repeat;
                    }
                }


                else
                {
                    if (input.ToUpper().Equals("X"))
                        goto repeat;

                    Console.WriteLine("Invalid input, restarting process...");
                    Thread.Sleep(1000);
                    goto repeat;
                }

            }

            else
            {
                if (input.ToUpper().Equals("X"))
                    goto repeat;

                Console.WriteLine("Invalid input, restarting process...");
                Thread.Sleep(1000);
                goto repeat;
            }
        }
        private static void getElevatorStats(List<Elevator> elevator)
        {
            for (int i = 0; i < elevator.Count; i++)
            {
                Console.WriteLine(string.Format(promptMsg1, (i + 1).ToString(), elevator[i].currentFloor.ToString(), elevator[i].currentOccupants.ToString()));
            }
            Console.WriteLine(promptMsg2);

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

        public static int getNearestElevator(List<Elevator> elevator, int userFloor)
        {
            List<int> currFloors = new List<int>();
            elevator.ToList().ForEach(x => currFloors.Add(x.currentFloor));
            int closest = currFloors.Aggregate((x, y) => Math.Abs(x - userFloor) < Math.Abs(y - userFloor) ? x : y);
            return elevator.FindIndex(p => p.currentFloor.Equals(closest));
        }
    }
}
