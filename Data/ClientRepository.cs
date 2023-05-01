using ClientsApi.Models;

namespace ClientsApi.Data
{
    public class ClientRepository : IClientRepository
    {
        DataContextEF _entityFramework;

        public ClientRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if(entityToAdd != null) 
            {
               _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToRemove) 
        {
            if(entityToRemove != null) 
            {
                 _entityFramework.Remove(entityToRemove);
            }
        }

        public IEnumerable<Client> GetClients()
        {
            IEnumerable<Client> clients = _entityFramework.Clients.ToList<Client>();
            return clients;
        }

        public Client GetSingleClient(int clientId)
        {
            Client? client = _entityFramework.Clients
                .Where(c => c.ClientId == clientId)
                .FirstOrDefault<Client>();

            if(client != null)
            {
                return client;
            }

            throw new Exception("No se encontró el cliente con dicho id " + clientId);
        }
    }
}