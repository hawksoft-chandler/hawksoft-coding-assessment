using System;
using System.Collections.Generic;
using System.Text;

namespace HawkSoft.CodingAssessment.Models.Commands
{
    public class CreateUserContactCommand
    {
        public string UserId { get; set; }
        public string ContactId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
