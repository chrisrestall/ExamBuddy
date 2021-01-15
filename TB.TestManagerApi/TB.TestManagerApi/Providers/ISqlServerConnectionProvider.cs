using System.Data;

namespace TB.TestManagerApi.Providers
{
    public interface ISqlServerConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}