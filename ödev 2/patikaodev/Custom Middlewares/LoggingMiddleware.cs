namespace patikaodev.CustomMiddlewares{
    public class LoggingMiddleware{
        private readonly RequestDelegate next;
		private readonly ILogger logger;
		public LoggingMiddleware(ILoggerFactory logger_factory, RequestDelegate next) {
			this.next = next;
			logger = logger_factory.CreateLogger<LoggingMiddleware>();
		}
		public async Task Invoke(HttpContext context) { // Logging after accessing to action.
			logger.LogInformation(context.Request.RouteValues["action"]!.ToString() + " action was accessed.");
			await next(context);
		}
    }
}