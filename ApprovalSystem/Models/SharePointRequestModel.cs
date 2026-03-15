using System.Text.Json.Serialization;

namespace ApprovalSystem.Models
{
    public class SharePointRequestModel
    {
        public string EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public string RequestId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Amount { get; set; }

        [JsonPropertyName("FirstApprover")]
        public string FirstApproverEmail { get; set; }

        [JsonPropertyName("SecondApprover")]
        public string SecondApproverEmail { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
