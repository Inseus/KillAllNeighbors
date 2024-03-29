﻿using KillAllNeighbors.Proxy;
using KillAllNeighbors.Resources;
using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Strategy;
using KillAllNeighbors.Resources.Strategy.Implementation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace KillAllNeighbors
{
    interface IControls
    {
        Vector2 Upwards();
        Vector2 Downwards();
        Vector2 Right();
        Vector2 Left();
    }

    public class ControlsHandler : IControls
    {
        VectorProxy proxy = new VectorProxy();
        private static ControlsHandler instance = null;
        private static readonly object instanceLock = new object();
        private int speedMultiplier = 5;
        private string lastDirection = "up";
        private Bullet bullet; 
        public static ControlsHandler Instance {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new ControlsHandler();
                    return instance;
                }
            }
        }

        private Vector2 zero;
        public ControlsHandler()
        {

            zero = proxy.Zero();
        }

        public Vector2 GetVector()
        {
            Vector2 _temp = new Vector2();
            foreach (Vector2 vector in GetCoord())
            {
                _temp += vector * speedMultiplier;
            }
            return _temp;
        }

        private IEnumerable GetCoord()
        {
            yield return Upwards();
            yield return Downwards();
            yield return Right();
            yield return Left();
        }
        public string GetDirection()
        {
            if (Upwards() != zero)
                lastDirection = "up";
            if (Downwards() != zero)
                lastDirection = "down";
            if (Right() != zero)
                lastDirection = "right";
            if (Left() != zero)
                lastDirection = "left";
            return lastDirection;
        }
        public Bullet GetWeapon(CreatorOfPictureBox creator,Player mainPlayer)
        {

            if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                bullet = new Bullet(new Pistol(creator));
                mainPlayer.shootingType = 1;
                return bullet.ContextInterface();
                
            }
            if (Keyboard.IsKeyDown(Key.NumPad2))
            {
                bullet = new Bullet(new Machinegun(creator));
                mainPlayer.shootingType = 2;
                return bullet.ContextInterface();
            }
            if (Keyboard.IsKeyDown(Key.NumPad3))
            {
                bullet = new Bullet(new Sniper(creator));
                mainPlayer.shootingType = 3;
                return bullet.ContextInterface();
                
            }
            return null;
        }
        public Bullet GetWeaponEnemy(CreatorOfPictureBox creator, int strategy)
        {

            if (strategy == 1)
            {
                bullet = new Bullet(new Pistol(creator));
                return bullet.ContextInterface();
            }
            if (strategy == 2)
            {
                bullet = new Bullet(new Machinegun(creator));
                return bullet.ContextInterface();
            }
            if (strategy == 3)
            {
                bullet = new Bullet(new Sniper(creator));
                return bullet.ContextInterface();

            }
            return null;
        }
        public Vector2 Upwards()
        {
            return Keyboard.IsKeyDown(Key.W) ? proxy.Up() : zero;
        }
        
        public Vector2 Downwards()
        {
            return Keyboard.IsKeyDown(Key.S) ? proxy.Down() : zero;
        }

        public Vector2 Right()
        {
            return Keyboard.IsKeyDown(Key.D)? proxy.Right() : zero;
        }

        public Vector2 Left()
        {
            return Keyboard.IsKeyDown(Key.A)? proxy.Left() : zero;
        }

        public bool IsAnyKeyDown()
        {
            if (Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.D))
            {
                return true;
            }
            return false;
        }

        public int KeysDownCount()
        {
            int _count = 0;
            int[] _keysVarNumbers = { 66, 44, 62, 47 };
            for (int i = 0; i < _keysVarNumbers.Length; i++)
            {
                _count += Keyboard.IsKeyDown((Key)_keysVarNumbers[i]) ? 1 : 0;
            }
            return _count;
        }
    }
}
