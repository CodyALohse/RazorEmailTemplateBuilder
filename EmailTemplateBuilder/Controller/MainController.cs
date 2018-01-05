using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Winmark.Client.Services.Interfaces;

namespace EmailTemplateBuilder.Controller
{
    public class MainController : Microsoft.AspNetCore.Mvc.Controller
    {
        protected IViewRenderService ViewRenderService;
        protected IEmailService EmailService;

        public MainController(IViewRenderService viewRenderService, IEmailService emailService)
        {
            this.ViewRenderService = viewRenderService;
            this.EmailService = emailService;
        }

        [HttpGet("testemail")]
        public virtual async Task<ActionResult> TestRazor()
        {
            var apt = new AppointmentInfo
            {
                DateRequested = "10/26/2018",
                GroupCode = "1234rf",
                NumberOfGuests = 2,
                TimeRequested = "Evening"
            };

            var contactDet = new ContactDetails
            {
                Email = "test@mail.com",
                Name = "Carl Junior",
                PhoneNumber = "555-333-3444"
            };

            var model = new PersonalStyleModel
            {
                AppointmentInfo     =  apt,
                ContactDetails = contactDet
            };

            var razor = await this.ViewRenderService.RenderToStringAsync("template",
                model);

            this.SendEmail("TEst", razor);

            return View("~/Views/template.cshtml", model);
        }


        private void SendEmail(string emailSubject, string emailBody)
        {
            var message = new MailMessage("no-reply@test.com", "test@mail.com")
            {
                Subject = emailSubject,
                Body = emailBody,
                IsBodyHtml = true
            };

            this.EmailService.SendMail(message);
        }
    }

    public class PersonalStyleModel
    {
        public AppointmentInfo AppointmentInfo { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }

    public class AppointmentInfo
    {
        [StringLength(12)]
        public string DateRequested { private get; set; }

        [StringLength(12)]
        public string TimeRequested { get; set; }

        public int NumberOfGuests { get; set; }

        [StringLength(7)]
        public string GroupCode { get; set; }

        public DateTime GetDateRequested()
        {
            if (!DateTime.TryParse(this.DateRequested, out var outDateTime))
            {
                outDateTime = DateTime.Today;
            }

            return outDateTime;
        }
    }

    public class ContactDetails
    {
        [StringLength(30)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
