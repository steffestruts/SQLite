namespace Data.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    // Letting CreatedAt to be, for now. Shows start time from year 0 and time 0. 
    public DateTime CreatedAt { get; set; }
}
