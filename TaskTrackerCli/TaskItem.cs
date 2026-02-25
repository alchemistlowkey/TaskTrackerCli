using System;

namespace TaskTrackerCli;

public class TaskItem
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public string Status { get; set; } = "todo";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
