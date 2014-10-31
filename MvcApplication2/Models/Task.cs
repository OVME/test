using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace MvcApplication2.Models
{
    public class Task : EntryBase
    {

        public string Name { get; set; }
        public StateEnum State { get; set; }
        public PriorityEnum Priority { set; get; }
        public DateTime Deadline { get; set; }
        public int? Task_Id { get; set; }

    }


    public enum StateEnum
    {
        New,
        Active,
        Resolved,
        Closed
    }

    public enum PriorityEnum
    {
        High,
        Middle,
        Low
    }
}
