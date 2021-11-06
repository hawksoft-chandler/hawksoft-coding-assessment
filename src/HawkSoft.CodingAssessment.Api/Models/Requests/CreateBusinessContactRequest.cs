namespace HawkSoft.CodingAssessment.Api.Models.Requests
{
    public class CreateBusinessContactRequest
    {
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}