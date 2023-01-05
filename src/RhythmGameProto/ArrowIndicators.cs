using MacksInterestingMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class ArrowIndicators : DrawableGameComponent, ISceneComponenet
    {
        GridSprite leftArrow;
        GridSprite rightArrow;
        GridSprite topArrow;
        GridSprite bottomArrow;
        List<GridSprite> arrows;
        Player player;
        GridManager gridManager;
        public ArrowIndicators(Game game, GridManager gm,Player player, Camera camera) : base(game)
        {
            gridManager = gm;
            this.player = player;
            arrows = new List<GridSprite>();
            rightArrow = new GridSprite(game, gm, "ArrowRightClear", camera);
            arrows.Add(rightArrow);
            leftArrow = new GridSprite(game, gm, "ArrowLeftClear", camera);
            arrows.Add(leftArrow);
            bottomArrow = new GridSprite(game, gm, "ArrowDownClear", camera);
            //had to flip top and bottom dont know why dont change
            topArrow = new GridSprite(game, gm, "ArrowUpClear", camera);
            arrows.Add(topArrow);
            arrows.Add(bottomArrow);
            
        }

        public override void Initialize()
        {
            //setting up arrows in initialize so they draw over everything else
            setupArrows();
            base.Initialize();
        }
        private void setupArrows()
        {
            foreach(var arrow in arrows)
            {
                Game.Components.Add(arrow);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            setPositions();
        }
        public void setPositions()
        {
            checkKeys();
        }
        private void right(GridSprite arrow)
        {
            if (player.gridPos.X + 1! < gridManager.gridWidth)
            {
                if (gridManager.Grid[(int)player.gridPos.X + 1, (int)player.gridPos.Y].IsWalkable)
                {
                    arrow.gridPos = new Vector2(player.gridPos.X + 1, player.gridPos.Y);
                    arrow.Visible = true;
                }
                else
                {
                    arrow.Visible = false;
                }
            }
            else
            {
                arrow.Visible = false;
            }
        }
        private void left(GridSprite arrow)
        {
            if (player.gridPos.X - 1! > -1)
            {
                if (gridManager.Grid[(int)player.gridPos.X - 1, (int)player.gridPos.Y].IsWalkable)
                {
                    arrow.gridPos = new Vector2(player.gridPos.X - 1, player.gridPos.Y);
                    arrow.Visible = true;
                }
                else
                {
                    arrow.Visible = false;
                }
            }
            else
            {
                arrow.Visible = false;
            }
        }
        private void down(GridSprite arrow)
        {
            if (player.gridPos.Y + 1! < gridManager.gridHeight)
            {
                if (gridManager.Grid[(int)player.gridPos.X, (int)player.gridPos.Y + 1].IsWalkable)
                {
                    arrow.gridPos = new Vector2(player.gridPos.X, player.gridPos.Y + 1);
                    arrow.Visible = true;
                }
                else
                {
                    arrow.Visible = false;
                }
            }
            else
            {
                arrow.Visible = false;
            }
        }
        private void up(GridSprite arrow)
        {
            if (player.gridPos.Y - 1! > -1)
            {
                if (gridManager.Grid[(int)player.gridPos.X, (int)player.gridPos.Y - 1].IsWalkable)
                {
                    arrow.gridPos = new Vector2(player.gridPos.X, player.gridPos.Y - 1);
                    arrow.Visible = true;
                }
                else
                {
                    arrow.Visible = false;
                }
            }
            else
            {
                arrow.Visible = false;
            }
        }
        //right left down up
        private void checkKeys()
        {
             right(arrows[player.Controller.Indicies[0]]);
             left(arrows[player.Controller.Indicies[1]]);
             down(arrows[player.Controller.Indicies[2]]);
             up(arrows[player.Controller.Indicies[3]]);
        }

        public void Load()
        {
            foreach(Sprite arrow in arrows)
            {
                arrow.Load();
            }
        }

        public void UnLoad()
        {
            foreach(Sprite arrow in arrows)
            {
                arrow.UnLoad();
            }
        }
    }
}
