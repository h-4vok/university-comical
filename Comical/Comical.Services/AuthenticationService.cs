using Comical.Models;
using Comical.Models.Enums;
using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class AuthenticationService
    {
        public class AuthenticateResponse
        {
            public string ValidationError { get; set; }
            public bool Authenticated { get; set; }
            public int UserId { get; set; }
            public IEnumerable<string> ChecksumErrors { get; set; } = new List<string>();

            public static implicit operator AuthenticateResponse(string validationError) => new AuthenticateResponse { ValidationError = validationError };

            public static implicit operator AuthenticateResponse(int userId) => new AuthenticateResponse { UserId = userId, Authenticated = true };
        }

        public AuthenticateResponse Authenticate(string login, string password)
        {
            var eventInfo = String.Format("Autenticación - {0}", login);
            var eventFormat = "{0} - {1}";
            int currentUserId = 0;
            
            void logEvent(string message) => LoggingService.obj.Log("Autenticación", String.Format(eventFormat, eventInfo, message), currentUserId);

            string logEventAndReturn(string message) {
                logEvent(message);
                return message;
            }

            var inputsAreValid = this.ValidateInput(login, password);
            if (!String.IsNullOrWhiteSpace(inputsAreValid)) return logEventAndReturn(inputsAreValid);

            var repository = new UserRepository();
            var user = repository.GetByLogin(login);
            if (user == null) return logEventAndReturn("El usuario no existe.");

            currentUserId = user.Id;

            if (user.Blocked) return logEventAndReturn("El usuario está bloqueado.");

            if (!user.Enabled) return logEventAndReturn("El usuario no está habilitado.");

            var passwordMatches = this.CheckPassword(password, user.Password);
            if (!passwordMatches)
            {
                var retries = repository.IncrementRetry(user.Id);

                if (retries >= 3)
                {
                    repository.ChangeBlocked(user.Id, true);
                    return logEventAndReturn("Se ha superado la cantidad de intentos, el usuario ha sido bloqueado.");
                }

                return logEventAndReturn("La contraseña es incorrecta.");
            }

            IEnumerable<string> checksumErrors = new List<string>();

            var mustCheckVerifiers = AuthorizationService.obj.IsEnabledFor(user.Id, PermissionCodes.VerifierDigits_CheckOnLogin);
            if (mustCheckVerifiers)
            {
                checksumErrors = ChecksumService.obj.CheckVerifiers();
                if (checksumErrors.Any())
                {
                    ChecksumService.obj.SetDatabaseToChecksumError();
                }
            }

            var databaseStatus = this.GetDatabaseStatus();

            if  (databaseStatus.UnderMaintenance)
            {
                var canContinue = AuthorizationService.obj.IsEnabledFor(user.Id, PermissionCodes.UnderMaintenance_CanLogin);
                if (!canContinue) return logEventAndReturn("El sistema se encuentra en mantenimiento.");
            }

            if (databaseStatus.HasChecksumError)
            {
                var canContinue = AuthorizationService.obj.IsEnabledFor(user.Id, PermissionCodes.HasChecksumError_CanLogin);
                if (!canContinue) return logEventAndReturn("El sistema se encuentra en mantenimiento.");

                if (checksumErrors.Any())
                {
                    logEvent("Autenticación exitosa.");
                    return new AuthenticateResponse
                    {
                        Authenticated = true,
                        ChecksumErrors = checksumErrors,
                        UserId = user.Id
                    };
                }
            }

            logEvent("Autenticación exitosa.");
            return user.Id;
        }

        protected string ValidateInput(string login, string password)
        {
            if (String.IsNullOrWhiteSpace(login)) return "El usuario no puede ser vacío.";
            if (String.IsNullOrWhiteSpace(password)) return "La contraseña no puede ser vacío.";

            return String.Empty;
        }

        protected bool CheckPassword(string actual, string expectedHashed)
        {
            var actualHashed = PasswordHasher.obj.Hash(actual);

            return String.Equals(actualHashed, expectedHashed);
        }

        protected DatabaseStatus GetDatabaseStatus()
        {
            var repository = new DatabaseStatusRepository();
            var output = repository.Get();

            return output;
        }
    }
}
