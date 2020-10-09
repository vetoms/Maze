using System;
using System.Collections.Generic;
using System.Text;

namespace AI.TheMaze
{
    public class Start
    {
        public static void StartMaze(string MazePath)
        {
            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            string file = sPath + MazePath;
            Maze M = new Maze(file);

            Console.WriteLine("Maze:");
            M.print();
            M.solve("QueueFrontier");
            Console.WriteLine();
            Console.WriteLine("States Explored: " + M.num_explored);
            Console.WriteLine("Solution:");
            M.print();
            M.output_image(sPath + @"\Test.png");
            Console.ReadLine();
        }
    }
}
