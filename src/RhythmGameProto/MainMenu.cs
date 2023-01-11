using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameProto.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class MainMenu : Scene
    {
        MenuController input;
        MenuUI display;
        public MainMenu(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            input = new MenuController();
            display = new MainMenuUI(game);
            Game.Components.Add(display);
            addComponentToScene(display);
        }

        protected override void SceneUpdate()
        {
            input.Update();
            base.SceneUpdate();
            if (input.PressedEnter)
            {
                sceneManager.ChangeScene(this, sceneManager.gamePlay);
            }
            if (input.PressedT)
            {
                sceneManager.ChangeScene(this, sceneManager.tutorial);
            }
        }



    }
}
