using System;
using System.Collections.Generic;
using System.Text;

namespace AI.TheMaze
{
    class Node
    {
        public (int, int) state;
        public string action;
        public Node parent;

        public Node((int, int) state, string action, Node parent)
        {
            this.state = state;
            this.action = action;
            this.parent = parent;
        }
    }
}
