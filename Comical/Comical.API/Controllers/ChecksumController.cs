using Comical.Models.Enums;
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
                return "Parámetro 'body' no puede ser nulo.";

            if (String.IsNullOrWhiteSpace(body.UserName))
                return "El usuario no puede ser vacío.";

            if (String.IsNullOrWhiteSpace(body.Password))
                return "La contraseña no puede ser vacía.";

            var authenticationService = new AuthenticationService();
            var authenticationResponse = authenticationService.Authenticate(body.UserName, body.Password);

            if (!authenticationResponse.Authenticated)
                return authenticationResponse.ValidationError;

            var isGranted = AuthorizationService.obj.IsEnabledFor(authenticationResponse.UserId, PermissionCodes.VerifierDigits_CanFix);

            if (!isGranted)
                return "No tiene permisos para recalcular los dígitos verificadores.";

            try
            {
                DatabaseStatusService.obj.KillAllConnections();

                DatabaseStatusService.obj.SetUnderMaintenance();
                DatabaseStatusService.obj.SetHasChecksumError();

                ChecksumService.obj.ResetHorizontalVerifiers();
                ChecksumService.obj.ResetVerticalVerifiers();

                DatabaseStatusService.obj.UnsetUnderMaintenance();
                DatabaseStatusService.obj.UnsetHasChecksumError();
            }
            catch(Exception ex)
            {
                LoggingService.obj.Log("ChecksumController", ex);
                return "Ha ocurrido un error en la ejecución. Consulte la bitácora.";
            }

            return new ChecksumRecalculationResponse();
        }
    }
}
