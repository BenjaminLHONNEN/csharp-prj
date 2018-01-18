using System;

namespace projetPOO
{
    public class Maze
    {
        //Tableau contenant le labyrinthe
        public Case[][] MazeArray;
        //Largeur
        private int x;
        //Longueur
        private int y;
        //X case depart
        private int startX;
        //Y case depart
        private int startY;
        
        //X case fin
        private int endX;
        //Y case fin
        private int endY;
        //Random de la class
        private Random rnd;

        public Maze(int x, int y)
        {
            rnd = new Random();
            MazeArray = new Case[x][];
            for (int i = 0; i < x; i++)
            {
                MazeArray[i] = new Case[y];
            }
            this.x = x;
            this.y = y;
            startX = rnd.Next(x);
            startY = rnd.Next(y);
            endX = rnd.Next(x);
            endY = rnd.Next(y);
        }

        //Genere le labyrinthe selon le mod choisi
        public void GenerateMaze(int mod = 1)
        {
            startX = rnd.Next(x);
            startY = rnd.Next(y);
            endX = rnd.Next(x);
            endY = rnd.Next(y);
            int nbCase = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    MazeArray[i][j] = new Case(15, i, j, nbCase);
                    nbCase++;
                }
            }
            if (mod == 1)
            {
                MakeAllLinkMod1();
            }
            else if (mod == 2)
            {
                MakeAllLinkMod2();
            }
        }
        //Est ce que le labyrinthe est parfait
        public bool IsAllCaseLinked()
        {
            bool response = true;
            for (int i = 0; i < MazeArray.Length; i++)
            {
                for (int j = 0; j < MazeArray[i].Length; j++)
                {
                    if (MazeArray[i][j].GetConnectedTo() != 0)
                    {
                        response = false;
                    }
                }
            }
            return response;
        }
        //Rendre le labyrinthe parfait avec le mod 1
        private void MakeAllLinkMod1()
        {
            int i = 0;
            int j = 0;
            while (!IsAllCaseLinked())
            {
                bool isTopAuthorised = true;
                bool isBottomAuthorised = true;
                bool isRightAuthorised = true;
                bool isLeftAuthorised = true;

                if (i == 0)
                {
                    isTopAuthorised = false;
                }
                if (j == 0)
                {
                    isLeftAuthorised = false;
                }
                if (i == x - 1)
                {
                    isBottomAuthorised = false;
                }
                if (j == y - 1)
                {
                    isRightAuthorised = false;
                }
                if (MazeArray[i][j].GetNbConnexion() < 3)
                {
                    ChooseRandomConnexion(i, j, isTopAuthorised, isBottomAuthorised, isRightAuthorised,
                        isLeftAuthorised);
                }
                j++;
                if (j == y)
                {
                    i++;
                    j = 0;
                }
                if (i == x)
                {
                    i = 0;
                }
            }
        }
        //Rendre le labyrinthe parfait avec le mod 2
        private void MakeAllLinkMod2()
        {
            int i = 0;
            int j = 0;
            int iReverse = x - 1;
            int jReverse = y - 1;
            while (!IsAllCaseLinked())
            {
                bool isTopAuthorised = true;
                bool isBottomAuthorised = true;
                bool isRightAuthorised = true;
                bool isLeftAuthorised = true;
                bool isTopAuthorisedReverse = true;
                bool isBottomAuthorisedReverse = true;
                bool isRightAuthorisedReverse = true;
                bool isLeftAuthorisedReverse = true;

                if (i == 0)
                {
                    isTopAuthorised = false;
                }
                if (j == 0)
                {
                    isLeftAuthorised = false;
                }
                if (i == x - 1)
                {
                    isBottomAuthorised = false;
                }
                if (j == y - 1)
                {
                    isRightAuthorised = false;
                }

                if (iReverse == 0)
                {
                    isTopAuthorisedReverse = false;
                }
                if (jReverse == 0)
                {
                    isLeftAuthorisedReverse = false;
                }
                if (iReverse == x - 1)
                {
                    isBottomAuthorisedReverse = false;
                }
                if (jReverse == y - 1)
                {
                    isRightAuthorisedReverse = false;
                }
                
                
                if (MazeArray[i][j].GetNbConnexion() < 3)
                {
                    ChooseRandomConnexion(
                        i,
                        j,
                        isTopAuthorised,
                        isBottomAuthorised,
                        isRightAuthorised,
                        isLeftAuthorised
                    );
                }
                
                if (!IsAllCaseLinked())
                {
                    if (MazeArray[iReverse][jReverse].GetNbConnexion() < 3)
                    {
                        ChooseRandomConnexion(
                            iReverse,
                            jReverse,
                            isTopAuthorisedReverse,
                            isBottomAuthorisedReverse,
                            isRightAuthorisedReverse,
                            isLeftAuthorisedReverse
                        );
                    }
                }

                j++;
                if (j == y)
                {
                    i++;
                    j = 0;
                    if (i == x)
                    {
                        i = 0;
                    }
                }

                jReverse--;
                if (jReverse == -1)
                {
                    iReverse--;
                    jReverse = y - 1;
                    if (iReverse == -1)
                    {
                        iReverse = x - 1;
                    }
                }
            }
        }

        //Choisir une connexion (haut bas droite gauche) aleatoire de la case cible
        private void ChooseRandomConnexion(int i, int j, bool isTopAuthorised, bool isBottomAuthorised,
            bool isRightAuthorised, bool isLeftAuthorised)
        {
            int lenListChoice = 4;
            bool isLinkAdded = false;
            int nbBCL = 0;

            while (!isLinkAdded && MazeArray[i][j].GetNbConnexion() < 3 && nbBCL < 10)
            {
                int tabChoice = rnd.Next(lenListChoice);

                if (isTopAuthorised && GetTopCase(i, j).GetNbConnexion() < 3 &&
                    GetTopCase(i, j).GetConnectedTo() != MazeArray[i][j].GetConnectedTo() && tabChoice == 0)
                {
                    ConnectCaseWithTopCase(i, j);
                    isLinkAdded = true;
                }
                else if (isLeftAuthorised && GetLeftCase(i, j).GetNbConnexion() < 3 &&
                         GetLeftCase(i, j).GetConnectedTo() != MazeArray[i][j].GetConnectedTo() && tabChoice == 1)
                {
                    ConnectCaseWithLeftCase(i, j);
                    isLinkAdded = true;
                }
                else if (isRightAuthorised && GetRightCase(i, j).GetNbConnexion() < 3 &&
                         GetRightCase(i, j).GetConnectedTo() != MazeArray[i][j].GetConnectedTo() && tabChoice == 2)
                {
                    ConnectCaseWithRightCase(i, j);
                    isLinkAdded = true;
                }
                else if (isBottomAuthorised && GetBottomCase(i, j).GetNbConnexion() < 3 &&
                         GetBottomCase(i, j).GetConnectedTo() != MazeArray[i][j].GetConnectedTo() && tabChoice == 3)
                {
                    ConnectCaseWithBottomCase(i, j);
                    isLinkAdded = true;
                }
                nbBCL++;
            }
        }
        // utilise la valeur connectedTo des cases pour passer les cases de la valeur {{from}} a la valeur {{to}}
        private void ChangeAllCase(int from, int to)
        {
            for (int i = 0; i < MazeArray.Length; i++)
            {
                for (int j = 0; j < MazeArray[i].Length; j++)
                {
                    if (MazeArray[i][j].GetConnectedTo() == from)
                    {
                        MazeArray[i][j].SetConnectedTo(to);
                    }
                }
            }
        }
        //fait la connexion entre la case du haut et la case cible
        private void ConnectCaseWithTopCase(int posx, int posy)
        {
            Case target = MazeArray[posx][posy];
            Case linkCase = MazeArray[posx - 1][posy];
            target.SetTop(true);
            linkCase.SetBottom(true);
            if (target.GetConnectedTo() < linkCase.GetConnectedTo())
            {
                ChangeAllCase(linkCase.GetConnectedTo(), target.GetConnectedTo());
            }
            else
            {
                ChangeAllCase(target.GetConnectedTo(), linkCase.GetConnectedTo());
            }
        }
        //fait la connexion entre la case du bas et la case cible
        private void ConnectCaseWithBottomCase(int posx, int posy)
        {
            Case target = MazeArray[posx][posy];
            Case linkCase = MazeArray[posx + 1][posy];
            target.SetBottom(true);
            linkCase.SetTop(true);
            if (target.GetConnectedTo() < linkCase.GetConnectedTo())
            {
                ChangeAllCase(linkCase.GetConnectedTo(), target.GetConnectedTo());
            }
            else
            {
                ChangeAllCase(target.GetConnectedTo(), linkCase.GetConnectedTo());
            }
        }
        //fait la connexion entre la case de gauche et la case cible
        private void ConnectCaseWithLeftCase(int posx, int posy)
        {
            Case target = MazeArray[posx][posy];
            Case linkCase = MazeArray[posx][posy - 1];
            target.SetLeft(true);
            linkCase.SetRight(true);
            if (target.GetConnectedTo() < linkCase.GetConnectedTo())
            {
                ChangeAllCase(linkCase.GetConnectedTo(), target.GetConnectedTo());
            }
            else
            {
                ChangeAllCase(target.GetConnectedTo(), linkCase.GetConnectedTo());
            }
        }
        //fait la connexion entre la case de droite et la case cible
        private void ConnectCaseWithRightCase(int posx, int posy)
        {
            Case target = MazeArray[posx][posy];
            Case linkCase = MazeArray[posx][posy + 1];
            target.SetRight(true);
            linkCase.SetLeft(true);
            if (target.GetConnectedTo() < linkCase.GetConnectedTo())
            {
                ChangeAllCase(linkCase.GetConnectedTo(), target.GetConnectedTo());
            }
            else
            {
                ChangeAllCase(target.GetConnectedTo(), linkCase.GetConnectedTo());
            }
        }

        //Recuperer la case a gauche de la case cible
        private Case GetLeftCase(int posx, int posy)
        {
            return MazeArray[posx][posy - 1];
        }
        //Recuperer la case a droite de la case cible
        private Case GetRightCase(int posx, int posy)
        {
            return MazeArray[posx][posy + 1];
        }
        //Recuperer la case en haut de la case cible
        private Case GetTopCase(int posx, int posy)
        {
            return MazeArray[posx - 1][posy];
        }
        //Recuperer la case en bas de la case cible
        private Case GetBottomCase(int posx, int posy)
        {
            return MazeArray[posx + 1][posy];
        }

        //Recupere la coordonne X de la case de depart
        public int GetStartX()
        {
            return startX;
        }
        //Recupere la coordonne Y de la case de depart
        public int GetStartY()
        {
            return startY;
        }

        //Longueur du tableau en X
        public int GetLenX()
        {
            return x;
        }
        //Longueur du tableau en Y
        public int GetLenY()
        {
            return y;
        }

        //Recupere la coordonne X de la case de fin
        public int GetEndX()
        {
            return endX;
        }
        //Recupere la coordonne Y de la case de fin
        public int GetEndY()
        {
            return endY;
        }
        
        //Renvoie le tableau du labyrinthe
        public Case[][] GetMazeArray()
        {
            return MazeArray;
        }
    }
}