namespace BoatRacingSimulator.Models.Boats
{
    using Interfaces;

    public abstract class MotorBoatBase : BoatBase
    {
        protected MotorBoatBase(string model, int weight) 
            : base(model, weight)
        {
        }

        public IBoatEngineBase BoatEngine1 { get; set; }
    }
}