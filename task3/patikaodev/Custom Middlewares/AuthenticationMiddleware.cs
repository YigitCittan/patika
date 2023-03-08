using patikaodev.CustomAttributes;
using patikaodev.Services;
namespace patikaodev.CustomMiddlewares{
    public class AuthenticationMiddleware{
        private readonly ILoggerService logger;
		private readonly RequestDelegate next;
		private readonly string account_role = "Admin"; // Static account role for implementing a fake authentication service.
		public AuthenticationMiddleware(ILoggerService loggerservice, RequestDelegate next) {
			this.next = next;
			logger = loggerservice;
		}
		public async Task Invoke(HttpContext context) { // Checking account role for login.
			string account_attribute = context.GetEndpoint()!.Metadata.GetMetadata<AccountAttribute>()!.GetRole();
			if (string.Equals(account_role, account_attribute)) {
				logger.Write(account_attribute + " logged into system.");
				await next(context);
			} else { // Returning 403 HTTP status code if account is not authorized.
				var response = context.Response;
				logger.Write("An unauthorized user attempted to log into system.");
				response.StatusCode = StatusCodes.Status403Forbidden;
				await response.StartAsync();
			}
		}
    }
}