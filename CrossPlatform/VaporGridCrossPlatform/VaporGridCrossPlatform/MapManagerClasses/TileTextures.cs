using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VaporGridCrossPlatform.GridClasses
{
    public class TileTextures : DrawableGameComponent
    {
        public Texture2D walkableOnB;
        public Texture2D walkableOffB;

        public Texture2D unWalkableOnB;
        public Texture2D unWalkableOffB;

        public Texture2D trapActivating;
        public List<Texture2D> trapActivatingAnim;
        public List<Texture2D> trapActiveAnim;
        public List<Texture2D> doorOpenAnim;
        public Texture2D trapInactive;

        public Texture2D DoorReady;
        public Texture2D DoorCooldown;
        public Texture2D DoorBasic;

        public Vector2 tileSpriteSize { get { return new Vector2(unWalkableOffB.Width, unWalkableOffB.Height); } }

        public TileTextures(Game game) : base(game)
        {
            Game.Components.Add(this);
            trapActivatingAnim= new List<Texture2D>();
            trapActiveAnim= new List<Texture2D>();
            doorOpenAnim= new List<Texture2D>();
        }

        protected override void LoadContent()
        {
            LoadTileTextures();
            base.LoadContent();
        }
        public void LoadTileTextures()
        {
            trapActivatingAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActivatingAnim/TrapTileInactive3"));
            trapActivatingAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActivatingAnim/TrapTileInactive4"));
            trapActivatingAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActivatingAnim/TrapTileInactive5"));
            trapActivatingAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActivatingAnim/TrapTileInactive6"));
            trapActiveAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActiveAnim/TrapTileInactive7"));
            trapActiveAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActiveAnim/TrapTileInactive8"));
            trapActiveAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActiveAnim/TrapTileInactive9"));
            trapActiveAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActiveAnim/TrapTileInactive10"));
            trapActiveAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActiveAnim/TrapTileInactive11"));
            trapActiveAnim.Add(Game.Content.Load<Texture2D>("Tiles/ActiveAnim/TrapTileInactive12"));

            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite3"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite4"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite5"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite6"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite7"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite8"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite9"));
            doorOpenAnim.Add(Game.Content.Load<Texture2D>("Tiles/DoorAnim/DoorSprite10"));

            walkableOnB = Game.Content.Load<Texture2D>("Tiles/WalkableOnBeat");
            walkableOffB = Game.Content.Load<Texture2D>("Tiles/WalkableOffBeat");

            unWalkableOnB = Game.Content.Load<Texture2D>("Tiles/UnwalkableOnBeat");
            unWalkableOffB = Game.Content.Load<Texture2D>("Tiles/UnwalkableOffBeat");

            trapActivating = Game.Content.Load<Texture2D>("Tiles/TrapTileActivating");
            trapInactive = Game.Content.Load<Texture2D>("Tiles/TrapTileInactive");

            DoorReady = Game.Content.Load<Texture2D>("Tiles/DoorTileReady");
            DoorBasic = Game.Content.Load<Texture2D>("Tiles/DoorTile");
            DoorCooldown = Game.Content.Load<Texture2D>("Tiles/DoorTileCooldown");
        }
    }
}
