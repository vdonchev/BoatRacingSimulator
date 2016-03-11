namespace BoatRacingSimulator.Tests
{
    using Controllers;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Boats;
    using Models.Engines;
    using Moq;

    [TestClass]
    public class SignUpBoatMocking
    {
        [TestMethod]
        public void SignUp_ShouldAddItCorrectly()
        {
            var mockDb = new Mock<IBoatSimulatorDatabase>();

            var engine = new JetEngine("SI10", 300, 200);
            var yacht = new Yacht("Luxury101", 700, 101, engine);
            mockDb.Setup(d => d.Boats.GetItem("Lodkata")).Returns(yacht);
        
            var controller = new BoatSimulatorController(mockDb.Object);
            controller.OpenRace(100, 100, 100, true);

            var res = controller.SignUpBoat("Lodkata");

            Assert.AreEqual("Boat with model Lodkata has signed up for the current Race.", res);
        }
    }
}
