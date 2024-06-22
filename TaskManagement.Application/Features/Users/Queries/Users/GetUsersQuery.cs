namespace TaskManagement.Application.Features.Users.Queries.Users;

public sealed record GetUsersQuery() : IRequest<ErrorOr<GetUsersResponse>>;
