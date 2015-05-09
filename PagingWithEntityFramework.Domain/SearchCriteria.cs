namespace PagingWithEntityFramework.Domain
{
    public class SearchCriteria
    {
        public string ServerName { get; set; }

        public string Severity { get; set; }
        
        public string StackTrace { get; set; }
    }
}