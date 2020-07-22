using System;
using Xunit;
using MarsRover;

namespace MarsRover.Tests
{

    //Contains XUnit tests for Mars Rover, tests input from files in TestInputFiles
    public class MarsRoverUnitTests
    {
        private string testsFolderPath = "../../../TestInputFiles/";

        //Sample input from docs
        [Fact]
        public void BasicTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "Basic");

            Assert.Equal("1 3 N\n5 1 E", output);

        }

        //Has an empty first line, the rest is correct format
        [Fact]
        public void BlankFirstLineTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "BlankFirstLine");

            Assert.Equal("1 3 N\n5 1 E", output);

        }

        //Rover circles the plateau, keeping to the outermost edges
        [Fact]
        public void CirclePlateauTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "CirclePlateau");

            Assert.Equal("0 0 S", output);

        }

        //A plateau with a max y value of 0
        [Fact]
        public void SingleRowPlateauTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "SingleRowPlateau");

            Assert.Equal("10 0 E", output);

        }
        
        //Checks invalid file input
        [Theory]
        [InlineData("InvalidFormatPlateauSize")]
        [InlineData("InvalidFormatRoverCommands")]
        [InlineData("InvalidFormatRoverInit")]
        [InlineData("EmptyFile")]
        public void InvalidFormatTest(string fileName)
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + fileName);

            Assert.Equal("", output);

        }        

        //Gives 1000 commands to one rover
        [Fact]
        public void OneRoverManyCommandsTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "OneRoverManyCommands");

            Assert.Equal("0 0 N", output);

        }

        //Rover goes out of bounds of plateau
        [Fact]
        public void OutOfBoundsTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "OutOfBounds");

            Assert.Equal("", output);

        }

        //A plateau with max x and y of 0
        [Fact]
        public void SingleSpotTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "SingleSpot");

            Assert.Equal("0 0 S", output);

        }

        //Tests input with 5000 rovers
        [Fact]
        public void ManyRoversTest()
        {
            string output = MarsRoverDataProcessor.ProcessRoverFile(testsFolderPath + "ManyRovers");

            string correctOutput = "";

            for(int i = 0; i < 2500; i++){
                if(i != 0){
                    correctOutput += "\n";
                }
                correctOutput += "1 3 N\n5 1 E";
            }

            Assert.Equal(correctOutput, output );

        }


    }
}
