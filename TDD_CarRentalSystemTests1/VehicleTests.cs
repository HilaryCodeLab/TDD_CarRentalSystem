using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_CarRentalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_CarRentalSystem.Tests
{
    [TestClass()]
    public class VehicleTests
    {
        [TestMethod()]
        public void AddVehicleTest()
        {
            var listCountBefore =Vehicle.vehicleList.Count;
            var listCountAfter = Vehicle.vehicleList.Count;
            Vehicle.AddVehicle("Honda", "Civic", 2008, "1DWU656", FuelTypes.Petrol, 62);
            Assert.AreEqual(listCountBefore, listCountAfter);
        }

    }

}