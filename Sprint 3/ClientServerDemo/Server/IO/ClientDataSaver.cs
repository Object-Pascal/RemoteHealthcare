using Newtonsoft.Json;
using Server.IO.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.IO
{
    public class ClientDataSaver
    {
        public static async Task<ClientCollection> LoadClients()
        {
            return await JsonHandler.LoadObject<ClientCollection>("clientData.json");
        }

        public async void ToevoegenAsync(Client client)
        {
            ClientCollection clientCollection = await LoadClients();
            clientCollection.clients.Add(client);

            JsonHandler.SaveObject("clientData.json", clientCollection);
        }

        public async void VerwijderenAsync(int id)
        {
            ClientCollection clientCollection = await LoadClients();
            clientCollection.clients.Remove(clientCollection.clients.Where(x => x.Id == id).First());

            JsonHandler.SaveObject("clientData.json", clientCollection);
        }

        public async void AanpassenAsync(int id, Client newClientData)
        {
            ClientCollection clientCollection = await LoadClients();
            Client selectedClient = clientCollection.clients.Where(x => x.Id == id).First();

            // Wat je wilt veranderen
            selectedClient.Name = newClientData.Name;

            JsonHandler.SaveObject("clientData.json", clientCollection);
        }
    }
}