using Newtonsoft.Json;
using Server.IO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.IO
{
    public class IODataHandler
    {
        public static async Task<ClientCollection> LoadClients()
        {
            return await JsonHandler.LoadObject<ClientCollection>("clientData.json");
        }

        public static async Task<ClientDataCollection> LoadClientData()
        {
            return await JsonHandler.LoadObject<ClientDataCollection>("clientHistoryData.json");
        }

        public static async Task<DoctorCollection> LoadDoctors()
        {
            return await JsonHandler.LoadObject<DoctorCollection>("doctorData.json");
        }

        public async Task<Client> GetClientAsync(int id)
        {
            try
            {
                ClientCollection clientCollection = await LoadClients();
                IEnumerable<Client> possibleMatches = clientCollection.clients.Where(x => x.Id == id);

                if (possibleMatches != null)
                {
                    if (possibleMatches.Count() > 0)
                    {
                        return possibleMatches.First();
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ClientData> GetClientDataAsync(int id, DateTime date)
        {
            try
            {
                ClientDataCollection clientDataCollection = await LoadClientData();
                IEnumerable<ClientData> possibleMatches = clientDataCollection.clientDataEntries.Where(x => x.clientId == id
                    && x.Date.Year == date.Year
                    && x.Date.Month == date.Month
                    && x.Date.Day == date.Day);

                if (possibleMatches != null)
                {
                    if (possibleMatches.Count() > 0)
                    {
                        return possibleMatches.First();
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Doctor> GetDoctorAsync(string userName, string password)
        {
            try
            {
                DoctorCollection doctorCollection = await LoadDoctors();
                IEnumerable<Doctor> possibleMatches = doctorCollection.doctors.Where(x => x.UserName == userName && x.Password == password);

                if (possibleMatches != null)
                {
                    if (possibleMatches.Count() > 0)
                    {
                        return possibleMatches.First();
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            try
            {
                ClientCollection clientCollection = await LoadClients();
                clientCollection.clients.Add(client);

                bool saved = await JsonHandler.SaveObject("clientData.json", clientCollection);

                if (saved)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddClientDataAsync(ClientData clientData)
        {
            try
            {
                ClientDataCollection clientDataCollection = await LoadClientData();
                clientDataCollection.clientDataEntries.Add(clientData);

                bool saved = await JsonHandler.SaveObject("clientHistoryData.json", clientDataCollection);

                if (saved)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> ProvideNewClientId()
        {
            try
            {
                ClientCollection clientCollection = await LoadClients();
                return clientCollection.clients.Last().Id + 1;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<bool> DeleteClientAsync(Client client)
        {
            try
            {
                ClientCollection clientCollection = await LoadClients();
                clientCollection.clients.Remove(clientCollection.clients.Where(x => x.Id == client.Id).First());

                bool saved = await JsonHandler.SaveObject("clientData.json", clientCollection);

                if (saved)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ModifyClientAsync(int id, Client newClientData)
        {
            try
            {
                ClientCollection clientCollection = await LoadClients();
                Client selectedClient = clientCollection.clients.Where(x => x.Id == id).First();

                // Wat je wilt veranderen
                selectedClient.Name = newClientData.Name;

                bool saved = await JsonHandler.SaveObject("clientData.json", clientCollection);

                if (saved)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}