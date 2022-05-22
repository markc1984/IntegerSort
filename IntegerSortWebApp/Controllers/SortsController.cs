using IntegerSortWebApp.App_Data;
using IntegerSortWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegerSortWebApp.Controllers
{
    public class SortsController : Controller
    {
        private readonly IntegerSortDBContext _database;

        public SortsController(IntegerSortDBContext database)
        {
            _database = database;
        }

        public IActionResult Index(int SortID)
        {
            IEnumerable<Sort> sortList = _database.Sorts;

            return View(sortList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSort(IFormCollection numbersToAdd)
        {
            Sort tempSort = new Sort();
            string formIntegerInput = numbersToAdd["Integer"];
            int sortOrder = Int32.Parse(numbersToAdd["SortOrder"]);
            tempSort.SortDirection = sortOrder;
            String[] numberStringArray = formIntegerInput.Split(",");
            List<Number> numbers = new List<Number>();

            for (int i = 0; i < numberStringArray.Length; i++)
            {
                numbers.Add(new Number { Integer = Convert.ToInt32(numberStringArray[i]) });
            }

            var watch = new System.Diagnostics.Stopwatch();

            // Start performance metric
            watch.Start();
            if (sortOrder == (int)SortOrder.Ascending)
                _database.Numbers.AddRange(numbers.OrderBy(num => num.Integer));
            else
                _database.Numbers.AddRange(numbers.OrderByDescending(num => num.Integer));
            watch.Stop();
            tempSort.SortTime = watch.ElapsedMilliseconds;
            tempSort.Numbers = numbers;
            _database.Sorts.Add(tempSort);
            _database.SaveChanges();

            return RedirectToAction("Index", "Sorts");
        }

        public IActionResult RemoveSort(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Sort sortRecord = _database.Sorts.Find(Id);
            _database.Remove(sortRecord);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteDatabase()
        {
            foreach (Sort obj in _database.Sorts)
            {
                _database.Remove(obj);
            }
            _database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ExportJSON()
        {
            if (_database.Numbers.ToList().Count > 0)
            {
                return Json(_database.Sorts.ToList(), new JsonSerializerOptions { WriteIndented = true });
            }

            return RedirectToAction("Index", "Sorts");
        }
    }
}
