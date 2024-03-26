using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Services
{
    public class FeedbackServices : BaseDbServices<Feedback>
    {
        public FeedbackServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Feedbacks)
        {

        }
    }
}
