using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace Game
{

    public class Map
    {
        public int l {get; set;}
        public int h{get; set;}
        public int[,] doors {get; set;}
        public char[,] layout{get; set;}
        private Random rand;
        public Map(int l, int h)
        {
            rand = new Random();
            this.l = l;
            this.h = h;
            this.layout = new char[l, h];
            for(int i = 0; i < l; i++)
            {
                for(int j = 0; j < h; j++)
                {
                    layout[i, j] = ' ';
                }
            }
            doors = new int[2, 2];
            generateDoors(l/2);
            

            layout[doors[1, 0], doors[1, 1]] = 'D';
            int[] start = {doors[0, 0], doors[0, 1]};

            pathfind(false, start);
            layout[doors[0, 0], doors[0, 1]] = 'D';
            populate(rand.Next((int)(0.005 * whitespace()), (int)(0.01 * whitespace())));
        }

        private void generateDoors(int minDistance)
        {
            doors[0, 0] = rand.Next(1, l);
            doors[0, 1] = rand.Next(1, h);
            doors[1, 0] = rand.Next(1, l);
            doors[1, 1] = rand.Next(1, h);
            if(Math.Sqrt(Math.Pow(doors[0,0] - doors[1,0], 2) + Math.Pow(doors[0,1] - doors[1,1], 2) ) < minDistance)
            {
                generateDoors(minDistance);
            }
        }

        public void pathfind(bool state, int[] centre)
        {
            Shape s = new Shape();
            int size = rand.Next(layout.GetLength(1)/4) + 5;
            if(state)
                return;
            else
                state = s.randomShape(layout, centre, size);
                int[] newCentre = nearestGround();
                pathfind(state, newCentre);
        }

        private int[] nearestGround()
        {
            double shortestDistance = Math.Pow(l, 2) + Math.Pow(h, 2);  //Larger than the maximum distance can be
            int[] result = {-1, -1};
            for(int i = 0; i < layout.GetLength(0); i++)
            {
                for(int j = 0; j < layout.GetLength(1); j++)
                {
                    if(layout[i, j] == 'X' && Math.Sqrt(Math.Pow(doors[1, 0] - i, 2) + Math.Pow(doors[1, 1] - j, 2)) < shortestDistance)
                    {
                        shortestDistance = Math.Sqrt(Math.Pow(doors[1, 0] - i, 2) + Math.Pow(doors[1, 1] - j, 2));
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }
            return result;
        }

        private void populate(int pop)
        {
            if(pop == 0) 
            {
                return;
            }
            int x = rand.Next(0, l);
            int y = rand.Next(0, h);
            if(emptySpace(x, y))
            {
                layout[x, y] = 'T';
                populate(pop - 1);
            }
            else
            {
                populate(pop);
            }
        }

        private int whitespace()
        {
            int result = 0;
            for(int i = 0; i < l; i++)
            {
                for(int j = 0; j < h; j++)
                {
                    if(layout[i, j] == 'X')
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private bool emptySpace(int x, int y)
        {
            for(int i = x - 1; i < x + 2 && i < l - 1; i++)
            {
                for(int j = y - 1; j < y + 2 && j < h - 1; j++)
                {
                    if(i < 0)
                        i = 0;
                    if(j < 0)
                        j = 0;
                    if(layout[i, j] != 'X')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void display(Console c)
        {
            Shape s = new Shape();
            int[] centre = {50, 25};
            s.randomShape(layout, centre, 20);
            c.Fill(ColorAnsi.BlueBright, ColorAnsi.Blue, 0);
            c.SetBackground(doors[0, 0], doors[0, 1], Color.White);
            c.SetBackground(doors[1, 0], doors[1, 1], Color.White);
            for(int i = 0; i < layout.GetLength(0); i++)
            {
                for(int j = 0; j < layout.GetLength(1); j++)
                {
                    if(layout[i, j] == 'X')
                    {
                        c.SetBackground(i, j, Color.White);
                        c.Print(i, j, ' '.ToString());
                    }
                    else if(layout[i, j] == 'T')
                    {
                        c.SetBackground(i, j, Color.White);
                        c.Print(i, j, layout[i, j].ToString());
                    }
                    else
                        c.Print(i, j, layout[i, j].ToString());
                }
            }
            SadConsole.Global.CurrentScreen = c;
        }
    }

}