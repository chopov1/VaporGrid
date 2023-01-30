using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;


namespace RhythmGameProto
{


    public class InputHandler
    {
        private KeyboardState KeyboardState;
        KeyboardState prevKeyboardState;
        GamePadState GamePadState;
        GamePadState prevGamePadState;

        //figure out how to make key names reassignable for each player so that in the player class
        //right can mean rightarrow if its p1 or d if its p2 etc..
        public Dictionary<string, Keys[]> inputKeys;
        public Dictionary<string, Buttons[]> inputButtons;
        public string[] stickDirs;

        public InputHandler()
        {
            setupKeyDic();
            setupButtonDic();
            setupStickDirs();
        }
        private void setupButtonDic()
        {
            inputButtons = new Dictionary<string, Buttons[]>();
            inputButtons.Add("Up", new Buttons[] { Buttons.A, Buttons.X });
        }
        private void setupKeyDic()
        {
            inputKeys = new Dictionary<string, Keys[]>();
            inputKeys.Add("Right", new Keys[] { Keys.Right, Keys.D });
            inputKeys.Add("Left", new Keys[] { Keys.Left, Keys.A });
            inputKeys.Add("Up", new Keys[] { Keys.Up, Keys.W });
            inputKeys.Add("Down", new Keys[] { Keys.Down, Keys.S });
        }
        private void setupStickDirs()
        {
            stickDirs = new string[4];
            stickDirs[0] = "Right";
            stickDirs[1] = "Left";
            stickDirs[2] = "Down";
            stickDirs[3] = "Up";
        }
        public virtual void Update()
        {
            prevKeyboardState = KeyboardState;
            prevGamePadState = GamePadState;
            KeyboardState = Keyboard.GetState();
            GamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public bool isUsingGamepad()
        {
            return GamePad.GetState(PlayerIndex.One).IsConnected;
        }

        public Vector2 Thumbstick(int player)
        {
            GamePadThumbSticks sticks = GamePadState.ThumbSticks;
            
            if (player == 1)
            {
                return sticks.Left;
            }
            else
            {
                return sticks.Right;
            }

        }

        public bool IsDirectionPressed(string dir, int playerNum)
        {
            Vector2 stickValue = Thumbstick(playerNum);
            switch (dir)
            {
                case "Right":
                    if (stickValue.X >= .6 && stickValue.X > Math.Abs(stickValue.Y))
                    {
                        return true;
                    }
                    break;
                case "Left":
                    if(stickValue.X <= -.6 && stickValue.X < -1 * Math.Abs(stickValue.Y))
                    {
                        return true;
                    }
                    break;
                case "Down":
                    if(stickValue.Y >= .6 && stickValue.Y > Math.Abs(stickValue.X))
                    {
                        return true;
                    }
                    break;
                case "Up":
                    if(stickValue.Y <= -.6 && stickValue.Y < -1 * Math.Abs(stickValue.X))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public bool IsButtonPressed(Buttons button)
        {
            return GamePadState.IsButtonDown(button);
        }

        public bool IsHoldingButton(Buttons button)
        {
            if (prevGamePadState.IsButtonDown(button) && GamePadState.IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        public bool ReleasedButton(Buttons button)
        {
            if (prevGamePadState.IsButtonDown(button) && !GamePadState.IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        public bool IsKeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public bool PressedKey(Keys key)
        {
            if (!prevKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool releasedAllKeys(List<Keys[]> keys, int playerNum)
        {
            foreach (var key in keys)
            {
                if (PressedKey(key[playerNum]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsHoldingKey(Keys key)
        {
            if (prevKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool ReleasedKey(Keys key)
        {
            if (prevKeyboardState.IsKeyDown(key) && !KeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool IsPressingAnyKey()
        {
            if (KeyboardState.GetPressedKeyCount() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
