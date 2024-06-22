namespace TaskManagement.Application.Features.Assignments.Commands.Update;

public class UpdateAssignmentHandler : IRequestHandler<UpdateAssignmentCommand, ErrorOr<DefaultResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAssignmentRepository _assignmentRepository;

    public UpdateAssignmentHandler(IUserRepository userRepository, IAssignmentRepository assignmentRepository)
    {
        _userRepository = userRepository;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        if (await _assignmentRepository.GetSingleOrDefaultAsync(x => x.Id == request.Id) is not Assignment assignment)
            return Error.NotFound(description: "Can't find assignment");

        if (assignment.IsDeleted) return Error.Conflict(description: "Assignment is deleted");

        if (request.UserId.HasValue && !(await _userRepository.AnyAsync(x => x.Id == request.UserId)))
            return Error.NotFound(description: "User not found");

        assignment.Update(
            request.Title,
            request.Description,
            request.DueDate,
            request.Priority,
            request.Status,
            request.UserId);

        _assignmentRepository.Update(assignment);

        await _assignmentRepository.SaveChanges(cancellationToken);

        return new DefaultResponse(true, nameof(UpdateAssignmentHandler));
    }
}
