using System;
namespace patikaodev.Services
{
    public class DBLogger : ILoggerService
    {
        public void Write(String message)
        {
            Console.WriteLine("[DBLogger] - " + message);
        }
    }
}