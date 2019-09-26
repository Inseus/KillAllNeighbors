using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources
{
    public class ConnectionHandler
    {
        private static readonly HttpClient client = new HttpClient();
        private string playerDataURL = "https://localhost:44312/api/player";
        private string coinsDataURL  = "https://localhost:44312/api/coins";
        public Player thisPlayer;

        public ConnectionHandler()
        {
            thisPlayer = new Player();
            Connect();
        }

        private async void Connect()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(playerDataURL, thisPlayer);
            if (response.IsSuccessStatusCode)
            {
                thisPlayer.id = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync()).id;
            }
        }

        public async void UpdatePlayerData()
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(playerDataURL + thisPlayer.id, thisPlayer);
            if (response.IsSuccessStatusCode)
            {
                Uri gizmoURL = response.Headers.Location;
            }
        }

        private async void GetAllPlayersData()
        {
            HttpResponseMessage response = await client.GetAsync(playerDataURL);
            if (response.IsSuccessStatusCode)
            {
                Uri gizmoURL = response.Headers.Location;
            }
        }


    }
}
