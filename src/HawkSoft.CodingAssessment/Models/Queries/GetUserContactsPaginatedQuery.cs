namespace HawkSoft.CodingAssessment.Models.Queries
{
    public class GetUserContactsPaginatedQuery
    {
        public string UserId { get; set; }
        public int Offset { get; set; }
        public int ChunkSize { get; set; }
    }
}