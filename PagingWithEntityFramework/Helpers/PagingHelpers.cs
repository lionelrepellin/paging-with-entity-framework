using PagingWithEntityFramework.Domain;

namespace PagingWithEntityFramework.Helpers
{
    public static class PagingHelpers
    {
        /// <summary>
        /// Return a SearchCriteria object if search criteria have been defined
        /// </summary>
        /// <returns></returns>
        public static SearchCriteria GetDefinedSearchCriteria(string serverName, string errorLevel, string errorMessage)
        {
            if (string.IsNullOrEmpty(serverName) && string.IsNullOrEmpty(errorLevel) && string.IsNullOrEmpty(errorMessage))
            {
                return null;
            }
            else
            {
                return new SearchCriteria
                {
                    ServerName = serverName,
                    Severity = errorLevel,
                    StackTrace = errorMessage
                };
            }
        }
    }
}