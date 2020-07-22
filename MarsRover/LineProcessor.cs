using System;
using System.Text.RegularExpressions;


namespace MarsRover{

    //Interface for classes that handle a line of input
    public interface IProcessLine{
        Regex LineRegex { get; set; }      //Valid format for line
        void ProcessLine(string line, ProcessorState state);    //Line to be parsed and processed, and current state of Processor
    }

    //Handles line that contains rover commands L,R,M
    //After executing commands, update rover end values in state
    public class ProcessMarsCommandsLine : IProcessLine{

        public Regex LineRegex { get; set; }

        public ProcessMarsCommandsLine(){
            LineRegex = new Regex("^[LRM]+$");
        }
        
        public void ProcessLine(string line, ProcessorState state){
            try{
                foreach(char c in line){
                    switch(c){
                        case 'L':
                            state.Rover.TurnLeft();
                            LogManager.LogDebug("Rover turned left");
                            break;
                        case 'R':
                            state.Rover.TurnRight();
                            LogManager.LogDebug("Rover turned right");
                            break;
                        case 'M':
                            state.Rover.MoveForward();
                            LogManager.LogDebug("Rover moved forward");
                            break;
                        case ' ':
                        default:
                            break;
                    }
                }
                
                state.UpdateRoverEndValues();
                
            }
            catch(Exception e){
                throw new Exception("Problem processing rover commands: " + e.Message + ", line value: " + line);
            }
        }
    }

    //Handles line that contains max x and y values for plateau
    public class ProcessPlateauSizeLine : IProcessLine{

        public Regex LineRegex { get; set; }

        public ProcessPlateauSizeLine(){
            LineRegex = new Regex("^[0-9]+ [0-9]+$");
        }

        public void ProcessLine(string line, ProcessorState state){
            string[] vals = line.Split(" ");

            Plateau plateau;

            try{
                int xmax = Int32.Parse(vals[0]);
                int ymax = Int32.Parse(vals[1]);
                plateau = new Plateau(xmax, ymax);
                LogManager.LogDebug("Set up plateau max values x = " + xmax + ", y = " + ymax);

                state.Plateau = plateau;
            }
            catch(Exception){
                throw new Exception("Invalid plateau size format: " + line);
            }

        }
    }

    //Handles line that contains initial rover location and heading
    public class ProcessNewRoverLine : IProcessLine{

        public Regex LineRegex { get; set; }

        public ProcessNewRoverLine(){
            LineRegex = new Regex("^[0-9]+ [0-9]+ [NSEW]{1}$");
        }

        public void ProcessLine(string line, ProcessorState state){
            string[] vals = line.Split(" ");
            int xStart;
            int yStart;
            Direction dStart;

            try{
                xStart = Int32.Parse(vals[0]);
                yStart = Int32.Parse(vals[1]);
                dStart = DirectionStringConverter.StringToEnum(vals[2]);

                if( xStart > state.Plateau.GetXMax() || yStart > state.Plateau.GetYMax() ){
                    LogManager.LogDebug("Rover initialization out of bounds");
                    throw new Exception("Rover initialization out of bounds");
                }

                Rover rover = new Rover(xStart, yStart, dStart, state.Plateau);     

                LogManager.LogDebug("Initialized rover with values x = " + xStart + ", y = " + 
                    yStart + ", direction = " + DirectionStringConverter.EnumToString(dStart));

                state.Rover = rover;        //Update current rover in state
            }
            catch(Exception e){
                throw new Exception("Problem initializing new rover: " + e.Message + ", line value:" + line);
            }
        }
    }

}