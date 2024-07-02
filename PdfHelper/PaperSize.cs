namespace PdfHelper
{
    public class PaperSize
    {
        public static PaperSizeData GetPaperSize(PaperType paperType)
        {
            return paperType switch
            {
                PaperType.Letter => new PaperSizeData("8.5", "11"),
                PaperType.Legal => new PaperSizeData("8.5", "14"),
                PaperType.Tabloid => new PaperSizeData("11", "17"),
                PaperType.Ledger => new PaperSizeData("17", "11"),
                PaperType.A0 => new PaperSizeData("33.1", "46.8"),
                PaperType.A1 => new PaperSizeData("23.4", "33.1"),
                PaperType.A2 => new PaperSizeData("16.54", "23.4"),
                PaperType.A3 => new PaperSizeData("11.7", "16.54"),
                PaperType.A4 => new PaperSizeData("8.27", "11.7"),
                PaperType.A5 => new PaperSizeData("5.83", "8.27"),
                PaperType.A6 => new PaperSizeData("4.13", "5.83"),
                _ => throw new ArgumentOutOfRangeException(nameof(paperType), paperType, null),
            };
        }
        
    }
}
