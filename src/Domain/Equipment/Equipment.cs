namespace Domain.Equipments;

public class Equipment
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Model { get; private set; }
    public string SerialNumber { get; private set; }
    public string Location { get; private set; }
    public EquipmentStatus Status { get; private set; }
    public DateTime InstallationDate { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    // приватн. конструкт
    private Equipment(Guid id,
        string name,
        string model,
        string serialNumber,
        string location,
        EquipmentStatus status,
        DateTime installationDate,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Model = model;
        SerialNumber = serialNumber;
        Location = location;
        Status = status;
        InstallationDate = installationDate;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Equipment New(
        Guid id,
        string name,
        string model,
        string serialNumber,
        string location,
        DateTime installationDate)
    {
        return new Equipment(
            id,
            name,
            model,
            serialNumber,
            location,
            EquipmentStatus.Operational,
            installationDate,
            DateTime.UtcNow,
            null);
    }

    public void UpdateDetails(string name, string model, string location)
    {
        Name = name;
        Model = model;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(EquipmentStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum EquipmentStatus
{
    Operational,
    UnderMaintenance,
    OutOfService
}