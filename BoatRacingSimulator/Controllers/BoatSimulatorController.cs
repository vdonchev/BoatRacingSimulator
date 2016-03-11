namespace BoatRacingSimulator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Enumerations;
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Boats;
    using Models.Engines;
    using Utility;

    public class BoatSimulatorController : IBoatSimulatorController
    {
        public BoatSimulatorController(IBoatSimulatorDatabase database)
        {
            this.Database = database;
        }

        public IRace CurrentRace { get; private set; }

        public IBoatSimulatorDatabase Database { get; private set; }

        /// <summary>
        /// Creates an engine for a boat.
        /// </summary>
        /// <param name="model">The model of the engine</param>
        /// <param name="horsepower">The actual horsepower of the engine</param>
        /// <param name="displacement">The displacement value of the engine</param>
        /// <param name="engineType">The type of the engine to be created</param>
        /// <returns>Message that the engine is successfully created!</returns>
        public string CreateBoatEngine(
            string model,
            int horsepower,
            int displacement,
            EngineType engineType)
        {
            IBoatEngineBase engine;
            switch (engineType)
            {
                case EngineType.Jet:
                    engine = new JetEngine(model, horsepower, displacement);
                    break;
                case EngineType.Sterndrive:
                    engine = new SterndriveEngine(model, horsepower, displacement);
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.Database.Engines.Add(engine);

            return string.Format(
                "Engine model {0} with {1} HP and displacement {2} cm3 created successfully.",
                model,
                horsepower,
                displacement);
        }

        public string CreateRowBoat(string model, int weight, int oars)
        {
            IBoatBase boat = new RowBoat(model, weight, oars);
            this.Database.Boats.Add(boat);

            return string.Format("Row boat with model {0} registered successfully.", model);
        }

        public string CreateSailBoat(string model, int weight, int sailEfficiency)
        {
            IBoatBase boat = new SailBoat(model, weight, sailEfficiency);
            this.Database.Boats.Add(boat);

            return string.Format("Sail boat with model {0} registered successfully.", model);
        }

        public string CreatePowerBoat(
            string model,
            int weight,
            string firstEngineModel,
            string secondEngineModel)
        {
            IBoatEngineBase firstEngine = this.Database.Engines.GetItem(firstEngineModel);
            IBoatEngineBase secondEngine = this.Database.Engines.GetItem(secondEngineModel);
            IBoatBase boat = new PowerBoat(model, weight, firstEngine, secondEngine);

            this.Database.Boats.Add(boat);

            return string.Format("Power boat with model {0} registered successfully.", model);
        }

        public string CreateYacht(
            string model,
            int weight,
            string engineModel,
            int cargoWeight)
        {
            IBoatEngineBase engine = this.Database.Engines.GetItem(engineModel);
            IBoatBase boat = new Yacht(model, weight, cargoWeight, engine);
            this.Database.Boats.Add(boat);

            return string.Format("Yacht with model {0} registered successfully.", model);
        }

        public string OpenRace(
            int distance,
            int windSpeed,
            int oceanCurrentSpeed,
            bool allowsMotorboats)
        {
            IRace race = new Race(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);

            this.ValidateRaceIsEmpty();
            this.CurrentRace = race;

            return string.Format(
                    "A new race with distance {0} meters, wind speed {1} m/s and ocean current speed {2} m/s has been set.",
                    distance,
                    windSpeed,
                    oceanCurrentSpeed);
        }

        /// <summary>
        /// Signs up a boat for a race
        /// </summary>
        /// <param name="model">The model of the boat that will be signed t othe race</param>
        /// <returns>A message confirmg or not if the boat is successfully signed up for the race.</returns>
        public string SignUpBoat(string model)
        {
            IBoatBase boat = this.Database.Boats.GetItem(model);

            this.ValidateRaceIsSet();
            if (!this.CurrentRace.AllowsMotorboats && boat is MotorBoatBase)
            {
                throw new ArgumentException(Constants.IncorrectBoatTypeMessage);
            }

            this.CurrentRace.AddParticipant(boat);
            return string.Format("Boat with model {0} has signed up for the current Race.", model);
        }

        public string StartRace()
        {
            this.ValidateRaceIsSet();
            var participants = this.CurrentRace.GetParticipants();
            if (participants.Count < 3)
            {
                throw new InsufficientContestantsException(Constants.InsufficientContestantsMessage);
            }

            var first = this.FindFastest(participants);
            participants.Remove(first.Value);
            var second = this.FindFastest(participants);
            participants.Remove(second.Value);
            var third = this.FindFastest(participants);
            participants.Remove(third.Value);

            var result = new StringBuilder();
            result.AppendLine(string.Format(
                "First place: {0} Model: {1} Time: {2}",
                first.Value.GetType().Name,
                first.Value.Model,
                first.Key <= 0 ? "Did not finish!" : first.Key.ToString("0.00") + " sec"));
            result.AppendLine(string.Format(
                "Second place: {0} Model: {1} Time: {2}",
                second.Value.GetType().Name,
                second.Value.Model,
                first.Key <= 0 ? "Did not finish!" : second.Key.ToString("0.00") + " sec"));
            result.Append(string.Format(
                "Third place: {0} Model: {1} Time: {2}",
                third.Value.GetType().Name,
                third.Value.Model,
                third.Key <= 0 ? "Did not finish!" : third.Key.ToString("0.00") + " sec"));

            this.CurrentRace = null;

            return result.ToString();
        }

        public string GetStatistic()
        {
            var participants = this.CurrentRace.GetParticipants();
            var percentage = participants
                .GroupBy(b => b.GetType())
                .Select(g => new
                {
                    Kor = g.Key.Name,
                    Counts = ((double)g.Count() / participants.Count) * 100
                })
                .OrderBy(b => b.Kor)
                .ToList();

            var res = new StringBuilder();
            foreach (var p in percentage)
            {
                res.AppendLine($"{p.Kor} -> {p.Counts:F2}%");
            }

            return res.ToString().Trim();
        }

        // POSSIBLE BOTTLE NECK: Method is called for each top 3 member
        private KeyValuePair<double, IBoatBase> FindFastest(IList<IBoatBase> participants)
        {
            double bestTime = double.MaxValue;
            IBoatBase winner = null;

            foreach (var participant in participants)
            {
                var speed = participant.CalculateRaceSpeed(this.CurrentRace);
                var time = this.CurrentRace.Distance / speed;
                if (time < bestTime && time > 0)
                {
                    bestTime = time;
                    winner = participant;
                }
            }

            if (winner == null)
            {
                bestTime = -1;
                winner = participants.FirstOrDefault();
            }

            return new KeyValuePair<double, IBoatBase>(bestTime, winner);
        }

        private void ValidateRaceIsSet()
        {
            if (this.CurrentRace == null)
            {
                throw new NoSetRaceException(Constants.NoSetRaceMessage);
            }
        }

        private void ValidateRaceIsEmpty()
        {
            if (this.CurrentRace != null)
            {
                throw new RaceAlreadyExistsException(Constants.RaceAlreadyExistsMessage);
            }
        }
    }
}
