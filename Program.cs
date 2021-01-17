using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab_MP_AP
{
    class Program
    {
        //Задача о гостинице.
        //В гостинице N номеров с ценой X руб., M номеров с ценой Y руб. и K номеров с ценой Z руб. (X<Y<Z).
        //Клиент, зашедший в гостиницу, обладает некоторой суммой S денег и получает номер по своим финансовым возможностям, если тот свободен.
        //Если среди доступных клиенту номеров нет свободных, клиент уходит  искать ночлег в другое место, но на следующее утро возвращается.
        //Если клиент получил номер, то он ночует и уходит.
        //Создать приложение, моделирующее работу гостиницы в течение месяца.
        //Клиенты появляются в гостинице в случайное время и днем, и ночью, группами и поодиночке.

        static void Main(string[] args)
        {
            int N = 5, M = 7, K = 3;
            int X = 75, Y = 100, Z = 150;

            var hotel = new Hotel();
            hotel.AddRooms(N, X);
            hotel.AddRooms(M, Y);
            hotel.AddRooms(K, Z);

            var currentDate = new DateTime(2021, 01, 01);
            var finalDate = currentDate.AddMonths(1);

            using (StreamWriter writer = new StreamWriter("./result.txt"))
            {
                while (currentDate < finalDate)
                {
                    writer.WriteLine(Environment.NewLine +
                                     $"Day {currentDate.ToString("dd, HH:mm:ss")}. " +
                                     $"({hotel.OccupiedRoomsCount} rooms are occupied, {hotel.awaitersCount} peoples in the waiting room)");

                    var movedClientsCount = hotel.MoveOutClients(currentDate);
                    if(movedClientsCount > 0)
                    {
                        writer.WriteLine($"{movedClientsCount} clients were moved out from hotel.");
                    }

                    var clientsForSettle = new List<Client>();

                    if(new Random().Next(0, 100) > 85) // check if there are new clients
                    {
                        int newClientsCount = new Random().Next(1, 5);
                        for(int i = 0; i < newClientsCount; i++)
                        {
                            clientsForSettle.Add(new Client());
                        }

                        writer.WriteLine($"{newClientsCount} new clients arrived.");
                    }

                    var clientsFromWaitingRoom = hotel.waitingRoom.GetAvailableClients(currentDate);
                    if(clientsFromWaitingRoom.Count > 0)
                    {
                        clientsForSettle.AddRange(clientsFromWaitingRoom);
                        writer.WriteLine($"{clientsFromWaitingRoom.Count} clients came again.");
                    }

                    if(clientsForSettle.Any())
                    {
                        var settledClientsCount = 0;
                        var clientsWithoutRoomCount = 0;
                        foreach (var newClient in clientsForSettle)
                        {
                            var room = hotel.GetAvailableRoom(newClient.Money);

                            if (room != null)
                            {
                                room.SettleClient(newClient, currentDate);
                                settledClientsCount++;
                            }
                            else
                            {
                                hotel.waitingRoom.AddClient(newClient, currentDate);
                                clientsWithoutRoomCount++;
                            }
                        }
                        writer.WriteLine($"{settledClientsCount} clients were settled. " +
                                         $"{clientsWithoutRoomCount} clients didn't find the room.");
                    }

                    currentDate = currentDate.AddMinutes(60);
                }
            }
        }
    }
}
