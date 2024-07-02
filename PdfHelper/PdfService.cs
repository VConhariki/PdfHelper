using PdfHelper;
using System.Text;

namespace PdfGenerator.Service
{
    public class PdfService
    {

        private static readonly HttpClient client = new();

        public static async Task<string> HtmlToBase64(string html, string url, PdfParameterDto parameter)
        {
            var bytes = await GetBytesFromHtml(html, url, parameter);
            var base64 = GetBase64FromBytes(bytes);
            return base64;
        }

        public static async Task<MemoryStream> HtmlToMemoryStream(string html, string url, PdfParameterDto parameter)
        {
            var bytes = await GetBytesFromHtml(html, url, parameter);
            var memoryStream = GetMemoryStreamFromBytes(bytes);
            return memoryStream;
        }

        public static async Task<string> LinkToBase64(string link, string url)
        {
            var bytes = await GetBytesFromLink(link, url);
            var base64 = GetBase64FromBytes(bytes);
            return base64;
        }

        public static async Task<MemoryStream> LinkToMemoryStrem(string link, string url)
        {
            var bytes = await GetBytesFromLink(link, url);
            var memoryStream = GetMemoryStreamFromBytes(bytes);
            return memoryStream;
        }

        #region private
        private static async Task<byte[]> GetBytesFromHtml(string html, string url, PdfParameterDto parameter)
        {
            var paperSize = PaperSize.GetPaperSize(parameter.PaperType);
            var stringContent = new StringContent(html, Encoding.UTF8, "text/html");
            var requestContent = new MultipartFormDataContent()
            {
                { stringContent, "file", "index.html" },
                { new StringContent(parameter.Landscape.ToString()), "landscape" },
                { new StringContent(parameter.Scale.ToString()), "scale" },
                { new StringContent(paperSize.Width.ToString()), "paperWidth" },
                { new StringContent(paperSize.Height.ToString()), "paperHeight" },
                { new StringContent(parameter.PerformanceMode.ToString()), "skipNetworkIdleEvent" }
            };

            var response = await client.PostAsync(url, requestContent);

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var byteArray = await response.Content.ReadAsByteArrayAsync();
                return byteArray;
            }
            return [];
        }

        private static async Task<byte[]> GetBytesFromLink(string link, string url)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(link), "url" },
                { new StringContent("PDF/A-1a"), "pdfFormat" }
            };

            var response = await client.PostAsync(url, requestContent);

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var byteArray = await response.Content.ReadAsByteArrayAsync();
                return byteArray;
            }
            return [];
        }

        private static MemoryStream GetMemoryStreamFromBytes(byte[] bytes)
        {
            if (bytes.Length > 0)
            {
                var memoryStream = new MemoryStream(bytes);
                return memoryStream;
            }

            return new MemoryStream();
        }

        private static string GetBase64FromBytes(byte[] bytes)
        {
            if (bytes.Length > 0)
            {
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }

            return string.Empty;
        }
        #endregion
    }
}
