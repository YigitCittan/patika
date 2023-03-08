using System;
namespace patikaodev.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(String message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}