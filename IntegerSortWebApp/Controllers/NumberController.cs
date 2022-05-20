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
            return View();
        }

        public IActionResult AddSort()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult AddSort(IFormCollection numbersToAdd)       
        {
            string formIntegerInput = numbersToAdd["Integer"];

            String[] strings = formIntegerInput.Split(",");

            Sort newSort = new Sort();
            List<Number> numbers = new List<Number>();


            for (int i = 0; i < strings.Length; i++)
            {
                Number number = new Number();
                number.Integer = Convert.ToInt32(strings[i]);
                numbers.Add(number); 
            }
       
            _database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveNumber(int Id)
        {
            Number numberRecord = _database.Numbers.Find(Id);
            _database.Remove(numberRecord);
            _database.SaveChanges();

            return RedirectToAction("ViewSort", "Number", new { id = numberRecord.SortID });
        }

        public IActionResult ViewSort(int Id)
        {
            IEnumerable<Number> numberList = _database.Numbers.Where(n => n.SortID == Id);

            if (numberList.Count() != 0)
            {
                return View(numberList);
            }
            else
            {
                Sort sortToRemove = _database.Sorts.Find(Id);
                _database.Sorts.Remove(sortToRemove);
                _database.SaveChanges();
                return RedirectToAction("Index", "Sorts");
            }
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

        public IActionResult ExportJSON()
        {

            return View();
            
        }


    }
}
