using Microsoft.EntityFrameworkCore;
using SafeChatMVC.Models;

namespace SafeChatMVC
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }
    }
}
