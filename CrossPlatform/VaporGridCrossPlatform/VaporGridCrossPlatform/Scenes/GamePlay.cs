using Microsoft.Xna.Framework;
using VaporGridCrossPlatform;
using VaporGridCrossPlatform.GridClasses;
using VaporGridCrossPlatform.UI;
using System.Collections.Generic;
using System;

namespace VaporGridCrossPlatform.Scenes
{
    public class GamePlay : Scene
    {
        protected GridManager gridManager;
        protected Player player;
        protected ArrowIndicators arrowIndicators;

        protected List<EnemySpawner> enemySpawners;
        protected EnemySpawner enemySpawner;
        protected SpittingEnemySpawner spittingEnemySpawner;
        protected PowerUpSpawner powerUpSpawner;
        protected Camera camera;
        protected FileReader fileReader;
        protected DynamicTileManager dynamicTileManager;
        protected PortalSpawner portalSpawner;

        ScoreUI display;
        GameplayUI rhythmDisplay;

        protected int level;

        MenuController input;

        Random rnd;
        public GamePlay(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm, int level) : base(game, manager, rm, sm)
        {
            this.level= level;
            enemySpawners = new List<EnemySpawner>();
            input= new MenuController();
            rnd = new Random();
        }

        public override void SetupScene()
        {
            setupGame();
            base.SetupScene();
        }

        protected virtual void setupGame()
        {
            fileReader = new FileReader();
            camera = new Camera(Game);
            Game.Components.Add(camera);

            gridManager = new GridManager(Game, camera, rm);
            
            Game.Components.Add(gridManager);

            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
            /*enemySpawner = new EnemySpawner(Game, gridManager, rm,  camera, player, 8);
            Game.Components.Add(enemySpawner);
            enemySpawners.Add(enemySpawner);*/
            spittingEnemySpawner = new SpittingEnemySpawner(Game, gridManager, rm, camera, player, 12);
            Game.Components.Add(spittingEnemySpawner);
            enemySpawners.Add(spittingEnemySpawner);
            powerUpSpawner = new PowerUpSpawner(Game, gridManager, rm, camera, player, 8);
            Game.Components.Add(powerUpSpawner);
            portalSpawner = new PortalSpawner(Game, gridManager,rm, camera, player, 1);
            Game.Components.Add(portalSpawner);


            arrowIndicators = new ArrowIndicators(Game, gridManager, rm,player, camera);
            Game.Components.Add(arrowIndicators);

            dynamicTileManager = new DynamicTileManager(Game, gridManager, player, enemySpawners);
            Game.Components.Add(dynamicTileManager);

            display = new ScoreUI(Game, scoreManager, 810);
            Game.Components.Add(display);

            rhythmDisplay = new GameplayUI(Game, player);
            Game.Components.Add(rhythmDisplay);

            addCompsToList();
        }

        protected virtual void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            //addComponentToScene(enemySpawner);
            addComponentToScene(spittingEnemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(portalSpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(dynamicTileManager);
            addComponentToScene(display);
            addComponentToScene(rhythmDisplay);
        }


        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            checkForExitInput();
            checkPlayerState();
            
        }

        private void checkForExitInput()
        {
            input.Update();
            if (input.PressedBack)
            {
                ResetGamePlay();
                arrowIndicators.UnLoad();
                sceneManager.ChangeScene(this, sceneManager.gameOver);
            }
        }

        public override void loadScene()
        {
            base.loadScene();
            ResetGamePlay();
            rm.state = SongState.reset;
            scoreManager.ShowScore = true;
            scoreManager.ShowHighScore = false;
            scoreManager.WriteEnabled = true;
        }

        protected virtual void ResetGamePlay()
        {
            player.ResetPlayer(new Vector2(rnd.Next(gridManager.GridWidth),rnd.Next(gridManager.GridHeight)));
            gridManager.GenerateNewGrid(player.gridPos);
            dynamicTileManager.AddTiles();
            
            foreach(Spawner s in enemySpawners)
            {
                s.spawnState = SpawnState.reset;
            }
            powerUpSpawner.spawnState = SpawnState.reset;
            //rm.state = SongState.reset;
        }
        protected virtual void checkPlayerState()
        {
            switch (player.State)
            {
                case PlayerState.Dead:
                    ResetGamePlay();
                    arrowIndicators.UnLoad();
                    sceneManager.ChangeScene(this, sceneManager.gameOver);
                    break;
                case PlayerState.Alive:
                    break;
            }
        }
    }
}
