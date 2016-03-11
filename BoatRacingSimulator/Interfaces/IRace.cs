namespace BoatRacingSimulator.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines behavior for a single race in the boat challange
    /// </summary>
    public interface IRace
    {
        /// <summary>
        /// Holds the lenght of the race
        /// </summary>
        int Distance { get; }

        /// <summary>
        /// Holds the speed of the wind in the race
        /// </summary>
        int WindSpeed { get; }

        /// <summary>
        /// Holds the speed of the ocean in the race
        /// </summary>
        int OceanCurrentSpeed { get; }

        /// <summary>
        /// Defines if Motor driven boats are allowed to the race
        /// </summary>
        bool AllowsMotorboats { get; }

        /// <summary>
        /// Functionality to add a boat to the race
        /// </summary>
        /// <param name="boat">The boat to be added to the race</param>
        void AddParticipant(IBoatBase boat);

        /// <summary>
        /// Collect all participants of the race and return
        /// </summary>
        /// <returns>Returns all participants of the race</returns>
        IList<IBoatBase> GetParticipants();
    }
}
