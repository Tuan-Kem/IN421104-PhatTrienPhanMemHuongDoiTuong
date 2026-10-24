namespace Ch02.Refactor.Domain;

public class Club
{
    public const int MaxMembers = 50;
    public const int MaxNameLength = 100;

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }


    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name is required");
        if (name.Length > MaxNameLength)
            throw new ArgumentException("Club name too long");

        return new Club { Id = Guid.NewGuid(), Name = name, Description = description };
    }

    public bool HasRoom(int currentMemberCount) => currentMemberCount < MaxMembers;
}
