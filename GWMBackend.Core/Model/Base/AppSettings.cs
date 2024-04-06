using System;
using System.Collections.Generic;
using System.Text;

namespace GWMBackend.Core.Model.Base
{
    public class AppSettings
    {
        public string TokenSecret { get; set; }
        public int TokenValidateInMinutes { get; set; }
        public string PublishImagePath { get; set; }
        public string SaveImagePath { get; set; }
        public string DefaultLanguage { get; set; }
        public string ApiKey { get; set; }
        public string Url { get; set; }
        public int MenuLevel { get; set; }
        public string EmailHost { get; set; }
        public int EmailPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

    }
}
