using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using VaporGridCrossPlatform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class ArrowIndicators : DrawableGameComponent, ISceneComponenet
    {
        Arrow leftArrow;
        Arrow rightArrow;
        Arrow topArrow;
        Arrow bottomArrow;
        List<Arrow> arrows;
        Player player;
        GridManager gridManager;
        RhythmManager rm;
        public ArrowIndicators(Game game, GridManager gm, RhythmManager rm,Player player, Camera camera) : base(game)
        {
            this.rm= rm;
            gridManager = gm;
            this.player = player;
            arrows = new List<Arrow>();
            rightArrow = new Arrow(game, gm, rm,"Arrows/ArrowUpReg4", "Arrows/ArrowsError4", "Arrows/ArrowsOnBeat4",camera, player);
            arrows.Add(rightArrow);
            leftArrow = new Arrow(game, gm, rm,"Arrows/ArrowUpReg3", "Arrows/ArrowsError3", "Arrows/ArrowsOnBeat3", camera, player);
            arrows.Add(leftArrow);
            bottomArrow = new Arrow(game, gm, rm, "Arrows/ArrowUpReg2", "Arrows/ArrowsError2", "Arrows/ArrowsOnBeat2", camera, player);
            //had to flip top and bottom dont know why dont change
            topArrow = new Arrow(game, gm, rm, "Arrows/ArrowUpReg1", "Arrows/ArrowsError1","Arrows/ArrowsOnBeat1" ,camera, player);
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
            foreach (var arrow in arrows)
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
        private void right(Arrow arrow)
        {
            if (player.gridPos.X + 1! < gridManager.GridWidth)
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
        private void left(Arrow arrow)
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
        private void down(Arrow arrow)
        {
            if (player.gridPos.Y + 1! < gridManager.GridHeight)
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
        private void up(Arrow arrow)
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
            foreach (Sprite arrow in arrows)
            {
                arrow.Load();
            }
            Enabled = true;
            Visible = true;
        }

        public void UnLoad()
        {
            foreach (Sprite arrow in arrows)
            {
                arrow.UnLoad();
            }
            Enabled = false;
            Visible = false;
        }

    }
}
