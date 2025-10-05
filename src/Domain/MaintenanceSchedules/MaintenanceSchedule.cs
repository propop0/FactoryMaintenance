namespace Domain.MaintenanceSchedules;

public class MaintenanceSchedule
{
    public Guid Id { get; }
    public Guid EquipmentId { get; private set; }
    public string TaskName { get; private set; }
    public string Description { get; private set; }
    public MaintenanceFrequency Frequency { get; private set; }
    public DateTime NextDueDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private MaintenanceSchedule(Guid id,
        Guid equipmentId,
        string taskName,
        string description,
        MaintenanceFrequency frequency,
        DateTime nextDueDate,
        bool isActive,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        EquipmentId = equipmentId;
        TaskName = taskName;
        Description = description;
        Frequency = frequency;
        NextDueDate = nextDueDate;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static MaintenanceSchedule New(
        Guid id,
        Guid equipmentId,
        string taskName,
        string description,
        MaintenanceFrequency frequency,
        DateTime nextDueDate)
    {
        return new MaintenanceSchedule(
            id,
            equipmentId,
            taskName,
            description,
            frequency,
            nextDueDate,
            true,
            DateTime.UtcNow,
            null);
    }

    public void UpdateSchedule(
        string taskName,
        string description,
        MaintenanceFrequency frequency,
        DateTime nextDueDate)
    {
        TaskName = taskName;
        Description = description;
        Frequency = frequency;
        NextDueDate = nextDueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        this.IsActive = false;
        this.UpdatedAt = DateTime.UtcNow;
    }
}

public enum MaintenanceFrequency
{
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    Annually
}
