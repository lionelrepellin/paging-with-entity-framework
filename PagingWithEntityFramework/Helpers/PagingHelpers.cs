using PagingWithEntityFramework.Domain;

namespace PagingWithEntityFramework.Helpers
{
    public static class PagingHelpers
    {
        /// <summary>
        /// Create a SearchCriteria object if all criterias have been defined
        /// </summary>
        /// <returns></returns>
        public static SearchCriteria CreateSearchCriteria(string serverName, string errorLevel, string errorMessage)
        {
            if (string.IsNullOrEmpty(serverName) && string.IsNullOrEmpty(errorLevel) && string.IsNullOrEmpty(errorMessage))
            {
                return null;
            }

            return new SearchCriteria
            {
                ServerName = serverName,
                Severity = errorLevel,
                StackTrace = errorMessage
            };
        }
    }
}