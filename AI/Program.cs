using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AI.TheMaze;

namespace AI
{
    public class Program
    {
        static void Main(string[] args)
        {
            string MazePath = "/MazeExamples/maze2.txt";
            Start.StartMaze(MazePath);
        }
    }
}
