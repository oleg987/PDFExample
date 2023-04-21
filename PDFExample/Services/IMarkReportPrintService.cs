using PDFExample.Models;

namespace PDFExample.Services
{
    public interface IMarkReportPrintService
    {
        Task<byte[]> GetPDF(MarkReportPrintDto model);
    }
}