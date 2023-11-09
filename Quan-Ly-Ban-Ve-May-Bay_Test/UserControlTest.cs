using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class UserControlTest
    {
        [TestMethod]
        public void ChuyenBayClassTest()
        {
            var chuyenbayclass = new chuyenbayclass()
            {
                STT = "1",
                maCB = "VN210",
                SBdi = "CXR",
                SBden = "HAN",
                datetime = "08/12/2023",
                tgBay = "30",
                Gia = "2000000"
            };
            Assert.AreEqual("1", chuyenbayclass.STT);
            Assert.AreEqual("VN210", chuyenbayclass.maCB);
            Assert.AreEqual("CXR", chuyenbayclass.SBdi);
            Assert.AreEqual("HAN", chuyenbayclass.SBden);
            Assert.AreEqual("08/12/2023", chuyenbayclass.datetime);
            Assert.AreEqual("30", chuyenbayclass.tgBay);
            Assert.AreEqual("2000000", chuyenbayclass.Gia);
        }
        [TestMethod]
        public void SanbayTGTest()
        {
            var SBTG = new SanbayTG()
            {
                STT = "1",
                tenSB = "HAN",
                TGdung = "15",
                ghichu = "Khong co"
            };
            Assert.AreEqual("1", SBTG.STT);
            Assert.AreEqual("HAN", SBTG.tenSB);
            Assert.AreEqual("15", SBTG.TGdung);
            Assert.AreEqual("Khong co", SBTG.ghichu);
        }
        [TestMethod]
        public void FareClassTest()
        {
            var hangVe = new FareClass()
            {
                id = "HV229365",
                name = "Hang 2",
                percentage = "100"
            };
            Assert.AreEqual("HV229365", hangVe.id);
            Assert.AreEqual("Hang 2", hangVe.name);
            Assert.AreEqual("100", hangVe.percentage);
        }
        [TestMethod]
        public void HangMBclassTest()
        {
            var hangMBclass = new HangMBclass()
            {
                STT = "1",
                tenhang = "Vietjet Air",
                mahang = "VJ"
            };
            Assert.AreEqual("1", hangMBclass.STT);
            Assert.AreEqual("Vietjet Air", hangMBclass.tenhang);
            Assert.AreEqual("VJ", hangMBclass.mahang);
        }
        [TestMethod]
        public void sanbayclassTest()
        {
            var sanbayclass = new sanbayclass()
            {
                STT = "1",
                maSB = "CXR",
                tenSB = "San bay Quoc te Cam Ranh",
                tinh = "Khanh Hoa"
            };
            Assert.AreEqual("1", sanbayclass.STT);
            Assert.AreEqual("CXR", sanbayclass.maSB);
            Assert.AreEqual("San bay Quoc te Cam Ranh", sanbayclass.tenSB);
            Assert.AreEqual("Khanh Hoa", sanbayclass.tinh);
        }
        [TestMethod]
        public void YearSaleTest()
        {
            var yearSale = new YearSale()
            {
                sochuyenbay = "10", 
                thang = "5", 
                doanhthu = "20000000", 
                tile = "30"
            };
            Assert.AreEqual("10", yearSale.sochuyenbay);
            Assert.AreEqual("5", yearSale.thang);
            Assert.AreEqual("20000000", yearSale.doanhthu);
            Assert.AreEqual("30", yearSale.tile);
        }
    }
}
