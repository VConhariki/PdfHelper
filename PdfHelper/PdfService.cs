using System.Text;

namespace PdfGenerator.Service
{
    public class PdfService
    {

        private static readonly HttpClient client = new();

        public static async Task<string> HtmlToBase64(string html, string url)
        {
            var bytes = await GetBytesFromHtml(html, url);
            var base64 = GetBase64FromBytes(bytes);
            return base64;
        }

        public static async Task<MemoryStream> HtmlToMemoryStream(string html, string url)
        {
            var bytes = await GetBytesFromHtml(html, url);
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
        private static async Task<byte[]> GetBytesFromHtml(string html, string url)
        {
            var requestContent = new MultipartFormDataContent();
            var stringContent = new StringContent(html, Encoding.UTF8, "text/html");

            requestContent.Add(stringContent, "file", "index.html");

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
