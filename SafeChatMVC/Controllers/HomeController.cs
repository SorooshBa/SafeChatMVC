using Microsoft.AspNetCore.Mvc;
using SafeChatMVC.Models;
using System.Diagnostics;

namespace SafeChatMVC.Controllers
{
    public class HomeController : Controller
	{
		private readonly ChatContext _context;
        public HomeController(ChatContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IActionResult Index()
        {
			
            return View();
        }
        [HttpPost]
		public IActionResult Index(Message message)
		{
            ViewData["Success"] = null;
            if (ModelState.IsValid)
			{
				if(message.PicUrl==null)
				{
					message.PicUrl = "/pictuures/nopic.jpg";

                }
				message.Text = Aes_256.Encrypt(message.Text, message.Password);
				message.Sender=Aes_256.Encrypt(message.Sender,message.Password);
				_context.Messages.Add(message);
				_context.SaveChanges();
				ViewData["Success"] = "Successfully sent";

            }
			return View(message);
		}
        public IActionResult ChatroomSend(IndexModel model)
        {
            ViewData["Success"] = null;
            if (ModelState.IsValid)
            {
                if (model.message.PicUrl == null)
                {
                    model.message.PicUrl = "/pictuures/nopic.jpg";

                }
                model.message.Text = Aes_256.Encrypt(model.message.Text, model.message.Password);
                model.message.Sender = Aes_256.Encrypt(model.message.Sender, model.message.Password);
                _context.Messages.Add(model.message);
                _context.SaveChanges();
                ViewData["Success"] = "Successfully sent";

            }
            return(View("Chatroom",new IndexModel()));
        }

        public IActionResult Chatroom()
		{
			return View(new IndexModel());
		}
		[HttpPost]
        public IActionResult Chatroom(Message message)
        {
            var imp = new IndexModel();
            imp.messages = _context.Messages.Where(x=>x.ChatRoomName==message.ChatRoomName).OrderByDescending(x=>x.Id).ToList();
			foreach (var item in imp.messages)
			{
				try
				{
					item.Text = Aes_256.Decrypt(item.Text, message.Password);
					item.Sender=Aes_256.Decrypt(item.Sender,message.Password);
				}
				catch 
				{

				}
			}
            return View(imp);
        }

        public IActionResult Chat()
        {
            return View(new IndexModel());
        }
        [HttpPost]
        public IActionResult Chat(Message message)
        {
            var imp = new IndexModel();
            imp.messages = _context.Messages.Where(x => x.ChatRoomName == message.ChatRoomName).ToList();
            foreach (var item in imp.messages)
            {
                try
                {
                    item.DecryptedText = Aes_256.Decrypt(item.Text, message.Password);
                    item.Sender = Aes_256.Decrypt(item.Sender, message.Password);
                }
                catch
                {

                }
            }
            return View(imp);
        }
        [HttpPost]
        public IActionResult SendChat(IndexModel model)
        {
            ViewData["Success"] = null;
            if (ModelState.IsValid)
            {
                if (model.message.PicUrl == null)
                {
                    model.message.PicUrl = "https://localhost:7090/pictuures/nopic.jpg";

                }
                model.message.Text = Aes_256.Encrypt(model.message.Text, model.message.Password);
                model.message.Sender = Aes_256.Encrypt(model.message.Sender, model.message.Password);
                _context.Messages.Add(model.message);
                _context.SaveChanges();
                ViewData["Success"] = "Successfully sent";

            }
            return View("Chat",new IndexModel());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}