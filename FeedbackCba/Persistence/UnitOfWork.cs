using FeedbackCba.Core;
using FeedbackCba.Core.Repositories;
using FeedbackCba.Persistence.Repositories;

namespace FeedbackCba.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFeedbackRepository Feedbacks { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Feedbacks = new FeedbackRepository(context);
            Users = new UserRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}