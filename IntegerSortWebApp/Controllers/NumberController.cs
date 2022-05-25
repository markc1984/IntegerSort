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

            if (numberList.Count() != 0)
                return View(numberList);


            try
            {
                Sort? sortToRemove = await _database.Sorts.FindAsync(Id);
                if (sortToRemove != null)
                {
                    _database.Sorts.Remove(sortToRemove);
                    await _database.SaveChangesAsync();
                    return RedirectToAction("Index", "Sorts");
                }
            }
            catch (Exception ex)
            {
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

        public async Task<IActionResult> RemoveNumberFromSort(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Sorts");

            Number? numberRecord = await _database.Numbers.FindAsync(Id);

            if (numberRecord == null)
                return View();

            try
            {
                _database.Remove(numberRecord);
                _database.SaveChanges();
                TempData["Success"] = "Successfully removed record";
                return RedirectToAction("Index", "Number", new { id = numberRecord.SortID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error removing the record";
                return RedirectToAction("Index", "Number", new { id = Id });
            }
        }

        public async Task<IActionResult> EditNumber(int Id)
        {
            try
            {
                if (Id == 0)
                    return RedirectToAction("Index");
                Number? numberRecord = await _database.Numbers.FindAsync(Id);
                return View(numberRecord);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Number", new { id = Id });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNumber(int? id, [Bind("Id", "Integer", "Sort", "SortID")] Number newNum)
        {
            try
            {
                _database.Numbers.Update(newNum);
                await _database.SaveChangesAsync();
                TempData["Success"] = "Successfully edited record";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was a problem editing the record";
            }
            return RedirectToAction("Index", "Number", new { id = newNum.SortID });
        }
    }
}
