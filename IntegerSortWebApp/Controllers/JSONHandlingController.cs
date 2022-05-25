using IntegerSortWebApp.App_Data;
using IntegerSortWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IntegerSortWebApp.Controllers
{
    public class JSONHandlingController : Controller
    {

        private readonly IntegerSortDBContext _database;

        public JSONHandlingController(IntegerSortDBContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportJSON()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImportJSONAsync(IFormFile fileToUpload)
        {
            try
            {
                if (!fileToUpload.FileName.EndsWith(".json"))
                {
                    return View();
                }

                var filePath = fileToUpload.FileName;
                fileToUpload.OpenReadStream();

                string fileContent = null;

                using (var stream = new StreamReader(fileToUpload.OpenReadStream(), true))
                {
                    fileContent = stream.ReadToEnd();

                    List<Sort> test = JsonSerializer.Deserialize<List<Sort>>(fileContent);

                    _database.AddRange(test);
                    _database.SaveChanges();
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public IActionResult ExportJSON()
        {
            try
            {
                if (_database.Numbers.ToList().Count > 0)
                    return Json(_database.Sorts.ToList(), new JsonSerializerOptions { WriteIndented = true });
                else
                    return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
