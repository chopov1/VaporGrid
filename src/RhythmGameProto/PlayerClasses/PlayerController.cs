using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RhythmGameProto
{
    public enum InputState { HasInput, NoInput }
    public class PlayerController : InputRandomizer
    {
        Player Player;
        public InputState State;

        public PlayerController(Player player) : base()
        {
            Player = player;
            State = InputState.NoInput;
        }

        public override void Update()
        {
            base.Update();

            if (isUsingGamepad())
            {
                getNormalGamePadInput();
            }
            else
            {
                getRandomInput();
            }
        }

        private void getRandomInput()
        {
            //TODO refactor all this into switch statement
            if (PressedKey(InputKeys[Indicies[0]][Player.PlayerNumber]))
            {
                if (Player.gridPos.X + 1! < Player.GridManager.GridWidth)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X + 1, (int)Player.gridPos.Y].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(1, 0);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            if (PressedKey(InputKeys[Indicies[1]][Player.PlayerNumber]))
            {
                if (Player.gridPos.X - 1! > -1)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X - 1, (int)Player.gridPos.Y].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(-1, 0);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            if (PressedKey(InputKeys[Indicies[2]][Player.PlayerNumber]))
            {
                if (Player.gridPos.Y + 1! < Player.GridManager.GridHeight)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y + 1].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(0, 1);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            if (PressedKey(InputKeys[Indicies[3]][Player.PlayerNumber]))
            {
                if (Player.gridPos.Y - 1! > -1)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y - 1].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(0, -1);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            else
            {
                State= InputState.NoInput;
            }
            
        }

        private void getRandomInputGamePad()
        {
            Vector2 stickValue = Thumbstick(1);
            
            if ( stickValue.X >= .2)
            {

            }
        }

        /*private int getRandStick(int index)
        {
            switch (index)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }*/

        private void getNormalGamePadInput()
        {
            Vector2 stickValue = Thumbstick(1);
            if (stickValue.X >= .2 && stickValue.X > Math.Abs(stickValue.Y))
            {
                if (Player.gridPos.X + 1! < Player.GridManager.GridWidth)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X + 1, (int)Player.gridPos.Y].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(1, 0);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            if(stickValue.X <= -.2 && stickValue.X < -1 * Math.Abs(stickValue.Y))
            {
                if (Player.gridPos.X - 1! > -1)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X - 1, (int)Player.gridPos.Y].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(-1, 0);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            if(stickValue.Y >= .2 && stickValue.Y > Math.Abs(stickValue.X))
            {
                if (Player.gridPos.Y - 1! > -1)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y - 1].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(0, -1);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            if(stickValue.Y <= -.2 && stickValue.Y < -1 * Math.Abs(stickValue.X))
            {
                if (Player.gridPos.Y + 1! < Player.GridManager.GridHeight)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y + 1].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(0, 1);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
            else if (stickValue.X > -.2 && stickValue.X < .2 && stickValue.Y > -.2 & stickValue.Y < .2)
            {
                State = InputState.NoInput;
            }
        }

        private void getNormalInput()
        {
            if (PressedKey(inputKeys["Right"][Player.PlayerNumber]))
            {
                if (Player.gridPos.X + 1! < Player.GridManager.GridWidth)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X + 1, (int)Player.gridPos.Y].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(1, 0);
                        State = InputState.HasInput;
                    }
                }
            }
            if (PressedKey(inputKeys["Left"][Player.PlayerNumber]))
            {
                if (Player.gridPos.X - 1! > -1)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X - 1, (int)Player.gridPos.Y].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(-1, 0);
                        State = InputState.HasInput;
                    }
                }
            }
            if (PressedKey(inputKeys["Down"][Player.PlayerNumber]))
            {
                if (Player.gridPos.Y + 1! < Player.GridManager.GridHeight)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y + 1].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(0, 1);
                        State = InputState.HasInput;
                    }
                }
            }
            if (PressedKey(inputKeys["Up"][Player.PlayerNumber]))
            {
                if (Player.gridPos.Y - 1! > -1)
                {
                    if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y - 1].IsWalkable)
                    {
                        Player.dirToMove = new Vector2(0, -1);
                        State = InputState.HasInput;
                    }
                }
            }
        }
    }
}
