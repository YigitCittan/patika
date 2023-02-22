

using patikaodev.Services;

namespace patikaodev.CustomMiddlewares{
    public class ExceptionMiddleware{
        private readonly RequestDelegate next;
		
		private readonly ILoggerService logger;

		public ExceptionMiddleware(ILoggerService loggerService, RequestDelegate next) {
			this.next = next;
			logger = loggerService;
			
		}
		public async Task Invoke(HttpContext context) { // Handling exception.
			try {
				string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path; 
				logger.Write(message);
				await next(context);
			} catch (Exception) { // Returning 500 HTTP status code if an exception occurs.
				var response = context.Response;
				string message = "An error occurred.";
				logger.Write(message);
				response.StatusCode = StatusCodes.Status500InternalServerError;
				await response.StartAsync();
			}
		}
    }
}