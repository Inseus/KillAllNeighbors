using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public class ConnectionHandler
    {
        private static readonly HttpClient client = new HttpClient(new HttpClientHandler
        {
            UseProxy = false
        });
        private string playerDataURL = "https://localhost:44312/api/player";
        private string coinsDataURL = "https://localhost:44312/api/coins";
        private Point lastLocation;
        private int lastShooting;

        public Boolean connectionEstablished = false;
        Unit thisPlayer;

        public ConnectionHandler(Unit player)
        {
            thisPlayer = player;
            lastLocation = new Point(0, 0);
            lastShooting = 1;
        }

        public async Task Connect()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(playerDataURL, thisPlayer);
            if (response.IsSuccessStatusCode)
            {
                thisPlayer.SetId(JsonConvert.DeserializeObject<Unit>(await response.Content.ReadAsStringAsync()).id);
                connectionEstablished = true;
            }
        }
        public async Task DisConnect()
        {
            thisPlayer.shootingType = 100;
            HttpResponseMessage response = await client.PutAsJsonAsync(playerDataURL + "/" + thisPlayer.id, thisPlayer);
            if (response.IsSuccessStatusCode)
            {
                Uri gizmoURL = response.Headers.Location;
            }
        }

        public async Task UpdatePlayerData()
        {
            if (thisPlayer.PosX != lastLocation.X || thisPlayer.PosY != lastLocation.Y || thisPlayer.isShooting != lastShooting)
            {
                lastLocation.X = (int)thisPlayer.PosX;
                lastLocation.Y = (int)thisPlayer.PosY;
                lastShooting = thisPlayer.isShooting;
                HttpResponseMessage response = await client.PutAsJsonAsync(playerDataURL + "/" + thisPlayer.id, thisPlayer);
                if (response.IsSuccessStatusCode)
                {
                    Uri gizmoURL = response.Headers.Location;
                }
            }
        }

        public async Task<ICollection<Unit>> GetAllPlayersData()
        {

            ICollection<Unit> players = null;
            HttpResponseMessage response = await client.GetAsync(playerDataURL);
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Unit>>();

            }
            return players;
        }

    }
}
