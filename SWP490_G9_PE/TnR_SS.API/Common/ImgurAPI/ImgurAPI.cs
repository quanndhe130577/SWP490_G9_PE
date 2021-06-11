using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.ImgurAPI
{
    public class ImgurAPI
    {
        public static async Task<string> UploadImgurAsync(string imgBase64)
        {
            if (imgBase64 is null)
            {
                return null;
            }
            var apiClient = new ApiClient(Startup.StaticConfig["Imgur:client-id"]);
            var httpClient = new HttpClient();

            byte[] bytes = Convert.FromBase64String(imgBase64);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
                var imageUpload = await imageEndpoint.UploadImageAsync(ms);
                return imageUpload.Link;
            }
        }

        /*public static async Task<string> UploadImgurv1Async(string imgBase64)
        {
            if (imgBase64 is null)
            {
                return null;
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Client-ID " + Startup.StaticConfig["Imgur:client-id"]);

            var content = new MultipartFormDataContent();
            Stream stream = new MemoryStream(Convert.FromBase64String(imgBase64));

            content.Add(new StreamContent(stream), "image");

            //var response = await client.PostAsync("https://api.imgur.com/3/image", new StringContent(JsonConvert.SerializeObject(new { image = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==" }), Encoding.UTF8, "multipart/form-data"));
            var response = await client.PostAsync("https://api.imgur.com/3/image", content);
            var rs = await response.Content.ReadAsStringAsync();

            return rs;


        }*/
    }
}
