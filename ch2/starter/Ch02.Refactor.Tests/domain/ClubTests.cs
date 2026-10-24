using Ch02.Refactor.Domain;

namespace starter.Ch02.Tests.domain;

public class Clubtests
{
    [Fact]
    public void Create_WithValidName_ReturnsClubWithNameAndNewId()
    {
        const string name = "Tech Club";

        var club = Club.Create(name, "Câu lạc bộ công nghệ");

        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithBlankName_ThrowsArgumentException(string name)
    {
        var act = () => Club.Create(name, "mô tả");

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_WithNameLongerThan100_ThrowsArgumentException()
    {
        var longName = new string('a', Club.MaxNameLength + 1);

        var act = () => Club.Create(longName, "mô tả");

        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(49, true)]
    [InlineData(50, false)]
    public void HasRoom_DependsOnCurrentMemberCount(int memberCount, bool expected)
    {
        var club = Club.Create("Tech Club", "mô tả");

        var result = club.HasRoom(memberCount);

        Assert.Equal(expected, result);
    }
}