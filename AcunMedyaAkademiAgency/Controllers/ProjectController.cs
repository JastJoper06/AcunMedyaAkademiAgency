using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiAgency.Entities;
using AcunMedyaAkademiAgency.Context;

namespace AcunMedyaAkademiAgency.Controllers
{
    public class ProjectController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult ProjectList(string searchText)
        {
            List<Project> values;
            if (searchText != null)
            {
                values = context.Projects.Where(x => x.Title.Contains(searchText)).ToList();
                return View(values);
            }

            values = context.Projects.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateProject()
        {
            List<SelectListItem> values1 = (from x in
                                              context.Categories.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryId.ToString(),
                                            }).ToList();
            ViewBag.v = values1;
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(Project project)
        {
            context.Projects.Add(project);
            context.SaveChanges();
            return RedirectToAction("ProjectList");
        }
        public ActionResult DeleteProject(int id)
        {
            var value = context.Projects.Find(id);
            context.Projects.Remove(value);
            context.SaveChanges();
            return RedirectToAction("ProjectList");
        }
        [HttpGet]
        public ActionResult UpdateProject(int id)
        {
            var project = context.Projects.Find(id);
            if (project == null)
            {
                return RedirectToAction("ProjectList");
            }

            // Kategori listesini ViewBag ile gönder
            var categories = context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();
            ViewBag.v = categories;
            return View(project);
        }
        public ActionResult UpdateProject(Project project)
        {
            // context'in doğru şekilde enjekte edildiğinden emin olun
            var value = context.Projects.Find(project.ProjectId);
            if (value == null)
            {
                // Eğer proje bulunamazsa, hata mesajı gösterip geri dön
                TempData["Error"] = "Proje bulunamadı.";
                return RedirectToAction("ProjectList");
            }

            value.Title = project.Title;
            value.ImageUrl = project.ImageUrl;
            value.CategoryId = project.CategoryId;
            context.SaveChanges();
            return RedirectToAction("ProjectList");
        }
    }
}