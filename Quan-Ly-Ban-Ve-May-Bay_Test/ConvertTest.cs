using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Ban_Ve_May_Bay.Converter;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class ConvertTest
    {
        [TestMethod]
        public void DateConvertTest()
        {
            DateTime date = DateTime.Now;
            DateConverter convert = new DateConverter();
            String expectedResult = date.ToString("dd/MM/yyyy");
            String actualResult = (String)convert.Convert(date, typeof(String), null, CultureInfo.InvariantCulture);// datetime not null
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(null, convert.Convert(null, typeof(String), null, CultureInfo.InvariantCulture));//datetime is null
        }
        [TestMethod]
        public void TimeConvertTest()
        {
            DateTime date = DateTime.Now;
            TimeConverter convert = new TimeConverter();
            String expectedResult = date.ToString("HH:mm");
            String actualResult = (String)convert.Convert(date, typeof(String), null, CultureInfo.InvariantCulture);// datetime not null
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(null, convert.Convert(null, typeof(String), null, CultureInfo.InvariantCulture));//datetime is null
        }
    }
}
