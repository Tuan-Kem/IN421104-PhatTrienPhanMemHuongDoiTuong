using Ch02.Refactor.Domain;

namespace Ch02.Refactor.Repositories;

public interface IEventRepository
{
    IReadOnlyList<Event> GetByClubId(Guid clubId);
    IReadOnlyList<Event> GetByClub(Guid clubId) => GetByClubId(clubId);
    Event? GetById(Guid id);
    void Add(Event ev);
}
