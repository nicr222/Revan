using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Service
{
    public class MessageServices : BaseDbServices<Message>
    {
        public MessageServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Messages)
        {

        }
    }
}
