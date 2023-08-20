using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SafeChatMVC.Models
{
    public class IndexModel
    {
        public Message? message { get; set; }
        public List<Message> messages { get; set; }


        public IndexModel()
        {
            messages = new List<Message>();
        }
    }
}
