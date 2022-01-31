using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolver.TheMaze
{
    class QueueFrontier : StackFrontier
    {
        public override Node remove()
        {
            if (this.empty())
            {
                throw new System.Exception("empty frontier");
            }
            else
            {
                Node node = frontier[0];
                frontier.RemoveAt(0);
                return node;
            }
        }
    
    }
}
