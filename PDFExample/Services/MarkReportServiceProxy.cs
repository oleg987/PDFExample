using PDFExample.Models;

namespace PDFExample.Services
{
    public class MarkReportServiceProxy : IMarkReportPrintService
    {
        private readonly MarkReportPrintService _service;
        private readonly IWebHostEnvironment _env;
        private const string _storagePath = "mark_report";

        public MarkReportServiceProxy(IWebHostEnvironment env)
        {
            _env = env;
            _service = new MarkReportPrintService(_env);
        }

        public async Task<byte[]> GetPDF(MarkReportPrintDto model)
        {
            var reportDir = new DirectoryInfo(Path.Combine(_env.WebRootPath, _storagePath));

            if (!reportDir.Exists)
            {
                reportDir.Create();
            }

            var fileInfo = reportDir.GetFiles().FirstOrDefault(f => f.Name == $"{model.Id}.pdf");

            if (fileInfo is not null)
            {
                var bytes = File.ReadAllBytesAsync(fileInfo.FullName);

                return await bytes;
            }
            else
            {
                var bytes = await _service.GetPDF(model);

                var task = File.WriteAllBytesAsync(Path.Combine(_env.WebRootPath, _storagePath, $"{model.Id}.pdf"), bytes);

                return bytes;
            }
        }
    }
}
