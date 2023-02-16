using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    //change name of class later
    public class Enemy : RhythmSprite
    {
        protected Player player;

        protected int moveBuffer;
        protected int bufferCount;

        protected bool hasRecievedBeat;

        protected Texture2D offBeatTexture;
        protected Texture2D onBeatTexture;
        protected Texture2D attackTexture;

        public Enemy(Game game, GridManager gm, string textureName ,RhythmManager rm, Camera camera, Player p) : base(game, gm, rm,textureName, camera)
        {
            player = p;
            Game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            offBeatTexture = spriteTexture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkBeatForMove();
            changeTexture();
        }

        protected virtual void checkBeatForMove()
        {
            switch (rhythmManager.RhythmState)
            {
                case RhythmState.Quarter:
                    if(!hasRecievedBeat)
                    {
                        hasRecievedBeat = true;
                        checkIfMove();
                    }
                    break;
                default:
                    hasRecievedBeat = false;
                    break;
            }
        }

        protected virtual void changeTexture()
        {
            if (bufferCount == moveBuffer)
            {
                spriteTexture = attackTexture;
            }
            else if (rhythmManager.RhythmState == RhythmState.Quarter)
            {
                spriteTexture = onBeatTexture;
            }
            else
            {
                spriteTexture = offBeatTexture;
            }
        }

        private void setPlayerState()
        {
            if (isTouchingPlayer())
            {
                player.State = PlayerState.Dead;
            }
        }
        private bool isTouchingPlayer()
        {
            if (this.gridPos == player.gridPos && this.Visible == true)
            {
                return true;
            }
            return false;
        }

        protected void checkIfMove()
        {
            setPlayerState();
            if (moveBufferFilled())
            {
                moveEnemy();
            }
        }

        protected virtual void moveEnemy()
        {

        }

        
        private bool moveBufferFilled()
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
    }
}
