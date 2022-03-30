using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private BowlerDbContext context { get; set; }

        public HomeController(BowlerDbContext temp)
        {
            context = temp;
        }

        //Default id is set to 0 here so it will load all records for every team by default
        public IActionResult Index(int id=0)
        {
            //If the id is 0, it will return a full list of bowlers to the view
            if(id == 0)
            {
                var bowlers = context.bowlers.OrderBy(x => x.BowlerLastName).ToList();
                ViewBag.Teams = context.teams.ToList();
                ViewBag.CurrentTeamID = id;
                return View(bowlers);
            }
            //Depending on the id that is passed in from the team list, it will return a list of bowlers on that team
            else
            {
                var bowlers = context.bowlers.Include(x => x.Team).Where(x => x.TeamID == id).ToList();
                ViewBag.Teams = context.teams.ToList();
                ViewBag.CurrentTeamID = id;
                return View(bowlers);
            }
            
        }
        

        [HttpGet]
        public IActionResult CreateBowler()
        {
            ViewBag.Teams = context.teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateBowler(Bowler bowler)
        {
            //Here is some data validation to make sure that the user is creating a valid record
            if (ModelState.IsValid)
            {
                context.Add(bowler);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = context.teams.ToList();
                return View(bowler);
            }

            
        }

        [HttpGet]
        public IActionResult EditBowler()
        {
            //This will take the user back to the createbowler page and preload it with info about the bowler they selected
            int id = Convert.ToInt32(RouteData.Values["id"]);
            var bowler = context.bowlers.Single(x => x.BowlerID == id);
            ViewBag.Teams = context.teams.ToList();
            return View("CreateBowler", bowler);
        }

        [HttpPost]
        public IActionResult EditBowler(Bowler bowler)
        {
            //Data validation for editing a bowler
            if (ModelState.IsValid)
            {
                context.Update(bowler);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = context.teams.ToList();
                return View("CreateBowler", bowler);
            }
            
        }

        [HttpGet]
        public IActionResult DeleteBowler()
        {
            //This will take the user to the deletebowler page to confirm that they want to delete this record
            int id = Convert.ToInt32(RouteData.Values["id"]);
            var bowler = context.bowlers.Single(x => x.BowlerID == id);

            return View(bowler);
        }

        [HttpPost]
        public IActionResult DeleteBowler(Bowler b)
        {
            context.Remove(b);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        
    }
}
