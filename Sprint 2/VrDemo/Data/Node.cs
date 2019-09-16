namespace VrDemo.Data
{
    public class Node
    {
        public string name { get; private set; }
        public string guid { get; private set; }

        public Node(string name, string guid)
        {
            this.name = name;
            this.guid = guid;
        }
    }
}