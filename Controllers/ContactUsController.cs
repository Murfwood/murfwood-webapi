using Microsoft.AspNetCore.Mvc;

namespace Murfwood_AngularNetcore2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly EmailService _emailService;

        public ContactUsController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactUs contactUs)
        {
            var _dtime = DateTime.UtcNow;
            var _date = _dtime.ToShortDateString();
            var _time = _dtime.ToShortTimeString();
            // Handle the contact form data here (e.g., save to database)
            var _request = new EmailRequest()
            {
                To = "messages@murfwood.com",
                Subject = "Murfwood Contact Submission from " + contactUs.FirstName + " " + contactUs.LastName,
                Body = "<html><h1>New Murfwood Contact Submission</h1> " +
                "Sent on : " + _date + " " + _time + "<br/><br/>" +
                "FirstName: " + contactUs.FirstName + "<br/>" +
                "LastName: " + contactUs.LastName + "<br/>" +
                "Email: " + contactUs.Email + "<br/>" +
                "Subject: " + contactUs.Subject + "<br/>" +
                "Message: " + contactUs.Message + "</html>"
               
            };

            await _emailService.SendEmailAsync(_request.To, _request.Subject, _request.Body);

            return Ok(new { message = "Email sent successfully." });
        }

    }


    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
