using Altria.PowerBIPortal.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories;

public abstract class Repository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    protected readonly DbSet<TAggregateRoot> _store;
    protected readonly IQueryable<TAggregateRoot> _readOnlyStore;
    protected readonly DataContext _dataContext;

    public Repository(DataContext dataContext)
    {
        _store = dataContext.Set<TAggregateRoot>();
        _readOnlyStore = dataContext.Set<TAggregateRoot>().AsNoTracking();
        _dataContext = dataContext;
    }
}
