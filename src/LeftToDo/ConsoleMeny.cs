using System;
using System.Collections.Generic;
using System.Linq;

namespace LeftToDo
{
    public class ConsoleMeny
    {
        TaskCollection myTasks;

        public ConsoleMeny()
        {
            this.myTasks = new TaskCollection();
        }

        public void UserInterface()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Välkommen till din uppgiftslista ***\n" +
                    "\nI menyn nedan kan du välja olika uppgifter att lägga i din lista\n" +
                    "\n[1] Lägg till en ny uppgift\n" +
                    "[2] Lista alla dagens uppgifter\n" +
                    "[3] Lista alla arkiverade uppgifter\n" +
                    "[4] Arkivera alla avklarade uppgifter\n" +
                    "[Q] Avsluta programmet\n");

                Console.Write("\nMenyval: ");
                string choise = Console.ReadLine();
                choise = choise.ToUpper();

                switch (choise)
                {
                    case "1":
                        TaskMenu(); //Menyn med olika uppgifter att lägga till.
                    break;
                        
                    case "2":
                        ListTodaysTasks(); //Listar dagens uppgifter samt låter användaren checka av uppgifter.
                    break;

                    case "3":
                        ListArchivedTasks(); //Listar arkiverade uppgifter.
                    break;

                    case "4":
                        ArchiveAllDoneTasks(); //Arkiverar uppgifter som är avklarade.
                    break;
                    
                    case "Q":
                    return;

                    default:
                        NotValidChoise(); //Metod som återanvänds om ett inmatningsval är fel.
                    break;                   
                }

                void TaskMenu()
                {
                    while (true){

                        Console.Clear();
                        Console.WriteLine("Välj vilken sorts uppgift du vill lägga till i din lista:\n" +
                            "\n[1] Tidlös uppgift\n" +
                            "[2] Uppgift med deadline\n" +
                            "[3] Uppgift med checklista\n" +
                            "[Q] Återgå till huvudmenyn\n");

                        Console.Write("\nMenyval: ");
                        string choise = Console.ReadLine();
                        choise = choise.ToUpper();

                        switch (choise)
                        {
                            case "1":
                                AddNewTimelessTask(); //Lägger till en tidlös uppgift.
                            break;

                            case "2":
                                AddNewDeadlineTask(); //Lägger till en uppgift med deadline.
                            break;

                            case "3":
                                AddNewChecklistTask(); //Lägger till en uppgift med checklista.
                            break;

                            case "Q":
                            return;

                            default:
                                NotValidChoise();
                            break;
                        }
                    }
                }

                void AddNewTimelessTask()
                {
                    Console.Clear();
                    Console.WriteLine("Skriv uppgiftens innehåll:");
                    myTasks.AddTimelessTask(ReadLineCheckString());
                    TaskDone(); //Metod som återanvänds och skriver ut att en uppgift är tillagd.
                }

                void AddNewDeadlineTask()
                {
                    Console.Clear();
                    Console.WriteLine("Skriv uppgiftens innehåll:");
                    string taskInfo = ReadLineCheckString();
                    Console.Clear();
                    Console.WriteLine("Skriv in datumet då uppgiften ska vara klar i formatet yyyy/mm/dd: ");
                    string pattern = "yyyy/M/dd";
                    DateTime deadline = new DateTime();

                    while (true)
                    {
                        string inputDeadline = Console.ReadLine();
                        try {
                            deadline = DateTime.ParseExact(inputDeadline, pattern, null);
                            break;
                        }
                        catch {
                            TryAgain();
                        }
                    }    
                    myTasks.AddDeadlineTask(taskInfo, deadline);
                    TaskDone();
                }

                void AddNewChecklistTask()
                {
                    Console.Clear();
                    Console.WriteLine("Skriv uppgiftens innehåll:");
                    myTasks.AddCheckListTask(ReadLineCheckString());
                    Console.Clear();
                    Console.WriteLine("Nu ska du lägga till uppgiftens delmoment. Du kan lägga till hur många du vill.");

                    do {   
                        string choise;

                        if (myTasks.GetSubtaskListLength(myTasks.GetLatestAddedCheckListTask()) > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Vill du lägga till ytterligare ett delmoment till uppgiften? [y/n]\n");
                            Console.Write("Val: ");
                            choise = Console.ReadLine();
                        }

                        else {
                            choise = "y";
                        }

                        switch (choise)
                        {
                            case "y":
                                Console.Clear();
                                Console.WriteLine("Skriv deluppgiftens innehåll: ");
                                myTasks.AddSubTask(myTasks.GetLatestAddedCheckListTask(), ReadLineCheckString());
                            break;

                            case "n":
                                TaskDone();
                            return;

                            default:
                                NotValidChoise();
                            break;
                        }

                    } while (true);
                }

