using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.ViewModel;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class RuleChangeVMTest
    {
        [TestMethod]
        public void RuleChangeViewModelTest()
        {
            var ruleChange = new RuleChangeViewModel();
            bool propertyWasUpdated = false;
            string[] props = new string[]
            {
                "ThoiGianBayToiThieu",
                "SoSanBayTrungGianToiDa",
                "ThoiGianDungToiThieu",
                "ThoiGianDungToiDa",
                "SoGioTruocKhoiHanhChoPhepHuyVe",
                "SoGioTruocKhoiHanhChoPhepDatVe"
            };
            ruleChange.PropertyChanged += (s, e) =>
            {
                if (props.Contains(e.PropertyName))
                {
                    propertyWasUpdated = true;
                }
            };
            ruleChange.ThoiGianBayToiThieu = 15;
            
            Assert.AreEqual(15, ruleChange.ThoiGianBayToiThieu);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ruleChange.SoSanBayTrungGianToiDa = 2;
            Assert.AreEqual(2, ruleChange.SoSanBayTrungGianToiDa);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ruleChange.ThoiGianDungToiThieu = 15;
            Assert.AreEqual(15, ruleChange.ThoiGianDungToiThieu);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ruleChange.ThoiGianDungToiDa = 30;
            Assert.AreEqual(30, ruleChange.ThoiGianDungToiDa);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ruleChange.SoGioTruocKhoiHanhChoPhepHuyVe = 24;
            Assert.AreEqual(24, ruleChange.SoGioTruocKhoiHanhChoPhepHuyVe);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ruleChange.SoGioTruocKhoiHanhChoPhepDatVe = 1;
            Assert.AreEqual(1, ruleChange.SoGioTruocKhoiHanhChoPhepDatVe);
            Assert.IsTrue(propertyWasUpdated);
        }
    }
}
