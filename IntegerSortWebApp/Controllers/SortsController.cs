using IntegerSortWebApp.App_Data;
using IntegerSortWebApp.Models;
using Microsoft.AspNetCore.Mvc;

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

            Sort sort = new Sort();
            string formIntegerInput = numbersToAdd["Integer"];
            String[] strings = formIntegerInput.Split(",");
            List<Number> numbers = new List<Number>();

            for (int i = 0; i < strings.Length; i++)
            {
                Number number = new Number();
                number.Integer = Convert.ToInt32(strings[i]);
                numbers.Add(number);
            }

            var watch = new System.Diagnostics.Stopwatch();

            // Start performance metric
            watch.Start();
            _database.Numbers.AddRange(numbers.OrderByDescending(num => num.Integer));
            watch.Stop();
            sort.SortTime = watch.ElapsedMilliseconds;
            sort.SortDirection = (int)SortOrder.Descending;

            sort.Numbers = numbers;
            _database.Sorts.Add(sort);
            _database.SaveChanges();

            return RedirectToAction("Index", "Sorts");
        }

        public IActionResult RemoveSort(int Id)
        {
            Sort sortRecord = _database.Sorts.Find(Id);
            _database.Remove(sortRecord);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditSort(int id)
        {
            return RedirectToAction("Index", "Sorts");

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
            return View();
        }
    }
}
