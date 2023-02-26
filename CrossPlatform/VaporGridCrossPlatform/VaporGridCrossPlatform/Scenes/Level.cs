using Microsoft.Xna.Framework;
using VaporGridCrossPlatform.GridClasses;
using VaporGridCrossPlatform.MapManagerClasses;
using VaporGridCrossPlatform.Spawners;
using VaporGridCrossPlatform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Scenes
{
    public class Level : GamePlay
    {
        int curKeys;

        LevelLoader lvlLoader;
        
        Collectable exit;

        KeySpawner keySpawner;

        int numOfKeys;

        public int LevelNumber { get; private set; }
        public Level(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm, int level) : base(game, manager, rm, sm, level)
        {
            lvlLoader = new LevelLoader();
            LevelNumber = level;
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

            enemySpawner = new EnemySpawner(Game, gridManager, rm,  camera, player, 8, 32);
            Game.Components.Add(enemySpawner);
            enemySpawner.spawnState = SpawnState.manualSpawner;
            enemySpawners.Add(enemySpawner);

            spittingEnemySpawner = new SpittingEnemySpawner(Game, gridManager, rm, camera, player, 8, 32);
            Game.Components.Add(spittingEnemySpawner);
            spittingEnemySpawner.spawnState = SpawnState.manualSpawner;
            enemySpawners.Add(spittingEnemySpawner);

            powerUpSpawner = new PowerUpSpawner(Game, gridManager, rm, camera, player, 8, 16);
            Game.Components.Add(powerUpSpawner);
            powerUpSpawner.spawnState = SpawnState.manualSpawner;

            keySpawner = new KeySpawner(Game, gridManager, rm,  camera, player, 8, 32);
            Game.Components.Add(keySpawner);
            keySpawner.spawnState = SpawnState.manualSpawner;

            arrowIndicators = new ArrowIndicators(Game, gridManager, rm,player, camera);
            Game.Components.Add(arrowIndicators);

            dynamicTileManager = new DynamicTileManager(Game, gridManager, player, enemySpawners);
            Game.Components.Add(dynamicTileManager);

            exit = new Collectable(Game, gridManager, "ExitTutorial", camera, player);

            addCompsToList();

        }

        protected override void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(enemySpawner);
            addComponentToScene(spittingEnemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(keySpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(dynamicTileManager);
            addComponentToScene(exit);

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
                            exit.gridPos= new Vector2(x,y);
                            exit.Enabled = false;
                            exit.Visible= false;
                            break;
                        case 4:
                            spittingEnemySpawner.SpawnObject(new Vector2(x, y));
                            break;

                    }
                    
                }
            }
        }

        protected override void ResetGamePlay()
        {
            numOfKeys = 0;
            curKeys = 0;
            resetSpawners();
            dynamicTileManager.AddTiles();
            player.ResetPlayer(new Vector2(0, 1));
            spawnObjects();
            //rm.state = SongState.reset;
        }

        private void resetSpawners()
        {
            enemySpawner.ResetObjects();
            spittingEnemySpawner.ResetObjects();
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
                    lose();
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
                        scoreManager.PlayScoreSFX();
                        key.Enabled = false;
                        key.Visible = false;
                    }
                }
            }
            if (curKeys >= numOfKeys)
            {
                if (!exit.Enabled)
                {
                    exit.Enabled = true;
                    exit.Visible = true;
                }
            }
            else if(exit.Enabled)
            {
                exit.Enabled = false;
                exit.Visible = false;
            }
            if (exit.Collected)
            {
                win();
            }
        }

        private void lose()
        {
            sceneManager.levelEndScreen.SetDisplayType(false);
            sceneManager.ChangeScene(this, sceneManager.levelEndScreen);
        }
        private void win()
        {
            sceneManager.levelEndScreen.SetDisplayType(true);
            scoreManager.SaveMoves(LevelNumber);
            sceneManager.levelEndScreen.SetLevelNum(LevelNumber);
            //load the win screen instead
            ResetGamePlay();
            exit.Collected = false;
            arrowIndicators.UnLoad();
            sceneManager.ChangeScene(this, sceneManager.levelEndScreen);
        }


    }
}
