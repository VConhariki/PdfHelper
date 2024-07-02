namespace PdfHelper
{
    public class PaperSizeData(string width, string height)
    {
        public string Width { get; set; } = width;
        public string Height { get; set; } = height;
    }
}
