using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public enum SpriteState { Active, Inactive }
    public enum MoveState { Stay, Move}
    public class GridSprite : Sprite
    {
        public SpriteState SpriteState;
        protected GridManager gridManager;
        protected MonogameTile prevTile;
        protected MonogameTile currentTile;
        protected MoveState moveState;
        public Vector2 gridPos;
        public GridSprite(Game game, GridManager gm, string texturename, Camera camera) : base(game, texturename, camera)
        {
            gridManager = gm;
            moveState = MoveState.Stay;
        }

        public override void Initialize()
        {
            base.Initialize();
            updateCurrentTile();
        }

        public override void Update(GameTime gameTime)
        {
            //updateCurrentTile();
            checkMoveState();
            base.Update(gameTime);
        }

        protected void checkMoveState()
        {
            switch(moveState)
            {
                case MoveState.Stay:
                    updateCurrentTile();
                    break;
                case MoveState.Move:
                    if(prevTile == null)
                    {
                        prevTile = currentTile;
                    }
                    if(prevTile.State != TileState.neverwalkable)
                    {
                        prevTile.State = TileState.empty;
                    }
                    if(currentTile.State != TileState.neverwalkable)
                    {
                        currentTile.State = TileState.empty;
                    }
                    prevTile = currentTile;
                    updateCurrentTile();
                    if(currentTile.State != TileState.neverwalkable)
                    {
                        currentTile.State = TileState.occupied;
                    }
                    moveState = MoveState.Stay;
                    break;
            }
        }

        protected void updateCurrentTile()
        {
            currentTile = gridManager.Grid[(int)gridPos.X, (int)gridPos.Y];
            Position = currentTile.Position;
        }

        public void ResetTiles()
        {
            currentTile.State = TileState.empty;
            if(prevTile!= null)
            {
                prevTile.State = TileState.empty;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
