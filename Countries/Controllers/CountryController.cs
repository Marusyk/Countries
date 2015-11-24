using System;
using System.Web.Mvc;
using Countries.Models;

namespace Countries.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _repository;

        public CountryController()
        {           
           _repository = new XmlCountryRepository();         
        }

        public ViewResult Create()
        {
            return View("Edit", new CountryInfo());
        }

        public ViewResult Edit(Guid guid)
        {
            var country = _repository.FindBy(x => x.Guid == guid);
            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(CountryInfo country)
        {
            if (!ModelState.IsValid)
            {
                return View(country);
            }

            _repository.Save(country);              
            TempData["message"] = string.Format("{0} has been saved", country.Name);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid guid)
        {
            var deletedCountry = _repository.Delete(guid);
           
            if (deletedCountry != null)
            {
                TempData["message"] = string.Format("{0} was deleted.", deletedCountry.Name);
            }

            return RedirectToAction("Index");
        }

        public ViewResult Details(Guid guid)
        {
            var country = _repository.FindBy(x => x.Guid == guid);
            return View(country);
        }

        public ActionResult Index()
        {
            return View(_repository.Countries);
        }
       
    }
}