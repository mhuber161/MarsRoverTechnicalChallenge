using System;
using System.Diagnostics;

namespace MarsRover
{

    //The Rover class keeps track of the rover's position and direction and provides methods to change them
    public class Rover
    {
        private int xPos;   //Current position of rover on x axis, minimum of 0, maximum of 
        private int yPos;   //Current position of rover on y axis

        private Direction direction;    //Current direction the rover is facing

        private Plateau plateau;    //Plateau the rover is on

        public Rover(){
            xPos = 0;
            yPos = 0;
            direction = Direction.North;
            plateau = new Plateau();
        }

        //Initializes the rover to initial values. This should be called before giving the rover commands
        public Rover(int x, int y, Direction d, Plateau p){
            xPos = x;
            yPos = y;
            direction = d;
            plateau = p;
        }

        //returns string representation of current rover position and direction
        //should be called after rover received all commands
        public string GetRoverStatsString(){
            return xPos + " " + yPos + " " + DirectionHelper.DirectionToString(direction);
        }

        //The turn left command turns the rover 90 degrees counterclockwise
        public void TurnLeft(){
            switch(direction){
                case Direction.North:
                    direction = Direction.West;
                    break;
                case Direction.South:
                    direction = Direction.East;
                    break;
                case Direction.West:
                    direction = Direction.South;
                    break;
                case Direction.East:
                    direction = Direction.North;
                    break;
            }
        }
        
        //The turn right command turns the rover 90 degrees clockwise
        public void TurnRight(){
            switch(direction){
                case Direction.North:
                    direction = Direction.East;
                    break;
                case Direction.South:
                    direction = Direction.West;
                    break;
                case Direction.West:
                    direction = Direction.North;
                    break;
                case Direction.East:
                    direction = Direction.South;
                    break;
            }
        }

        //The move forward command moves the rover one unit in the direction it's facing
        public void MoveForward(){
            switch(direction){
                case Direction.North:
                    yPos++;
                    break;
                case Direction.South:
                    yPos--;
                    break;
                case Direction.West:
                    xPos--;
                    break;
                case Direction.East:
                    xPos++;
                    break;
            }

            if(yPos > plateau.GetYMax() || yPos < 0 || xPos > plateau.GetXMax() || xPos < 0){
                Debug.WriteLine("Rover is out of bounds, current position: x = " + xPos + ", y = " + yPos);
                throw new Exception("Rover is out of bounds, current position: x = " + xPos + ", y = " + yPos);
            }
        }
    }
}
