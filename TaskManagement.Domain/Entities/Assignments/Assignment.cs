namespace TaskManagement.Domain.Entities.Assignments;

public class Assignment : Entity
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public DateTimeOffset DueDate { get; private set; }
    public AssignmentPriority Priority { get; private set; }
    public AssignmentStatus Status { get; private set; }
    public Guid? UserId { get; private set; }

    public User User { get; private set; } = null!;

    public Assignment() {}

    public Assignment(
        Guid id,
        string title,
        string? description,
        DateTimeOffset dueDate,
        AssignmentPriority priority,
        AssignmentStatus status,
        Guid? userId) : base(id, DateTimeOffset.Now) 
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = status;
        UserId = userId;
    }

    public void Update(
        string title,
        string? description,
        DateTimeOffset dueDate,
        AssignmentPriority priority,
        AssignmentStatus status,
        Guid? userId) 
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = status;
        UserId = userId;

        Validate(this);

        Update(DateTimeOffset.Now);
    }

    public void Delete() 
    {
        Delete(DateTimeOffset.Now);
    }

    private void Validate(Assignment assignment)
    {
        if (string.IsNullOrEmpty(assignment.Title)) throw new Exception("Invalid title");

        if (assignment.DueDate == default) throw new Exception("Invalid due date");
    }
}
