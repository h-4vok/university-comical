using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Http
{
    public class ChecksumRecalculationResponse
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
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
