using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIntegrations.API.Models
{
    public class StorageConfiguration
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string FileShare { get; set; }
        public string ProxyKey { get; set; }
    }
}
