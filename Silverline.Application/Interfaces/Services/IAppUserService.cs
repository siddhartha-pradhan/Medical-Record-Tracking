using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IAppUserService
{
    AppUser GetUser(string Id);
}
