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

	public byte[] GetImage(string email)
	{
		return _unitOfWork.AppUser.GetAll().Where(x => x.UserName == email).FirstOrDefault().ProfileImage;
	}
}
