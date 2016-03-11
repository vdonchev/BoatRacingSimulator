namespace BoatRacingSimulator.Core
{
    using System;
    using System.Linq;
    using Interfaces;

    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        public Engine(
            IReader reader,
            IWriter writer,
            ICommandHandler commandHandler)
        {
            this.reader = reader;
            this.writer = writer;
            this.CommandHandler = commandHandler;
        }

        public ICommandHandler CommandHandler { get; private set; }

        public void Run()
        {
            while (true)
            {
                string line = this.reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var tokens = line.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var name = tokens[0];
                var parameters = tokens.Skip(1).ToArray();

                try
                {
                    string commandResult = this.CommandHandler.ExecuteCommand(name, parameters);
                    this.writer.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
