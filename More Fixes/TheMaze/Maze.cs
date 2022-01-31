using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace MazeSolver.TheMaze
{
    class Maze
    {
        public int height;
        public int width;
        public bool[] row;
        public List<bool[]> walls;
        public List<(int, int)> solution;
        public (int, int) start;
        public (int, int) goal;
        public int num_explored;
        public List<(int, int)> explored;

        public Maze(string fileName)
        {            
            if (!fileName.Contains("A"))
            {
                throw new System.Exception("maze must have exactly one start point");
            }
            if (!fileName.Contains("B"))
            {
                throw new System.Exception("maze must have exactly one goal");
            }
            string[] lines = fileName.Split(new[] { '\r', '\n' });//File.ReadAllLines(fileName);

            lines = lines.Where( x => !string.IsNullOrEmpty(x)).ToArray();

            height = lines.Length;
            int max = 0;
            foreach (string line in lines)
            {
                int currentLenght = line.Length;

                if (currentLenght > max)
                {
                    max = currentLenght;
                }
            }
            width = max;
            walls = new List<bool[]>();
            for (int i = 0; i < height; i++)
            {
                row = new bool[width];
                for (int j = 0; j < width; j++)
                {
                    try
                    {
                        if (lines[i].Substring(j, 1) == "A")
                        {
                            start = (i, j);
                            row[j] = false;
                        }
                        else if (lines[i].Substring(j, 1) == "B")
                        {
                            goal = (i, j);
                            row[j] = false;
                        }
                        else if (lines[i].Substring(j, 1) == " ")
                        {
                            row[j] = false;
                        }
                        else
                        {
                            row[j] = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        row[j] = false;
                    }

                }

                walls.Add(row);
            }
            solution = new List<(int, int)>();
        }

        public void print()
        {
            //solution = self.solution[1] if self.solution is not None else None
            Console.WriteLine();
            int i = 0;
            foreach (bool[] row in walls)
            {
                int j = 0;
                foreach (bool col in row)
                {
                    if (col)
                    {
                        Console.Write("█");
                        Console.Write("");
                    }
                    else if ((i, j) == start)
                    {
                        Console.Write("A");
                        Console.Write("");
                    }
                    else if ((i, j) == goal)
                    {
                        Console.Write("B");
                        Console.Write("");
                    }
                    else if (solution != null && solution.Contains((i, j)))
                    {                        
                        Console.Write("*");
                        Console.Write("");                
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.Write("");
                    }
                    j++;
                }
                i++;
                Console.WriteLine();
            }
        }

        public List<(string, (int, int))> neighbors((int, int) state)
        {
            int row = state.Item1;
            int col = state.Item2;

            List<(string, (int, int))> candidates = new List<(string, (int, int))>();
            candidates.Add(("up", (row - 1, col)));
            candidates.Add(("down", (row + 1, col)));
            candidates.Add(("left", (row, col - 1)));
            candidates.Add(("right", (row, col + 1)));

            List<(string, (int, int))> result = new List<(string, (int, int))>();


            foreach (var action in candidates)
            {
                int r = action.Item2.Item1;
                int c = action.Item2.Item2;
                if (0 <= r && r < height && 0 <= c && c < width && !walls[r][c])
                {
                    result.Add((action.Item1, (r, c)));
                }
            }

            return result;
        }
        public void solve(string Type)
        {
            List<string> actions;
            List<(int, int)> cells;

            num_explored = 0;
            explored = new List<(int, int)>();
            Node newNode = new Node(start, "NONE", null);
            var frontier = new StackFrontier();
            if (Type == "QueueFrontier")
            {
                frontier = new QueueFrontier();
            }

            frontier.add(newNode);

            while (true)
            {
                if (frontier.empty() == true)
                {
                    throw new System.Exception("no solution");
                }
                newNode = frontier.remove();
                num_explored += 1;
                if (newNode.state == goal)
                {
                    actions = new List<string>();
                    cells = new List<(int, int)>();
                    while (newNode.parent != null)
                    {
                        actions.Add(newNode.action);
                        cells.Add(newNode.state);
                        newNode = newNode.parent;
                    }
                    solution.AddRange(cells);
                    break;
                }
                explored.Add(newNode.state);

                foreach (var state in neighbors(newNode.state))
                {
                    if (!frontier.contains_state(state.Item2) && !explored.Exists(x => x.Item1 == state.Item2.Item1 && x.Item2 == state.Item2.Item2))
                    {
                        Node child = new Node(state.Item2, state.Item1, newNode);
                        frontier.add(child);
                    }
                }

            }

        }

        public void output_image(string filename, bool show_solution = true, bool show_explored = false)
        {
            int cell_size = 50;
            int cell_border = 2;

            using (var bmp = new System.Drawing.Bitmap(width * cell_size, height * cell_size))
            {
                using (var graphics = System.Drawing.Graphics.FromImage(bmp))
                {
                    int i = 0;
                    foreach (bool[] row in walls)
                    {
                        int j = 0;
                        foreach (bool col in row)
                        {
                            int Fill = 0;
                            int Fill2 = 0;
                            int Fill3 = 0;
                            if (col)
                            {
                                Fill = 40;
                                Fill2 = 40;
                                Fill3 = 40;
                            }
                            else if ((i, j) == start)
                            {
                                Fill = 255;
                                Fill2 = 0;
                                Fill3 = 0;

                            }
                            else if ((i, j) == goal)
                            {
                                Fill = 0;
                                Fill2 = 171;
                                Fill3 = 28;
                            }
                            else if (solution != null && solution.Contains((i, j)))
                            {
                                Fill = 220;
                                Fill2 = 235;
                                Fill3 = 113;
                            }
                            else if (explored != null && explored.Contains((i, j)))
                            {
                                Fill = 212;
                                Fill2 = 97;
                                Fill3 = 85;
                            }
                            else
                            {
                                Fill = 237;
                                Fill2 = 240;
                                Fill3 = 252;
                            }

                            // Create rectangle.
                            Point NewOne = new Point(j * cell_size + cell_border, i * cell_size + cell_border);
                            Size NewSecond = new Size((j + 1) * cell_size - cell_border, (i + 1) * cell_size - cell_border);

                            Rectangle rect = new Rectangle(NewOne, NewSecond);
                            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(Fill, Fill2, Fill3));

                            graphics.FillRectangle(blueBrush, rect);
                            j++;

                        }
                        i++;
                        
                    }


                }
                bmp.Save(filename, ImageFormat.Png);
            }

        }



    }
}
