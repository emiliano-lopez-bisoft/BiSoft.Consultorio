using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BiSoft.Consultorio.Api.Helpers.HealthChecks
{
    public class DataBaseHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public DataBaseHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var canConnect = CanConnectToDataBase();
            if (canConnect)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, description: "Database connection is healthy");
            }
            return new HealthCheckResult(HealthStatus.Healthy, description: "Database connection is healthy");
        } 
        private bool CanConnectToDataBase()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                var canConnect = connection.State == System.Data.ConnectionState.Open;
                connection.Dispose();
                return canConnect;
            }
            catch
            {
                return false;
            }
        }
    }
}
