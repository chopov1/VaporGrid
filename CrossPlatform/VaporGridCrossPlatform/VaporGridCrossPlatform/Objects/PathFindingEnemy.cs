using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{

    public class PathfindingEnemy : Enemy
    {
        Random rnd;

        public Node nodeToMoveTo;
        Pathfinder pathfinder;

        public PathfindingEnemy(Game game, GridManager gm, RhythmManager rm,Camera camera, Player p) : base(game, gm,"EnemySkull", rm, camera, p)
        {
            pathfinder = new Pathfinder(gridManager, this);
            moveBuffer = 2;
            player = p;
            rnd = new Random();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            onBeatTexture = Game.Content.Load<Texture2D>("weirdFace3");
            attackTexture = Game.Content.Load<Texture2D>("weirdFaceAttack3");
            offBeatTexture = spriteTexture;
        }
        
        protected override void moveEnemy()
        {
            pathFindingMove();
        }

        private void pathFindingMove()
        {
            List<Node> path = pathfinder.findPath(this.gridManager.NodeGrid[(int)this.gridPos.X, (int)this.gridPos.Y], this.gridManager.NodeGrid[(int)player.gridPos.X, (int)player.gridPos.Y]);
            if (path.Count > 0)
            {
                gridPos = path[0].pos;
            }
            else
            {
                Debug.WriteLine("ERROR: COULDNTFIND PATH!");
            }
        }
        private void simpleMove()
        {
            int dirToMove = rnd.Next(0, 2);
            switch (dirToMove)
            {
                case 0:
                    moveX();
                    break;
                case 1:
                    moveY();
                    break;
            }
        }
        private void moveY()
        {
            if (this.gridPos.Y > player.gridPos.Y)
            {
                gridPos.Y--;
            }
            else if (this.gridPos.Y == player.gridPos.Y)
            {
                moveX();
            }
            else
            {
                gridPos.Y++;
            }
        }
        private void moveX()
        {
            if (this.gridPos.X > player.gridPos.X)
            {
                gridPos.X--;
            }
            else if (this.gridPos.X == player.gridPos.X)
            {
                if (this.gridPos == player.gridPos)
                {
                    return;
                }
                else
                {
                    moveY();
                }
            }
            else
            {
                gridPos.X++;
            }
        }

    }
}
