using Microsoft.Xna.Framework;
using RhythmGameProto;
using RhythmGameProto.GridClasses;
using RhythmGameProto.UI;
using System.Collections.Generic;

namespace RhythmGameProto.Scenes
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

        ScoreUI display;

        protected int level;
        public GamePlay(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm, int level) : base(game, manager, rm, sm)
        {
            this.level= level;
            enemySpawners = new List<EnemySpawner>();
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
            arrowIndicators = new ArrowIndicators(Game, gridManager, player, camera);
            Game.Components.Add(arrowIndicators);

            dynamicTileManager = new DynamicTileManager(Game, gridManager, player, enemySpawners);
            Game.Components.Add(dynamicTileManager);

            display = new ScoreUI(Game, scoreManager);
            Game.Components.Add(display);

            addCompsToList();
        }

        protected virtual void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            //addComponentToScene(enemySpawner);
            addComponentToScene(spittingEnemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(dynamicTileManager);
            addComponentToScene(display);
        }


        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            checkPlayerState();
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
            gridManager.RandomGrid(player.gridPos);
            dynamicTileManager.AddTiles();
            player.ResetPlayer(new Vector2(0, 1));
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
