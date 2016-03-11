namespace BoatRacingSimulator.Models.Boats
{
    using Interfaces;

    public class PowerBoat : MotorBoatBase
    {
        private const double SpeedCoeficent = 5;

        public PowerBoat(
            string model, 
            int weight,
            IBoatEngineBase boatEngine1,
            IBoatEngineBase boatEngine2) 
            : base(model, weight)
        {
            this.BoatEngine1 = boatEngine1;
            this.BoatEngine2 = boatEngine2;
        }

        public IBoatEngineBase BoatEngine2 { get; set; }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = (this.BoatEngine1.Output + this.BoatEngine2.Output) 
                - this.Weight 
                + (race.OceanCurrentSpeed / SpeedCoeficent);

            return speed;
        }
    }
}