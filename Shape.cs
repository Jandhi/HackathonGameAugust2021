using System;

public class Shape
{
    char identifier = 'X';
    int numberOfShapes = 3;
    int[,] size{get; set;}
    int[,] startingPoint{get; set;}
    Random rand;
    public Shape()
    {
        rand = new Random();
    }
    
    public bool randomShape(char[,] layout, int[] centre, int size){
        /*
        Layout: array of characters to be modified
        Centre: location at which the shape will be centred
        Bounds
        */
        int a = rand.Next(numberOfShapes);
        switch(a)
        {
            case 0:
                return rectangle(layout, centre, size);
            case 1:
                return circle(layout, centre, size);
            case 2:
                return triangle(layout, centre, size);
            default:
                return false;
        }
    }

    private bool rectangle(char[,] layout, int[] centre, int a)
    {
        int length = rand.Next(a/2 + 1) + a/2;
        int height = rand.Next(a/2 + 1) + a/2;
        bool isTouching = false;
        for(int i = centre[0] - length/2; i < centre[0] + length/2; i++)
        {
            for(int j = centre[1] - height/2; j < centre[1] + height/2; j++)
            {
                if(i > 0 && i < layout.GetLength(0) && j > 0 && j < layout.GetLength(1))
                {
                    if(layout[i, j] == 'D')
                        isTouching = true;
                    else
                        layout[i, j] = identifier;
                }

            }
        }
        return isTouching;
    }

    private bool circle(char[,] layout, int[] centre, int a)
    {
        var random = new Random();
        int radius = random.Next(a/2 + 1) + a/2;
        bool isTouching = false;

        for(int i = centre[0] - radius; i < centre[0] + radius; i++)
        {
            for(int j = centre[1] - radius; j < centre[1] + radius; j++)
            {
                if(i > 0 && i < layout.GetLength(0) && j > 0 && j < layout.GetLength(1))
                {
                    if(layout[i, j] == 'D')
                        isTouching = true;
                    else if(Math.Sqrt(Math.Pow(i - centre[0], 2) + Math.Pow(j - centre[1], 2)) < radius)
                        layout[i, j] = identifier;
                }
            }
        }
        return isTouching;
    }

    private bool triangle(char[,] layout, int[] centre, int a)
    {
        var random = new Random();
        int height = random.Next(a/2 + 1) + a/2;
        bool isTouching = false;

        for(int j = centre[1] + height/2; j > centre[1] - height/2; j--)
        {
            for(int i = centre[0] - (j - centre[1] + height/2); i < centre[0] + (j - centre[1] + height/2); i++)
            {
                if(i > 0 && i < layout.GetLength(0) && j > 0 && j < layout.GetLength(1))
                {
                    if(layout[i, j] == 'D')
                        isTouching = true;
                    else
                        layout[i, j] = identifier;
                }
            }
        }
        return isTouching;
    }
    /*private bool rhombus(char[,] layout, int[] centre, int a)
    {

    }*/
}