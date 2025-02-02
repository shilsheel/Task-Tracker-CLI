// See https://aka.ms/new-console-template for more information
using TaskTrackerCLI.Models;

TaskStorage.EnsureJsonFileExists();
if (args.Length == 0)
{
    Console.WriteLine("Please enter a command.");
    return;
}

var command = args[0].ToLower();


var tasks = TaskStorage.LoadTasks();

switch (command)
{
    case "add":
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: add <title> <description> <status>");
            return;
        }
        string title = args[1];
        string description = args[2];
        string status = args[3];

        int newId = tasks.Count + 1;

        var newTask = new TaskTrackerCLI.Models.Task
        {
            Id = newId,
            Status = status,
            Description = description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        tasks.Add(newTask);
        try
        {
            TaskStorage.SaveTasks(tasks);
            Console.WriteLine($"Task added successfully (ID: {newId})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks: {ex.Message}");
        }
        break;

    case "update":
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: update <task_id> <new_description> <new_status>");
            return;
        }
        if (!int.TryParse(args[1], out int updateId))
        {
            Console.WriteLine("Invalid task ID.");
            return;
        }

        string newDescription = args[2];
        string newStatus = args[3];


        var taskToUpdate = tasks.Find(t => t.Id == updateId);
        if (taskToUpdate == null)
        {
            Console.WriteLine("Task with the provided ID was not found.");
            return;
        }

        // Update the title and the updated time
        taskToUpdate.Description = newDescription;
        taskToUpdate.Status = newStatus;
        taskToUpdate.UpdatedAt = DateTime.Now;

        try
        {
            TaskStorage.SaveTasks(tasks);
            Console.WriteLine($"Task with ID {updateId} has been updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving update changes: {ex.Message}");
        }
        break;

    case "delete":
        if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
        {
            Console.WriteLine("Please enter the task ID.");
            return;
        }
        tasks.RemoveAll(t => t.Id == deleteId);
        try
        {
            TaskStorage.SaveTasks(tasks);
            Console.WriteLine($"Task with ID {deleteId} has been deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving delete changes: {ex.Message}");
        }
        break;

    case "mark-in-progress":
        if (args.Length < 2 || !int.TryParse(args[1], out int progressId))
        {
            Console.WriteLine("Please enter a valid task ID.");
            return;
        }

        var taskInProgress = tasks.Find(t => t.Id == progressId);
        if (taskInProgress == null)
        {
            Console.WriteLine("Task with the provided ID was not found.");
            return;
        }

        taskInProgress.Status = "in-progress";
        taskInProgress.UpdatedAt = DateTime.Now;
        try
        {
            TaskStorage.SaveTasks(tasks);
            Console.WriteLine($"Task with ID {progressId} is now in progress.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving mark changes: {ex.Message}");
        }
        break;

    case "mark-done":
        if (args.Length < 2 || !int.TryParse(args[1], out int doneId))
        {
            Console.WriteLine("Please enter a valid task ID.");
            return;
        }

        var taskDone = tasks.Find(t => t.Id == doneId);
        if (taskDone == null)
        {
            Console.WriteLine("Task with the provided ID was not found.");
            return;
        }

        taskDone.Status = "done";
        taskDone.UpdatedAt = DateTime.Now;
        try
        {
            TaskStorage.SaveTasks(tasks);
            Console.WriteLine($"Task with ID {doneId} has been marked as done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving mark changes: {ex.Message}");
        }
        
        break;
    case "mark-not-done":
        if (args.Length < 2 || !int.TryParse(args[1], out int notDoneId))
        {
            Console.WriteLine("Please enter a valid task ID.");
            return;
        }

        var taskNotDone = tasks.Find(t => t.Id == notDoneId);
        if (taskNotDone == null)
        {
            Console.WriteLine("Task with the provided ID was not found.");
            return;
        }

        taskNotDone.Status = "not-done";
        taskNotDone.UpdatedAt = DateTime.Now;
        try
        {
            TaskStorage.SaveTasks(tasks);
            Console.WriteLine($"Task with ID {notDoneId} has been marked as not done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving mark changes: {ex.Message}");
        }

        break;

    case "list":
        string statusFilter = args.Length > 1 ? args[1].ToLower() : "all";

        var filteredTasks = statusFilter switch
        {
            "done" => tasks.FindAll(t => t.Status == "done"),
            "not-done" => tasks.FindAll(t => t.Status == "not-done"),
            "in-progress" => tasks.FindAll(t => t.Status == "in-progress"),
            _ => tasks
        };

        Console.WriteLine("Task List:");
        foreach (var task in filteredTasks)
        {
            Console.WriteLine($"ID: {task.Id} | Description: {task.Description} | Status: {task.Status}");
        }
        break;
    default:
        Console.WriteLine("Unknown command.");
        break;
}
