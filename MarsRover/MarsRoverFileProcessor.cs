using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MarsRover{

    //Contains methods to parse and process input for mars rovers
    //Keeps track of current rover and plateau object as internal variables
    public class MarsRoverDataProcessor{
        private Plateau currentPlateau;
        private Rover currentRover;

        public MarsRoverDataProcessor(){
            currentPlateau = new Plateau();
            currentRover = new Rover();
        }

        //The input is the file path to a file that contains plateau size, rover initialization data, and rover commands
        //Returns a string that contains final location and direction of all rovers from input file, each separated by a line break
        public string ProcessRoverFile(string filePath){
            string roverData = "";  //contains all output from end coordinates and direction from each rover
            try{
                using (StreamReader sr = new StreamReader(filePath)){
                    string line;
                    
                    //Process each line individually, update output string as we go
                    while((line = sr.ReadLine()) != null){                        
                        line = line.Trim();

                        roverData += ProcessRoverLine(line);
                    }

                    roverData = roverData.TrimEnd('\n');
                }
            }
            catch(Exception e){
                Console.WriteLine("ERROR: " + e.Message);
                return "";
            }

            return roverData;
        }

        //Checks format of one line of rover input and calls appropriate method for it
        //Initializes current rover and plateau object as necessary
        //Returns string with final rover location and direction if rover commands ran, else return empty string
        private string ProcessRoverLine(string line){

            //three types of input lines, use regex to figure out which type it is and verify correct format
            Regex plateauSizeRgx = new Regex("^[0-9]+ [0-9]+$");
            Regex newRoverRgx = new Regex("^[0-9]+ [0-9]+ [NSEW]{1}$");
            Regex roverCommandsRgx = new Regex("^[LRM]+$");

            string roverData = "";

            if(line == ""){
                //Blank line, move on to next line
                return "";
            }
            else if(plateauSizeRgx.IsMatch(line)){
                //Initialize max x and max y values for plateau size
                currentPlateau = InitializePlateauSize(line);
            }
            else if(newRoverRgx.IsMatch(line)){
                //Create and initialize new rover
                currentRover = InitializeNewRover(line);
            }
            else if(roverCommandsRgx.IsMatch(line)){
                //execute rover commands in order, then add rover end coordinates and direction to output string
                ProcessRoverCommands(line);

                roverData += currentRover.GetRoverStatsString();
                roverData += "\n";
            }
            else{
                throw new Exception("Invalid input line format");
            }

            return roverData;

        }

        //Sets maximum x and y values for plateau from input string
        private Plateau InitializePlateauSize(string s){
            string[] vals = s.Split(" ");

            Plateau plateau;

            try{
                int xmax = Int32.Parse(vals[0]);
                int ymax = Int32.Parse(vals[1]);
                plateau = new Plateau(xmax, ymax);
                Debug.WriteLine("Set up plateau max values x = " + xmax + ", y = " + ymax);

                return plateau;
            }
            catch(Exception){
                throw new Exception("Invalid plateau size format: " + s);
            }
        }

        //Creates and returns new rover object with values from input string
        private Rover InitializeNewRover(string s){
            string[] vals = s.Split(" ");
            int xStart;
            int yStart;
            Direction dStart;

            try{
                xStart = Int32.Parse(vals[0]);
                yStart = Int32.Parse(vals[1]);
                dStart = DirectionHelper.StringToDirection(vals[2]);

                if( xStart > currentPlateau.GetXMax() || yStart > currentPlateau.GetYMax() ){
                    Debug.WriteLine("Rover initialization out of bounds");
                    throw new Exception("Rover initialization out of bounds");
                }

                Rover rover = new Rover(xStart, yStart, dStart, currentPlateau);

                Debug.WriteLine("Initialized rover with values x = " + xStart + ", y = " + 
                    yStart + ", direction = " + DirectionHelper.DirectionToString(dStart));

                return rover;
            }
            catch(Exception e){
                throw new Exception("Problem initializing new rover: " + e.Message + ", line value:" + s);
            }            
        }

        //Goes through each L,R,M command from input string and calls corresponding rover commands
        private void ProcessRoverCommands(string s){
            try{
                foreach(char c in s){
                    switch(c){
                        case 'L':
                            currentRover.TurnLeft();
                            Debug.WriteLine("Rover turned left");
                            break;
                        case 'R':
                            currentRover.TurnRight();
                            Debug.WriteLine("Rover turned right");
                            break;
                        case 'M':
                            currentRover.MoveForward();
                            Debug.WriteLine("Rover moved forward");
                            break;
                        case ' ':
                        default:
                            break;
                    }
                }
            }
            catch(Exception e){
                throw new Exception("Problem processing rover commands: " + e.Message + ", line value: " + s);
            }
        }

    }
}