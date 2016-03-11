namespace BoatRacingSimulator.IO
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string message, params object[] @params)
        {
            Console.WriteLine(message, @params);
        }
    }
}