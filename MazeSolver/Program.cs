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
            string MazePath = Properties.Resources.maze2;
            Start.StartMaze(MazePath);
            
        }
    }
}
