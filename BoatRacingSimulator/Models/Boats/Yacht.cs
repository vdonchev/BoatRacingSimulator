namespace BoatRacingSimulator.Models.Boats
{
    using Interfaces;
    using Utility;

    public class Yacht : MotorBoatBase
    {
        private const double SpeedCoeficent = 2;

        private int cargoWeight;

        public Yacht(
            string model, 
            int weight,
            int cargoWeight,
            IBoatEngineBase boatEngine) 
            : base(model, weight)
        {
            this.CargoWeight = cargoWeight;
            this.BoatEngine1 = boatEngine;
        }

        public int CargoWeight
        {
            get
            {
                return this.cargoWeight;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Cargo Weight");
                this.cargoWeight = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = this.BoatEngine1.Output 
                - this.Weight 
                - this.CargoWeight 
                + (race.OceanCurrentSpeed / SpeedCoeficent);

            return speed;
        }
    }
}