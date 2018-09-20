using System;
using System.Collections.Generic;

namespace CapsuleTaskManage
{
    public class TaskView
    {
        public int Task_ID { get; set; }
        public int Parent_ID { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string TaskDec { get; set; }
        public string ParentTaskDesc { get; set; }
    }
}