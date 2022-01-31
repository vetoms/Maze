using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using MazeSolver.TheMaze;

namespace MazeSolver
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
            Developer Notes
            Choose between the 3 mazes 
            Chose the type of Algorithm you want the program to solve (QueueFrontier or StackFrontier)
            */


            string MazePath = Properties.Resources.maze2;
            Start.StartMaze(MazePath, AlgorithmType.QueueFrontier);
            
        }
    }
}
