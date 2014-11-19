using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using MvcApplication2.DataBase;
using MvcApplication2.Models;


namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        private IContainer _container; 
        public HomeController()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Repository>().As<IDataRepository<Task>>();
            builder.RegisterType<TaskSevice>().As<IService<Task>>();
            _container = builder.Build();

        }
        public ActionResult Index()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                var list = new List<Task>(_service.GetRoot());
                return View(list);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetChildren(int pid)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                var list = _service.GetChildren(pid);
                return PartialView(list);
            }
        }

        public ActionResult NewTaskWindow(int? pid)
        {
            if (pid == 0)
                pid = null;
            ViewBag.Pid = pid;

            return PartialView();
        }

        public ActionResult EditTaskWindow(int id)
        {

            if (ModelState.IsValid)
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    var _service = scope.Resolve<IService<Task>>();
                    Task task = _service.GetFirst(new { Id = id });
                    return PartialView(task);
                }
            }
            return new HttpStatusCodeResult(500);
        }

        [HttpPost]
        public ActionResult AddNewTask(Task task)
        {
            if (ModelState.IsValid)
            {
                if (task.Task_Id == 0)
                    task.Task_Id = null;
                using (var scope = _container.BeginLifetimeScope())
                {
                    var _service = scope.Resolve<IService<Task>>();
                    _service.Insert(task);
                    var list = new List<Task>();
                    list.Add(task);
                    return PartialView("GetChildren", list);
                }
            }
            return new HttpStatusCodeResult(500);
        }

        public ActionResult DeleteTask(int id)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                if (_service.GetWhere(new { Id = id }).First().Task_Id == null)
                {
                    _service.DeleteChildren(id);
                    _service.Delete(new { Id = id });
                    //return PartialView("GetChildren", _sevice.GetRoot());
                    return new HttpStatusCodeResult(200);
                }
                int pid = (int)(_service.GetWhere(new { Id = id }).First().Task_Id);
                _service.DeleteChildren(id);
                _service.Delete(new { Id = id });
                //return PartialView("GetChildren", _sevice.GetWhere(new { Task_Id = pid }));
                return new HttpStatusCodeResult(200);
            }
        }

        public ActionResult UpdateTask(Task task)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                _service.Update(task);
                return PartialView("TaskPart", task);
            }
        }

        public ActionResult SetActive(int id)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                Task task = _service.GetFirst(new { Id = id });
                task.State = StateEnum.Active;
                _service.Update(task);
                return PartialView("TaskPart", task);
            }
        }
        [HttpPost]
        public ActionResult SetResolved(int id)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                Task task = _service.GetFirst(new { Id = id });
                task.State = StateEnum.Resolved;
                _service.Update(task);
            }
            return new HttpStatusCodeResult(200);

        }
        [HttpPost]
        public ActionResult FromResolvedToActive(int id)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                Task task = _service.GetFirst(new { Id = id });
                task.State = StateEnum.Active;
                _service.Update(task);
                return new HttpStatusCodeResult(200);
            }
        }
        [HttpPost]
        public ActionResult FromResolvedToClosed(int id)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var _service = scope.Resolve<IService<Task>>();
                Task task = _service.GetFirst(new { Id = id });
                task.State = StateEnum.Closed;
                _service.Update(task);
            }
            return new HttpStatusCodeResult(200);
        }

    }
}
