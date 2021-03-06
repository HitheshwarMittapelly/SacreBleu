﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.Managers;
using System;
using System.Diagnostics;

namespace SacreBleu.GameObjects
{
    class PowerBar : GameObject
    {
        public bool startBar;

        Vector2 direction;
        Texture2D powerBarTexture, powerGaugeTexture;
        float powerBarHeight;
        float powerMagnitude;
        int heightFactor;
        float minPower;

        public PowerBar(Vector2 position, Texture2D healthBar, Texture2D healthGauge) : base(position, healthBar)
        {
            powerBarTexture = healthBar;
            powerGaugeTexture = healthGauge;
            powerBarHeight = powerBarTexture.Height;
            heightFactor = -1;
            _tag = "power bar";
            startBar = false;
            minPower = 0.01f;
        }

        public void Update(GameTime gameTime)
        {

            if (startBar)
            {
                powerBarHeight += heightFactor;

                if ((powerBarHeight > (powerBarTexture.Height)) || (powerBarHeight < 0))
                    heightFactor = -heightFactor;
            }
        }

        public void setpower()
        {

            powerMagnitude = (float)Math.Round((1 - Math.Abs(powerBarHeight / _sprite.Height)), 1);
            direction = Vector2.Zero;
            LevelManager._instance.currentLevel._frog.SetVelocity(powerMagnitude * direction * LevelManager._instance.currentLevel._frog._maxVelocity);

            float angle = LevelManager._instance.currentLevel._directionGauge._angle;
            float degrees = MathHelper.ToDegrees(angle);

            degrees = 90 - degrees;

            float tanValue;
            float radians = MathHelper.ToRadians(degrees);
            tanValue = (float)Math.Tan(radians);

            if (degrees < 90)
            {
                direction.X = (float)Math.Sqrt(1 / 1 + Math.Pow(tanValue, 2));
                direction.Y = -direction.X * tanValue;
            }
            if (degrees > 90)
            {
                direction.X = -(float)Math.Sqrt(1 / 1 + Math.Pow(tanValue, 2));
                direction.Y = -direction.X * tanValue;
            }
            if (degrees == 90)
            {
                direction = new Vector2(0, -1);
            }

            direction.Normalize();
            if (powerMagnitude == 0f)
                powerMagnitude = minPower;

            LevelManager._instance.currentLevel._frog.SetVelocity(powerMagnitude * direction * LevelManager._instance.currentLevel._frog._maxVelocity);
            if (powerMagnitude != 0)
            {
                LevelManager._instance.currentLevel.numberOfHits++;
               
            }
            powerMagnitude = 0f;
            powerBarHeight = _sprite.Height;
        }
        public override void Draw()
        {
            SacreBleuGame._instance._spriteBatch.Draw(powerGaugeTexture, new Vector2(_position.X, _position.Y - _sprite.Height / 2), new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height), Color.White);
            SacreBleuGame._instance._spriteBatch.Draw(powerBarTexture, new Vector2(_position.X, _position.Y - _sprite.Height / 2), new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, (int)powerBarHeight), Color.DarkBlue);
        }

    }
}
