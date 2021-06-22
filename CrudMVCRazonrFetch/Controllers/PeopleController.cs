using CrudMVCRazonrFetch.Models;
using CrudMVCRazonrFetch.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudMVCRazonrFetch.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<ListPeopleViewModel> lst = new List<ListPeopleViewModel>();
            using (CrudMVCRazorFechEntities db = 
                new CrudMVCRazorFechEntities())
            {
                lst =
                    (from d in db.people
                     orderby d.id descending
                     select new ListPeopleViewModel
                     {
                         Id = d.id,
                         Name = d.name,
                         Age = d.age
                     }).ToList();
            }
                return View(lst);
        }

        public ActionResult New()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Save(PeopleViewModel model)
        {
            try
            {
                using (CrudMVCRazorFechEntities db = new CrudMVCRazorFechEntities())
                {
                    var oPeople = new person();
                    oPeople.name = model.Name;
                    oPeople.age = model.Age;
                    db.people.Add(oPeople);
                    db.SaveChanges();
                }

                    return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return View();
        }

        public ActionResult Edit(int Id)
        {
            PeopleViewModel model = new PeopleViewModel();
            using (CrudMVCRazorFechEntities db = new CrudMVCRazorFechEntities())
            {
                var oPeople = db.people.Find(Id);
                model.Name = oPeople.name;
                model.Age = oPeople.age;
                model.Id = oPeople.id;
            }

                return View(model);
        }

        [HttpPost]
        public ActionResult Update(PeopleViewModel model)
        {
            try
            {
                using (CrudMVCRazorFechEntities db = new CrudMVCRazorFechEntities())
                {
                    var oPeople = db.people.Find(model.Id);
                    oPeople.name = model.Name;
                    oPeople.age = model.Age;
                    db.Entry(oPeople).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(PeopleViewModel model)
        {
            try
            {
                using (CrudMVCRazorFechEntities db = new CrudMVCRazorFechEntities())
                {
                    var oPeople = db.people.Find(model.Id);
                    db.people.Remove(oPeople);
                    db.SaveChanges();
                }

                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View();
        }
    }
}