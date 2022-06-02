using System;
using System.Collections.Generic;
using Xunit;

namespace LeftToDo.Tests
{
    public class LeftToDoTests
    {
        [Fact]
        public void AddTaskToList()
        {
            //Arrange
            var niklas = new TaskCollection();
            string taskInfo1 = "Min första uppgift!";
            string taskInfo2 = "Min andra uppgift!";
            int expectedTaskAmountInList = 2;

            //Act
            niklas.AddTimelessTask(taskInfo1);
            niklas.AddTimelessTask(taskInfo2);

            int taskAmount = niklas.GetTaskAmountInTaskList();

            //Assert
            Assert.Equal(expectedTaskAmountInList,taskAmount);

        }

        [Fact]
        public void TaskIsMarkedAsDone()
        {
            //Arrange
            var niklas = new TaskCollection();
            string taskInfo1 = "Min första uppgift!";
            string taskInfo2 = "Min andra uppgift!";
            bool expectedThatTaskIsDone = true;
            bool expectedThatTaskIsNotDone = false;

            //Act
            niklas.AddTimelessTask(taskInfo1);
            niklas.AddTimelessTask(taskInfo2);
            niklas.SetTaskToDone(0);
            bool taskIdx1 = niklas.GetTaskDone(0);
            bool taskIdx2 = niklas.GetTaskDone(1);

            //Assert
            Assert.Equal(expectedThatTaskIsDone,taskIdx1);
            Assert.Equal(expectedThatTaskIsNotDone,taskIdx2);
        }

        [Fact]
        public void FileDoneTasks()
        {
            //Arrange
            var niklas = new TaskCollection();
            string taskInfo1 = "Min första uppgift!";
            string taskInfo2 = "Min andra uppgift!";
            bool expectedFiledTasks = true;
            bool expectedNotFiledTasks = false;

            //Act
            niklas.AddTimelessTask(taskInfo1);
            niklas.AddTimelessTask(taskInfo2);

            niklas.SetTaskToDone(0);

            niklas.ArchiveDoneTasks();

            bool taskOneDone = niklas.GetTaskDone(0);
            bool taskTwoDone = niklas.GetTaskDone(1);

            //Assert
            Assert.Equal(expectedFiledTasks, taskOneDone);
            Assert.Equal(expectedNotFiledTasks, taskTwoDone);
        }

        [Fact]
        public void CheckDaysToDeadlineForDeadlineTask()
        {
            //Arrange
            var niklas = new TaskCollection();
            string taskInfo1 = "Min första tidssatta uppgift!";
            string taskInfo2 = "Min andra tidssatta uppgift!";

            int expectedDaysToDeadline1 = 27;
            int expectedDaysToDeadline2 = 13;

            string pattern = "yyyy/M/dd";
            DateTime deadlineDate1 = DateTime.ParseExact("2021/12/30", pattern, null);
            DateTime deadlineDate2 = DateTime.ParseExact("2021/12/16", pattern, null);

            //Act
            //**** Första uppgiften ****
            niklas.AddDeadlineTask(taskInfo1, deadlineDate1);
            int daysToDeadline1 = Int32.Parse(niklas.taskList[0].ToString());

            //**** Andra uppgiften ****
            niklas.AddDeadlineTask(taskInfo2, deadlineDate2);
            int daysToDeadline2 = Int32.Parse(niklas.taskList[1].ToString());

            //Assert
            Assert.Equal(expectedDaysToDeadline1, daysToDeadline1);
            Assert.Equal(expectedDaysToDeadline2, daysToDeadline2);

        }

        [Fact]
        public void CheckIfAllSubtasksIsCheckedForCheckListTask()
        {
            //Arrange
            var niklas = new TaskCollection();
            string taskInfo = "Min första uppgift med checklista!";
            string subTask1 = "subTask 1";
            string subTask2 = "subTask 2";
            string subTask3 = "subTask 3";
            string subTask4 = "subTask 4";
            bool expektedAllSubTasksIsChecked = true;
            bool AllSubTasksIsChecked = false;

            //Act
            niklas.AddCheckListTask(taskInfo);
            niklas.AddSubTask(0, subTask1);
            niklas.AddSubTask(0, subTask2);
            niklas.AddSubTask(0, subTask3);
            niklas.AddSubTask(0, subTask4);

            niklas.SetSubtaskToDone(0, 0);
            niklas.SetSubtaskToDone(0, 1);
            niklas.SetSubtaskToDone(0, 2);
            niklas.SetSubtaskToDone(0, 3);

            AllSubTasksIsChecked = niklas.CheckIfAllSubTasksAreDone(0);

            //Assert
            Assert.Equal(expektedAllSubTasksIsChecked, AllSubTasksIsChecked);
        }
    }
}
