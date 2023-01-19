using Microsoft.Xna.Framework;
using RhythmGameProto.GridClasses;
using RhythmGameProto.MapManagerClasses;
using RhythmGameProto.Spawners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.Scenes
{
    public class Level : GamePlay
    {
        int curKeys;

        LevelLoader lvlLoader;
        
        Collectable exit;

        KeySpawner keySpawner;

        int numOfKeys;
        public Level(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm, int level) : base(game, manager, rm, sm, level)
        {
            lvlLoader = new LevelLoader();
        }

        protected override void setupGame()
        {
            fileReader = new FileReader();
            camera = new Camera(Game);
            Game.Components.Add(camera);

            gridManager = new GridManager(Game, camera, rm);
            if (level != 0)
            {
                gridManager.loadLevel(level);
            }
            Game.Components.Add(gridManager);

            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
            enemySpawner = new EnemySpawner(Game, gridManager, rm, player, camera, 8);
            Game.Components.Add(enemySpawner);
            enemySpawner.spawnState = SpawnState.inactive;
            powerUpSpawner = new PowerUpSpawner(Game, gridManager, rm, player, camera, 8);
            Game.Components.Add(powerUpSpawner);
            keySpawner = new KeySpawner(Game, gridManager, rm, player, camera, 3);
            keySpawner.spawnState = SpawnState.inactive;
            Game.Components.Add(keySpawner);
            powerUpSpawner.spawnState = SpawnState.inactive;
            arrowIndicators = new ArrowIndicators(Game, gridManager, player, camera);
            Game.Components.Add(arrowIndicators);

            dynamicTileManager = new DynamicTileManager(Game, gridManager, player, enemySpawner);
            Game.Components.Add(dynamicTileManager);

            addCompsToList();

        }

        protected override void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(enemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(keySpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(dynamicTileManager);
        }

        protected void spawnObjects()
        {
            int[,] objs = lvlLoader.levelList[level].objectInfo;
            for(int x = 0; x < objs.GetLength(0); x++)
            {
                for(int y =0; y < objs.GetLength(1); y++)
                {
                    
                    switch(objs[x, y])
                    {
                        default: break;
                        case 1:
                            enemySpawner.SpawnObject(new Vector2(x,y));
                            break;
                        case 2:
                            keySpawner.SpawnObject(new Vector2(x,y));
                            numOfKeys++;
                            break;
                        case 3:
                            exit = new Collectable(Game, gridManager, "ExitTutorial", camera, player);
                            exit.gridPos= new Vector2(x,y);
                            addComponentToScene(exit);
                            break;

                    }
                    
                }
            }
        }

        protected override void ResetGamePlay()
        {
            curKeys = 0;
            numOfKeys= 0;
            resetSpawners();
            dynamicTileManager.AddTiles();
            player.ResetPlayer(new Vector2(0, 1));
            spawnObjects();
            //rm.state = SongState.reset;
        }

        private void resetSpawners()
        {
            enemySpawner.ResetObjects();
            keySpawner.ResetObjects();
            powerUpSpawner.ResetObjects();
        }

        protected override void checkPlayerState()
        {
            switch (player.State)
            {
                case PlayerState.Dead:
                    ResetGamePlay();
                    arrowIndicators.UnLoad();
                    sceneManager.ChangeScene(this, sceneManager.gameOver);
                    break;
                case PlayerState.Alive:
                    checkWinCondition();
                    break;
            }
        }

        private void checkWinCondition()
        {
            foreach (Collectable key in keySpawner.Keys)
            {
                if (key.Collected)
                {
                    if (key.Enabled == true)
                    {
                        curKeys++;
                        key.Enabled = false;
                        key.Visible = false;
                    }
                }
            }
            if (curKeys >= numOfKeys)
            {
                exit.Enabled = true;
                exit.Visible = true;
            }
            else
            {
                exit.Enabled = false;
                exit.Visible = false;
            }
            if (exit.Collected)
            {
                win();
            }
        }

        private void win()
        {
            //load the win screen instead
            ResetGamePlay();
            arrowIndicators.UnLoad();
            sceneManager.ChangeScene(this, sceneManager.gameOver);
        }


    }
}
