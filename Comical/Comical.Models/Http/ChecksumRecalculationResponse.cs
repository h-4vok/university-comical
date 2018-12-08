using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Comical.Models.Http
{
    [XmlRoot]
    public class ChecksumRecalculationResponse
    {
        [XmlElement]
        public bool Success { get; set; } = true;

        [XmlElement]
        public string ErrorMessage { get; set; }

        [XmlElement]
        public bool Error { get { return !String.IsNullOrWhiteSpace(this.ErrorMessage); } }

        public static implicit operator ChecksumRecalculationResponse(string errorMessage)
        {
            return new ChecksumRecalculationResponse
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
