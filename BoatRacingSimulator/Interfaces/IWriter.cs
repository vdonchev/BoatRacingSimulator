namespace BoatRacingSimulator.Interfaces
{
    public interface IWriter
    {
        void WriteLine(string message, params object[] @params);
    }
}