namespace ApprovalSystem.Dtos
{
    public class RequestStatusDto
    {
        public DateTime UpdatedAt { get; set; }

        public string FinalStatus { get; set; }

        public string Comments { get; set; }
    }
}
