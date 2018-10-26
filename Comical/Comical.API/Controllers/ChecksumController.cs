using Comical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Comical.API.Controllers
{
    public class ChecksumController : ApiController
    {
        public class ChecksumRecalculationRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

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

        public ChecksumRecalculationResponse Post(ChecksumRecalculationRequest body)
        {
            if (body == null)
                return "Parameter 'body' cannot be null.";

            if (String.IsNullOrWhiteSpace(body.UserName))
                return "UserName cannot be empty.";

            if (String.IsNullOrWhiteSpace(body.Password))
                return "Password cannot be empty.";

            var authenticationService = new AuthenticationService();
            var authenticationResponse = authenticationService.Authenticate(body.UserName, body.Password);

            if (!authenticationResponse.Authenticated)
                return authenticationResponse.ValidationError;

            // TODO: Verificar que este usuario tiene permisos para resetear los verifiers

            // TODO: Matar todas las conexiones 
            // TODO: Colocar aplicacion en mantenimiento

            ChecksumService.obj.ResetHorizontalVerifiers();
            ChecksumService.obj.ResetVerticalVerifiers();

            // TODO: Sacar modo de mantenimiento

            return new ChecksumRecalculationResponse();
        }
    }
}
