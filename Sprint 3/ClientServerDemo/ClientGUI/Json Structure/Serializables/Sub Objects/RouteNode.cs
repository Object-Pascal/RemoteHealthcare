namespace ClientGUI.Sub_Objects
{
    public class RouteNode
    {
        public RouteNode(int[] pos, int[] dir)
        {
            this.pos = pos;
            this.dir = dir;
        }

        public int[] pos { get; set; }
        public int[] dir { get; set; }
    }
}