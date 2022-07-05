﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Collections.Generic;

namespace PongGame
{
    class GameScene : Scene, IScene
    {
        Matrix4 projectionMatrix;

        PlayerPaddle paddlePlayer;
        AIPaddle paddleAI;
        Ball ball;
        int scorePlayer = 9;
        int scoreAI = 0;
        double gameTime = 2;

        public GameScene(SceneManager sceneManager) 
            : base(sceneManager)
        {
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class
            sceneManager.Keyboard.KeyDown += Keyboard_KeyDown;

            ResetGame();

            GL.ClearColor(Color.Black);
        }

        private void ResetGame()
        {
            paddlePlayer = new PlayerPaddle(40, (int)(SceneManager.WindowHeight * 0.5));
            paddlePlayer.Init();
            paddleAI = new AIPaddle(SceneManager.WindowWidth - 40, (int)(SceneManager.WindowHeight * 0.5));
            paddleAI.Init();
            ball = new Ball((int)(SceneManager.WindowWidth * 0.5), (int)(SceneManager.WindowHeight * 0.5));
            ball.Init();
        }

        public void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    paddlePlayer.Move(20);
                    break;
                case Key.Down:
                    paddlePlayer.Move(-20);
                    break;
            }
        }

        public void Update(FrameEventArgs e)
        {
            // Set the title of the window
            if (gameTime > 0)
            {
                sceneManager.Title = "Pong - Player Score: " + scorePlayer + " - AI Score: " + scoreAI + " time remaining " + Math.Truncate(gameTime);

                paddleAI.Move(ball.Position);

                ball.Update((float)e.Time);
                paddleAI.Update((float)e.Time);

                CollisionDetection();
                if (GoalDetection())
                {
                    ResetGame();
                }
                gameTime -= (1.0 * e.Time);
            }
            else
            {
                sceneManager.HighScore(scorePlayer);
            }
            
        }

        private bool GoalDetection()
        {
            if (ball.Position.X < 0)
            {
                scoreAI++;
                return true;
            }
            else if (ball.Position.X > SceneManager.WindowWidth)
            {
                scorePlayer++;
                return true;
            }

            return false;
        }

        private void CollisionDetection()
        {
            // AI
            if ((paddleAI.Position.X - ball.Position.X) < ball.Radius &&
               ball.Position.Y > (paddleAI.Position.Y - 35.0f) && ball.Position.Y < (paddleAI.Position.Y + 35.0f))
            {
                ball.Position = new Vector2(paddleAI.Position.X - ball.Radius, ball.Position.Y);
                ball.Velocity = new Vector2(ball.Velocity.X * -1.0f, ball.Velocity.Y) * 2.0f;
            }
            // Player
            if ((ball.Position.X - paddlePlayer.Position.X) < ball.Radius &&
               ball.Position.Y > (paddlePlayer.Position.Y - 35.0f) && ball.Position.Y < (paddlePlayer.Position.Y + 35.0f))
            {
                ball.Position = new Vector2(paddlePlayer.Position.X + ball.Radius, ball.Position.Y);
                ball.Velocity = new Vector2(ball.Velocity.X * -1.0f, ball.Velocity.Y) * 2.0f;
            }
        }

        public void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, sceneManager.Width, 0, sceneManager.Height, -1.0f, +1.0f);

            ball.Render(projectionMatrix);
            paddlePlayer.Render(projectionMatrix);
            paddleAI.Render(projectionMatrix);
        }
    }
}

