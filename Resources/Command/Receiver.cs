using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Command
{
    class Receiver
    {
        static List<int> xHistory;
        static List<int> yHistory;
        public void Do(int x, int y,Player thisPlayer)
        {
            thisPlayer.getMovableObject().Location = new Point(thisPlayer.getMovableObject().Location.X + x, thisPlayer.getMovableObject().Location.Y + y);
            thisPlayer.setCordinatesFromPictureBoxToPlayer();
            if (xHistory == null)
                xHistory = new List<int>();
            if (yHistory == null)
                yHistory = new List<int>();
            if (x != 0 || y != 0)
            {
                xHistory.Add(x);
                yHistory.Add(y);
            }
        }
        public void Undo(int x, int y, Player thisPlayer)
        {
            for (int i = 0; i < xHistory.Count; i++)
                if (xHistory[i] == x && yHistory[i] == y)
                {
                    thisPlayer.getMovableObject().Location = new Point(thisPlayer.getMovableObject().Location.X - x, thisPlayer.getMovableObject().Location.Y - y);
                    thisPlayer.setCordinatesFromPictureBoxToPlayer();
                    xHistory.RemoveAt(i);
                    yHistory.RemoveAt(i);
                }
        }
    }
}
