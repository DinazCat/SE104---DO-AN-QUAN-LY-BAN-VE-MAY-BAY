using Quan_Ly_Ban_Ve_May_Bay.Data;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class BANGTHAMSOTEST
    {
        [TestMethod]
        public void ThoiGianBayToiThieuTest()
        {
            BANGTHAMSO.ThoiGianBayToiThieu = 15;
            BANGTHAMSO.SoSBTGToiDa = 3;
            BANGTHAMSO.ThoiGianDungToiThieu = 15;
            BANGTHAMSO.ThoiGianDungToiDa = 30;
            BANGTHAMSO.SoGioTruocKhoiHanhChoPhepDatVe = 1;
            BANGTHAMSO.SoGioTruocKhoiHanhHuyPhieuDat = 24;
            Assert.AreEqual(15, BANGTHAMSO.ThoiGianBayToiThieu);
            Assert.AreEqual(3, BANGTHAMSO.SoSBTGToiDa);
            Assert.AreEqual(15, BANGTHAMSO.ThoiGianDungToiThieu);
            Assert.AreEqual(30, BANGTHAMSO.ThoiGianDungToiDa);
            Assert.AreEqual(24, BANGTHAMSO.SoGioTruocKhoiHanhHuyPhieuDat);
            Assert.AreEqual(1, BANGTHAMSO.SoGioTruocKhoiHanhChoPhepDatVe);
        }
    }
}
