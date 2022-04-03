using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Interfaces;
using ProfileService.Models;
using WebUtilities.Model;
using WebUtilities.Services;

namespace ProfileService.Controllers
{
    [Route("profile/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository profileRepository;
        public ProfileController(IProfileRepository profileRepository)
        {
            if(profileRepository == null)
                throw new ArgumentNullException(nameof(profileRepository));
            this.profileRepository = profileRepository;
        }
        [HttpPost]
        public IActionResult SaveProfile(Profile profile) 
        {
            if (ModelState.IsValid)
            {
                var operationResult = profileRepository.Save(profile).Build();
                if (operationResult.Status == OperationStatus.Success)
                    return Ok(JsonService.GetOkJson(profile));
                return BadRequest(JsonService.GetErrorJson(profile, operationResult.Messages));
            }
            var errors = new List<string>();
            foreach (var state in ModelState.Values)
                errors.AddRange(state.Errors.Select(x => x.ErrorMessage));
            return BadRequest(JsonService.GetErrorJson(profile, errors));
        }
    }
}
