using Comical.Models.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Comical.Test.Models.Http
{
    [TestClass]
    public class Test_ChecksumRecalculationResponse
    {
        private ChecksumRecalculationResponse subject;

        [TestInitialize]
        public void Init()
        {
            this.subject = new ChecksumRecalculationResponse();
        }

        [TestMethod]
        public void ChecksumRecalculationResponse_EmptyXmlSerialization()
        {
            var serializer = new XmlSerializer(typeof(ChecksumRecalculationResponse));
            string result;

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this.subject);
                result = writer.ToString();
            }

            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ChecksumRecalculationResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Success>true</Success>\r\n</ChecksumRecalculationResponse>", result);
        }

        [TestMethod]
        public void ChecksumRecalculationResponse_FullXmlSerialization()
        {
            var serializer = new XmlSerializer(typeof(ChecksumRecalculationResponse));
            string result;

            this.subject.ErrorMessage = "Un error ocurrió durante la ejecución.";
            this.subject.Success = false;

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this.subject);
                result = writer.ToString();
            }

            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ChecksumRecalculationResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Success>false</Success>\r\n  <ErrorMessage>Un error ocurrió durante la ejecución.</ErrorMessage>\r\n</ChecksumRecalculationResponse>", result);
        }
    }
}
