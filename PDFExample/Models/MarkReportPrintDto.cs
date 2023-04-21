namespace PDFExample.Models
{
    public class MarkDto
    {
        public string FullName { get; set; }
        public int Value { get; set; }
    }

    public class MarkReportPrintDto
    {
        public static MarkReportPrintDto Instanse { get; private set; } = new()
        {
            Id = 1,
            Date = DateTime.Now,
            Marks = new List<MarkDto>()
            {
                new MarkDto{ FullName = "Пупкин Феликс Эмануилович", Value = 59 },
                new MarkDto{ FullName = "Лупкина Эльвира Зигфридовна", Value = 61 }
            }
        };

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public ICollection<MarkDto> Marks { get; set; }
    }
}
