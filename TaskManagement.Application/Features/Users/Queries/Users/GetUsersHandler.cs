namespace TaskManagement.Application.Features.Users.Queries.Users;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, ErrorOr<GetUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.GetQueryable(x => !x.IsDeleted);

        var users = await query.Select(x => new UserListItem(x.Id, x.FullName)).ToListAsync();

        return new GetUsersResponse(users);
    }
}
