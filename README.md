# TaskTrackerCli

A simple command-line task tracker built with C# and .NET. Tasks are persisted locally in a `task.json` file.

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (10.0)

## Getting Started

Clone the repository and navigate to the project directory:

```bash
git clone https://github.com/alchemistlowkey/TaskTrackerCli.git
cd TaskTrackerCli/TaskTrackerCli
```

Run any command using `dotnet run`:

```bash
dotnet run <command> [arguments]
```

## Usage

### Add a task

```bash
dotnet run add "Buy groceries"
```

### Update a task's description

```bash
dotnet run update 1 "Buy groceries and cook dinner"
```

### Delete a task

```bash
dotnet run delete 1
```

### Mark a task as in-progress

```bash
dotnet run mark-in-progress 1
```

### Mark a task as done

```bash
dotnet run mark-done 1
```

### List all tasks

```bash
dotnet run list
```

### List tasks by status

```bash
dotnet run list todo
dotnet run list in-progress
dotnet run list done
```

## Task Properties

Each task stores the following fields:

| Field | Description |
|---|---|
| `id` | Auto-incremented unique identifier |
| `description` | The task description |
| `status` | Current status: `todo`, `in-progress`, or `done` |
| `createdAt` | Timestamp when the task was created |
| `updatedAt` | Timestamp when the task was last updated |

## Data Storage

Tasks are saved to a `task.json` file in the project directory. The file is created automatically on the first `add` command.

## Project Structure

```
TaskTrackerCli/
├── TaskTrackerCli/
│   ├── Program.cs       # CLI entry point and command handling
│   ├── TaskItem.cs      # Task model
│   └── task.json        # Auto-generated task data file
```

## Inspiration

This project is based on the [Task Tracker](https://roadmap.sh/projects/task-tracker) project idea from [roadmap.sh](https://roadmap.sh).