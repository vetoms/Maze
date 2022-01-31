using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolver.TheMaze
{
    class StackFrontier
    {
        public List<Node> frontier;

        public StackFrontier()
        {
            frontier = new List<Node>();
        }

        public void add(Node node)
        {
            frontier.Add(node);
        }

        public bool contains_state((int, int) state)
        {
            return frontier.Exists(x => x.state == state);
        }

        public bool empty()
        {
            return frontier.Count == 0;
        }

        public virtual Node remove()
        {
            if (this.empty())
            {
                throw new System.Exception("empty frontier");
            }
            else
            {
                Node node = frontier[frontier.Count - 1];
                frontier.RemoveAt(frontier.Count - 1);
                return node;
            }
        }
    }
}
