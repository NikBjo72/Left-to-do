using System;
using System.Collections.Generic;

namespace LeftToDo
{
    public abstract class Task //Superklass som uppgiftsklasserna ärver av.
    {   
        string taskInfo;
        bool done;
        bool archived;
        
        public Task(string taskInfo)
        {
            this.taskInfo = taskInfo;
            this.done = false;
            this.archived = false;
        }

        public void SetTaskToDone()
        {
            this.done = true;
        }

        public void SetTaskToArchived()
        {
            this.archived = true;
        }

        public bool GetTaskArchived()
        {
            return this.archived;
        }
        
        public bool GetTaskDone()
        {
            return this.done;
        }

        public override string ToString()
        {
           return taskInfo.ToString();
        }
    }
}