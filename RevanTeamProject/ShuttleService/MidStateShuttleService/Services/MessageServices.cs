using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Services
{
    public class MessageServices : BaseDbServices<Message>
    {
        public MessageServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Messages)
        {

        }
    }
}
