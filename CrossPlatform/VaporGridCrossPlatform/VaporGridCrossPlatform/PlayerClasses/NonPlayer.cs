using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class NonPlayer : Player
    {
        Random rnd;

        public NonPlayer(Game game, GridManager gm, RhythmManager rhythmManager, int playernumber, Camera camera, ScoreManager sm) : base(game, gm, rhythmManager, playernumber, camera, sm)
        {
            rnd = new Random();
        }

        protected override void updateMovement()
        {
            switch (rhythmManager.RhythmState)
            {
                case RhythmState.Quarter:
                    int choice = rnd.Next(0,7);
                    switch (choice)
                    {
                        case 0:
                            if (gridPos.X + 1! < GridManager.GridWidth)
                            {
                                if (gridManager.Grid[(int)gridPos.X + 1, (int)gridPos.Y].IsWalkable)
                                {
                                    dirToMove = new Vector2(1, 0);
                                    return;
                                }
                            }
                            break; 
                        case 1:
                            if (gridPos.X - 1! > -1)
                            {
                                if (GridManager.Grid[(int)gridPos.X - 1, (int)gridPos.Y].IsWalkable)
                                {
                                    dirToMove = new Vector2(-1, 0);
                                    return;
                                }
                            }
                            break; 
                        case 2:
                            if (gridPos.Y + 1! < GridManager.GridHeight)
                            {
                                if (GridManager.Grid[(int)gridPos.X, (int)gridPos.Y + 1].IsWalkable)
                                {
                                    dirToMove = new Vector2(0, 1);
                                    return;
                                }
                            }
                            break;
                        case 3:
                            if (gridPos.Y - 1! > -1)
                            {
                                if (GridManager.Grid[(int)gridPos.X, (int)gridPos.Y - 1].IsWalkable)
                                {
                                    dirToMove = new Vector2(0, -1);
                                    return;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    camera.AdjustZoom(.05f);
                    updatePlayerGridPos();
                    break;
                case RhythmState.Sixteenth:
                    break;
                case RhythmState.Offbeat:
                    break;
            }
        }
    }
}
