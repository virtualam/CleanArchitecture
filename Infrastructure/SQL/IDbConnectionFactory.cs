using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.SQL
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
