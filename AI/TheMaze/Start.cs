using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolver.TheMaze
{
    public class Start
    {
        public static void StartMaze(string MazePath)
        {
            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            Maze M = new Maze(MazePath);

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
