using System;
using System.Diagnostics;

//Class for logging messages to console or debug
public static class LogManager{

    private static ILogger logger;
    public static void LogConsole(string message){
        logger = new ConsoleLogger();
        logger.LogString(message);
    }

    public static void LogDebug(string message){
        logger = new DebugLogger();
        logger.LogString(message);
    }
}

interface ILogger{
    void LogString(string message);
}



public class ConsoleLogger : ILogger{
    public void LogString(string message){
        Console.WriteLine(message);
    }
}

public class DebugLogger : ILogger{
    public void LogString(string message){
        Debug.WriteLine(message);
    }
}