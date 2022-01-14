﻿using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace CadastroDeClientes.Domain
{
    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
            => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "dbClientes",
                        ProviderName = "System.Data.SqlClient",
                        ConnectionString =
                            @"Server=INVENT044\SAP;Database=dbClientes;Trusted_Connection=True;Enlist=False;"
                    };
            }
        }
    }
}
