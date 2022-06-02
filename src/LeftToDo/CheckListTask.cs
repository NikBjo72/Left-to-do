using System;
using System.Collections.Generic;

namespace LeftToDo
{
    public class CheckListTask : Task
    {

        public List<SubTask> subTaskList {get; private set;}

        public CheckListTask(string taskInfo) : base(taskInfo)
        {
           this.subTaskList = new List<SubTask>(); 
        }

        public void AddSubTask(string subTaskInfo)
        {
            subTaskList.Add (new SubTask(subTaskInfo));
        }

        public void SetSubtaskToDone(int idx)
        {
            subTaskList[idx].SetSubtaskToDone();
        }

        public bool GetSubtaskDone(int idx)
        {
            return subTaskList[idx].GetSubtaskDone();
        }

        public int GetSubtaskListLength()
        {
            return subTaskList.Count;
        }

        public bool CheckIfAllSubTasksAreDone()
        {
            var checkedSubTasks = subTaskList.FindAll(t => t.GetSubtaskDone() == true);
            if (checkedSubTasks.Count == subTaskList.Count)
            {
                return true;
            }
            else return false;
        }
    }
}