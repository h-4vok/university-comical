using Comical.Models.Enums;
using Comical.Models.Http;
using Comical.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace Comical.API.Controllers
{
    public class ChecksumController : ApiController
    {
        public string Post(ChecksumRecalculationRequest body)
        {
            string serializeResponse(ChecksumRecalculationResponse response)
            {
                var serializer = new XmlSerializer(typeof(ChecksumRecalculationResponse));
                string result;

                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, response);
                    result = writer.ToString();
                }

                return result;
            }

            string prepareErrorResponse(string errorMessage)
            {
                ChecksumRecalculationResponse response = errorMessage;

                var result = serializeResponse(response);
                return result;
            }

            if (body == null)
                return prepareErrorResponse("Parámetro 'body' no puede ser nulo.");

            if (String.IsNullOrWhiteSpace(body.UserName))
                return prepareErrorResponse("El usuario no puede ser vacío.");

            if (String.IsNullOrWhiteSpace(body.Password))
                return prepareErrorResponse("La contraseña no puede ser vacía.");

            var authenticationService = new AuthenticationService();
            var authenticationResponse = authenticationService.Authenticate(body.UserName, body.Password);

            if (!authenticationResponse.Authenticated)
                return authenticationResponse.ValidationError;

            var isGranted = AuthorizationService.obj.IsEnabledFor(authenticationResponse.UserId, PermissionCodes.VerifierDigits_CanFix);

            if (!isGranted)
                return prepareErrorResponse("No tiene permisos para recalcular los dígitos verificadores.");

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
                return prepareErrorResponse("Ha ocurrido un error en la ejecución. Consulte la bitácora.");
            }

            return serializeResponse(new ChecksumRecalculationResponse());
        }
    }
}
