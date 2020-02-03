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
using Microsoft.AspNetCore.Authorization;

namespace MarketMoves.Util
{
    public class NotificationManager
    {
        private const string _AccountSid = "ACd19fb1303b22ffdce2e3ac4d6d57ba58";
        private const string _AuthToken = "b42722404049c6a14a4a44d850fc851f";
        private const string _Email = "tradealert.notifications@gmail.com";
        private const string _Password = "Zoisthebesttrader";
        private const string _From = "+13158885657";
        private readonly UserManager<Account> _userManager;

        public NotificationManager(UserManager<Account> userManager)
        {
            TwilioClient.Init(_AccountSid, _AuthToken);
            _userManager = userManager;
        }
        public bool SendSMS(string message)
        {
            PhoneNumber from = new PhoneNumber(_From);
            PhoneNumber to ;
            try
            {
                foreach (var user in _userManager.Users)
                {
                    if (user.Suscribed && !string.IsNullOrEmpty(user.PhoneNumber) && user.GetSmsNotification)
                    {
                        to = new PhoneNumber("+1" + user.PhoneNumber);
                        Task<MessageResource> outBoundMessage = MessageResource.CreateAsync(to: to, from: from, body: message);
                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
        [AllowAnonymous]
        public bool SendSMS(string number, string message)
        {
            PhoneNumber from = new PhoneNumber(_From);
            PhoneNumber to = new PhoneNumber("+1" + number);
            try
            {
                Task<MessageResource> outBoundMessage = MessageResource.CreateAsync(to: to, from: from, body: message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendMail(string subject, string body)
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
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
