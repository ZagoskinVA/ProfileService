using ProfileService.Models;
using WebUtilities.Interfaces;
using WebUtilities.Model;

namespace ProfileService.Interfaces
{
    public interface IProfileRepository
    {
        IOperationResultBuilder<OperationResult> Save(Profile profile);
    }
}
