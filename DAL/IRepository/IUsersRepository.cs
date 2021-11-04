using DAL.Data;

namespace DAL.IRepository
{
  public interface IUsersRepository : IGenericRepository<User>
  {
    public bool IsValidUser(string userId);
    public bool DoesPhoneNumberExist(string phoneNumber);
    public bool DoesEmailExist(string email);
  }
}
