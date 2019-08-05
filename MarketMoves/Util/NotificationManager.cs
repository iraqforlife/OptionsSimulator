using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using MarketMoves.Models;

namespace MarketMoves.Util
{
    public class NotificationManager
    {
        const string _AccountSid = "AC039f6db0be5ab9f247cd582a4d2c4a37";
        const string _AuthToken = "a95f09c51e554595fc996a6659f0e190";
        const string _Email = "tradealert.notifications@gmail.com";
        const string _Password = "Zoisthebesttrader";
        const string _From = "+12048085431";
        private readonly UserManager<Account> _userManager;

        public NotificationManager(UserManager<Account> userManager)
        {
            TwilioClient.Init(_AccountSid, _AuthToken);
            _userManager = userManager;
        }
        public void SendSMS(string message)
        {
            PhoneNumber from = new PhoneNumber(_From);
            PhoneNumber to ;

            foreach (var user in _userManager.Users)
            {
                if(user.Suscribed && !string.IsNullOrEmpty(user.PhoneNumber))
                {
                    to = new PhoneNumber("+1" + user.PhoneNumber);
                    Task<MessageResource> outBoundMessage = MessageResource.CreateAsync(to: to, from:from,body:message);
                }
            }
        }
        public void SendMail(string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Market Moves", _Email)); 
            message.To.Add(new MailboxAddress("The best", "ahmadaltahir@gmail.com"));
            //message.Subject = "This is the alert system";
            message.Subject = subject;
            message.Body = new TextPart("plain"){
                //Text = "Zo is the best has been triggerred"
                Text = body
            };
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587);

                    //Remove any OAuth functionality as we won't be using it. 
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_Email, _Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                var www = ex.Message;
                throw;
            }
        }
    }
}
