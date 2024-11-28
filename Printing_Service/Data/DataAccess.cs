namespace Printing_Service.Data
{
    public class DataAccess
    {
        protected readonly string _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException(nameof(_connectionString), "Connection string cannot be null or empty.");
        }

    }
}
// GithubTest
