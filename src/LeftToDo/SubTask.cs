using System;
using System.Collections.Generic;

namespace LeftToDo
{
    public class SubTask
    {
        bool done;
        string subTaskInfo;

        public SubTask(string subTaskInfo)
        {
            this.subTaskInfo = subTaskInfo;
            this.done = false;
        }

        public void SetSubtaskToDone()
        {
            this.done = true;
        }

        public bool GetSubtaskDone()
        {
            return this.done;
        }

        public string GetSubTaskInfo()
        {
            return this.subTaskInfo;
        }
    }
}