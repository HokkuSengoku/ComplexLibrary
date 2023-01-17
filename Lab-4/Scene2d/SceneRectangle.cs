namespace Scene2d
{
    public struct SceneRectangle
    {
        public ScenePoint Vertex1 { get; set; }

        public ScenePoint Vertex2 { get; set; }

        public ScenePoint CalculateTheCenterOfCircumscribedRectangle()
        {
            ScenePoint left = Vertex1;
            ScenePoint right = Vertex2;
            ScenePoint point = new ScenePoint { X = (right.X + left.X) / 2, Y = (left.Y + right.Y) / 2 };
            return point;
        }
    }
}
