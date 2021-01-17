using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_MP_AP
{
    public class Awaiter
    {
        public Client Client { get; }
        public DateTime WaitingFor { get; }

        public Awaiter(Client client, DateTime currentDate)
        {
            this.Client = client;
            this.WaitingFor = currentDate.AddHours(new Random().Next(12, 24));
        }
    }
}
