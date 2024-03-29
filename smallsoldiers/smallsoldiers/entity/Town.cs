﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.gui;
using Microsoft.Xna.Framework.Input;

namespace smallsoldiers.entity
{
    class Town : Entity
    {
        #region Var & get-set
        private Flag fanion;
        public bool display_flag, blind_flag, dead;
        private int delay, time_since_last, building_state, elapsed, life, life_max, level;
        //private float xp;
        //private Animation working_anim;private Rectangle rect;
        private bool free, is_selected, een, right_click;
        private Player owner;

        private Random r;
        private float elapsed_time = 0f;
        private bool ai_wait_to_create;

        public Point GetPosition() { return rect.Location; }
        public void SetOwner(Player _owner) { if (owner == null) owner = _owner; }
        public Player GetOwner() { return owner; }
        public void SetFree(bool _b) { free = _b; }
        public bool is_dead()
        {
            return dead;
        } 
        #endregion

        public void SetPosition(Point _p) { rect.X = _p.X; rect.Y = _p.Y; }

        public Town(string _asset, Player _owner, bool _isplayer)
            : base(_asset,
                   new Rectangle(0, (Cons.MAP_HEIGHT - Cons.TOWN_SIZE) / 2, Cons.TOWN_SIZE, Cons.TOWN_SIZE),
                   new Rectangle(0, 0, Cons.TOWN_SIZE, Cons.TOWN_SIZE),
                   Color.White, 0.3f)
        {
            fanion = new Flag("flag_louis");
            display_flag = false;
            delay = 60; // 700
            time_since_last = 0;
            building_state = 3;
            elapsed = 0;
            blind_flag = false;
            life_max = 100;
            life = life_max;
            level = 1;

            //working_anim = new Animation(_asset, new Rectangle(0, 0, 96, 96), 3, 0, depth, false);
            //model = new Soldier("fighter_louis", 50, 75, fanion);
            //model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
            r = new Random();

            een = false;
            ai_wait_to_create = false;
            is_selected = false;
            free = true;
            owner = _owner;

            if (!_isplayer)
            {
                rect.X = Cons.MAP_WIDTH - Cons.TOWN_SIZE - 10;
            }
            else
            {
                rect.X = 10;
            }
        }

        public void Update(GameTime _gameTime, Inputs _inputs)
        {
            Update1(_gameTime);
            Update2(_gameTime, _inputs);
        }

        private void Update1(GameTime _gameTime)
        {
            if (building_state < 3)
            {
                #region Build
                elapsed += _gameTime.ElapsedGameTime.Milliseconds;
                if (elapsed > 1000)
                {
                    elapsed -= 1000;
                    building_state++;
                    source.X = Cons.TOWN_SIZE * (3 + building_state);
                    if (building_state == 3)
                    {
                        source.X = 0;
                    }
                }
                #endregion
            }
            else
            {
            }

            if (display_flag)
            {
                fanion.Update(_gameTime);
            }

            if (life < 0)
            {
                dead = true;
            }
        }
        private void Update2(GameTime _gameTime, Inputs _inputs)
        {
            if (is_selected)
                Update_when_selected(_inputs);

            #region mouse
            if (rect.Contains(_inputs.GetRelativeX(), _inputs.GetRelativeY()))
            {
                if (_inputs.GetMLreleased())
                {
                    een = false;
                }
                else
                {
                    if (!een)
                    {
                        is_selected = true;
                        een = true;
                    }
                }
            }
            else
            {
                if (_inputs.GetMLpressed())
                {
                    is_selected = false;
                }
            }
            #endregion
        }
        private void Update_when_selected(Inputs _inputs)
        {
                if (right_click && _inputs.GetMRpressed())
                {
                    right_click = false;
                    owner.default_flag.set_new_pos(_inputs);
                }
                if (!right_click && _inputs.GetMRreleased())
                {
                    right_click = true;
                }
        }
        public void Update_IA(GameTime _gameTime)
        {
            Update1(_gameTime);
        }

        public void Draw_flag()//or_not
        {
            if (display_flag)
                fanion.Draw(true);
        }
        public void set_new_flag_pos(int _x, int _y, bool _blindness)
        {
            blind_flag = _blindness;
            fanion.set_new_pos(_x, _y, _blindness);
        }
        private void level_up()
        {
            //working_anim.level_up(Cons.BUILDING_SIZE);
        }
    }
}
