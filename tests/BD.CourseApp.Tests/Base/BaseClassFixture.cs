using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace BD.CourseApp.Tests.Base
{
    public class DbConnectionFixture:IDisposable
    {

        public readonly SqlConnection DbConnection;
        private readonly SqlTransaction _transaction;

        public DbConnectionFixture()
        {
            string? ConnectionString = "data source=.;initial catalog=CourseAppDB;persist security info=True;user id=SA;password=fMw4WQkzD8;TrustServerCertificate=True;";
            DbConnection = new SqlConnection(ConnectionString);
        }

        public void Dispose()
        {
            DbConnection.Dispose();
        }
    }
}
