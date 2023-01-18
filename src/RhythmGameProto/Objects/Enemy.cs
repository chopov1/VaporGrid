using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{

    public class Enemy : GridSprite
    {
        RhythmManager rhythmManager;
        Player player;
        Random rnd;

        int moveBuffer;

        public Node nodeToMoveTo;
        Pathfinder pathfinder;

        Texture2D texture;
        Texture2D beatTexture;
        Texture2D attacktexture;


        public Enemy(Game game, GridManager gm, RhythmManager rm, Player p, Camera camera) : base(game, gm, "EnemySkull", camera)
        {
            pathfinder = new Pathfinder(gridManager, this);
            rhythmManager = rm;
            moveBuffer = 2;
            player = p;
            rnd = new Random();
            Game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            beatTexture = Game.Content.Load<Texture2D>("weirdFace3");
            attacktexture = Game.Content.Load<Texture2D>("weirdFaceAttack3");
            texture = spriteTexture;
        }

        bool hasRecievedBeat;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (rhythmManager.songPlayer.IsOnQuarter() && !hasRecievedBeat)
            {
                hasRecievedBeat = true;
                moveEnemy();
            }
            if (!rhythmManager.songPlayer.IsOnQuarter() && hasRecievedBeat)
            {
                hasRecievedBeat = false;
            }
            changeTexture();
        }

        private void changeTexture()
        {
            if (bufferCount == moveBuffer)
            {
                spriteTexture = attacktexture;
            }
            else if (rhythmManager.songPlayer.IsOnQuarter())
            {
                spriteTexture = beatTexture;
            }
            else
            {
                spriteTexture = texture;
            }
        }

        int bufferCount;
        private bool checkIfMove()
        {
            if (bufferCount >= moveBuffer)
            {
                bufferCount = 0;
                return true;
            }
            else
            {
                bufferCount++;
            }

            return false;
        }

        private bool isTouchingPlayer()
        {
            if (this.gridPos == player.gridPos && this.Visible == true)
            {
                return true;
            }
            return false;
        }

        private void checkPlayerPos()
        {
            if (isTouchingPlayer())
            {
                player.State = PlayerState.Dead;
            }
        }

        private void moveEnemy()
        {
            checkPlayerPos();
            if (checkIfMove())
            {
                pathFindingMove();
                //moveState = MoveState.Move;
            }
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
