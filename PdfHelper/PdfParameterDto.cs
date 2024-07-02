namespace PdfHelper
{
    public class PdfParameterDto
    {
        public PaperType PaperType { get; set; } = PaperType.A4;
        public bool Landscape { get; set; } = false;
        public string Scale { get; set; } = "1";
        public bool PerformanceMode { get; set; } = true;
    }
}
