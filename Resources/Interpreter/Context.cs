using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors
{
    public class Context
    {
        public string Command { get; set; }
        public Color bgColor { get; set; }

        public Context(string comm, Color color)
        {
            Command = comm;
            bgColor = color;
        }
    }
}
