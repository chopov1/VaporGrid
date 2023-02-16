using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    //change name of class later
    public class Enemy : GridSprite
    {
        protected RhythmManager rhythmManager;
        protected Player player;

        protected int moveBuffer;
        protected int bufferCount;

        protected bool hasRecievedBeat;

        protected Texture2D offBeatTexture;
        protected Texture2D onBeatTexture;
        protected Texture2D attackTexture;
        public Enemy(Game game, GridManager gm, string textureName ,RhythmManager rm, Camera camera, Player p) : base(game, gm, textureName, camera)
        {
            rhythmManager = rm;
            player = p;
            Game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            /*onBeatTexture = Game.Content.Load<Texture2D>("");
            attackTexture = Game.Content.Load<Texture2D>("");*/
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
            if (rhythmManager.songPlayer.IsOnQuarter() && !hasRecievedBeat)
            {
                hasRecievedBeat = true;
                checkIfMove();
            }
            if (!rhythmManager.songPlayer.IsOnQuarter() && hasRecievedBeat)
            {
                hasRecievedBeat = false;
            }
        }

        protected virtual void changeTexture()
        {
            if (bufferCount == moveBuffer)
            {
                spriteTexture = attackTexture;
            }
            else if (rhythmManager.songPlayer.IsOnQuarter())
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
