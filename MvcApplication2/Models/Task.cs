using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace MvcApplication2.Models
{
    public class Task : EntryBase
    {
        [Required(ErrorMessage = "Требуется имя задачи")]
        [MaxLength(50, ErrorMessage = "Не больше 50 символов в названии задачи.")]
        public string Name { get; set; }
        [Required]
        public StateEnum State { get; set; }
        [Required]
        public PriorityEnum Priority { set; get; }
        [Required]
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
