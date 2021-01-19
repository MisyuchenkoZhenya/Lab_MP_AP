using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_MP_AP
{
    public class WaitingRoom
    {
        public List<Awaiter> Awaiters;

        public WaitingRoom()
        {
            this.Awaiters = new List<Awaiter>();
        }

        public void AddClient(Client client, DateTime currentDate)
        {
            Awaiters.Add(new Awaiter(client, currentDate));
        }

        public List<Client> GetAvailableClients(DateTime currentDate)
        {
            var availableClients = this.Awaiters.Where(a => a.WaitingFor <= currentDate).ToList();

            if (availableClients.Any())
            {
                foreach(var client in availableClients)
                {
                    Awaiters.Remove(client);
                }
            }

            return availableClients.Select(a => a.Client).ToList();
        }
    }
}
