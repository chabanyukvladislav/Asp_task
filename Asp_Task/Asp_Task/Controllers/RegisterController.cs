using Asp_Task.Models;
using Asp_Task.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Task.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ICookieProvider _provider;

        public RegisterController(ICookieProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public IActionResult Complete()
        {
            var user = _provider.GetUser();
            if (!TryValidateModel(user.FullName))
                return RedirectToAction("Step1");

            if (!TryValidateModel(user.AdditionalInfo))
                return RedirectToAction("Step2");

            return View(user);
        }

        [HttpGet]
        public IActionResult Step1()
        {
            var fio = _provider.GetUser().FullName;
            return View(fio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step1(UserFullName fio)
        {
            _provider.IncrementValidationCounter();
            if (!ModelState.IsValid)
                return View(fio);

            var user = _provider.GetUser();
            user.FullName = fio;
            _provider.SaveUser(user);
            return RedirectToAction("Continue");
        }

        [HttpGet]
        public IActionResult Step2()
        {
            var additionalInfo = _provider.GetUser().AdditionalInfo;
            return View(additionalInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step2(UserAdditionalInfo additionalInfo)
        {
            _provider.IncrementValidationCounter();
            if (!ModelState.IsValid)
                return View(additionalInfo);

            var user = _provider.GetUser();
            user.AdditionalInfo = additionalInfo;
            _provider.SaveUser(user);
            return RedirectToAction("Continue");
        }

        [HttpGet]
        public IActionResult Continue()
        {
            var user = _provider.GetUser();
            var validate = TryValidateModel(user.FullName);
            ModelState.Clear();
            if (!validate)
                return RedirectToAction("Step1");

            validate = TryValidateModel(user.AdditionalInfo);
            ModelState.Clear();
            return RedirectToAction(!validate ? "Step2" : "Complete");
        }
    }
}