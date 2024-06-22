namespace TaskManagement.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(TaskManagementContext dbContext) : base(dbContext) {}
}