                void ListTodaysTasks()
                {
                    while (true) 
                    {
                        Console.Clear();
                        int count = 1;

                        for (int i = 0; i < myTasks.taskList.Count; i++) //Loop som listar alla uppgifter i listan taskList (tidlösa och tidssatta uppgifter)
                        {
                            if (!myTasks.GetTaskArchived(i)) //Om inte arkiverad uppgift.
                            {
                                Console.WriteLine($"({count}) [{ListCheck(i)}] {myTasks.taskList[i].ToString()}");
                                count++;
                            }    
                        }

                        for (int i = 0; i < myTasks.checkListTaskList.Count; i++) //Loop som listar alla uppgifter med underuppgifter.
                        {
                            if (!myTasks.GetCheckListTaskArchived(i)) //Om inte arkiverad uppgift.
                            {
                                Console.WriteLine($"({count}) [{CheckListCheck(i)}] {myTasks.checkListTaskList[i].ToString()}");
                                count++;
                                int subCount = 1;

                                for (int j = 0; j < myTasks.checkListTaskList[i].subTaskList.Count; j++) //Listar alla underuppgifter.
                                {
                                    Console.WriteLine($"   ({subCount}) [{SubCheck(i,j)}] {myTasks.checkListTaskList[i].subTaskList[j].GetSubTaskInfo()}");
                                    subCount++;
                                }
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;       
                        Console.WriteLine("\nFör att markera en uppgift som klar, skriv siffran för uppgiften och tryck sedan [Enter]\n" +
                            "Vill du markera en underuppgift som klar, skriver du först uppgiftsnumret [semikolon] följt av underuppgiftsnumret\n" +
                            "Ex: (5) [ ] Min uppgift -- (1) [ ] Min underuppgift = Menyval: 5;1\n" +
                            "\nVill du återgå till huvudmenyn tryck Q och [Enter].");
                        Console.ResetColor();
                        
                        while (true)
                        {   //För att inte behöva skriva långa strängar lägger jag dem i variabler.
                            int taskListAmount = myTasks.taskList.Count; //Längd på listan med tidlösa och tidssatta uppgifter.
                            int checkListListAmount = myTasks.checkListTaskList.Count; //Längd på lista med uppgifter med checklista.
                            int totalTaskAmount = taskListAmount + checkListListAmount;

                            Console.Write("\nMenyval: ");
                            string choise = Console.ReadLine();
                            choise = choise.ToUpper();
                            string[] choiseArray = choise.Split(';'); //Splittar valet till en array, så användaren kan mata in två värden i samma inmatning.

                            if (choiseArray[0] == "Q" && choiseArray.Length == 1)
                            {
                                return;
                            }

                            else if (choiseArray.Length == 1) //Eftersom jag har två listor som skrivs ut i numrerad ordning, så blir detta en avancerad uträkning,
                            {                                 //för att hitta rätt index. Går troligt göra på lättare sätt, men kunskapen om inbygda metoder är för dålig.
                                int idx; //Inmatat index
                                int idxAchived = myTasks.GetAmountArchivedTasks(); //Antal arkiverade tidlösa och tidssatta uppgifter.
                                int idxAchivedCheck = myTasks.GetAmountArchivedCeckTasks(); //Antal arkiverade uppgifter med checklista.

                                if (!Int32.TryParse(choiseArray[0], out idx)){ //Om inte giltig inmatning
                                    NotValidChoise();
                                    continue;
                                }

                                else if (idx + idxAchived < (count + idxAchived - checkListListAmount)) //Om uppgiften finns i första taskList.
                                {
                                    myTasks.SetTaskToDone((idx - 1) + idxAchived);
                                    break;
                                }
                                //Om uppgiften finns i checkListTaskList (uppgifter med checklista)
                                else if (idx + idxAchived >= (count + idxAchivedCheck + idxAchived - checkListListAmount) && idx + idxAchivedCheck + idxAchived <= totalTaskAmount)
                                {
                                    if (myTasks.CheckIfAllSubTasksAreDone((idx + idxAchivedCheck + idxAchived - taskListAmount) - 1)) //Kollar om alla uderuppgifter är klara innan hela uppgiften kan sättas som klar.
                                    {
                                        myTasks.SetCheckListTaskToDone((idx + idxAchivedCheck  + idxAchived - taskListAmount) - 1);
                                        break;
                                    }

                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nAlla underuppgifter i din uppgift är inte klara!\n" +
                                            "Markera först dem klara för att kunna markera hela uppgiften som klar.\n");
                                        Console.ResetColor();
                                        Console.WriteLine("Tryck på valfri tangent för att göra ett nytt val.");
                                        Console.ReadKey(false);
                                        continue;  
                                    }
                                }

                                else
                                {
                                   NotValidChoise(); 
                                }
                                
                            }

                            else if (choiseArray.Length == 2) //Om inmatningen har två värden.
                            {   
                                int idxAchived = myTasks.GetAmountArchivedTasks();
                                int idxAchivedCheck = + myTasks.GetAmountArchivedCeckTasks();
                                //Kollar plats 0 i array att värdena som matats in finns i bland de utskrivna samt bara nummer.
                                if (!Int32.TryParse(choiseArray[0], out int FirstIdx) || FirstIdx + idxAchived + idxAchivedCheck > totalTaskAmount) 
                                {
                                    NotValidChoise();
                                    continue;  
                                }

                                int thisSubtaskListLength = myTasks.checkListTaskList[(FirstIdx + idxAchived + idxAchivedCheck - taskListAmount)-1].subTaskList.Count;
                                //Kollar på plats 1 i array att värdena som matats in finns i bland de utskrivna samt bara nummer.
                                if (!Int32.TryParse(choiseArray[1], out int SecondIdx) || SecondIdx > thisSubtaskListLength)
                                {
                                    NotValidChoise();
                                    continue; 
                                }
                                //Räknar ut index samt sätter uppgiften till done.
                                FirstIdx = (FirstIdx + idxAchived + idxAchivedCheck - taskListAmount) - 1;
                                SecondIdx -= 1;
                                myTasks.checkListTaskList[FirstIdx].SetSubtaskToDone(SecondIdx);
                                break;
                            }

                            else 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nSiffran du matade in låg utanför giltigt intervall.\n");
                                Console.ResetColor();
                                Console.WriteLine("Tryck på valfri tangent för att göra ett nytt val.");
                                Console.ReadKey();
                            }
                        }
                    }    
                }

                void ListArchivedTasks()
                {
                    Console.Clear();
                    Console.WriteLine("*** Dina arkiverade uppgifter ***\n");
                    int count = 1;
                    for (int i = 0; i < myTasks.taskList.Count; i++) //Listar alla arkiverade uppgifter.
                    {
                        if (myTasks.GetTaskArchived(i)) //Om arkiverad.
                        {   
                            Console.WriteLine($"({count}) {myTasks.taskList[i].ToString()}");
                            count++;
                        }
                    }
                    for (int i = 0; i < myTasks.checkListTaskList.Count; i++) //Listar alla arkiverade uppgifter med checklista.
                    {
                        if (myTasks.GetCheckListTaskArchived(i)) //Om arkiverad.
                        {
                            Console.WriteLine($"({count}) {myTasks.checkListTaskList[i].ToString()}");
                            count++;
                            int subCount = 1;
                                for (int j = 0; j < myTasks.checkListTaskList[i].subTaskList.Count; j++)
                                {
                                    Console.WriteLine($"   ({subCount}) {myTasks.checkListTaskList[i].subTaskList[j].GetSubTaskInfo()}");
                                    subCount++;
                                }
                        }
                    }
                    Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn.");
                    Console.ReadKey();
                }

                void ArchiveAllDoneTasks()
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Är du säker på att du vill arkivera alla färdigmarkerade uppgifter? [y/n]");
                        Console.Write("Val: ");
                        var answer = Console.ReadLine();
                        answer = answer.ToLower();

                        if (answer == "y") { //Vid svar [y] vid säkerhetsfråga.

                            myTasks.ArchiveDoneTasks();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n*** Alla dina färdiga uppgifter är arkiverade ***");
                            Console.ResetColor();
                            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn.");
                            Console.ReadKey();
                            break;
                        }

                        else if (answer == "n") { //Vid svar [n] vid säkerhetsfråga.
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*** Inga uppgifter arkiverades ***");
                            Console.ResetColor();
                            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn.");
                            Console.ReadKey();
                            break;
                        }

                        else {
                            NotValidChoise();
                        }
                    }
                }

                string ListCheck(int idx) //Metod som returnerer X eller " " om uppfiften är done (för tidlösa och tidssatta uppgifter).
                {
                    string check = " ";
                    if (myTasks.GetTaskDone(idx) == true)
                    {
                        return check = "X";
                    }
                    else return check;
                }

                string CheckListCheck(int idx) //Metod som returnerer X eller " " om uppfiften är done (för checklistuppgifter).
                {
                    string check = " ";
                    if (myTasks.GetCheckListTaskDone(idx) == true)
                    {
                        return check = "X";
                    }
                    else return check;
                }

                string SubCheck(int idx, int SubIdx) //Metod som returnerer X eller " " om uppfiften är done (för underuppgifter).
                {
                    string check = " ";
                    if (myTasks.checkListTaskList[idx].GetSubtaskDone(SubIdx) == true)
                    {
                        return check = "X";
                    }
                    else return check;
                }

                void TaskDone()
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** Din uppgift är tillagd! ***");
                    Console.ResetColor();
                    Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn.");
                    Console.ReadKey();
                }

                string ReadLineCheckString() //Kollar inmatad sträng så den inte är tom.
                {
                    string valueString;
                    while (true)
                    {
                        valueString = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(valueString)) {
                            break;
                        }

                        else {
                            TryAgain();
                        }
                    }    
                    return valueString;
                }

                void TryAgain()
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt inmatning!");
                    Console.ResetColor();
                    Console.WriteLine("Försök igen: "); 
                }

                void NotValidChoise()
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOgiltigt val!\nVar god och välj något av ovanstående val.");
                    Console.ResetColor();
                    Console.WriteLine("Tryck på valfri tangent för att göra ett nytt val.");
                    Console.ReadKey(false);
                }
            }
        }
    }
}
