using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Service
{
    public class FeedbackServices : BaseDbServices<Feedback>
    {
        public FeedbackServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Feedbacks)
        {

        }
    }
}
