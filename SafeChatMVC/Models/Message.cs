using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeChatMVC.Models
{
    public class Message
    {
        public Message()
        {
            Date = DateTime.Now;
            PicUrl = "~/pictuures/nopic.jpg";
        }
        public Message(string text, string sender )
        {
            Text = text;
            Sender = sender;
            Date = DateTime.Now;
            PicUrl = "~/pictuures/nopic.jpg";

        }
        public Message(string text)
        {
            Text = text;
            Date = DateTime.Now;
            PicUrl = "~/pictuures/nopic.jpg";
        }

        public int Id { get; set; }
        [DisplayName("Message")]
        [Required(ErrorMessage = "Field is required")]
        public string Text { get; set; }
        [DisplayName("Sender Name")]
        [Required(ErrorMessage = "Field is required")]
        public string? Sender { get; set; }
        [DisplayName("ChatRoom Name")]
        [Required(ErrorMessage = "Field is required")]
        public string ChatRoomName { get; set; }
        [NotMapped]
        [DisplayName("Password")]
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Picture Url")]
        public string? PicUrl { get; set; }
        [NotMapped]
        public string? DecryptedText { get; set; }
    }
}
