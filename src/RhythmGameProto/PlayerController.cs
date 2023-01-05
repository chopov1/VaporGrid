using MacksInterestingMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RhythmGameProto
{
    public enum InputState { HasInput, NoInput}
    public class PlayerController : InputRandomizer
    {
        Player Player;
        public InputState State;

        public PlayerController(Player player) : base()
        {
            this.Player = player;
            State= InputState.NoInput;
        }

        public override void Update()
        {
            base.Update();
            getRandomInput();
        }

        private void getRandomInput()
        {
            //TODO refactor all this into switch statement
            if (PressedKey(InputKeys[Indicies[0]][Player.PlayerNumber]))
            {
                if (Player.gridPos.X + 1! < Player.GridManager.gridWidth)
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
                if (Player.gridPos.Y + 1! < Player.GridManager.gridHeight)
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
                        Player.dirToMove = new Vector2(0,-1);
                        State = InputState.HasInput;
                        return;
                    }
                }
            }
        }

        private void getNormalInput()
        {
            if (PressedKey(inputKeys["Right"][Player.PlayerNumber]))
            {
                if (Player.gridPos.X + 1! < Player.GridManager.gridWidth)
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
                if (Player.gridPos.Y + 1! < Player.GridManager.gridHeight)
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
