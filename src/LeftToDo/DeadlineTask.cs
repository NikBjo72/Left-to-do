using System;
using System.Collections.Generic;

namespace LeftToDo
{
    public class DeadlineTask : Task
    {
        DateTime deadline;
        public DeadlineTask(string taskInfo, DateTime deadline) : base(taskInfo)
        {
            this.deadline = deadline;
        }

        public double GetDeadlineDays()
        {
           DateTime todayDate = DateTime.Now;

           var days = (this.deadline - todayDate).TotalDays;
           return Math.Round(days);
        }

        public override string ToString()
        {
            return base.ToString() + " => Dagar kvar till deadline: " + GetDeadlineDays();
            //return GetDeadlineDays().ToString(); // Endats för testet. Kommentera bort ovan för att det ska funka.
        }
    }
}