using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.GridClasses
{
    public class DynamicTileManager : GameComponent, ISceneComponenet
    {
        List<TrapTile> traps;
        List<BreakableTile> breakables;
        Player player;
        EnemySpawner enemySpawner;
        GridManager gm;
        public DynamicTileManager(Game game, GridManager gm ,Player p, EnemySpawner enemies) : base(game)
        {
            this.gm = gm;
            traps = new List<TrapTile>();
            breakables= new List<BreakableTile>();
            player= p;
            enemySpawner = enemies;
        }

        public void AddTiles()
        {
            addTraps();
            addBreakables();
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
        private void addBreakables()
        {
            breakables.Clear();
            foreach (MonogameTile t in gm.Grid)
            {
                if (t.tile is BreakableTile)
                {
                    breakables.Add((BreakableTile)t.tile);
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
                        foreach(Enemy e in enemySpawner.getActiveObjs())
                        {
                            if (tile.IsUnderObject(e.gridPos))
                            {
                                enemySpawner.DeSpawn(e);
                            }
                        }
                        break;
                    case TrapState.inactive:
                        if (tile.IsUnderObject(player.gridPos) && !(tile is AutoTrapTile))
                        {
                            tile.State = TrapState.activate;
                        }
                        break;
                }
            }
        }

        private void updateBreakables()
        {
            foreach(BreakableTile b in breakables)
            {
                b.checkIfBroken(player.gridPos);
                gm.NodeGrid[(int)b.tileGridPos.X, (int)b.tileGridPos.Y].iswalkable = b.IsWalkable;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateTraps();
            updateBreakables();
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
