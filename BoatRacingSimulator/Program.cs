namespace BoatRacingSimulator
{
    using Controllers;
    using Core;
    using Database;
    using Interfaces;
    using IO;

    public class Program
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IBoatSimulatorDatabase database = new BoatSimulatorDatabase();
            IBoatSimulatorController boatSimulatorController = new BoatSimulatorController(database);
            ICommandHandler commandHandler = new CommandHandler(boatSimulatorController);

            IEngine engine = new Engine(
                reader,
                writer,
                commandHandler);

            engine.Run();
        }
    }
}
