using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Redis
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }

        public string ConnectionString { get; set; }
    }
}
