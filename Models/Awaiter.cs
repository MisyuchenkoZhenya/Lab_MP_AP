using System;

namespace Lab_MP_AP
{
    public class Awaiter
    {
        public Client Client { get; }
        public DateTime WaitingFor { get; }

        public Awaiter(Client client, DateTime currentDate)
        {
            Client = client;
            WaitingFor = currentDate.AddHours(new Random().Next(12, 24));
        }
    }
}
