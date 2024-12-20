// Controllers/PhotoController.cs
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

				// OpenAI ile analiz veya görüntü oluşturma işlemi
				ViewBag.ImageUrl = await GenerateImage("Bir erkeğin yüz şekli ve saç dokusuna dayalı modern bir saç modeli önerisi oluştur. Saç modeli, erkekler için uygun olmalı ve kişinin görünümünü geliştirecek şekilde tasarlanmalıdır. Saç uzunluğu ve tarzı erkeklere hitap eden bir şekilde olmalı." +
					"");
			}
			else
			{
				ViewBag.Error = "Lütfen geçerli bir dosya seçin.";
			}

			return View();
		}

		private async Task<string> GenerateImage(string prompt)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

			var requestContent = new StringContent($@"
            {{
                ""prompt"": ""{prompt}"",
                ""n"": 1,
                ""size"": ""1024x1024""
            }}", Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://api.openai.com/v1/images/generations", requestContent);

			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine(responseContent); // Yanıt JSON formatında yazdırılır
				dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
				return jsonResponse.data[0].url; // Oluşturulan görüntünün URL'sini alıyoruz
			}
			else
			{
				return $"API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
			}
		}
	}
}
