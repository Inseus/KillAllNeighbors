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
        private static readonly HttpClient client = new HttpClient();
        private string playerDataURL = "https://localhost:44312/api/player";
        private string coinsDataURL  = "https://localhost:44312/api/coins";
        public Player thisPlayer;
        public List<PlayerWithObject> playerWithObjectCollection;

        public ConnectionHandler()
        {
            thisPlayer = new Player();
            playerWithObjectCollection = new List<PlayerWithObject>();
            Connect().Wait(10);



        }

        private async Task Connect()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(playerDataURL, thisPlayer);
            if (response.IsSuccessStatusCode)
            {
                thisPlayer.SetId(JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync()).id);
            }
        }

        public async Task UpdatePlayerData(int x , int y)
        {
            this.thisPlayer.PosX = x;
            this.thisPlayer.PosY = y;
            HttpResponseMessage response = await client.PutAsJsonAsync(playerDataURL + "/" + thisPlayer.id, thisPlayer);
                if (response.IsSuccessStatusCode)
                {
                    Uri gizmoURL = response.Headers.Location;
                }
           
        }

        private async Task<ICollection<Player>> GetAllPlayersData()
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(playerDataURL);
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();

            }
            return players;
        }
        public async Task CreateAndUpdateIfNeeded(Form form)
        {
            ICollection<Player> playerCollection = await GetAllPlayersData();
            if (playerCollection == null)
            {
                return;
            }

            playerCollection = playerCollection.Where(player => player.id != this.thisPlayer.id).ToList();
            // Surandam dar nesancius zaidejus ir pridedam
            foreach (Player p in playerCollection)
            {

                if (playerWithObjectCollection.Find(x => x.player.id == p.id) == null)
                {
                    // Create
                    PictureBox a = new PictureBox
                    {
                        Name = "pictureBox",
                        Size = new Size(18, 18),
                        Location = new Point((int)p.PosX, (int)p.PosY),
                        BackColor = Color.Black,
                    };
                    form.Controls.Add(a);
                    PlayerWithObject temp = new PlayerWithObject(a, p);
                    playerWithObjectCollection.Add(temp);
                }
                else
                {
                    playerWithObjectCollection.Find(x => x.player.id == p.id).movableObject.Location = new Point((int)p.PosX, (int)p.PosY);

                }

            }

        }


    }
}
