using Lab_MP_AP.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

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

            //var hotel = new Hotel(new FileLogger("./result.txt"));
            var hotel = new Hotel(new ConsoleLogger());
            hotel.AddRooms(N, X);
            hotel.AddRooms(M, Y);
            hotel.AddRooms(K, Z);

            hotel.StartWork();
        }
    }
}
