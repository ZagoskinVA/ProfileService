using ProfileService.DataBase;
using ProfileService.Interfaces;
using ProfileService.Models;
using WebUtilities.Interfaces;
using WebUtilities.Model;
using WebUtilities.Services;

namespace ProfileService.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileContext profileContext;
        public ProfileRepository(ProfileContext profileContext)
        {
            if(profileContext == null)
                throw new ArgumentNullException(nameof(profileContext));
            this.profileContext = profileContext;
        }
        public IOperationResultBuilder<OperationResult> Save(Profile profile)
        {
            IOperationResultBuilder<OperationResult> operationResultBuilder = new OperationResultBuilder<OperationResult>();
            if (profile.AccountId == 0 || profile.DepartmentId == 0)
                return operationResultBuilder.SetFailureStatus().AddMessage("К профилю не привязан аккаунт или департамент");
            var oldProfile = profileContext.Profiles.FirstOrDefault(x => x.Id == profile.Id);
            if (profile.Id == 0)
            {
                if (oldProfile != null)
                    return operationResultBuilder.SetFailureStatus().AddMessage("К этому аккаунту уже привязан проффиль");
                profileContext.Profiles.Add(profile);
            }
            else 
            {
                if(oldProfile.Id != profile.Id)
                    return operationResultBuilder.SetFailureStatus().AddMessage("К этому аккаунту уже привязан проффиль");
                oldProfile.FirstName = profile.FirstName;
                oldProfile.LastName = profile.LastName;
                oldProfile.DepartmentId = profile.DepartmentId;
            }
            profileContext.SaveChanges();
            return operationResultBuilder.SetSuccessStatus();
        }
    }
}
