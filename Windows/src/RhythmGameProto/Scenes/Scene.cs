using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public enum SceneState { inactive, active, readyToClose, loading}

    public class Scene : GameComponent
    {

        protected RhythmManager rm;
        protected ScoreManager scoreManager;

        protected SceneManager sceneManager;
        public List<ISceneComponenet> sceneComponents;
        public SceneState State;
        public Scene(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) :base(game)
        {
            this.rm = rm;
            this.scoreManager = sm;
            sceneManager = manager;
            sceneComponents = new List<ISceneComponenet>();
        }
        public override void Initialize()
        {
            base.Initialize();
            SetupScene();
        }

        public void addComponentToScene(ISceneComponenet component)
        {
            sceneComponents.Add(component);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (State)
            {
                case SceneState.readyToClose:
                    closeScene();
                    State = SceneState.inactive;
                    break;
                case SceneState.loading:
                    loadScene();
                    State = SceneState.active;
                    break;
                case SceneState.active:
                    SceneUpdate();
                    break;
                case SceneState.inactive: 
                    break;
            }
        }

        protected virtual void SceneUpdate()
        {

        }

        public virtual void SetupScene()
        {
            closeScene();
        }

        public virtual void loadScene()
        {
            foreach (ISceneComponenet component in sceneComponents)
            {
                component.Load();
            }
        }

        public void closeScene()
        {
            foreach(ISceneComponenet component in sceneComponents)
            {
                component.UnLoad();
            }
        }

    }
}
