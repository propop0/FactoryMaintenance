namespace Domain.WorkOrders;

public class WorkOrder
{
    public Guid Id { get; }
    public string WorkOrderNumber { get; private set; }
    public Guid EquipmentId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public WorkOrderPriority Priority { get; private set; }
    public WorkOrderStatus Status { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? CompletionNotes { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private WorkOrder(Guid id,
        string workOrderNumber,
        Guid equipmentId,
        string title,
        string description,
        WorkOrderPriority priority,
        WorkOrderStatus status,
        DateTime scheduledDate,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? completedAt,
        string? completionNotes)
    {
        Id = id;
        WorkOrderNumber = workOrderNumber;
        EquipmentId = equipmentId;
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        ScheduledDate = scheduledDate;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        CompletedAt = completedAt;
        CompletionNotes = completionNotes;
    }

    public static WorkOrder New(
        Guid id,
        string workOrderNumber,
        Guid equipmentId,
        string title,
        string description,
        WorkOrderPriority priority,
        DateTime scheduledDate)
    {
        return new WorkOrder(
            id,
            workOrderNumber,
            equipmentId,
            title,
            description,
            priority,
            WorkOrderStatus.Open,
            scheduledDate,
            DateTime.UtcNow,
            null,
            null,
            null);
    }

    public void UpdateDetails(
        string title,
        string description,
        WorkOrderPriority priority,
        DateTime scheduledDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        ScheduledDate = scheduledDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void StartWork()
    {
        Status = WorkOrderStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(string completionNotes)
    {
        Status = WorkOrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        CompletionNotes = completionNotes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = WorkOrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum WorkOrderPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum WorkOrderStatus
{
    Open,
    InProgress,
    Completed,
    Cancelled
}
