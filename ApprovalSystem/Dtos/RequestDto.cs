namespace ApprovalSystem.Dtos
{
    public class RequestDto
    {
        public string EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Amount { get; set; }
    }
}
