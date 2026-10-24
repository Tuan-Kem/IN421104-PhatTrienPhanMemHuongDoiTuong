using Ch02.Refactor.Domain;

namespace Ch02.Refactor.Tests.Domain;

public class EventTests
{
    private static Event NewEvent() =>
        new() { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop SOLID", Date = new DateTime(2026, 11, 20) };

    [Fact]
    public void Register_NewMember_ReturnsTrueAndStoresMember()
    {
        // Arrange
        var ev = NewEvent();
        var memberId = Guid.NewGuid();

        // Act
        var added = ev.Register(memberId);

        // Assert
        Assert.True(added);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Register_SameMemberTwice_ReturnsFalseAndKeepsOneEntry()
    {
        // Arrange
        var ev = NewEvent();
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var addedAgain = ev.Register(memberId);

        // Assert
        Assert.False(addedAgain);
        Assert.Single(ev.RegisteredMemberIds);
    }

    [Fact]
    public void CheckIn_RegisterMember_RecordsCheckIn()
    {
        // Given
        var ev = NewEvent();
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // When
        ev.CheckIn(memberId);

        // Then
        Assert.Contains(memberId, ev.CheckedInMemberIds);
    }

    [Fact]
    public void CheckIn_Unregisteredmember_ThrowsInvalidOperationException()
    {
        // arrange
        var ev = NewEvent();

        // When
        var act = () => ev.CheckIn(Guid.NewGuid());

        // Then
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void AttendanceRate_WithNoRegistration_IsZero()
    {
        // Arrange
        var ev = NewEvent();

        // Act
        var rate = ev.AttendanceRate;

        // Assert
        Assert.Equal(0, rate);
    }

    [Fact]
    public void AttendanceRate_OneOfTwoCheckedIn_IsHalf()
    {
        // Arrange
        var ev = NewEvent();
        var a = Guid.NewGuid();
        var b = Guid.NewGuid();
        ev.Register(a);
        ev.Register(b);
        ev.CheckIn(a);

        // Act
        var rate = ev.AttendanceRate;

        // Assert
        Assert.Equal(0.5, rate);
    }
}
