using OpenTK;

namespace PongGame
{
    internal abstract class GameObject
    {
        protected Vector2 position;
        protected Vector2 velocity;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public abstract void Render(Matrix4 projectionMatrix);

        public abstract void Update(float dt);

        // Graphics
        protected int pgmID;

        protected int vsID;
        protected int fsID;
        protected int attribute_vcol;
        protected int attribute_vpos;
        protected int uniform_mview;
        protected int vao_Handle;
        protected int vbo_position;
        protected int vbo_color;
        protected Vector3[] vertdata;
        protected Vector3[] coldata;
        protected Matrix4 viewMatrix;
    }
}