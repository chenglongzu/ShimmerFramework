namespace ShimmerNote
{
    public class Point
    {
        public Point Parent { get; set; }

        //A*三个参数
        public float F { get; set; }

        public float G { get; set; }
        public float H { get; set; }

        //点的位置信息
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsWall { get; set; }

        public Point(int x, int y, Point parent = null)
        {
            X = x;
            Y = y;
            Parent = parent;
            IsWall = false;
        }

        /// <summary>
        /// 更新父节点的g值
        /// </summary>
        public void UpdateParent(Point parent, float g)
        {
            Parent = parent;
            G = g;
            F = G + H;
        }
    }
}