using System.Diagnostics;

namespace PDFExample.Middlewares
{
    public class PageLoadingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PageLoadingMiddleware> _log;

        public PageLoadingMiddleware(RequestDelegate next, ILogger<PageLoadingMiddleware> log)
        {
            _next = next;
            _log = log;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            await _next(context);

            sw.Stop();
            _log.LogInformation($"Method: {context.Request.Method}; Path: {context.Request.Path}; Query: {string.Join(';', context.Request.Query.Select(q => q.Key + ":" + string.Join(',', q.Value)))}; Time: {sw.Elapsed}.");
        }
    }
}
