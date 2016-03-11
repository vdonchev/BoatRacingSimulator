namespace BoatRacingSimulator.Models
{
    using System.Collections.Generic;
    using Exceptions;
    using Interfaces;
    using Utility;

    public class Race : IRace
    {
        private int distance;

        public Race(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats)
        {
            this.Distance = distance;
            this.WindSpeed = windSpeed;
            this.OceanCurrentSpeed = oceanCurrentSpeed;
            this.AllowsMotorboats = allowsMotorboats;
            this.RegisteredBoats = new Dictionary<string, IBoatBase>();
        }

        public int Distance
        {
            get
            {
                return this.distance;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Distance");
                this.distance = value;
            }
        }

        public int WindSpeed { get; private set; }

        public int OceanCurrentSpeed { get; private set; }

        public bool AllowsMotorboats { get; private set; }

        protected Dictionary<string, IBoatBase> RegisteredBoats { get; set; }

        public void AddParticipant(IBoatBase boat)
        {
            if (this.RegisteredBoats.ContainsKey(boat.Model))
            {
                throw new DuplicateModelException(Constants.DuplicateModelMessage);
            }

            this.RegisteredBoats.Add(boat.Model, boat);
        }

        public IList<IBoatBase> GetParticipants()
        {
            return new List<IBoatBase>(this.RegisteredBoats.Values);
        }
    }
}
