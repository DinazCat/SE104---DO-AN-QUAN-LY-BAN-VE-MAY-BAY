using Quan_Ly_Ban_Ve_May_Bay.Data;
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
            BANGTHAMSO.ThoiGianBayToiThieu.CompareTo(30);
            BANGTHAMSO.ThoiGianBayToiDa.CompareTo(2);
            BANGTHAMSO.ThoiGianDungToiThieu.CompareTo(10);
            BANGTHAMSO.ThoiGianDungToiDa.CompareTo(20);
            BANGTHAMSO.SoGioTruocKhoiHanhChoPhepDatVe.CompareTo(24);
            BANGTHAMSO.SoGioTruocKhoiHanhHuyPhieuDat.CompareTo(1);

        }
    }
}
