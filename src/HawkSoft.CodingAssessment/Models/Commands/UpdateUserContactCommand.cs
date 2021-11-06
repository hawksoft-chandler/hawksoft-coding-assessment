namespace HawkSoft.CodingAssessment.Models.Commands
{
    public class UpdateUserContactCommand
    {
        public string UserId { get; set; }
        public string ContactId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}