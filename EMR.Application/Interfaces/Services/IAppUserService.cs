using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IAppUserService
{
	string GetUserName(string email);

	string GetImage (string email);

	User GetUser(string Id);

	List<User> GetAllUsers();

	void LockUser(string Id);

    void UnlockUser(string Id);


}
