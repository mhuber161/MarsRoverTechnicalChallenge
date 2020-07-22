using System;
using System.Collections.Generic;
using System.Diagnostics;


//This file contains the Direction enum and methods to convert enum to string and vice versa
namespace MarsRover{
    public enum Direction{
        North,
        South,
        East,
        West
    }

    public static class DirectionHelper{
        public static Direction StringToDirection(string s){
            switch(s){
                case "N":
                    return Direction.North;
                case "S":
                    return Direction.South;
                case "W":
                    return Direction.West;
                case "E":
                    return Direction.East;
                default:
                    Debug.WriteLine("Invalid direction string");
                    throw new Exception("Invalid direction string");
            }
        }

        public static string DirectionToString(Direction d){
            switch(d){
                case Direction.North:
                    return "N";
                case Direction.South:
                    return "S";
                case Direction.West:
                    return "W";
                case Direction.East:
                    return "E";
                default:
                    Debug.WriteLine("Invalid direction enum");
                    throw new Exception("Invalid direction enum");
            }
        }
    }
}