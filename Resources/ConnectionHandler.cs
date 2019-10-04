using KillAllNeighbors.Resources.Builder;
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

        public Boolean connectionEstablished = false;
        Player thisPlayer;


        public ConnectionHandler(Player player)
        {
            this.thisPlayer = player;
        }

        public async Task Connect()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(playerDataURL, thisPlayer);
            if (response.IsSuccessStatusCode)
            {
                thisPlayer.SetId(JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync()).id);
                connectionEstablished = true;
            }
            Console.WriteLine("DD");
        }

        public async Task UpdatePlayerData(int x, int y)
        {

                this.thisPlayer.PosX = x;
                this.thisPlayer.PosY = y;
                HttpResponseMessage response = await client.PutAsJsonAsync(playerDataURL + "/" + thisPlayer.id, thisPlayer);
                if (response.IsSuccessStatusCode)
                {
                    Uri gizmoURL = response.Headers.Location;
                }

            }

            public async Task<ICollection<Player>> GetAllPlayersData()
            {

                ICollection<Player> players = null;
                HttpResponseMessage response = await client.GetAsync(playerDataURL);
                if (response.IsSuccessStatusCode)
                {
                    players = await response.Content.ReadAsAsync<ICollection<Player>>();

                }
                return players;
            }

        }
    }
