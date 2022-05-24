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

        public async Task<IActionResult> Index(int? Id)
        {
            if (Id == null || _database.Numbers == null || _database.Sorts == null)
                return RedirectToAction("Index", "Sorts");

            IEnumerable<Number> numberList = _database.Numbers.Where(n => n.SortID == Id);

            int numb = numberList.Count();

            if (numberList.Count() != 0)
                return View(numberList);

            // remove sort as all numbers have been deleted from the sort
                Sort? sortToRemove = await _database.Sorts.FindAsync(Id);
            if (sortToRemove != null)
            {
                _database.Sorts.Remove(sortToRemove);
                await _database.SaveChangesAsync();
                return RedirectToAction("Index", "Sorts");
            }

            return RedirectToAction("Index", "Sorts");           
        }

        public IActionResult AddNumbers()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNumbers(IFormCollection numbersToAdd)
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

            await _database.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveNumberFromSort(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Number numberRecord = _database.Numbers.Find(Id);

            if (numberRecord == null)
            {
                return NotFound();
            }

            _database.Remove(numberRecord);
            _database.SaveChanges();
            return RedirectToAction("Index", "Number", new { id = numberRecord.SortID });
        }

        public IActionResult EditNumber(int Id)
        {
            if (Id == 0)
            return RedirectToAction("Index");


            var numberRecord = _database.Numbers.Find(Id);

            return View(numberRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNumber(int? id, [Bind("Id", "Integer", "Sort", "SortID")] Number newNum)
        {

            _database.Numbers.Update(newNum);
            await _database.SaveChangesAsync();
            return RedirectToAction("Index", "Number", new { id = newNum.SortID });
        }
    }
}
