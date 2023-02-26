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

        protected List<Texture2D> enemyTextures;

        public Enemy(Game game, GridManager gm, string textureName ,RhythmManager rm, Camera camera, Player p) : base(game, gm, rm,textureName, camera)
        {
            player = p;
            Game.Components.Add(this);
            enemyTextures= new List<Texture2D>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            for(int i =1; i <= 6; i++)
            {
                enemyTextures.Add(Game.Content.Load<Texture2D>("enemies/SaucerEnemy" + i.ToString())); 
            }
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
            if (bufferCount >= moveBuffer)
            {
                if(rhythmManager.RhythmState == RhythmState.Quarter)
                {
                    spriteTexture = enemyTextures[4];
                }
                else if(rhythmManager.RhythmState == RhythmState.Sixteenth)
                {
                    spriteTexture = enemyTextures[4];
                }
                else
                {
                    spriteTexture = enemyTextures[5];
                }
            }
            else
            {
                if (rhythmManager.RhythmState == RhythmState.Quarter)
                {
                    spriteTexture = enemyTextures[0];
                }
                else if (rhythmManager.RhythmState == RhythmState.Sixteenth)
                {
                    spriteTexture = enemyTextures[2];
                }
                else
                {
                    spriteTexture = enemyTextures[1];
                }
            }
        }

        private void setPlayerState()
        {
            if (isTouchingPlayer())
            {
                player.State = PlayerState.Dead;
                player.SetDeathMessage(0);
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
