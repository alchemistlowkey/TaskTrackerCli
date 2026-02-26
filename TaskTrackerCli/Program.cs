using System.Text.Json;
using TaskTrackerCli;

const string FilePath = "task.json";

var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

// Handle command-line arguments
if (args.Length == 0)
{
    Console.WriteLine("""
        Task Tracker CLI Usage:
        task-cli add "Task description" - Adds a new task
        task-cli update <id> "New description" - Updates a task's description
        task-cli delete <id> - Deletes a task
        task-cli mark-in-progress <id> - Marks a task as in-progress
        task-cli mark-done <id> - Marks a task as done
        task-cli list - Lists all tasks
        task-cli list <status> - Lists tasks by status (todo, in-progress, done)
    """);

    return;
}

string command = args[0].ToLower();

if (command == "add")
{
    if (args.Length < 2)
    {
        throw new ArgumentException("Please provide a description for your task");
    }

    var description = args[1];

    List<TaskItem> taskItems;

    if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
    {
        taskItems = [];
    }
    else
    {
        string json = File.ReadAllText(FilePath);
        taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
    }

    var newId = taskItems.Count > 0 ? taskItems.Max(t => t.Id) + 1 : 1;

    var taskToBeAdded = new TaskItem
    {
        Id = newId,
        Description = description,
    };

    taskItems.Add(taskToBeAdded);

    File.WriteAllText(FilePath, JsonSerializer.Serialize(taskItems, jsonOptions));

}

if (command == "update")
{
    if (args.Length < 3)
    {
        throw new ArgumentException("Please provide a task id and a new description");
    }

    var id = args[1];
    if (!int.TryParse(id, out int taskId))
    {
        throw new ArgumentException("Invalid Id format");
    }

    var description = args[2];

    List<TaskItem> taskItems;

    if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
    {
        throw new ArgumentException("No tasks found");
    }
    else
    {
        string json = File.ReadAllText(FilePath);
        taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
    }

    TaskItem taskToUpdate = null!;

    foreach (var taskItem in taskItems)
    {
        if (taskItem.Id == taskId)
        {
            taskToUpdate = taskItem;
        }
    }

    if (taskToUpdate == null)
    {
        throw new ArgumentException("Task not found");
    }

    taskToUpdate.Description = description;
    taskToUpdate.UpdatedAt = DateTime.Now;

    File.WriteAllText(FilePath, JsonSerializer.Serialize(taskItems, jsonOptions));
}

if (command == "delete")
{
    if (args.Length < 2)
    {
        throw new ArgumentException("Please provide a task id");
    }

    var id = args[1];
    if (!int.TryParse(id, out int taskId))
    {
        throw new ArgumentException("Invalid Id format");
    }

    List<TaskItem> taskItems;

    if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
    {
        throw new ArgumentException("No tasks found");
    }
    else
    {
        string json = File.ReadAllText(FilePath);
        taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
    }

    TaskItem taskToDelete = null!;

    foreach (var taskItem in taskItems)
    {
        if (taskItem.Id == taskId)
        {
            taskToDelete = taskItem;
        }
    }

    if (taskToDelete == null)
    {
        throw new ArgumentException("Task not found");
    }

    taskItems.Remove(taskToDelete);

    File.WriteAllText(FilePath, JsonSerializer.Serialize(taskItems, jsonOptions));

}

if (command == "mark-in-progress")
{
    if (args.Length < 2)
    {
        throw new ArgumentException("Please provide a task id");
    }

    var id = args[1];
    if (!int.TryParse(id, out int taskId))
    {
        throw new ArgumentException("Invalid Id format");
    }

    List<TaskItem> taskItems;

    if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
    {
        throw new ArgumentException("No tasks found");
    }
    else
    {
        string json = File.ReadAllText(FilePath);
        taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
    }

    TaskItem taskToMarkInProgress = null!;

    foreach (var taskItem in taskItems)
    {
        if (taskItem.Id == taskId)
        {
            taskToMarkInProgress = taskItem;
        }
    }

    if (taskToMarkInProgress == null)
    {
        throw new ArgumentException("Task not found");
    }

    if (taskToMarkInProgress.Status == "in-progress")
    {
        throw new ArgumentException("Task is already in in-progress status");
    }

    taskToMarkInProgress.Status = "in-progress";
    taskToMarkInProgress.UpdatedAt = DateTime.Now;

    File.WriteAllText(FilePath, JsonSerializer.Serialize(taskItems, jsonOptions));
}

if (command == "mark-done")
{
    if (args.Length < 2)
    {
        throw new ArgumentException("Please provide a task id");
    }

    var id = args[1];
    if (!int.TryParse(id, out int taskId))
    {
        throw new ArgumentException("Invalid Id format");
    }

    List<TaskItem> taskItems;

    if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
    {
        throw new ArgumentException("No tasks found");
    }
    else
    {
        string json = File.ReadAllText(FilePath);
        taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
    }

    TaskItem taskDone = null!;

    foreach (var taskItem in taskItems)
    {
        if (taskItem.Id == taskId)
        {
            taskDone = taskItem;
        }
    }

    if (taskDone == null)
    {
        throw new ArgumentException("Task not found");
    }

    if (taskDone.Status == "done")
    {
        throw new ArgumentException("Task is already in done status");
    }

    taskDone.Status = "done";
    taskDone.UpdatedAt = DateTime.Now;

    File.WriteAllText(FilePath, JsonSerializer.Serialize(taskItems, jsonOptions));
}

if (command == "list")
{
    if (args.Length == 1)
    {

        List<TaskItem> taskItems;

        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            throw new ArgumentException("No tasks found");
        }
        else
        {
            string json = File.ReadAllText(FilePath);
            taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
        }

        Console.WriteLine($"{"Id",-3} | {"Description",-30} | {"Status",-15} | {"Created At",-25} | {"Updated At"}");
        Console.WriteLine(new string('-', 110));

        foreach (var taskItem in taskItems)
        {
            Console.WriteLine($"{taskItem.Id,-3} | {taskItem.Description,-30} | {taskItem.Status,-15} | {taskItem.CreatedAt,-25} | {taskItem.UpdatedAt}");
        }

    }

    if (args.Length > 1)
    {
        var status = args[1];
        if (status.Trim().ToLower() != "todo" && status.Trim().ToLower() != "in-progress" && status.Trim().ToLower() != "done")
        {
            throw new ArgumentException("Invalid status format");
        }

        List<TaskItem> taskItems;

        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            throw new ArgumentException("No tasks found");
        }
        else
        {
            string json = File.ReadAllText(FilePath);
            taskItems = JsonSerializer.Deserialize<List<TaskItem>>(json, jsonOptions) ?? [];
        }

        Console.WriteLine($"{"Id",-3} | {"Description",-30} | {"Status",-15} | {"Created At",-25} | {"Updated At"}");
        Console.WriteLine(new string('-', 110));

        foreach (var taskItem in taskItems)
        {
            if (taskItem.Status.Trim().ToLower() == status.Trim().ToLower())
            {
                Console.WriteLine($"{taskItem.Id,-3} | {taskItem.Description,-30} | {taskItem.Status,-15} | {taskItem.CreatedAt,-25} | {taskItem.UpdatedAt}");
            }
        }

        File.WriteAllText(FilePath, JsonSerializer.Serialize(taskItems, jsonOptions));
    }
}