using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOrm.Pocos.SqlGenerator.Attributes;

namespace MvcApplication2.Models
{
    public abstract class EntryBase
    {
        [KeyProperty(Identity = true)]
        public int Id { get; set; }
    }
}
