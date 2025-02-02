
namespace TaskTrackerCLI.Models
{
    public class Task 
    {
        public int Id { get; set; }
        public string Status { get; set; } // InProgress, Completed, Deleted, etc.
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
