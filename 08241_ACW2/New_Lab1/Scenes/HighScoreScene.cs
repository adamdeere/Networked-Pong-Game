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
    class HighScoreScene : Scene, IScene
    {
        Bitmap textBMP;
        int textTexture;
        Graphics textGFX;
        static string master;
        static string outServerAddress;
        static int Port;
        Client client;// = new Client(outServerAddress, Port, master);

        #region
        bool aPressed = true;
        bool bPressed = true;
        bool cPressed = true;
        bool dPressed = true;
        bool ePressed = true;
        bool fPressed = true;
        bool gPressed = true;
        bool hPressed = true;
        bool iPressed = true;
        bool jPressed = true;
        bool kPressed = true;
        bool lPressed = true;
        bool mPressed = true;
        bool nPressed = true;
        bool oPressed = true;
        bool pPressed = true;
        bool qPressed = true;
        bool rPressed = true;
        bool sPressed = true;
        bool tPressed = true;
        bool uPressed = true;
        bool vPressed = true;
        bool wPressed = true;
        bool xPressed = true;
        bool yPressed = true;
        bool zPressed = true;
        #endregion
        string name;
        int highScore;
        string nameAndStuff;

        int tempScore;
        string tempName;

        public int playerOneScore;
        public int playerTwoScore;
        public int playerThreeScore;
        public int playerFourScore;
        public int playerFiveScore;
        public string playerOneName;
        public string playerTwoName;
        public string playerThreeName;
        public string playerFourName;
        public string playerFiveName;

        List<string> NameInput = new List<string>();
        List<int> ScoreInput = new List<int>();

        int[] scores = new int[5];

        string[] namesFromServer = new string[5];

        
        float offset = 300f;
        public HighScoreScene(SceneManager sceneManager, int inScore, Client inClient)
            : base(sceneManager)
        {
            highScore = inScore;
            outServerAddress = inClient.serverAddress;
            Port = inClient.port;
            master = inClient.masterAddress;
            client = inClient;
            client.LookUp(outServerAddress, Port);
            nameAndStuff = client.lineFromServer;
            string[] sections = nameAndStuff.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            playerOneName = sections[1];
            playerOneScore = int.Parse(sections[0]);

            playerTwoName = sections[3];
            playerTwoScore = int.Parse(sections[2]);

            playerThreeName = sections[5];
            playerThreeScore = int.Parse(sections[4]);

            playerFourName = sections[7];
            playerFourScore = int.Parse(sections[6]);

            playerFiveName = sections[9];
            playerFiveScore = int.Parse(sections[8]);
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
            EnterName(new KeyboardKeyEventArgs());
        }
        public void EnterName(KeyboardKeyEventArgs e)
        {

            name = string.Join("", NameInput.ToArray());
            KeyboardState keyState = Keyboard.GetState();
            //this region ctrols the input from the keys
            #region
            if (keyState.IsKeyDown(Key.A) && aPressed == true)
            {
                RenderText("A", offset, 50f);
                offset = offset + 20f;
                aPressed = false;
                NameInput.Add("A");
            }
            if (keyState.IsKeyUp(Key.A))
            {
                aPressed = true;
            }
            if (keyState.IsKeyDown(Key.B) && bPressed == true)
            {
                RenderText("B", offset, 50f);
                offset = offset + 20f;
                bPressed = false;
                NameInput.Add("B");
            }
            if (keyState.IsKeyUp(Key.B))
            {
                bPressed = true;
            }
            if (keyState.IsKeyDown(Key.C) && cPressed == true)
            {
                RenderText("C", offset, 50f);
                offset = offset + 20f;
                cPressed = false;
                NameInput.Add("C");
            }
            if (keyState.IsKeyUp(Key.C))
            {
                cPressed = true;
            }
            if (keyState.IsKeyDown(Key.D) && dPressed == true)
            {
                RenderText("D", offset, 50f);
                offset = offset + 20f;
                dPressed = false;
                NameInput.Add("D");
            }
            if (keyState.IsKeyUp(Key.D))
            {
                dPressed = true;
            }
            if (keyState.IsKeyDown(Key.E) && ePressed == true)
            {
                RenderText("E", offset, 50f);
                offset = offset + 20f;
                ePressed = false;
                NameInput.Add("E");
            }
            if (keyState.IsKeyUp(Key.E))
            {
                ePressed = true;
            }
            if (keyState.IsKeyDown(Key.F) && fPressed == true)
            {
                RenderText("F", offset, 50f);
                offset = offset + 20f;
                fPressed = false;
                NameInput.Add("F");
            }
            if (keyState.IsKeyUp(Key.F))
            {
                fPressed = true;
            }
            if (keyState.IsKeyDown(Key.G) && gPressed == true)
            {
                RenderText("G", offset, 50f);
                offset = offset + 20f;
                gPressed = false;
                NameInput.Add("G");
            }
            if (keyState.IsKeyUp(Key.G))
            {
                gPressed = true;
            }
            if (keyState.IsKeyDown(Key.H) && hPressed == true)
            {
                RenderText("H", offset, 50f);
                offset = offset + 20f;
                hPressed = false;
                NameInput.Add("H");
            }
            if (keyState.IsKeyUp(Key.H))
            {
                hPressed = true;
            }
            if (keyState.IsKeyDown(Key.I) && iPressed == true)
            {
                RenderText("I", offset, 50f);
                offset = offset + 20f;
                iPressed = false;
                NameInput.Add("I");
            }
            if (keyState.IsKeyUp(Key.I))
            {
                iPressed = true;
            }
            if (keyState.IsKeyDown(Key.J) && jPressed == true)
            {
                RenderText("J", offset, 50f);
                offset = offset + 20f;
                jPressed = false;
                NameInput.Add("J");
            }
            if (keyState.IsKeyUp(Key.J))
            {
                jPressed = true;
            }
            if (keyState.IsKeyDown(Key.K) && kPressed == true)
            {
                RenderText("K", offset, 50f);
                offset = offset + 20f;
                kPressed = false;
                NameInput.Add("K");
            }
            if (keyState.IsKeyUp(Key.K))
            {
                kPressed = true;
            }
            if (keyState.IsKeyDown(Key.L) && lPressed == true)
            {
                RenderText("L", offset, 50f);
                offset = offset + 20f;
                lPressed = false;
                NameInput.Add("L");
            }
            if (keyState.IsKeyUp(Key.L))
            {
                lPressed = true;
            }
            if (keyState.IsKeyDown(Key.M) && mPressed == true)
            {
                RenderText("M", offset, 50f);
                offset = offset + 20f;
                mPressed = false;
                NameInput.Add("M");
            }
            if (keyState.IsKeyUp(Key.M))
            {
                mPressed = true;
            }
            if (keyState.IsKeyDown(Key.N) && nPressed == true)
            {
                RenderText("N", offset, 50f);
                offset = offset + 20f;
                nPressed = false;
                NameInput.Add("N");
            }
            if (keyState.IsKeyUp(Key.N))
            {
                nPressed = true;
            }
            if (keyState.IsKeyDown(Key.O) && oPressed == true)
            {
                RenderText("O", offset, 50f);
                offset = offset + 20f;
                oPressed = false;
                NameInput.Add("O");
            }
            if (keyState.IsKeyUp(Key.O))
            {
                oPressed = true;
            }
            if (keyState.IsKeyDown(Key.P) && pPressed == true)
            {
                RenderText("P", offset, 50f);
                offset = offset + 20f;
                pPressed = false;
                NameInput.Add("P");
            }
            if (keyState.IsKeyUp(Key.P))
            {
                pPressed = true;
            }
            if (keyState.IsKeyDown(Key.Q) && qPressed == true)
            {
                RenderText("Q", offset, 50f);
                offset = offset + 20f;
                qPressed = false;
                NameInput.Add("Q");
            }
            if (keyState.IsKeyUp(Key.Q))
            {
                qPressed = true;
            }
            if (keyState.IsKeyDown(Key.R) && rPressed == true)
            {
                RenderText("R", offset, 50f);
                offset = offset + 20f;
                rPressed = false;
                NameInput.Add("R");
            }
            if (keyState.IsKeyUp(Key.R))
            {
                rPressed = true;
            }
            if (keyState.IsKeyDown(Key.S) && sPressed == true)
            {
                RenderText("S", offset, 50f);
                offset = offset + 20f;
                sPressed = false;
                NameInput.Add("S");
            }
            if (keyState.IsKeyUp(Key.S))
            {
                sPressed = true;
            }
            if (keyState.IsKeyDown(Key.T) && tPressed == true)
            {
                RenderText("T", offset, 50f);
                offset = offset + 20f;
                tPressed = false;
                NameInput.Add("T");
            }
            if (keyState.IsKeyUp(Key.T))
            {
                tPressed = true;
            }
            if (keyState.IsKeyDown(Key.U) && uPressed == true)
            {
                RenderText("U", offset, 50f);
                offset = offset + 20f;
                uPressed = false;
                NameInput.Add("U");
            }
            if (keyState.IsKeyUp(Key.U))
            {
                uPressed = true;
            }
            if (keyState.IsKeyDown(Key.V) && vPressed == true)
            {
                RenderText("V", offset, 50f);
                offset = offset + 20f;
                vPressed = false;
                NameInput.Add("V");
            }
            if (keyState.IsKeyUp(Key.V))
            {
                vPressed = true;
            }
            if (keyState.IsKeyDown(Key.W) && wPressed == true)
            {
                RenderText("W", offset, 50f);
                offset = offset + 20f;
                wPressed = false;
                NameInput.Add("W");
            }
            if (keyState.IsKeyUp(Key.W))
            {
                wPressed = true;
            }
            if (keyState.IsKeyDown(Key.X) && xPressed == true)
            {
                RenderText("X", offset, 50f);
                offset = offset + 20f;
                xPressed = false;
                NameInput.Add("X");
            }
            if (keyState.IsKeyUp(Key.X))
            {
                xPressed = true;
            }
            if (keyState.IsKeyDown(Key.Y) && yPressed == true)
            {
                RenderText("Y", offset, 50f);
                offset = offset + 20f;
                yPressed = false;
                NameInput.Add("Y");
            }
            if (keyState.IsKeyUp(Key.Y))
            {
                yPressed = true;
            }
            if (keyState.IsKeyDown(Key.Z) && zPressed == true)
            {
                RenderText("Z", offset, 50f);
                offset = offset + 20f;
                zPressed = false;
                NameInput.Add("Z");
            }
            if (keyState.IsKeyUp(Key.Z))
            {
                zPressed = true;
            }
            #endregion //this region controls the input from the keys

            if (keyState.IsKeyDown(Key.Enter))
            {
                for (int i = 0; i < scores.Length; i++)
                {
                    //this region checks the high scores
                    #region
                    if (highScore >= playerOneScore)
                    {
                        tempName = playerOneName;
                        tempScore = playerOneScore;

                        playerOneName = name;
                        playerOneScore = highScore;

                        playerFiveName = playerFourName;
                        playerFiveScore = playerFourScore;

                        playerFourName = playerThreeName;
                        playerFourScore = playerThreeScore;

                        playerThreeName = playerTwoName;
                        playerThreeScore = playerTwoScore;

                        playerTwoName = tempName;
                        playerTwoScore = tempScore;

                        RenderText("Congratulations! " + name + " you have a new high score", 300f, 0f);
                        client.Update(playerOneScore, playerOneName, playerTwoScore, playerTwoName, playerThreeScore, playerThreeName, playerFourScore, playerFourName, playerFiveScore, playerFiveName);
                        sceneManager.GameOver();
                        break;
                    }
                    else if (highScore >= playerTwoScore)
                    {
                        tempName = playerTwoName;
                        tempScore = playerTwoScore;

                        playerTwoName = name;
                        playerTwoScore = highScore;

                        playerFiveName = playerFourName;
                        playerFiveScore = playerFourScore;

                        playerFourName = playerThreeName;
                        playerFourScore = playerThreeScore;

                        playerThreeName = tempName;
                        playerThreeScore = tempScore;

                        RenderText("Congratulations! " + name + " you have a new high score", 300f, 0f);
                        client.Update(playerOneScore, playerOneName, playerTwoScore, playerTwoName, playerThreeScore, playerThreeName, playerFourScore, playerFourName, playerFiveScore, playerFiveName);
                        sceneManager.GameOver();
                        break;
                    }
                    else if (highScore >= playerThreeScore)
                    {
                        tempName = playerThreeName;
                        tempScore = playerThreeScore;

                        playerThreeName = name;
                        playerThreeScore = highScore;

                        playerFiveName = playerFourName;
                        playerFiveScore = playerFourScore;

                        playerFourName = tempName;
                        playerFourScore = tempScore;

                        RenderText("Congratulations! " + name + " you have a new high score", 300f, 0f);
                        client.Update(playerOneScore, playerOneName, playerTwoScore, playerTwoName, playerThreeScore, playerThreeName, playerFourScore, playerFourName, playerFiveScore, playerFiveName);
                        sceneManager.GameOver();
                        break;
                    }
                    else if (highScore >= playerFourScore)
                    {
                        tempName = playerFourName;
                        tempScore = playerFourScore;

                        playerFourName = name;
                        playerFourScore = highScore;

                        playerFiveName = tempName;
                        playerFiveScore = tempScore;

                        RenderText("Congratulations! " + name + " you have a new high score", 300f, 0f);
                        client.Update(playerOneScore, playerOneName, playerTwoScore, playerTwoName, playerThreeScore, playerThreeName, playerFourScore, playerFourName, playerFiveScore, playerFiveName);
                        sceneManager.GameOver();
                        break;
                    }
                    else if (highScore >= playerFiveScore)
                    {
                        playerFiveName = name;
                        playerFiveScore = highScore;
                        RenderText("Congratulations! " + name + " you have a new high score", 300f, 0f);
                        client.Update(playerOneScore, playerOneName, playerTwoScore, playerTwoName, playerThreeScore, playerThreeName, playerFourScore, playerFourName, playerFiveScore, playerFiveName);
                        sceneManager.GameOver();
                        break;
                    }
                    else
                    {
                        RenderText(name + " has scored " + highScore, 300f, 150f);
                    }
                    #endregion
                }
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
                RenderText("Please enter your name", 0f, 50f);
            }
        }

    }
}
