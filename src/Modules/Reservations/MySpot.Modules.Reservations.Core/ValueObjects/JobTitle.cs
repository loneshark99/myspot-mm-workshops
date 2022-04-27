namespace MySpot.Modules.Reservations.Core.ValueObjects;

public sealed record JobTitle(string Value)
{
    public const string None = nameof(None);
    public const string Employee = nameof(Employee);
    public const string Manager = nameof(Manager);
    public const string Boss = nameof(Boss);

    public static implicit operator string(JobTitle licensePlate)
        => licensePlate.Value;
    
    public static implicit operator JobTitle(string value)
        => new(value);
}