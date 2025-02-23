using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IAppUserService
{
	string GetUserName(string email);

	string GetImage (string email);

	AppUser GetUser(string Id);

	List<AppUser> GetAllUsers();

	void LockUser(string Id);

    void UnlockUser(string Id);


}
