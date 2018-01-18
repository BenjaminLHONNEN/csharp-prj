namespace projetPOO
{
    public class Case
    {
        //s'il y a un mur true autrement false
        private bool top = false;
        private bool bottom = false;
        private bool left = false;
        private bool right = false;
        //position de la casse dans le tableau
        private int posx;
        private int posy;
        //variable utilise pour faire l'algorithme de generation de labyrinthe parfait
        private int connectedTo;
        //nombre de case connecte a la case
        private int nbConnexion = 0;
        //Valeur pour l'image (utilise pour l'affichage)
        private int Value;

        public Case(int value,int posx,int posy,int connectedTo)
        {
            this.connectedTo = connectedTo;
            this.posx = posx;
            this.posy = posy;
            Value = value;
            FromValueToBool();
        }

        
        //Getter / Setter
        public int GetValue()
        {
            return Value;
        }
        public void SetValue(int val)
        {
            Value = val;
            FromValueToBool();
        }
        public bool GetTop()
        {
            return top;
        }
        public void SetTop(bool b)
        {
            top = b;
            nbConnexion++;
            FromBoolToValue();
        }
        public bool GetBottom()
        {
            return bottom;
        }
        public void SetBottom(bool b)
        {
            bottom = b;
            nbConnexion++;
            FromBoolToValue();
        }
        public bool GetRight()
        {
            return right;
        }
        public void SetRight(bool b)
        {
            right = b;
            nbConnexion++;
            FromBoolToValue();
        }
        public bool GetLeft()
        {
            return left;
        }
        public void SetLeft(bool b)
        {
            left = b;
            nbConnexion++;
            FromBoolToValue();
        }
        public int GetPosX()
        {
            return posx;
        }
        public int GetPosY()
        {
            return posy;
        }
        public int GetConnectedTo()
        {
            return connectedTo;
        }
        public void SetConnectedTo(int i)
        {
            connectedTo = i;
        }
        public int GetNbConnexion()
        {
            return nbConnexion;
        }
        public void SetNbConnexion(int i)
        {
            nbConnexion = i;
        }

        
        //Fonction pour avoir le numero d'image correspondant
        private void FromBoolToValue()
        {
            if (top && bottom && left && right)
            {
                Value = 0;
            }
            else if (bottom && left && right)
            {
                Value = 1;
            }
            else if (top && bottom && right)
            {
                Value = 2;
            }
            else if (top && left && right)
            {
                Value = 3;
            }
            else if (top && bottom && left )
            {
                Value = 4;
            }
            else if (bottom && right )
            {
                
                Value = 5;
            }
            else if (bottom && left)
            {
                Value = 6;
            }
            else if (left && right)
            {
                Value = 7;
            }
            else if (top && right)
            {
                Value = 8;
            }
            else if (top && bottom)
            {
                Value = 9;
            }
            else if (top && left)
            {
                Value = 10;
            }
            else if (bottom)
            {
                Value = 11;
            }
            else if (left)
            {
                Value = 12;
            }
            else if (top)
            {
                Value = 13;
            }
            else if (right)
            {
                Value = 14;
            }
            else
            {
                Value = 15;
            }
        }
        //Fonction pour avoir les boolean a partir d'un numero d'image
        private void FromValueToBool()
        {
            if (Value == 0)
            {
                top = true;
                bottom = true;
                left = true;
                right = true;
            }
            else if (Value == 1)
            {
                bottom = true;
                left = true;
                right = true;
            }
            else if (Value == 2)
            {
                top = true;
                bottom = true;
                right = true;
            }
            else if (Value == 3)
            {
                top = true;
                left = true;
                right = true;
            }
            else if (Value == 4)
            {
                top = true;
                bottom = true;
                left = true;
            }
            else if (Value == 5)
            {
                bottom = true;
                right = true;
            }
            else if (Value == 6)
            {
                bottom = true;
                left = true;
            }
            else if (Value == 7)
            {
                left = true;
                right = true;
            }
            else if (Value == 8)
            {
                top = true;
                right = true;
            }
            else if (Value == 9)
            {
                top = true;
                bottom = true;
            }
            else if (Value == 10)
            {
                top = true;
                left = true;
            }
            else if (Value == 11)
            {
                bottom = true;
            }
            else if (Value == 12)
            {
                left = true;
            }
            else if (Value == 13)
            {
                top = true;
            }
            else if (Value == 14)
            {
                right = true;
            }
            else if (Value == 15)
            {
                //none
            }
        }
    }
}