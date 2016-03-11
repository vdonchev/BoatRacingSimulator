namespace BoatRacingSimulator.Interfaces
{
    public interface IBoatBase : IModelable
    {
        int Weight { get; }

        double CalculateRaceSpeed(IRace race);
    }
}