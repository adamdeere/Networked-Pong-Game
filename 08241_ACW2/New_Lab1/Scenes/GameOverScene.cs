using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame.Scenes
{
    class GameOverScene : Scene, IScene
    {
        HighScoreScene namesAndStuff;
        Bitmap textBMP;
        int textTexture;
        Graphics textGFX;

        public static Dictionary<int ,string> ScoresOnTheDoors = new Dictionary<int ,string>();
        

        #region
        
        int inScore;
        int playerOneScore;
        int playerTwoScore;
        int playerThreeScore;
        int playerFourScore;
        int playerFiveScore;
       
        string playerOneName;
        string playerTwoName;
        string playerThreeName;
        string playerFourName;
        string playerFiveName;
        Client client;
       
        #endregion
        
        public GameOverScene(SceneManager sceneManager, Client inClient)
            : base(sceneManager)
        {
            client = inClient;
            namesAndStuff = new HighScoreScene(sceneManager, inScore, client);
             playerOneScore = namesAndStuff.playerOneScore;
             playerTwoScore = namesAndStuff.playerTwoScore;
             playerThreeScore = namesAndStuff.playerThreeScore;
             playerFourScore = namesAndStuff.playerFourScore;
             playerFiveScore = namesAndStuff.playerFiveScore;
             playerOneName = namesAndStuff.playerOneName;
             playerTwoName = namesAndStuff.playerTwoName;
             playerThreeName = namesAndStuff.playerThreeName;
             playerFourName = namesAndStuff.playerFourName;
             playerFiveName = namesAndStuff.playerFiveName;

            // Set the title of the window
            sceneManager.Title = "Pong - high score menu";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;

            // Create Bitmap and OpenGL texture for rendering text
            textBMP = new Bitmap(sceneManager.Width, sceneManager.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb); // match window size
            textGFX = Graphics.FromImage(textBMP);
            textGFX.Clear(Color.CornflowerBlue);
            textTexture = GL.GenTexture();
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textBMP.Width, textBMP.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Texture2D);
        }
        public void Update(FrameEventArgs e)
        {
            SelectMenu(new KeyboardKeyEventArgs());
        }
        public void SelectMenu(KeyboardKeyEventArgs e)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Key.Space))
            {
                sceneManager.StartMenu();
            }
        }

        private void RenderText(string text, float x, float y)
        {
            textGFX.DrawString(text, new Font("Arial", 20), Brushes.White, x, y);

            // Enable the texture
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textTexture);
            
            BitmapData data = textBMP.LockBits(new Rectangle(0, 0, textBMP.Width, textBMP.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)textBMP.Width, (int)textBMP.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            textBMP.UnlockBits(data);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f, 1f); GL.Vertex2(0f, 0f);
            GL.TexCoord2(1f, 1f); GL.Vertex2(sceneManager.Width, 0f);
            GL.TexCoord2(1f, 0f); GL.Vertex2(sceneManager.Width, sceneManager.Height);
            GL.TexCoord2(0f, 0f); GL.Vertex2(0f, sceneManager.Height);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Texture2D);
        }
        public void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, sceneManager.Width, 0, sceneManager.Height, -1, 1);

            if (textBMP != null)
            {
                RenderText("game over", 400f, 0f);
                RenderText("1. " + playerOneName + " " + playerOneScore, 0f, 20f);
                RenderText("2. " + playerTwoName + " " + playerTwoScore, 0f, 60f);
                RenderText("3. " + playerThreeName + " " + playerThreeScore, 0f, 120f);
                RenderText("4. " + playerFourName + " " + playerFourScore, 0f, 180f);
                RenderText("5. " + playerFiveName + " " + playerFiveScore, 0f, 240f);
                RenderText("press space bar to exit to main menu", 0f, 300f);
               
            }
        }
        
    }
}
