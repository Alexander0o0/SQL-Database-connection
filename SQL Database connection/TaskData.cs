using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Database_connection
{
    internal class TaskData
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }   
        public string PersonInCharge { get; set; }
        public DateTime Deadline { get; set; }
    }
}
