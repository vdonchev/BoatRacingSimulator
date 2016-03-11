namespace BoatRacingSimulator.Models.Engines
{
    using Interfaces;
    using Utility;

    public abstract class BoatEngineBase : IBoatEngineBase
    {
        private string model;
        private int horsepower;
        private int displacement;

        protected BoatEngineBase(
            string model, 
            int horsepower, 
            int displacement)
        {
            this.Model = model;
            this.Horsepower = horsepower;
            this.Displacement = displacement;
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            private set
            {
                Validator.ValidateModelLength(value, Constants.MinBoatEngineModelLength);
                this.model = value;
            }
        }

        public int Horsepower
        {
            get
            {
                return this.horsepower;
            }

            set
            {
                Validator.ValidatePropertyValue(value, "Horsepower");
                this.horsepower = value;
            }
        }

        public int Displacement
        {
            get
            {
                return this.displacement;
            }

            set
            {
                Validator.ValidatePropertyValue(value, "Displacement");
                this.displacement = value;
            }
        }

        public abstract int Output { get; }

        public int CachedOutput { get; set; }
    }
}