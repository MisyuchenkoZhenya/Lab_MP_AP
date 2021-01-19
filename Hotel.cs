using Lab_MP_AP.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lab_MP_AP
{
    public class Hotel : IHotel
    {
        private List<Room> rooms;
        private WaitingRoom waitingRoom;
        private List<Client> clientsForSettle;
        private ILogger logger;
        private int TotalClientsArrived { get; set; }

        public DateTime CurrentDate { get; set; }
        public DateTime FinalDate { get; set; }

        public int OccupiedRoomsCount
        {
            get {return rooms.Where(r => !r.IsAvailabe).Count();}
        }

        public int AwaitersCount
        {
            get {return waitingRoom.Awaiters.Count();}
        }

        public Hotel(ILogger realizedLogger)
        {
            rooms = new List<Room>();
            waitingRoom = new WaitingRoom();
            clientsForSettle = new List<Client>();
            logger = realizedLogger;
            TotalClientsArrived = 0;

            CurrentDate = new DateTime(2021, 01, 01);
            FinalDate = CurrentDate.AddMonths(1);
        }

        public void StartWork()
        {
            while (CurrentDate < FinalDate)
            {
                int timePointsLeft = 6; //one tp is 10 abstract minutes
                logger.Log(Environment.NewLine +
                            $"Day {CurrentDate.ToString("dd, HH:mm:ss")}. " +
                            $"({OccupiedRoomsCount} rooms are occupied, {AwaitersCount} peoples in the waiting room)");

                var movedClientsCount = MoveOutClients(CurrentDate);
                if (movedClientsCount > 0)
                {
                    logger.Log($"{movedClientsCount} clients were moved out from hotel.");
                }

                if (new Random().Next(0, 100) > 85) // check if there are new clients
                {
                    int newClientsCount = new Random().Next(1, 6);
                    for (int i = 0; i < newClientsCount; i++)
                    {
                        clientsForSettle.Add(new Client($"Client-{++TotalClientsArrived}"));
                    }

                    logger.Log($"{newClientsCount} new clients arrived.");
                }

                var clientsFromWaitingRoom = waitingRoom.GetAvailableClients(CurrentDate);
                if (clientsFromWaitingRoom.Count > 0)
                {
                    clientsForSettle.AddRange(clientsFromWaitingRoom);
                    logger.Log($"{clientsFromWaitingRoom.Count} clients came again.");
                }

                if (clientsForSettle.Any())
                {
                    var settledClientsCount = 0;
                    var clientsWithoutRoomCount = 0;

                    while (clientsForSettle.Any())
                    {
                        if(timePointsLeft <= 0)
                        {
                            break;
                        }
                        var newClient = clientsForSettle.First();
                        clientsForSettle.Remove(newClient);
                        var room = GetAvailableRoom(newClient.Money);

                        if (room != null)
                        {
                            room.SettleClient(newClient, CurrentDate);
                            settledClientsCount++;
                            timePointsLeft -= 2;
                            Thread.Sleep(timePointsLeft);
                        }
                        else
                        {
                            waitingRoom.AddClient(newClient, CurrentDate);
                            clientsWithoutRoomCount++;
                            timePointsLeft -= 1;
                            Thread.Sleep(timePointsLeft);
                        }
                    }
                    logger.Log($"{settledClientsCount} clients were settled. " +
                               $"{clientsWithoutRoomCount} clients didn't find the room.");
                }

                Thread.Sleep(timePointsLeft);
                CurrentDate = CurrentDate.AddMinutes(60);
            }
            
        }

        public void AddRooms(int count, int price)
        {
            for(int i = 0; i < count; i++)
            {
                rooms.Add(new Room(price));
            }
        }

        public Room GetAvailableRoom(int clientMoney)
        {
            var availableRooms = rooms.Where(r => (r.IsAvailabe && r.Price <= clientMoney)).
                                 OrderByDescending(r => r.Price).
                                 FirstOrDefault();

            return availableRooms;
        }

        public int MoveOutClients(DateTime currentDate)
        {
            List<Room> roomsForMovingOut = rooms.Where(r => !r.IsAvailabe && r.CheckOutDate <= currentDate).ToList();
            foreach (var room in roomsForMovingOut)
            {
                room.RemoveClient();
            }

            return roomsForMovingOut.Count();
        }
    }
}
