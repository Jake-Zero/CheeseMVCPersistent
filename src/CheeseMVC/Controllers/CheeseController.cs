using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        
        public IActionResult Index()
        {
            IList<Cheese>cheeses = context.Cheeses.Include(c => c.Category).ToList();
           
            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();
                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        [HttpPost]
        [Route("/Cheese")]
        [Route("/Cheese/Index")]
        public IActionResult RemoveCheese(int[] cheese_remove)
        {
            foreach (int cheese in cheese_remove)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheese);
                context.Cheeses.Remove(theCheese);
            }
            context.SaveChanges();
            
            return Redirect("/Cheese");
        }
 
    }
}