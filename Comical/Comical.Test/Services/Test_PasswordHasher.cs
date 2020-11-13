using Comical.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Test.Services
{
    [TestClass]
    public class Test_PasswordHasher
    {
        [TestMethod]
        public void PasswordHasher_Hash()
        {
            var actual = PasswordHasher.obj.Hash("admin");
            Assert.AreEqual("fIdUH9Pz71AW4S1BGQDIemBGqOg=", actual);
        }
    }
}
