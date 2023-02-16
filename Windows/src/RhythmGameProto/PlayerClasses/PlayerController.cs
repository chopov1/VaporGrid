using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                getRandomGamePadInput();
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

        private void getRandomGamePadInput()
        {
            //right
            if (IsDirectionPressed(stickDirs[Indicies[0]], Player.PlayerNumber))
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
            //left
            if (IsDirectionPressed(stickDirs[Indicies[1]], Player.PlayerNumber))
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
            //down
            if (IsDirectionPressed(stickDirs[Indicies[2]], Player.PlayerNumber))
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
            //up
            if (IsDirectionPressed(stickDirs[Indicies[3]], Player.PlayerNumber))
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
                State = InputState.NoInput;
            }
        }

        private void getRandStick(int index)
        {
            if (State == InputState.HasInput)
            {
                return;
            }
            foreach (int i in Indicies)
            {
                Debug.WriteLine(i);
            }
            Debug.WriteLine("-");
            Debug.WriteLine(index);
            Debug.WriteLine("------");
            switch (index)
            {
                case 0:
                    //right
                    if (Player.gridPos.X + 1! < Player.GridManager.GridWidth)
                    {
                        if (Player.GridManager.Grid[(int)Player.gridPos.X + 1, (int)Player.gridPos.Y].IsWalkable)
                        {
                            Player.dirToMove = new Vector2(1, 0);
                            State = InputState.HasInput;
                            return;
                        }
                    }
                    break;
                case 3:
                    //left
                    if (Player.gridPos.X - 1! > -1)
                    {
                        if (Player.GridManager.Grid[(int)Player.gridPos.X - 1, (int)Player.gridPos.Y].IsWalkable)
                        {
                            Player.dirToMove = new Vector2(-1, 0);
                            State = InputState.HasInput;
                            return;
                        }
                    }
                    break;
                case 2:
                    //down
                    if (Player.gridPos.Y + 1! < Player.GridManager.GridHeight)
                    {
                        if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y + 1].IsWalkable)
                        {
                            Player.dirToMove = new Vector2(0, 1);
                            State = InputState.HasInput;
                            return;
                        }
                    }
                    break;
                case 1:
                    //up
                    if (Player.gridPos.Y - 1! > -1)
                    {
                        if (Player.GridManager.Grid[(int)Player.gridPos.X, (int)Player.gridPos.Y - 1].IsWalkable)
                        {
                            Player.dirToMove = new Vector2(0, -1);
                            State = InputState.HasInput;
                            return;
                        }
                    }
                    break;
            }
        }

        private void getNormalGamePadInput()
        {
            
            Vector2 stickValue = Thumbstick(1);
            //right
            if (stickValue.X >= .6 && stickValue.X > Math.Abs(stickValue.Y))
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
            //left
            if(stickValue.X <= -.6 && stickValue.X < -1 * Math.Abs(stickValue.Y))
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
            //down
            if(stickValue.Y >= .6 && stickValue.Y > Math.Abs(stickValue.X))
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
            //up
            if(stickValue.Y <= -.6 && stickValue.Y < -1 * Math.Abs(stickValue.X))
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
