using Asp_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Task.Controllers
{
    public class UserController : Controller
    {
        public const string PRESS_COUNT_KEY = "PressCount";

        [HttpGet]
        public IActionResult Index()
        {
            var fio = new Fio
            {
                Name = Request.Cookies[nameof(Models.User.Fio.Name)],
                Surname = Request.Cookies[nameof(Models.User.Fio.Surname)]
            };
            if (!TryValidateModel(fio))
                return RedirectToAction("CreateFio");

            var user = new User(fio)
            {
                Email = Request.Cookies[nameof(Models.User.Email)]
            };
            if (!TryValidateModel(user))
                return RedirectToAction("CreateEmail");

            return View(user);
        }

        [HttpGet]
        public IActionResult CreateFio()
        {
            var fio = new Fio
            {
                Name = Request.Cookies[nameof(Models.User.Fio.Name)],
                Surname = Request.Cookies[nameof(Models.User.Fio.Surname)]
            };
            return View(fio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFio(Fio fio)
        {
            IncrementValidationCount();
            if (!ModelState.IsValid)
                return View(fio);

            Response.Cookies.Append(nameof(fio.Name), fio.Name);
            Response.Cookies.Append(nameof(fio.Surname), fio.Surname);
            return Continue(new User(fio));
        }

        [HttpGet]
        public IActionResult CreateEmail()
        {
            var fio = new Fio
            {
                Name = Request.Cookies[nameof(Models.User.Fio.Name)],
                Surname = Request.Cookies[nameof(Models.User.Fio.Surname)]
            };
            var validate = TryValidateModel(fio);
            ModelState.Clear();
            if (!validate)
                return RedirectToAction("CreateFio");

            var user = new User(fio)
            {
                Email = Request.Cookies[nameof(Models.User.Email)]
            };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmail(User user)
        {
            IncrementValidationCount();
            if (!ModelState.IsValid)
                return View(user);

            Response.Cookies.Append(nameof(user.Email), user.Email);
            return Continue(user);
        }

        [HttpGet]
        public IActionResult Continue(User user)
        {
            var validate = TryValidateModel(user.Fio);
            ModelState.Clear();
            if (!validate)
                return RedirectToAction("CreateFio");

            validate = TryValidateModel(user);
            ModelState.Clear();
            if (!validate)
                return RedirectToAction("CreateEmail");

            return RedirectToAction("Index");
        }

        private void IncrementValidationCount()
        {
            int.TryParse(Request.Cookies[PRESS_COUNT_KEY], out var validateCount);
            validateCount++;
            Response.Cookies.Append(PRESS_COUNT_KEY, validateCount.ToString());
        }
    }
}