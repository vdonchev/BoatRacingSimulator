namespace BoatRacingSimulator.Database
{
    using Interfaces;

    public class BoatSimulatorDatabase : IBoatSimulatorDatabase
    {
        public BoatSimulatorDatabase()
        {
            this.Boats = new Repository<IBoatBase>();
            this.Engines = new Repository<IBoatEngineBase>();
        }

        public IRepository<IBoatBase> Boats { get; private set; }

        public IRepository<IBoatEngineBase> Engines { get; private set; }
    }
}
