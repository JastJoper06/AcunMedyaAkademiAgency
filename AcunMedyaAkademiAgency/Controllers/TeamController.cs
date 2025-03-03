using AcunMedyaAkademiAgency.Context;
using AcunMedyaAkademiAgency.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyWebSite.Controllers
{
    public class TeamController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult TeamList(string searchText)
        {
            List<Team> values;
            if (searchText != null)
            {
                values = context.Teams.Where(x => x.NameSurname.Contains(searchText)).ToList();
                return View(values);
            }

            values = context.Teams.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateTeam()
        {
            List<SelectListItem> values1 = (from x in
                                              context.Branches.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.BranchName,
                                                Value = x.BranchId.ToString(),
                                            }).ToList();
            ViewBag.v = values1;
            return View();
        }
        [HttpPost]
        public ActionResult CreateTeam(Team team)
        {
            context.Teams.Add(team);
            context.SaveChanges();
            return RedirectToAction("TeamList");
        }
        public ActionResult DeleteTeam(int id)
        {
            var value = context.Teams.Find(id);
            context.Teams.Remove(value);
            context.SaveChanges();
            return RedirectToAction("TeamList");
        }
        [HttpGet]
        public ActionResult UpdateTeam(int id)
        {
            var team = context.Teams.Find(id);
            if (team == null)
            {
                TempData["Error"] = "Ekip bulunamadı.";
                return RedirectToAction("TeamList");
            }

            // Kategori listesini ViewBag ile gönder
            var branches = context.Branches
                .Select(c => new SelectListItem
                {
                    Value = c.BranchId.ToString(),
                    Text = c.BranchName
                }).ToList();
            ViewBag.v = branches;

            return View(team);
        }
        [HttpPost]
        public ActionResult UpdateTeam(Team team)
        {
            var value = context.Teams.Find(team.TeamId);
            if (value == null)
            {
                TempData["Error"] = "Ekip bulunamadı.";
                return RedirectToAction("TeamList");
            }

            // BranchID'nin Branches tablosunda var olup olmadığını kontrol et
            var branchExists = context.Branches.Any(b => b.BranchId == team.BranchId);
            if (!branchExists)
            {
                TempData["Error"] = "Seçilen branş mevcut değil. Lütfen geçerli bir branş seçin.";
                return RedirectToAction("UpdateTeam", new { id = team.TeamId });
            }

            value.NameSurname = team.NameSurname;
            value.ImageUrl = team.ImageUrl;
            value.Gender = team.Gender;
            value.City = team.City;
            value.BranchId = team.BranchId; // Artık güvenli bir şekilde atanabilir
            context.SaveChanges();
            return RedirectToAction("TeamList");
        }
    }
}