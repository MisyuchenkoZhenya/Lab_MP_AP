using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_MP_AP
{
    public class Hotel
    {
        private List<Room> rooms;

        public WaitingRoom waitingRoom;

        public int OccupiedRoomsCount
        {
            get
            {
                return rooms.Where(r => !r.IsAvailabe).Count();
            }
        }

        public int awaitersCount
        {
            get
            {
                return waitingRoom.Awaiters.Count();
            }
        }

        public Hotel()
        {
            rooms = new List<Room>();
            waitingRoom = new WaitingRoom();
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
            return rooms.Where(r => (r.IsAvailabe && r.Price <= clientMoney)).
                         OrderByDescending(r => r.Price).
                         FirstOrDefault();
        }

        public int MoveOutClients(DateTime currentDate)
        {
            var roomsForMovingOut = rooms.Where(r => !r.IsAvailabe && r.CheckOutDate <= currentDate).ToList();
            foreach (var room in roomsForMovingOut)
            {
                room.RemoveClient();
            }

            return roomsForMovingOut.Count();
        }
    }
}
