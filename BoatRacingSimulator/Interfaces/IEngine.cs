namespace BoatRacingSimulator.Interfaces
{
    public interface IEngine
    {
        ICommandHandler CommandHandler { get; }

        void Run();
    }
}