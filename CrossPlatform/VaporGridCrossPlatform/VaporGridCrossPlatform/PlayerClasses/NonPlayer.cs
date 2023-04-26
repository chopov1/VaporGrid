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
            /*int moveornot = rnd.Next(0,4);
            switch (moveornot)
            {
                case 0:
                    break;
                default:
                    break;
            }*/
            switch (rhythmManager.RhythmState)
            {
                case RhythmState.Quarter:
                    int choice = rnd.Next(0,4);
                    switch (choice)
                    {
                        case 0:
                            if (gridPos.X + 1! < GridManager.GridWidth)
                            {
                                if (gridManager.Grid[(int)gridPos.X + 1, (int)gridPos.Y].IsWalkable)
                                {
                                    dirToMove = new Vector2(1, 0);
                                }
                            }
                            break; 
                        case 1:
                            if (gridPos.X - 1! > -1)
                            {
                                if (GridManager.Grid[(int)gridPos.X - 1, (int)gridPos.Y].IsWalkable)
                                {
                                    dirToMove = new Vector2(-1, 0);
                                }
                            }
                            break; 
                        case 2:
                            if (gridPos.Y + 1! < GridManager.GridHeight)
                            {
                                if (GridManager.Grid[(int)gridPos.X, (int)gridPos.Y + 1].IsWalkable)
                                {
                                    dirToMove = new Vector2(0, 1);
                                }
                            }
                            break;
                        case 3:
                            if (gridPos.Y - 1! > -1)
                            {
                                if (GridManager.Grid[(int)gridPos.X, (int)gridPos.Y - 1].IsWalkable)
                                {
                                    dirToMove = new Vector2(0, -1);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (!rhythmManager.HasHitQuarterBeat)
                    {
                        rhythmManager.HasHitQuarterBeat = true;
                        camera.AdjustZoom(.05f);
                        updatePlayerGridPos();
                    }
                   
                    break;
                case RhythmState.Sixteenth:
                    break;
                case RhythmState.Offbeat:
                    break;
            }


        }

        protected override void updateInputAnalytics()
        {
            
        }
    }
}
