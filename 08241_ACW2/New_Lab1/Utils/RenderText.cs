using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace PongGame.Utils
{
    class RenderText
    {
        private readonly Bitmap textBMP;
        private readonly int textTexture;
        private readonly Graphics textGFX;
        private int screenWidth, screenHeight; 
        public RenderText(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            // Create Bitmap and OpenGL texture for rendering text
            textBMP = new Bitmap(this.screenWidth, this.screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb); // match window size
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
        public void renderTextOnScreen(string text, float x, float y)
        {
            if (textBMP != null){

                textGFX.DrawString(text, new Font("Arial", 20), Brushes.White, x, y);
                // Enable the texture
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, textTexture);

                BitmapData data = textBMP.LockBits(new Rectangle(0, 0, textBMP.Width, textBMP.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)textBMP.Width, (int)textBMP.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                textBMP.UnlockBits(data);

                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0f, 1f); GL.Vertex2(0f, 0f);
                GL.TexCoord2(1f, 1f); GL.Vertex2(screenWidth, 0f);
                GL.TexCoord2(1f, 0f); GL.Vertex2(screenWidth, screenHeight);
                GL.TexCoord2(0f, 0f); GL.Vertex2(0f, screenHeight);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.Disable(EnableCap.Texture2D);
            }
        }
    }
   
}
