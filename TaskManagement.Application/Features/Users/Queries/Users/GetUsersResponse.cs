namespace TaskManagement.Application.Features.Users.Queries.Users;

public sealed record GetUsersResponse(IList<UserListItem> Users);