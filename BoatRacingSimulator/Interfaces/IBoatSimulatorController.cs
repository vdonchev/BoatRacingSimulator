namespace BoatRacingSimulator.Interfaces
{
    using Enumerations;

    public interface IBoatSimulatorController
    {
        IRace CurrentRace { get; }

        IBoatSimulatorDatabase Database { get; }

        /// <summary>
        /// Creates an engine for a boat.
        /// </summary>
        /// <param name="model">The model of the engine</param>
        /// <param name="horsepower">The actual horsepower of the engine</param>
        /// <param name="displacement">The displacement value of the engine</param>
        /// <param name="engineType">The type of the engine to be created</param>
        /// <returns>Message that the engine is successfully created!</returns>
        string CreateBoatEngine(string model, int horsepower, int displacement, EngineType engineType);

        string CreateRowBoat(string model, int weight, int oars);

        string CreateSailBoat(string model, int weight, int sailEfficiency);

        string CreatePowerBoat(string model, int weight, string firstEngineModel, string secondEngineModel);

        string CreateYacht(string model, int weight, string engineModel, int cargoWeight);

        string OpenRace(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats);

        /// <summary>
        /// Signs up a boat for a race
        /// </summary>
        /// <param name="model">The model of the boat that will be signed t othe race</param>
        /// <returns>A message confirmg or not if the boat is successfully signed up for the race.</returns>
        string SignUpBoat(string model);

        string StartRace();

        string GetStatistic();
    }
}
