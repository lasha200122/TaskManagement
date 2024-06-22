namespace TaskManagement.Application.Features.Assignments.Commands.Create;

public class CreateAssignmentHandler : IRequestHandler<CreateAssignmentCommand, ErrorOr<DefaultResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAssignmentRepository _assignmentRepository;

    public CreateAssignmentHandler(IUserRepository userRepository, IAssignmentRepository assignmentRepository)
    {
        _userRepository = userRepository;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId.HasValue && !(await _userRepository.AnyAsync(x => x.Id == request.UserId)))
            return Error.NotFound(description:"User not found");

        Assignment assignment = new (
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.DueDate,
            request.Priority,
            request.Status,
            request.UserId);

        _assignmentRepository.Create(assignment);

        await _assignmentRepository.SaveChanges(cancellationToken);

        return new DefaultResponse(true, nameof(CreateAssignmentHandler));
    }
}