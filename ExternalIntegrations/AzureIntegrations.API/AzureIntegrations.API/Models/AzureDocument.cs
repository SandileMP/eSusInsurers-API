namespace AzureIntegrations.API.Models
{
    public class AzureDocument
    {
        public string DocumentName { get; set; }

        public byte[] DocumentData { get; set; }

        public string MainDirectory { get; set; }

        public string ChildDirectory { get; set; }

        public string ChildSubDirectory { get; set; }
    }
}
