namespace TaskManagement.Infrastructure.Persistence.Repositories;

public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
{
    public AssignmentRepository(TaskManagementContext dbContext) : base(dbContext) {}
}
