using OfficeOpenXml;
using PDFExample.Models;
using Spire.Xls;

namespace PDFExample.Services
{
    public class MarkReportPrintService : IMarkReportPrintService
    {
        private readonly IWebHostEnvironment _env;

        public MarkReportPrintService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<byte[]> GetPDF(MarkReportPrintDto model)
        {
            var report = new ExcelPackage();            

            ExcelWorksheet worksheet;

            using (var template = new ExcelPackage(Path.Combine(_env., "baseTemplate.xlsx")))
            {
                worksheet = report.Workbook.Worksheets.Add("report", template.Workbook.Worksheets[0]);
            }


            worksheet.Cells["E9"].Value = model.Id;
            worksheet.Cells["D10"].Value = model.Date.Day + "." + model.Date.Month;
            worksheet.Cells["F10"].Value = model.Date.Year + " року";

            int studentsDataBeginRow = 18;

            foreach (var mark in model.Marks)
            {
                worksheet.Cells["B" + studentsDataBeginRow].Value = mark.FullName;
                worksheet.Cells["E" + studentsDataBeginRow].Value = mark.Value;
                studentsDataBeginRow++;
            }

            var reportStream = new MemoryStream();

            await report.SaveAsAsync(reportStream);

            worksheet.Dispose();

            report.Dispose();

            var pdf = new Workbook();

            pdf.LoadFromStream(reportStream);

            reportStream.Dispose();

            var pdfStream = new MemoryStream();

            pdf.SaveToStream(pdfStream, FileFormat.PDF);

            pdf.Dispose();

            var bytes = pdfStream.ToArray();

            pdfStream.Dispose();

            return bytes;
        }
    }
}
