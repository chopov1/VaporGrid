using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class Projectile : Enemy
    {
        public Vector2 Dir;
        public Projectile(Game game, GridManager gm, string textureName, RhythmManager rm, Camera camera, Player p) : base(game, gm, textureName, rm, camera, p)
        {
        }
        protected override void moveEnemy()
        {
            if (canMoveForward())
            {
                
                gridPos += Dir;
            }
            else
            {
                rotation = 0;
                Enabled= false;
                Visible= false;
            }
        }

        private void setRotation()
        {
            switch(Dir)
            {
            
                case Vector2(0, 1):
                    rotation = MathHelper.ToRadians(0);
                    break;
                case Vector2(0, -1):
                    rotation = MathHelper.ToRadians(180);
                    break;
                case Vector2(1, 0):
                    rotation = MathHelper.ToRadians(-90);
                    break;
                case Vector2(-1, 0):
                    rotation = MathHelper.ToRadians(90);
                    break;
            }
        }

        protected override void changeTexture()
        {
            setRotation();
        }

        protected bool canMoveForward()
        {
            if(gridManager.Grid[(int)(gridPos.X), (int)(gridPos.Y)].IsWalkable)
            {
                if (gridPos.X + Dir.X < gridManager.GridWidth && gridPos.X + Dir.X >= 0)
                {
                    if (gridPos.Y + Dir.Y < gridManager.GridHeight && gridPos.Y + Dir.Y >= 0)
                    {
                        if (gridManager.Grid[(int)(gridPos.X + Dir.X), (int)(gridPos.Y + Dir.Y)].IsWalkable)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}
