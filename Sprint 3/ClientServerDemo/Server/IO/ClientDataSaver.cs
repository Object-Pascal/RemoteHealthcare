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
            // Ik ga er hiervan uit dat de data in de clientData.json alleen bestaat uit een json array (JArray) met client objecten.
            // In de file staat een voorbeeldje.
            return await JsonHandler.LoadObject<ClientCollection>("clientData.json");
        }

        public async Task ToevoegenAsync(Client client)
        {
            ClientCollection clientCollection = await LoadClients();
            List<object> list = clientCollection.clients.ToList<object>();
            list.Add(client);
            list.ToArray();
            string output = JsonConvert.SerializeObject(list);
            JsonHandler.SaveFile("clientData.json", output);
        }

        public async Task VerwijderenAsync(Client client)
        {
            ClientCollection clientCollection = await LoadClients();
            clientCollection.clients.Where(x => x.Id == client.Id).First();

            string output = JsonConvert.SerializeObject(clientCollection.clients);
            JsonHandler.SaveFile("clientData.json", output);
        }

        public async Task AanpassenAsync(Client client1, Client client2)
        {
            ClientCollection clientCollection = await LoadClients();
            List<Client> list = clientCollection.clients.ToList<Client>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(client1))
                    list[i] = client2;
            }
            list.ToArray();
            string output = JsonConvert.SerializeObject(list);
            JsonHandler.SaveFile("clientData.json", output);
        }

        /*  
            Toevoegen()
                1. Inladen naar ClientCollection
                2. Client data toevoegen aan de array in ClientCollection, het makkelijkste is om te converten naar een list, dan toevoegen en dan terug converten naar een array :)
                3. Serializen naar een json string met JsonConvert
                4. Bestand overschrijven met JsonHandler.Save (is een async void dus hoeft geen await voor)

            Verwijderen()
                1. Inladen naar ClientCollection
                2. Array item deleten waarbij het client id de filter matched, tip: gebruik linq -> clientCollection.clients.Where(x => x.Id == filter).First()
                3. Serializen naar een json string
                4. Bestand overschrijven

            Aanpassen()
                1. Inladen naar ClientCollection
                2. Array item aanpassen waarbij het client id de filter matched
                3. Serializen naar een json string
                4. Bestand overschrijven
        */
    }
}