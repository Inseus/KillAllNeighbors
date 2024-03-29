﻿using KillAllNeighbors.Resources.Visitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Composite
{
    public abstract class DrawingElement : Element
    {
        public PictureBox line { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Color Color { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        protected string _name;

        // Constructor
        public DrawingElement(string name, int posX, int posY, int sizeX, int sizeY)
        {
            this._name = name;
            PosX = posX;
            PosY = posY;
            Color = Color.Black;
            SizeX = sizeX;
            SizeY = sizeY;
        }

        public abstract void Add(DrawingElement d);

        public abstract void Remove(DrawingElement d);

        public abstract void Display(int indent);
    }
}
