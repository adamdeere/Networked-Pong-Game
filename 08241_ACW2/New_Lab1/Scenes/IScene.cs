using OpenTK;

namespace PongGame
{
    internal interface IScene
    {
        void Render(FrameEventArgs e);

        void Update(FrameEventArgs e);
    }
}