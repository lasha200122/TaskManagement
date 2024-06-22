namespace TaskManagement.Application.Features.Assignments.Commands.Delete;

public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignmentCommand, ErrorOr<DefaultResponse>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public DeleteAssignmentHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        if (await _assignmentRepository.GetSingleOrDefaultAsync(x => x.Id == request.Id) is not Assignment assignment)
            return Error.NotFound(description:"Can't find assignment");

        if (assignment.IsDeleted) return Error.Conflict(description:"Assignment is already deleted");

        assignment.Delete();

        _assignmentRepository.Update(assignment);

        await _assignmentRepository.SaveChanges(cancellationToken);

        return new DefaultResponse(true, nameof(DeleteAssignmentHandler));
    }
}