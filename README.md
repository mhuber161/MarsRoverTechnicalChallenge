# Mars Rover Technical Challenge
.Net Core console app implementation of the Mars Rover Technical Challenge: https://code.google.com/archive/p/marsrovertechchallenge/

## Installation
Download and install .Net Core 3.1 from Microsoft

## Usage

To run all tests in MarsRover.tests:
dotnet test

To run program with a user-defined input file (Example files located in MarsRover.Tests/TestInputFiles):
dotnet run -- --file <filepath>


### Assumptions/Bad input handling
- Rovers are assumed to not have to deal with collision with each other, i.e. multiple rovers can be in the same spot.
- If a rover goes outside the plateau area, the program will end execution.
- If input does not follow correct format, the program returns an empty string and prints an error message
- Blank lines are skipped and don't affect execution
- The plateau has maximum x and y coordinates of Int32.MaxValue