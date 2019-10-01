using Server.IO.Data;
using System.Threading.Tasks;

namespace Server.IO
{
    public class ClientDataSaver
    {
        // Allemaal netjes asynchroon :))))
        public async Task<ClientCollection> LoadClients()
        {
            // Ik ga er hiervan uit dat de data in de clientData.json alleen bestaat uit een json array (JArray) met client objecten.
            // In de file staat een voorbeeldje.
            return await JsonHandler.LoadObject<ClientCollection>("clientData.json");
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