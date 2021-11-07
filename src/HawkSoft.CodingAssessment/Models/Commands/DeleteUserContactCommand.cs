namespace HawkSoft.CodingAssessment.Models.Commands
{
    public class DeleteUserContactCommand
    {
        public string UserId { get; set; }
        public string ContactId { get; set; }
    }
}