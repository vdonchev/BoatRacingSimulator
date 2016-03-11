namespace BoatRacingSimulator.Tests
{
    using Controllers;
    using Database;
    using Exceptions;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OpenRaceTests
    {
        private IBoatSimulatorDatabase db;
        private IBoatSimulatorController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            this.db = new BoatSimulatorDatabase();
            this.controller = new BoatSimulatorController(this.db);
        }

        [TestMethod]
        public void NewRace_EmptyRace_ShouldAddNewRace()
        {
            int distance = 50;
            int windSpeed = 150;
            int oceanCurrentSpeed = 5;
            bool allowsMotorboats = true;

            var result = this.controller.OpenRace(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);

            var expectedOutput =
                $"A new race with distance {distance} meters, wind speed {windSpeed} m/s and ocean current speed {oceanCurrentSpeed} m/s has been set.";

            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        [ExpectedException(typeof(RaceAlreadyExistsException))]
        public void NewRace_WithAlreadyExistingRace_ShouldThrow()
        {
            int distance = 50;
            int windSpeed = 150;
            int oceanCurrentSpeed = 5;
            bool allowsMotorboats = true;

            this.controller.OpenRace(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);
            this.controller.OpenRace(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);
        }
    }
}
