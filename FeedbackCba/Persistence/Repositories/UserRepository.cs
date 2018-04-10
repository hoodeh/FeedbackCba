using System;
using System.Linq;
using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;

namespace FeedbackCba.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private readonly ApplicationDbContext _context;

        //public UserRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public User GetUser(string userId)
        //{
        //    try
        //    {
        //        return _context.Users.FirstOrDefault(u => u.Guid == userId) ?? new User { Guid = Guid.NewGuid().ToString() };
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return new User();
        //    }
        //}

        //public bool Update(User user)
        //{
        //    try
        //    {
        //        var existUser = _context.Users.FirstOrDefault(u => u.Guid == user.Guid);
        //        if (existUser == null)
        //        {
        //            _context.Users.Add(user);
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrWhiteSpace(user.Email) && existUser.Email != user.Email)
        //            {
        //                existUser.Email = user.Email;
        //            }

        //            if (!string.IsNullOrWhiteSpace(user.Name) && existUser.Name != user.Name)
        //            {
        //                existUser.Name = user.Name;
        //            }
        //        }

        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return false;
        //    }
        //}
    }
}