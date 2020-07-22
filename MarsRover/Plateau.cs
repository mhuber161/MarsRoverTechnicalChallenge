using System;

namespace MarsRover{

    //The plateau class contains the maximum x and y values of a plateau and has get methods to access them
    public class Plateau{
        private int xMax;
        private int yMax;

        public Plateau(){
            xMax = 0;
            yMax = 0;
        }

        public Plateau(int xmax, int ymax){
            xMax = xmax;
            yMax = ymax;
        }

        public int GetXMax(){
            return xMax;
        }

        public int GetYMax(){
            return yMax;
        }
    }

    interface IOnPlateau{
        public Plateau Plateau {get; set;}
    }

}