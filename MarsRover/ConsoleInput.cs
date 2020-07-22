using System;

namespace MarsRover{
    
    //Contains method for a console app to run
    //App accepts user-specified file as input
    class MarsRoverConsole{

        static int Main(string[] args){

            for(int i = 0; i < args.Length; i++){

                //Run program with custom user input file from specified filepath
                if(args[i] == "--file"){
                    try{
                        if( i + 1 == args.Length){
                            throw new Exception("No file specified");
                        }
                        string output = MarsRoverDataProcessor.ProcessRoverFile(args[i+1]);
                        Console.WriteLine(output);
                    }
                    catch(Exception e){
                        Console.WriteLine("Failed to process input: " + e.Message);
                        return 1;
                    }
                    
                    return 0;
                }
                else if (args[i] == "--help"){
                    Console.WriteLine("Mars Rover Application");
                    Console.WriteLine("Usage: MarsRover {option} [argument]");
                    Console.WriteLine("Options:");
                    Console.WriteLine(" --help                  Displays this help screen");
                    Console.WriteLine(" --file <filepath>       Runs the Mars Rover app with file from <filepath> as input");
                    return 0;
                }
            }

            Console.WriteLine("No input specified, run with --help for instructions");
            return 1;
        }
    }
}