using Microsoft.AspNetCore.Mvc;
using PDFExample.Models;
using PDFExample.Services;
using System.Diagnostics;

namespace PDFExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMarkReportPrintService _service;

        public HomeController(ILogger<HomeController> logger, IMarkReportPrintService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Download(int id)
        {
            var bytes = await _service.GetPDF(MarkReportPrintDto.Instanse);           

            return File(bytes, "application/pdf", $"{MarkReportPrintDto.Instanse.Id}.pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}