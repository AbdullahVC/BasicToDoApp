using Microsoft.AspNetCore.Mvc;
using ToDoApp.Entities;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TaskController : Controller
    {
        static List<TaskEntity> _tasks = new List<TaskEntity>()
        {
            new TaskEntity{Id = 1, Title = "Toplantıya Katıl", Content = "Müştei Toplantısına katıl.", OwnerId = 1},
            new TaskEntity{Id = 2, Title = "Rapor Hazırla", Content = "Satış raporunu hazırla."},
            new TaskEntity{Id = 3, Title = "Egzersiz Yap", Content = "30 dk. yürüyüş yap.", IsDone = true},
        };

        static List<OwnerEntity> _owners = new List<OwnerEntity>()
        {
        new OwnerEntity { Id = 1, Name = "Sertan Bozkuş" },
        new OwnerEntity { Id = 2, Name = "Ajda Pekkan" }
        };





        public IActionResult List()
        {

            var viewModel = _tasks.Where(x => x.IsDeleted == false).Select(x => new TaskListViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                IsDone = x.IsDone,
            }).ToList();



            return View(viewModel);

        }
        [HttpGet]
        public IActionResult CompleteTask(int id)
        {
            var task = _tasks.Find(x => x.Id == id);

            task.IsDone = !task.IsDone;

            task.CompletedDate = DateTime.Now;

            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Owners = _owners;

            return View();
        }

        [HttpPost]
        public IActionResult Add(TaskAddViewModel fromData)
        {
            if (!ModelState.IsValid)
            {
                return View(fromData);
            }

            int maxId = _tasks.Max(x => x.Id);

            var newTask = new TaskEntity()
            {
                Id = maxId+1,
                Title = fromData.Title,
                Content = fromData.Content,
                OwnerId = fromData.OwnerId
            };

            _tasks.Add(newTask);

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        { 
            var task = _tasks.Find( x=>  x.Id == id );

            var viewModel = new TaskEditViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Content = task.Content,
                OwnerId = task.OwnerId
            };

            ViewBag.Owners = _owners;

            return View(viewModel);



        }

        [HttpPost]
        public IActionResult Edit(TaskEditViewModel fromData)
        {
            if (!ModelState.IsValid) 
            {
                return View(fromData);
            }

            var task = _tasks.Find(x=>  x.Id == fromData.Id);

            task.Title = fromData.Title;
            task.Content = fromData.Content;
            task.OwnerId = fromData.OwnerId;



            return RedirectToAction("List");
        }








        public IActionResult CancelTask(int id) 
        {
            var task = _tasks.Find(x => x.Id == id);
            
            task.IsDeleted = true;
        
        
            return RedirectToAction("List");
        }







    }
}
