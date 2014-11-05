using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication2.DataBase;
using MvcApplication2.Models;


namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        private TaskSevice _sevice;


        public HomeController()
        {
            _sevice = new TaskSevice();
        }
        public ActionResult Index()
        {
            var list = new List<Task>(_sevice.GetRoot());
            return View(list);
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
            var list = _sevice.GetChildren(pid);
            return PartialView(list);
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
            if(ModelState.IsValid)
            { 
                Task task = _sevice.GetFirst(new { Id = id });
                return PartialView(task);
            }
            return new HttpStatusCodeResult(500);
        }

        [HttpPost]
        public ActionResult AddNewTask(Task task)
        {
            if(ModelState.IsValid)
             { 
                if (task.Task_Id == 0)
                    task.Task_Id = null;
                _sevice.Insert(task);
                List<Task> list = new List<Task>();
                list.Add(task);
                return PartialView("GetChildren", list);
             }
            return new HttpStatusCodeResult(500);
        }

        public ActionResult DeleteTask(int id)
        {

            if (_sevice.GetWhere(new { Id = id }).First().Task_Id == null)
            {
                _sevice.DeleteChildren(id);
                _sevice.Delete(new { Id = id });
                //return PartialView("GetChildren", _sevice.GetRoot());
                return new HttpStatusCodeResult(200);
            }
            else
            {
                int pid = (int)(_sevice.GetWhere(new { Id = id }).First().Task_Id);
                _sevice.DeleteChildren(id);
                _sevice.Delete(new { Id = id });
                //return PartialView("GetChildren", _sevice.GetWhere(new { Task_Id = pid }));
                return new HttpStatusCodeResult(200);
            }
        }

        public ActionResult UpdateTask(Task task)
        {
            _sevice.Update(task);
            //List<Task> list = task.Task_Id == null ? new List<Task>(_sevice.GetRoot()) : new List<Task>(_sevice.GetWhere(new { Task_Id = task.Task_Id }));
            return PartialView("TaskPart", task);
        }

        public ActionResult SetActive(int id)
        {
            Task task = _sevice.GetFirst(new {Id = id});
            task.State = StateEnum.Active;
            _sevice.Update(task);
            return PartialView("TaskPart",task);
        }

        public ActionResult SetResolved(int id)
        {
            Task task = _sevice.GetFirst(new { Id = id });
            task.State = StateEnum.Resolved;
            _sevice.Update(task);
            return new HttpStatusCodeResult(200);
        }

        public ActionResult FromResolvedToActive(int id)
        {
            Task task = _sevice.GetFirst(new { Id = id });
            task.State = StateEnum.Active;
            _sevice.Update(task);
            return new HttpStatusCodeResult(200);
        }

        public ActionResult FromResolvedToClosed(int id)
        {
            Task task = _sevice.GetFirst(new { Id = id });
            task.State = StateEnum.Closed;
            _sevice.Update(task);
            return new HttpStatusCodeResult(200);
        }

    }
}
