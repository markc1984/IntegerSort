using IntegerSortWebApp.App_Data;
using IntegerSortWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegerSortWebApp.Controllers
{
    public class NumberController : Controller
    {
        private readonly IntegerSortDBContext _database;

        public NumberController(IntegerSortDBContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            IEnumerable<Number> numList = _database.Numbers;

            return View(numList);
        }

        public IActionResult AddNumber()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult AddNumber(IFormCollection numbersToAdd)       
        {
            string formIntegerInput = numbersToAdd["Integer"];

            String[] strings = formIntegerInput.Split(",");

            List<Number> numbers = new List<Number>();
            SortPerformance newSortPerformance = new SortPerformance();

            for (int i = 0; i < strings.Length; i++)
            {
                Number number = new Number();
                number.Integer = Convert.ToInt32(strings[i]);
                numbers.Add(number); 
            }

            var watch = new System.Diagnostics.Stopwatch();

            // Start performance metric
            watch.Start();
            _database.AddRange(numbers.OrderByDescending(num => num.Integer));
            watch.Stop();
            newSortPerformance.SortTime = watch.ElapsedMilliseconds;
            _database.SortPerformance = newSortPerformance;
            _database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveNumber(int Id)
        {
            Number numberRecord = _database.Numbers.Find(Id);
            _database.Remove(numberRecord);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult SortAscending()
        {
            IEnumerable<Number> numList = _database.Numbers;
            numList = numList.OrderBy(num => num.Integer);
            return View("Index", numList);
        }

        public IActionResult SortDescending()
        {
            IEnumerable<Number> numList = _database.Numbers;
            numList = numList.OrderByDescending(num => num.Integer);
            return View("Index", numList);
        }

        public IActionResult DeleteDatabase()
        {
            foreach (Number obj in _database.Numbers)
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
