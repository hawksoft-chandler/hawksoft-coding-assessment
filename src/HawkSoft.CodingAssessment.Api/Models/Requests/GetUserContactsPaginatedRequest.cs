namespace HawkSoft.CodingAssessment.Api.Models.Requests
{
    public class GetUserContactsPaginatedRequest
    {
        public string UserId { get; set; }
        public int Offset { get; set; }
        public int ChunkSize { get; set; }
    }
}