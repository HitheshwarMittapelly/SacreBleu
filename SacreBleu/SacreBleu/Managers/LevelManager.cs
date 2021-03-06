﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Levels;
using System.Collections.Generic;

namespace SacreBleu.Managers
{
    class LevelManager
    {
        public static LevelManager _instance;

        public List<BaseLevel> levels;
        private int levelIndex;
        public BaseLevel currentLevel;
        public TitleScreen titleScreen;

        private bool keyPressed;

        public LevelManager()
        {
            _instance = this;
            titleScreen = new TitleScreen();
            levels = new List<BaseLevel>
            {
                new PrototypeDemo()
        };

            currentLevel = titleScreen;
        }

        public void Update(GameTime gameTime)
        {
            if (GameManager._instance._currentState == GameManager.GameStates.IDLE)
                titleScreen.Update(gameTime);
            currentLevel.Update(gameTime);

            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Left) && levelIndex > 0 && !keyPressed)
            {
                keyPressed = true;

                levelIndex--;
                ChangeLevel(levels[levelIndex]);
            }
            else if (kstate.IsKeyDown(Keys.Right) && !keyPressed)
            {
                keyPressed = true;
                GoToNextLevel();
            }
            else if (kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && keyPressed)
                keyPressed = false;
        }


        private void ChangeLevel(BaseLevel newLevel)
        {
            currentLevel = newLevel;
        }

        public void GoToFirstLevel()
        {
            ChangeLevel(levels[levelIndex]);

        }

        public void GoToNextLevel()
        {
            if (levelIndex < levels.Count - 1)
            {
                levelIndex++;
                ChangeLevel(levels[levelIndex]);
            }
        }
    }
}
