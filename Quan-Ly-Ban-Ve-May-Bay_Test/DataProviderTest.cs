using System;
using Quan_Ly_Ban_Ve_May_Bay.Model;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class DataProviderTest
    {
        [TestMethod]
        public void isPositiveIntgerTest()
        {
            Assert.IsFalse(DataProvider.isPositiveInteger("a"));
            Assert.IsFalse(DataProvider.isPositiveInteger("0"));
            Assert.IsTrue(DataProvider.isPositiveInteger("30"));
        }
    }
}
