using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

public class AppUserService : IAppUserService
{
    private readonly IUnitOfWork _unitOfWork;

	public AppUserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public AppUser GetUser(string Id)
    {
        return _unitOfWork.AppUser.Retrieve(Id);
    }

    public List<AppUser> GetAllUsers()
    {
        return _unitOfWork.AppUser.GetAll();
    }

	public string GetUserName(string email)
	{
		return _unitOfWork.AppUser.GetAll().Where(x => x.UserName == email).FirstOrDefault().FullName;
	}

	public string GetImage(string email)
	{
		return _unitOfWork.AppUser.GetAll().Where(x => x.UserName == email).FirstOrDefault().ImageURL;
	}

    public void LockUser(string Id)
    {
        var user = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == Id);

        if (user != null)
        {
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.Now.AddDays(5);
            _unitOfWork.Save();
        }
    }

    public void UnlockUser(string Id)
    {
        var user = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == Id);

        if (user != null)
        {
            user.LockoutEnabled = false;
            user.LockoutEnd = DateTime.Now;
			_unitOfWork.Save();
		}
	}
}
