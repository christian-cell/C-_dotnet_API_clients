namespace ClientsApi.Models
{
    public partial class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string DNI { get; set; }

        /* creamos un constructor para convertir todos los nulls a empty str */
        public Client()
        {
            if(Name == null)
            {
                Name = "";
            };
            if (LastName == null)
            {
                LastName = "";
            };
            if (DNI == null)
            {
                DNI = "";
            };
        }
    }
}