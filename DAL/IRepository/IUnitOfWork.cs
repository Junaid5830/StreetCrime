using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IRepository;
using Microsoft.AspNetCore.Identity;

namespace DAL.IRepository
{
  public interface IUnitOfWork: IDisposable
  {
    IUsersRepository Users { get;}
    Task Save();
  }
}
