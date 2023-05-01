using ClientsApi.Models;
using System.Collections.Generic;

namespace ClientsApi.Data
{
    public interface IClientRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToRemove);
        public IEnumerable<Client> GetClients();
        public Client GetSingleClient(int clientId);
    }
}