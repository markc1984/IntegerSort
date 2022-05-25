using IntegerSortWebApp.App_Data;
using IntegerSortWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        public IActionResult CreateSort()
        {
            return RedirectToAction("Index", "Sorts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSort(IFormCollection formCollection)
        {
            Sort newSort = new Sort();
            string integer = formCollection["Integer"];
            int sortOrder;
            if (!Int32.TryParse(formCollection["SortOrder"], out sortOrder))
                return RedirectToAction("Index", "Sorts");
            newSort.SortDirection = sortOrder;
            String[] numberStringArray = integer.Split(",");
            List<Number> numbers = new List<Number>();

            for (int i = 0; i < numberStringArray.Length; i++)
            {
                int newNumber;
                if (!Int32.TryParse(numberStringArray[i], out newNumber))
                    return RedirectToAction("Index", "Sorts");
                numbers.Add(new Number { Integer = newNumber });
            }

            try
            {
                var watch = new System.Diagnostics.Stopwatch();
                // Start performance metric
                watch.Start();
                if (sortOrder == (int)SortOrder.Ascending)
                    _database.Numbers.AddRange(numbers.OrderBy(num => num.Integer));
                else
                    _database.Numbers.AddRange(numbers.OrderByDescending(num => num.Integer));
                watch.Stop();
                newSort.SortTime = watch.ElapsedMilliseconds;
                newSort.Numbers = numbers;
                _database.Sorts.Add(newSort);
                _database.SaveChanges();
                TempData["Success"] = "Successfully added new integers to database";
                return RedirectToAction("Index", "Sorts");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding new integers to database";
                return RedirectToAction("Index");
            }
        }

        public IActionResult RemoveSort(int? Id)
        {
            Sort? sortRecord = _database.Sorts.Find(Id);
            if (sortRecord == null)
                return View();

            try
            {
                _database.Remove(sortRecord);
                _database.SaveChanges();
                TempData["Success"] = "Successfully removed sort from database";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error removing sort from database";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
