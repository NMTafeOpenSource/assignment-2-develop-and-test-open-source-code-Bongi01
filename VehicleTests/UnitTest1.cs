using Car_Rental_Program;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace VehicleTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddVehicles()
        {

            var listCountBefore = Vehicle.vehicleList.Count;
            var listCountAfter = Vehicle.vehicleList.Count;

            Vehicle.AddVehicle(1, "Ford", "Ranger", 2020, "1DGZ262", 60, 1700, 6000);
            Vehicle.AddVehicle(2, "Holden", "SS", 2020, "1EAG789", 55, 1400, 4550);
            Assert.AreEqual(listCountBefore, listCountAfter);

        }

        [TestMethod]
        public void SaveVehicles()
        {
            
            var listBefore = Vehicle.vehicleList.Count;
            var fileAfter = Vehicle.vehicleList.Count;

            SaveVehicles();

            Assert.AreEqual(listBefore, fileAfter);

        }

    }

}
