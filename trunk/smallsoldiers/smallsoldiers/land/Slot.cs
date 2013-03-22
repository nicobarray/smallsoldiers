﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.land
{
    class Slot
    {
        private Rectangle rect;
        private bool free;
        private Color color;

        public Slot(int _i, int _j)
        {
            rect = new Rectangle(_i, _j, Cons.building_size, Cons.building_size);
            color = Color.Red;
            free = true;
        }

        public void Update(int _mx, int _my, bool _mpressed)
        {

            if (rect.Contains(_mx, _my))
            {
                if (!_mpressed)
                    color = Color.Yellow;
                else
                    color = Color.Purple;
            }
            else
            {
                color = Color.Red;
            }
        }

        public void Draw()
        {
            if (free)
                Ressource.sb.Draw(Ressource.Get("pixel"), rect, color);
        }
    }
}