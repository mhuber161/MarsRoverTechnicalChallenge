using System;
using System.IO;
using System.Collections.Generic;

namespace MarsRover{

    //ProcessorState class contains the current plateau and rover being processed from input
    //It also has a string with the ending coordinates and heading of each rover encountered
    public class ProcessorState{
        public Plateau Plateau { get; set; }    //Current plateau
        public Rover Rover { get; set; }        //Current rover
        public string RoverEndValues { get; set; }  //string with ending coordinates and heading of each rover encountered

        public ProcessorState(){
            Plateau = new Plateau();
            Rover = new Rover();
            RoverEndValues = "";
        }

        //Adds current rover coordinates and heading to RoverEndValues
        public void UpdateRoverEndValues(){
            RoverEndValues += Rover.ToString();
            RoverEndValues += "\n";
        }        
    }

    //Contains methods to parse and process input for mars rovers
    //Keeps track of current rover and plateau object as internal variables
    public class MarsRoverDataProcessor{
        private ProcessorState state;

        List<IProcessLine> lineProcessors;

        public MarsRoverDataProcessor(){
            state = new ProcessorState();
            lineProcessors = new List<IProcessLine>();
            lineProcessors.Add(new ProcessMarsCommandsLine());
            lineProcessors.Add(new ProcessNewRoverLine());
            lineProcessors.Add(new ProcessPlateauSizeLine());
        }

        //The input is the file path to a file that contains plateau size, rover initialization data, and rover commands
        //Returns a string that contains final location and direction of all rovers from input file, each separated by a line break
        public string ProcessRoverFile(string filePath){
            try{
                using (StreamReader sr = new StreamReader(filePath)){
                    string line;
                    
                    //Process each line individually, update output string as we go
                    while((line = sr.ReadLine()) != null){
                        line = line.Trim();

                        ProcessRoverLine(line);
                    }

                    state.RoverEndValues = state.RoverEndValues.TrimEnd('\n','\r');
                    return state.RoverEndValues;
                }
            }
            catch(Exception e){
                LogManager.LogConsole("ERROR: " + e.Message);
                return "";
            }
        }

        //Checks format of one line of rover input and calls appropriate method for it
        private void ProcessRoverLine(string line){
            //Blank line, move on to next line
            if(line == ""){
                return;
            }

            foreach(IProcessLine processLine in lineProcessors){
                if(processLine.LineRegex.IsMatch(line)){
                    processLine.ProcessLine(line, state);
                    return;
                }
            }
            
            //No matching regex, invalid format
            throw new Exception("Invalid input line format");
        }    
    }
}