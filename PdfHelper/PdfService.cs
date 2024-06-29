using System.Text;

namespace PdfGenerator.Service
{
    public class PdfService
    {

        private static readonly HttpClient client = new();

        public static async Task<string> HtmlToBase64(string html, string url)
        {
            var requestContent = new MultipartFormDataContent();
            var stringContent = new StringContent(html, Encoding.UTF8, "text/html");

            requestContent.Add(stringContent, "file", "index.html");

            var response = await client.PostAsync(url, requestContent);

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var byteArray = await response.Content.ReadAsByteArrayAsync();
                var base64 = Convert.ToBase64String(byteArray);
                return base64;
            }

            return string.Empty;
        }

        public static async Task<string> LinkToBase64(string link, string url) 
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
                var base64 = Convert.ToBase64String(byteArray);
                return base64;
            }

            return string.Empty; 
        }
    }
}
