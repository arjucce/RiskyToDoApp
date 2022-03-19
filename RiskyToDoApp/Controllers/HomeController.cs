using Microsoft.AspNetCore.Mvc;
using RiskyToDoApp.Data;
using RiskyToDoApp.Models;
using System.Diagnostics;

namespace RiskyToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public TaskVM taskVM { get; set; }


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            taskVM = new TaskVM()
            {
                Task = new Models.Task(),
                TaskList = _context.Tasks.ToList()
            };
            return View(taskVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                task = new Models.Task()
                {
                    Name = task.Name,
                    Date = DateTime.Now,
                    Importance = 0,
                    Remove = 0,
                    Complete = 0
                };

                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        public IActionResult Important(int id)
        {
            var task = _context.Tasks.FirstOrDefault(a => a.Id == id);
            if (task.Importance == 0)
            {
                task.Importance = 1;
            }
            else
            {
                task.Importance = 0;
            }
            
            
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int? id)
        {
            var task = _context.Tasks.FirstOrDefault(a => a.Id == id);            
            _context.Remove(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Complete(int? id)
        {
            var task = _context.Tasks.FirstOrDefault(a => a.Id == id);
            if (task.Complete == 0)
            {
                task.Complete = 1;
            }
            else
            {
                task.Complete = 0;
            }
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}