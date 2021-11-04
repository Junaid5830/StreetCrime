using DAL.Data;
using DAL.IRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private IUsersRepository _users;

        #region Lookups
        IGenericRepository<IdentityRole> _userRoles;
        #endregion

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
       
        public IUsersRepository Users => _users ??= new UsersRepository(_context);

        #region Lookups
        public IGenericRepository<IdentityRole> UserRoles => _userRoles ??= new GenericRepository<IdentityRole>(_context);
        #endregion


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
