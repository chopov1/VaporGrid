using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.GridClasses
{
    public class DynamicTileManager : GameComponent, ISceneComponenet
    {
        List<TrapTile> traps;
        List<DoorTile> doors;
        Player player;
        List<EnemySpawner> enemySpawners;
        GridManager gm;
        public DynamicTileManager(Game game, GridManager gm ,Player p, List<EnemySpawner> enemies) : base(game)
        {
            this.gm = gm;
            traps = new List<TrapTile>();
            doors= new List<DoorTile>();
            player= p;
            enemySpawners = enemies;
        }

        public void AddTiles()
        {
            addTraps();
            addDoors();
        }

        private void addTraps()
        {
            traps.Clear();
            foreach(MonogameTile t in gm.Grid)
            {
                if(t.tile is TrapTile)
                {
                    traps.Add((TrapTile)t.tile);
                }
            }
        }
        private void addDoors()
        {
            doors.Clear();
            foreach (MonogameTile t in gm.Grid)
            {
                if (t.tile is DoorTile)
                {
                    doors.Add((DoorTile)t.tile);
                }
            }
        }
        private void updateTraps()
        {
            foreach(TrapTile tile in traps)
            {
                switch (tile.State)
                {
                    case TrapState.active:
                        if (tile.IsUnderObject(player.gridPos))
                        {
                            player.State = PlayerState.Dead;
                        }
                        foreach(EnemySpawner es in enemySpawners)
                        {
                            foreach (PathfindingEnemy e in es.getActiveObjs())
                            {
                                if (tile.IsUnderObject(e.gridPos))
                                {
                                    es.DeSpawn(e);
                                }
                            }
                        }
                        gm.NodeGrid[(int)tile.tileGridPos.X, (int)tile.tileGridPos.Y].iswalkable = false;
                        break;
                    case TrapState.inactive:
                        if (tile.IsUnderObject(player.gridPos) && !(tile is AutoTrapTile))
                        {
                            tile.State = TrapState.activate;
                        }
                        gm.NodeGrid[(int)tile.tileGridPos.X, (int)tile.tileGridPos.Y].iswalkable = true;
                        break;
                }
            }
        }

        private void updateDoors()
        {
            foreach(DoorTile b in doors)
            {
                b.checkIfActivated(player.gridPos);
                gm.NodeGrid[(int)b.tileGridPos.X, (int)b.tileGridPos.Y].iswalkable = b.IsWalkable;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateTraps();
            updateDoors();
        }

        public void Load()
        {
            Enabled = true;
        }

        public void UnLoad()
        {
            Enabled= false;
        }
    }
}
