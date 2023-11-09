using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class SymbolTickerTest
    {
        [TestMethod]
        [DataRow("001", "SGN", "15:00 19/08/2023", "30", "1", "Nguyen Ha Mi")]
        public void SymbolTicketConstructorTest(string maVe, string tuyen, string ngayGio, string soGhe, string hangVe, string tenHK)
        {
            SymbolTicket symbolTicker = new SymbolTicket(maVe, tuyen, ngayGio, soGhe, hangVe, tenHK);
            Assert.AreEqual(maVe, symbolTicker.MaVe);
            symbolTicker = new SymbolTicket(maVe, tuyen, ngayGio, soGhe, hangVe, tenHK);
            Assert.AreEqual(maVe, symbolTicker.MaVe);
        }
        [TestMethod]
        [DataRow("001", "30", "1", "Nguyen Ha Mi")]
        public void SymbolTicketConstructorTest1(string maVe, string soGhe, string hangVe, string tenHK)
        {
            SymbolTicket symbolTicker = new SymbolTicket(maVe, soGhe, hangVe, tenHK);
            Assert.AreEqual(maVe, symbolTicker.MaVe);
            symbolTicker = new SymbolTicket(maVe, soGhe, hangVe, tenHK);
            Assert.AreEqual(maVe, symbolTicker.MaVe);
        }
        [TestMethod]
        public void SymbolTicketMaVeTest()
        {
            SymbolTicket symbolTicker = new SymbolTicket();

            symbolTicker.MaVe = "001";
            Assert.AreEqual("001", symbolTicker.MaVe);
            Trace.Write(symbolTicker.MaVe);
        }
        [TestMethod]
        public void SymbolTicketTuyenTest()
        {
            SymbolTicket symbolTicker = new SymbolTicket();

            symbolTicker.Tuyen = "001";
            Assert.AreEqual("001", symbolTicker.Tuyen);
            Trace.Write(symbolTicker.Tuyen);
        }
        [TestMethod]
        public void SymbolTicketSoGheTest()
        {
            SymbolTicket symbolTicker = new SymbolTicket();

            symbolTicker.SoGhe = "10";
            Assert.AreEqual("10", symbolTicker.SoGhe);
            Trace.Write(symbolTicker.SoGhe);
        }
        [TestMethod]
        public void SymbolTicketHangVeTest()
        {
            SymbolTicket symbolTicker = new SymbolTicket();

            symbolTicker.HangVe = "1";
            Assert.AreEqual("1", symbolTicker.HangVe);
            Trace.Write(symbolTicker.HangVe);
        }
        [TestMethod]
        public void SymbolTicketTenHKTest()
        {
            SymbolTicket symbolTicker = new SymbolTicket();

            symbolTicker.TenHK = "Nguyen Ha Mi";
            Assert.AreEqual("Nguyen Ha Mi", symbolTicker.TenHK);
            Trace.Write(symbolTicker.TenHK);
        }
        [TestMethod]
        public void SymbolTicketNgayGioTest()
        {
            SymbolTicket symbolTicker = new SymbolTicket();

            symbolTicker.NgayGio = "15:00 19/08/2023";
            Assert.AreEqual("15:00 19/08/2023", symbolTicker.NgayGio);
            Trace.Write(symbolTicker.NgayGio);
        }

    }
}
