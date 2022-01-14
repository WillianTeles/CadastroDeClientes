using LinqToDB.Configuration;
using LinqToDB.Data;

public class AppDataConnection : DataConnection
{
    public AppDataConnection(LinqToDbConnectionOptions<AppDataConnection> options)
        : base(options)
    { 
        
    }
}
