namespace FTS.Precatorio.Infrastructure.Amazon
{
    public class AmazonConfiguration
    {
        public string AcessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
        public AmazonConfigurationSQS SQS { get; set; }
        public AmazonConfigurationSNS SNS { get; set; }
        public AmazonConfigurationDynamoDB DynamoDB { get; set; }
    }

    public class AmazonConfigurationSQS
    {
        public string Region { get; set; }
    }

    public class AmazonConfigurationSNS
    {
        public string Region { get; set; }
    }

    public class AmazonConfigurationDynamoDB
    {
        public string Region { get; set; }
    }
}