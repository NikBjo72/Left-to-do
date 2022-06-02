using System;
using System.Collections.Generic;

namespace LeftToDo
{
    public class TaskCollection
    {
        public List<Task> taskList {get; private set;}
        public List<CheckListTask> checkListTaskList {get; private set;}

        public TaskCollection()
        {
            this.taskList = new List<Task>();
            this.checkListTaskList = new List<CheckListTask>();
        }

        public void AddTimelessTask(string taskInfo)
        {
            this.taskList.Add(new TimelessTask(taskInfo));
        }

        public void AddDeadlineTask(string taskInfo, DateTime deadline)
        {
            this.taskList.Add(new DeadlineTask(taskInfo, deadline));
        }

        public void AddCheckListTask(string taskInfo)
        {
            this.checkListTaskList.Add(new CheckListTask(taskInfo));
        }

        public void AddSubTask(int idx, string subTaskInfo)
        {
            checkListTaskList[idx].AddSubTask(subTaskInfo);
        }

        public void SetTaskToDone(int idx)
        {
            taskList[idx].SetTaskToDone();
        }

        public bool GetTaskDone(int idx)
        {
            return taskList[idx].GetTaskDone();
        }

        public bool GetTaskArchived(int idx)
        {
            return taskList[idx].GetTaskArchived();
        }

        public bool GetCheckListTaskDone(int idx)
        {
            return checkListTaskList[idx].GetTaskDone();
        }

        public bool GetCheckListTaskArchived(int idx)
        {
            return checkListTaskList[idx].GetTaskArchived();
        }
        public void SetCheckListTaskToDone(int idx)
        {
            checkListTaskList[idx].SetTaskToDone();
        }

        public void ArchiveDoneTasks()
        {
            foreach (var task in taskList)
            {
                if (task.GetTaskDone() == true)
                {
                    task.SetTaskToArchived();
                }
            }
            foreach (var task in checkListTaskList)
            {
                if (task.GetTaskDone() == true)
                {
                    task.SetTaskToArchived();
                }
            }
        }

        public void SetSubtaskToDone(int TaskIdx, int SubIdx)
        {
            this.checkListTaskList[TaskIdx].SetSubtaskToDone(SubIdx);
        }

        public int GetTaskAmountInTaskList()
        {
            return taskList.Count;
        }

        public bool CheckIfAllSubTasksAreDone(int idx)
        {
            return checkListTaskList[idx].CheckIfAllSubTasksAreDone();
        }

        public int GetLatestAddedCheckListTask()
        {
            return checkListTaskList.Count - 1;
        }

        public int GetSubtaskListLength(int idx){
            return checkListTaskList[idx].GetSubtaskListLength();
        }

        public int GetAmountArchivedTasks(){
            var amountArchivedTasks = taskList.FindAll(t => t.GetTaskArchived() == true);
            return amountArchivedTasks.Count;      
        }
        public int GetAmountArchivedCeckTasks(){
            var amountArchivedCheckTasks = checkListTaskList.FindAll(t => t.GetTaskArchived() == true);
            return amountArchivedCheckTasks.Count;       
        }

    }
}