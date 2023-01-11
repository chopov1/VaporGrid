using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public enum SpawnState { reset, active, readyToSpawn, inactive }
    public class Spawner : GameComponent, ISceneComponenet
    {
        protected RhythmManager rhythmManager;
        protected GridManager gridManager;
        protected Player player;
        protected Queue<GridSprite> objects;
        protected Random rnd;
        protected int numOfObjects;
        protected int spawnBuffer;
        protected Camera camera;

        public SpawnState spawnState;
        public Spawner(Game game, GridManager gm, RhythmManager rm, Player p, Camera camera,int numberOfObjects) : base(game)
        {
            objects = new Queue<GridSprite>();
            this.camera = camera;
            gridManager = gm;
            rhythmManager = rm;
            player = p;
            numOfObjects = numberOfObjects;
            spawnBuffer = 16;
            rnd = new Random();
            spawnState = SpawnState.reset;
        }

        public override void Initialize()
        {
            base.Initialize();
            objects = createObjects(numOfObjects);
            SpawnObject();
        }

        public virtual GridSprite createSpawnableObject()
        {
            return null;
        }

        public Queue<GridSprite> createObjects(int size)
        {
            Queue<GridSprite> objs = new Queue<GridSprite>();
            for (int i = 0; i < size; i++)
            {
                GridSprite sprite = createSpawnableObject();
                objs.Enqueue(sprite);
            }
            return objs;
        }

        bool hasRecievedBeat;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (spawnState)
            {
                case SpawnState.reset:
                    ResetObjects();
                    BufferCount = 0;
                    spawnState = SpawnState.active;
                    //SpawnObject();
                    break;
                case SpawnState.active:
                    if (rhythmManager.songPlayer.IsOnQuarter() && !hasRecievedBeat)
                    {
                        spawnState = SpawnState.readyToSpawn;
                        hasRecievedBeat = true;
                    }
                    if (!rhythmManager.songPlayer.IsOnQuarter())
                    {
                        hasRecievedBeat = false;
                    }
                    break;
                case SpawnState.readyToSpawn:
                    spawnState = SpawnState.active;
                    SpawnObject();
                    break;
            }
        }

        Vector2 temp;
        private void SpawnObject()
        {
            if (checkSpawnBuffer() && objects.Count > 0)
            {
                if (objects.Peek().Enabled == false)
                {
                    GridSprite objToSpawn = objects.Dequeue();
                    objects.Enqueue(objToSpawn);
                    temp = getRandomPos();
                    while (!canSpawn(temp))
                    {
                        temp = getRandomPos();
                    }
                    objToSpawn.gridPos = temp;
                    objToSpawn.SpriteState = SpriteState.Active;
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                }
            }
        }

        private bool canSpawn(Vector2 posToSpawn)
        {
            if (!gridManager.Grid[(int)posToSpawn.X, (int)posToSpawn.Y].IsWalkable)
            {
                return false;
            }
            if (!awayFromPlayer(posToSpawn))
            {
                return false;
            }
            return true;
        }

        private bool awayFromPlayer(Vector2 posToSpawn)
        {
            if (posToSpawn == player.gridPos)
            {
                return false;
            }
            if (posToSpawn.X < player.gridPos.X - 2 || posToSpawn.X > player.gridPos.X + 2 || posToSpawn.Y < player.gridPos.Y - 2 || posToSpawn.Y > player.gridPos.Y + 2)
            {
                return true;
            }
            return false;
        }

        public int BufferCount;
        private bool checkSpawnBuffer()
        {
            if (BufferCount >= spawnBuffer)
            {
                BufferCount = 0;
                return true;
            }
            else
            {
                BufferCount++;
            }
            return false;
        }

        private Vector2 getRandomPos()
        {
            Vector2 posToSpawn = new Vector2(rnd.Next(1, gridManager.gridWidth), rnd.Next(1, gridManager.gridHeight));
            return posToSpawn;
        }

        public void ResetObjects()
        {
            foreach (GridSprite s in objects)
            {
                s.SpriteState = SpriteState.Inactive;
                s.ResetTiles();
                s.Enabled = false;
                s.Visible = false;
            }
        }

        public void DeSpawn(GridSprite s)
        {
            s.SpriteState = SpriteState.Inactive;
            s.Enabled = false;
            s.Visible = false;
        }

        public void Load()
        {
            Enabled= true;
        }

        public void UnLoad()
        {
            ResetObjects();
            Enabled = false;
        }
    }
}
