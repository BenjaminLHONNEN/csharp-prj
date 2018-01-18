using System;

namespace projetPOO
{
    public class PathFinding
    {
        //Contient le labyrinthe
        private Maze Maze;
        //Contient le tableau du labyrinthe
        private Case[][] MazeArray;
        //Contient les coordonnes des cases du chemin de sortie
        private int[][] PathTake;
        //Contient les priorite de chaque case
        private int[][] Priority;
        //contient la direction a prendre des cases appartenant au chemin de sortie
        private string[] direction;
        //Coordonnes Case depart
        private int[] Start;
        //Coordonnes Case arrive 
        private int[] End;
        //Largeur Labyrinthe
        private int x;
        //Longueur Labyrinthe
        private int y;

        public PathFinding(Maze maze)
        {
            Maze = maze;
            MazeArray = maze.GetMazeArray();
            Start = new[] {maze.GetStartX(), maze.GetStartY()};
            End = new[] {maze.GetEndX(), maze.GetEndY()};
            x = maze.GetLenX();
            y = maze.GetLenY();
            Priority = new int[maze.GetLenX()][];
            for (int i = 0; i < Priority.Length; i++)
            {
                Priority[i] = new int[maze.GetLenY()];
                for (int j = 0; j < Priority[i].Length; j++)
                {
                    Priority[i][j] = -1;
                }
            }
            Attributepriority();
            MakePath();
        }

        //Trouve le chemin le plus rapide pour aller de l'entre a la sortie (le pathfinding s'inspire l'algorithme A*)
        private void MakePath()
        {
            string lastDirection = "none";
            int i = End[0], j = End[1];
            int priorityOfEndCase = Priority[i][j];
            PathTake = new int[priorityOfEndCase][];
            direction = new string[priorityOfEndCase];
            for (int z = 0; z < priorityOfEndCase; z++)
            {
                PathTake[z] = new int[2];
                PathTake[z] = new[] {-1, -1};
                if (i != 0 && MazeArray[i][j].GetTop() && Priority[i][j] > Priority[i - 1][j])
                {
                    PathTake[z][0] = i;
                    PathTake[z][1] = j;
                    i--;
                    if (lastDirection != "none")
                    {
                        direction[z] = lastDirection;
                    }
                    lastDirection = "bottom";
                }
                else if (i != x - 1 && MazeArray[i][j].GetBottom() && Priority[i][j] > Priority[i + 1][j])
                {
                    PathTake[z][0] = i;
                    PathTake[z][1] = j;
                    i++;
                    if (lastDirection != "none")
                    {
                        direction[z] = lastDirection;
                    }
                    lastDirection = "top";
                }
                else if (j != 0 && MazeArray[i][j].GetLeft() && Priority[i][j] > Priority[i][j - 1])
                {
                    PathTake[z][0] = i;
                    PathTake[z][1] = j;
                    j--;
                    if (lastDirection != "none")
                    {
                        direction[z] = lastDirection;
                    }
                    lastDirection = "right";
                }
                else if (j != y - 1 && MazeArray[i][j].GetRight() && Priority[i][j] > Priority[i][j + 1])
                {
                    PathTake[z][0] = i;
                    PathTake[z][1] = j;
                    j++;
                    if (lastDirection != "none")
                    {
                        direction[z] = lastDirection;
                    }
                    lastDirection = "left";
                }
            }
        }

        //Est ce que la case cible apprtient au chemin de sortie
        public bool IsCaseInPath(int posx,int posy)
        {
            for (int i = 0; i < PathTake.Length; i++)
            {
                if (posx == PathTake[i][0] && posy == PathTake[i][1])
                {
                    return true;
                }
            }
            return false;
        }
        //Renvoie la direction d'une case appartenant au chemin de sortie (la direction pointe vers la sortie)
        public string GetDirectionOfCase(int posx,int posy)
        {
            for (int i = 0; i < PathTake.Length; i++)
            {
                if (posx == PathTake[i][0] && posy == PathTake[i][1])
                {
                    return direction[i];
                }
            }
            return "";
        }

        //Verifie que toute les cases ont une priorite
        private bool IsAllPrioritySet()
        {
            bool response = true;
            for (int i = 0; i < Priority.Length; i++)
            {
                for (int j = 0; j < Priority[i].Length; j++)
                {
                    if (Priority[i][j] == -1)
                    {
                        response = false;
                    }
                }
            }
            return response;
        }
        //Attribu une priorite a chaque case en fonction de sa distance avec la case de depart
        private void Attributepriority()
        {
            Priority[Start[0]][Start[1]] = 0;
            while (!IsAllPrioritySet())
            {
                int min;
                for (int i = 0; i < MazeArray.Length; i++)
                {
                    for (int j = 0; j < MazeArray[i].Length; j++)
                    {
                        if (Priority[i][j] == -1)
                        {
                            min = MazeArray.Length * MazeArray[i].Length + 1;
                        }
                        else
                        {
                            min = Priority[i][j];
                        }

                        if (i != 0 && MazeArray[i][j].GetTop())
                        {
                            if (Priority[i - 1][j] != -1 && min > Priority[i - 1][j] + 1)
                            {
                                min = Priority[i - 1][j] + 1;
                            }
                        }
                        if (i != x - 1 && MazeArray[i][j].GetBottom())
                        {
                            if (Priority[i + 1][j] != -1 && min > Priority[i + 1][j] + 1)
                            {
                                min = Priority[i + 1][j] + 1;
                            }
                        }

                        if (j != 0 && MazeArray[i][j].GetLeft())
                        {
                            if (Priority[i][j - 1] != -1 && min > Priority[i][j - 1] + 1)
                            {
                                min = Priority[i][j - 1] + 1;
                            }
                        }
                        if (j != y - 1 && MazeArray[i][j].GetRight())
                        {
                            if (Priority[i][j + 1] != -1 && min > Priority[i][j + 1] + 1)
                            {
                                min = Priority[i][j + 1] + 1;
                            }
                        }
                        if (min == MazeArray.Length * MazeArray[i].Length + 1)
                        {
                            Priority[i][j] = -1;
                        }
                        else
                        {
                            Priority[i][j] = min;
                        }
                    }
                }
            }
        }

        
        //GETTER
        public int[][] GetPriority()
        {
            return Priority;
        }

        public int[][] GetPathTake()
        {
            return PathTake;
        }
        
        
        //Recupere la case a gauche de la case cible
        private Case GetLeftCase(int posx, int posy)
        {
            return MazeArray[posx][posy - 1];
        }
        //Recupere la case a droite de la case cible
        private Case GetRightCase(int posx, int posy)
        {
            return MazeArray[posx][posy + 1];
        }
        //Recupere la case en haut de la case cible
        private Case GetTopCase(int posx, int posy)
        {
            return MazeArray[posx - 1][posy];
        }
        //Recupere la case en bas de la case cible
        private Case GetBottomCase(int posx, int posy)
        {
            return MazeArray[posx + 1][posy];
        }
    }
}