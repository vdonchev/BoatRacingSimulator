namespace BoatRacingSimulator.Interfaces
{
    public interface IBoatSimulatorDatabase
    {
        IRepository<IBoatBase> Boats { get; }

        IRepository<IBoatEngineBase> Engines { get; }
    }
}