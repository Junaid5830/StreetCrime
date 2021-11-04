using DAL.Data;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repository
{
  class UsersRepository : GenericRepository<User>, IUsersRepository
  {
    public UsersRepository(DatabaseContext context) : base(context)
    {
    }

    public bool IsValidUser(string userId)
    {
      return _db.AsNoTracking().Any(x => x.Id == userId);
    }
    
    public bool DoesPhoneNumberExist(string phoneNumber)
    {
      return _db.AsNoTracking().Any(x => x.PhoneNumber == phoneNumber);
    }

    public bool DoesEmailExist(string email)
    {
      return _db.AsNoTracking().Any(x => x.Email == email);
    }
  }
}
