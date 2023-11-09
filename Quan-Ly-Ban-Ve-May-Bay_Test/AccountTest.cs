using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]

    public class AccountTest
    {
        [TestMethod]
        [DataRow("001", "admin", "123123", 1, "abc@gmail.com", "ADMIN")]
        public void AccountConstructorTest(string id, string name, string pass, int type, string email, string displayname)
        {
            Account account = new Account(id, name, pass, type, email, displayname);
            Assert.AreEqual(id, account.id);
            account = new Account(id, name, pass, type, email, displayname);
            Assert.AreEqual(id, account.id);
        }


        [TestMethod]
        public void AccountIdTest()
        {
            Account account = new Account();

            account.id = "001";
            Assert.AreEqual("001", account.id);
            Trace.Write(account.id);
        }
        [TestMethod]
        public void AccountUsernameTest()
        {
            Account account = new Account();

            account.username = "001";
            Assert.AreEqual("001", account.username);
            Trace.Write(account.username);
        }
        [TestMethod]
        public void AccountPasswordTest()
        {
            Account account = new Account();

            account.password = "ABC";
            Assert.AreEqual("ABC", account.password);
            Trace.Write(account.password);
        }
        [TestMethod]
        public void AccountEmailTest()
        {
            Account account = new Account();

            account.email = "abc@gmail.com";
            Assert.AreEqual("abc@gmail.com", account.email);
            Trace.Write(account.email);
        }
        [TestMethod]
        public void AccountDisplaynameTest()
        {
            Account account = new Account();

            account.displayname = "Admin";
            Assert.AreEqual("Admin", account.displayname);
            Trace.Write(account.displayname);
        }
        [TestMethod]
        public void AccountTypeTest()
        {
            Account account = new Account();

            account.type = 1;
            Assert.AreEqual(1, account.type);
            Trace.Write(account.type);
        }
    }
}
