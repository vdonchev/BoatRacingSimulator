namespace BoatRacingSimulator.Tests
{
    using System;
    using Controllers;
    using Database;
    using Enumerations;
    using Exceptions;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StartRaceTests
    {
        private IBoatSimulatorDatabase db;
        private IBoatSimulatorController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            this.db = new BoatSimulatorDatabase();
            this.controller = new BoatSimulatorController(this.db);

            int distance = 1000;
            int windSpeed = 10;
            int oceanCurrentSpeed = 5;
            bool allowsMotorboats = true;

            this.controller.OpenRace(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);
            
            this.controller.CreateBoatEngine("GPH01", 250, 100, EngineType.Jet);
            this.controller.CreateBoatEngine("GPH02", 150, 150, EngineType.Sterndrive);

            // CreateSailBoat\SailBoatPro\200\98
            this.controller.CreateRowBoat("Rower15", 450, 6);
            this.controller.CreatePowerBoat("PB150", 2200, "GPH01", "GPH02");
            this.controller.CreateSailBoat("SailBoatPro", 200, 98);
        }

        [TestMethod]
        [ExpectedException(typeof(NoSetRaceException))]
        public void NewRace_RaceNotSet_ShouldThrow()
        {
            this.controller = new BoatSimulatorController(this.db);
            this.controller.StartRace();
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientContestantsException))]
        public void NewRace_ZeroContestants_ShouldThrow()
        {
            this.controller.StartRace();
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientContestantsException))]
        public void NewRace_InsufficientContestants_ShouldThrow()
        {
            this.controller.SignUpBoat("Rower15");
            this.controller.SignUpBoat("PB150");

            this.controller.StartRace();
        }

        [TestMethod]
        public void NewRace_AllSet_AllFinishes_ShouldReturnStandings()
        {
            this.controller.SignUpBoat("Rower15");
            this.controller.SignUpBoat("PB150");
            this.controller.SignUpBoat("SailBoatPro");

            var result = this.controller.StartRace();

            var expected = "First place: PowerBoat Model: PB150 Time: 2.85 sec" + Environment.NewLine;
            expected += "Second place: RowBoat Model: Rower15 Time: 6.45 sec" + Environment.NewLine;
            expected += "Third place: SailBoat Model: SailBoatPro Time: Did not finish!";

            Assert.AreEqual(expected, result);
        }
    }
}
