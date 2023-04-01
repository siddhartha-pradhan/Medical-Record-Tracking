using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IAppUserService
{
	string GetUserName(string email);

	byte[] GetImage (string email);

	AppUser GetUser(string Id);

	List<AppUser> GetAllUsers();
}
