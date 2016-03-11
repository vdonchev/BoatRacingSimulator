namespace BoatRacingSimulator.Models.Boats
{
    using Interfaces;
    using Utility;

    public class RowBoat : BoatBase
    {
        private int oars;

        public RowBoat(
            string model, 
            int weight,
            int oars) 
            : base(model, weight)
        {
            this.Oars = oars;
        }

        public int Oars
        {
            get
            {
                return this.oars;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Oars");
                this.oars = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            // Bug Zero instead of o
            var speed = (this.oars * 100) - this.Weight + race.OceanCurrentSpeed;

            return speed;
        }
    }
}