using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class QLHangVeTest
    {
        [TestMethod]
        public void QLHangVeClassMachuyenbayTest()
        {
            QLHangVeClass qLHangVe = new QLHangVeClass();
            bool propertyWasUpdated = false;
            string[] props = new string[]
            {
                "Mahangve",
                "Soluong"
            };
            qLHangVe.PropertyChanged += (s, e) =>
            {
                if (props.Contains(e.PropertyName))
                {
                    propertyWasUpdated = true;
                }
            };
            qLHangVe.Machuyenbay = "002";
            Assert.AreEqual("002", qLHangVe.Machuyenbay);
            qLHangVe.Mahangve = "001";
            Assert.AreEqual("001", qLHangVe.Mahangve);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            qLHangVe.Soluong = "20";
            Assert.AreEqual("20", qLHangVe.Soluong);
            Assert.IsTrue(propertyWasUpdated);
        }
    }
}
