﻿using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;

namespace SacreBleu
{
	public class Camera
	{
		public static Camera _instance;
		public Matrix Transform { get; private set; }
		public Vector2 _position { get; set; }

		Viewport viewPort;
		float Ymovement;
		KeyboardState keyboardState, previousKeyboardState;

		public bool normalMap;


		public Camera(Viewport vp)
		{
			_instance = this;
			viewPort = vp;
			normalMap = false;
		}
		
		public void Update( Vector2 targetPosition)
		{
			keyboardState = Keyboard.GetState();
			normalMap = false;
			if (keyboardState.IsKeyDown(Keys.W) )
			{
				Ymovement = -10f;
				normalMap = true;
			}

			if (keyboardState.IsKeyDown(Keys.S) )
			{
				Ymovement = 10f;
				normalMap = true;
			}
			
			if (normalMap)
			{
				_position = new Vector2(_position.X, _position.Y + Ymovement);
			}
			if (!normalMap)
			{
				_position = new Vector2(targetPosition.X, targetPosition.Y);
				
			}

			Vector2 clampMaxforCamera = new Vector2(viewPort.Width,1650f - viewPort.Height);

			Vector2 clampMinforCamera = new Vector2(viewPort.Width, viewPort.Height - viewPort.Height / 2);

			_position =Vector2.Clamp(_position, clampMinforCamera, clampMaxforCamera);
			var translation = Matrix.CreateTranslation(
			  -SacreBleuGame._instance._screenWidth / 2,
			 -_position.Y ,
			  0);

			var offset = Matrix.CreateTranslation(
				viewPort.Bounds.Width / 2,
				viewPort.Bounds.Height / 2,
				0);
			Transform = Matrix.Lerp(offset,translation * offset, 1f);
			//Transform = translation * offset;
								
			previousKeyboardState = keyboardState;
		}

		
	}
}