using Microsoft.Xna.Framework;
using RhythmGameProto.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public enum SpawnState { reset, active, readyToSpawn, manualSpawner }
    public class Spawner : GameComponent, ISceneComponenet
    {
        protected RhythmManager rhythmManager;
        protected GridManager gridManager;
        protected Player player;
        protected Queue<GridSprite> objects;
        protected Random rnd;
        protected int numOfObjects;
        public int NumberOfObjects { get { return objects.Count; } }

        protected int spawnBuffer;
        protected Camera camera;

        public SpawnState spawnState;

        List<GridSprite> activeObjs;
        public Spawner(Game game, GridManager gm, RhythmManager rm,  Camera camera, Player p, int numberOfObjects) : base(game)
        {
            activeObjs= new List<GridSprite>();
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
            spawnObjectInRandomPos();
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
                    readyToSpawn();
                    break;
                case SpawnState.manualSpawner:
                    break;
            }
        }

        protected virtual void readyToSpawn()
        {
            for(int i = 0; i < rhythmManager.SongsComplete + 1; i++)
            {
                spawnObjectInRandomPos();
            }
        }

        Vector2 temp;
        protected void spawnObjectInRandomPos()
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

        public GridSprite SpawnObject(Vector2 pos)
        {
            if (objects.Count > 0)
            {
                if (objects.Peek().Enabled == false)
                {
                    GridSprite objToSpawn = objects.Dequeue();
                    objects.Enqueue(objToSpawn);
                    objToSpawn.gridPos = pos;
                    objToSpawn.SpriteState = SpriteState.Active;
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                    return objToSpawn;
                }
            }
            return null;
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
            Vector2 posToSpawn = new Vector2(rnd.Next(1, gridManager.GridWidth), rnd.Next(1, gridManager.GridHeight));
            return posToSpawn;
        }

        public virtual void ResetObjects()
        {
            foreach (GridSprite s in objects)
            {
                DeSpawn(s);
            }
        }

        public virtual void DeSpawn(GridSprite s)
        {
            s.SpriteState = SpriteState.Inactive;
            s.Enabled = false;
            s.Visible = false;
            if(s is Collectable)
            {
                ((Collectable)s).Collected = false;
            }
        }

        
        public List<GridSprite> getActiveObjs()
        {
            activeObjs.Clear();
            foreach(GridSprite s in objects)
            {
                if (s.Enabled)
                {
                    activeObjs.Add(s);
                }
            }
            return activeObjs;
        }

        public void Load()
        {
            Enabled = true;
        }

        public void UnLoad()
        {
            ResetObjects();
            Enabled = false;
        }
    }
}
