using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WEB_Project.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class PhotoController : Controller
	{
		private readonly string apiKey = "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";

		[HttpGet]
		public IActionResult UploadPhoto()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UploadPhoto(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				// Fotoğrafı sunucuya kaydetme
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}

				var filePath = Path.Combine(uploadsFolder, Path.GetFileName(file.FileName));
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				// OpenAI API ile birden fazla görsel oluşturma
				ViewBag.ImageUrls = await GenerateImages("Bir erkeğin yüz şekli ve saç dokusuna dayalı modern bir saç modeli önerisi oluştur. Saç modeli, erkekler için uygun olmalı ve kişinin görünümünü geliştirecek şekilde tasarlanmalıdır. Saç uzunluğu ve tarzı erkeklere hitap eden bir şekilde olmalı.");
			}
			else
			{
				ViewBag.Error = "Lütfen geçerli bir dosya seçin.";
			}

			return View();
		}

		private async Task<List<string>> GenerateImages(string prompt)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

			var requestContent = new StringContent($@"
            {{
                ""prompt"": ""{prompt}"",
                ""n"": 3,
                ""size"": ""1024x1024""
            }}", Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://api.openai.com/v1/images/generations", requestContent);

			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);

				var imageUrls = new List<string>();
				foreach (var image in jsonResponse.data)
				{
					imageUrls.Add((string)image.url);
				}

				return imageUrls;
			}
			else
			{
				return new List<string> { $"API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}" };
			}
		}
	}
}
