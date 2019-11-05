using KillAllNeighbors.Resources.Prototype;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public static class CoinsFactory
    {
        public static ICurrency MakeCoin(int coinValue, int posX, int posY)
        {
            CoinPrototype defaultC = new CoinPrototype(posX, posY, coinValue, Color.Yellow);
            CoinPrototype CoolC = new CoinPrototype(posX, posY, coinValue, Color.Blue);
            CoinPrototype SuperC = new CoinPrototype(posX, posY, coinValue, Color.Gold);
            CoinPrototype MegaC = new CoinPrototype(posX, posY, coinValue, Color.DeepPink);
            if (coinValue < 4)
            {
                return defaultC.Clone();
            }
            else if (coinValue >= 4 && coinValue < 7)
            {
                return CoolC.Clone();
            }
            else if (coinValue >= 7 && coinValue < 9)
            {
                return SuperC.Clone();
            }
            else
            {
                return MegaC.Clone();
            }
        }
    }
    public interface ICurrency
    {
        int Value { get; set; }
        int PosX { get; set; }
        int PosY { get; set; }
        Color Color { get; set; }
        int SizeX { get; set; }
        int SizeY { get; set; }
        PictureBox controlItem { get; set; }
    }

    //public class DefaultCoin : ICurrency
    //{
    //    public int Value { get; set; }
    //    public int PosX { get; set; }
    //    public int PosY { get; set; }
    //    public Color Color { get; set; }
    //    public int SizeX { get; set; }
    //    public int SizeY { get; set; }
    //    public PictureBox controlItem { get; set; }

    //    public DefaultCoin(int posX, int posY, int value)
    //    {
    //        Value = value;
    //        PosX = posX;
    //        PosY = posY;
    //        Color = Color.Yellow;
    //        SetSize();
    //        SetControlItem();
    //    }

    //    void SetSize()
    //    {
    //        SizeX = Constants.COINS_SIZE_MULTIPLIER * Value;
    //        SizeY = SizeX;
    //    }

    //    void SetControlItem()
    //    {
    //        controlItem = new PictureBox();
    //        controlItem.Size = new Size(SizeX, SizeY);
    //        controlItem.BackColor = Color;
    //        controlItem.Location = new Point(PosX, PosY);
    //    }
    //}

        //public class CoolCoin : ICurrency
        //{
        //    public int Value { get; set; }
        //    public int PosX { get; set; }
        //    public int PosY { get; set; }
        //    public Color Color { get; set; }
        //    public int SizeX { get; set; }
        //    public int SizeY { get; set; }
        //    public PictureBox controlItem { get; set; }

        //    public CoolCoin(int posX, int posY, int value)
        //    {
        //        Value = value;
        //        PosX = posX;
        //        PosY = posY;
        //        Color = Color.Blue;
        //        SetSize();
        //        SetControlItem();
        //    }

        //    void SetSize()
        //    {
        //        SizeX = Constants.COINS_SIZE_MULTIPLIER * Value;
        //        SizeY = SizeX;
        //    }
        //    void SetControlItem()
        //    {
        //        controlItem = new PictureBox();
        //        controlItem.Size = new Size(SizeX, SizeY);
        //        controlItem.BackColor = Color;
        //        controlItem.Location = new Point(PosX, PosY);
        //    }
        //}

        //public class SuperCoin : ICurrency
        //{
        //    public int Value { get; set; }
        //    public int PosX { get; set; }
        //    public int PosY { get; set; }
        //    public Color Color { get; set; }
        //    public int SizeX { get; set; }
        //    public int SizeY { get; set; }
        //    public PictureBox controlItem { get; set; }

        //    public SuperCoin(int posX, int posY, int value)
        //    {
        //        Value = value;
        //        PosX = posX;
        //        PosY = posY;
        //        Color = Color.Gold;
        //        SetSize();
        //        SetControlItem();
        //    }

        //    void SetSize()
        //    {
        //        SizeX = Constants.COINS_SIZE_MULTIPLIER * Value;
        //        SizeY = SizeX;
        //    }
        //    void SetControlItem()
        //    {
        //        controlItem = new PictureBox();
        //        controlItem.Size = new Size(SizeX, SizeY);
        //        controlItem.BackColor = Color;
        //        controlItem.Location = new Point(PosX, PosY);
        //    }
        //}

        //public class MegaCoin : ICurrency
        //{
        //    public int Value { get; set; }
        //    public int PosX { get; set; }
        //    public int PosY { get; set; }
        //    public Color Color { get; set; }
        //    public int SizeX { get; set; }
        //    public int SizeY { get; set; }
        //    public PictureBox controlItem { get; set; }

        //    public MegaCoin(int posX, int posY, int value)
        //    {
        //        Value = value;
        //        PosX = posX;
        //        PosY = posY;
        //        Color = Color.DeepPink;
        //        SetSize();
        //        SetControlItem();
        //    }
        //    void SetSize()
        //    {
        //        SizeX = Constants.COINS_SIZE_MULTIPLIER * Value;
        //        SizeY = SizeX;
        //    }
        //    void SetControlItem()
        //    {
        //        controlItem = new PictureBox();
        //        controlItem.Size = new Size(SizeX, SizeY);
        //        controlItem.BackColor = Color;
        //        controlItem.Location = new Point(PosX, PosY);
        //    }
        //}
    }
