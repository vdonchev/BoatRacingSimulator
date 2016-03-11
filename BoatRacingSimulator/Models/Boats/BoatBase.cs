namespace BoatRacingSimulator.Models.Boats
{
    using Interfaces;
    using Utility;

    public abstract class BoatBase : IBoatBase
    {
        private string model;
        private int weight;

        protected BoatBase(
            string model, 
            int weight)
        {
            this.Model = model;
            this.Weight = weight;
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                Validator.ValidateModelLength(value, Constants.MinBoatModelLength);
                this.model = value;
            }
        }

        public int Weight
        {
            get
            {
                return this.weight;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Weight");
                this.weight = value;
            }
        }

        public abstract double CalculateRaceSpeed(IRace race);
    }
}