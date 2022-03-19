namespace RiskyToDoApp.Models
{
    public class TaskVM
    {
        public Task Task { get; set; }
        public IEnumerable<Task> TaskList { get; set; }  
    }
}
