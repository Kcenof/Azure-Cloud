using Azure.AI.TextAnalytics;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;
using System.IO;
using System;
using System.Threading.Tasks;

namespace LanguageBasedDocumentClassifier
{
    public class LangBasedDocumentClassifier
    {
        private readonly ILogger<LangBasedDocumentClassifier> _logger;
        private readonly TextAnalyticsClient _textAnalyticsClient;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobSourceContainerClient;
        private readonly BlobContainerClient _blobDestinationContainerClient;

        public LangBasedDocumentClassifier(ILogger<LangBasedDocumentClassifier> logger,
            TextAnalyticsClient textAnalyticsClient,
            BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _textAnalyticsClient = textAnalyticsClient;
            _blobServiceClient = blobServiceClient;
            _blobSourceContainerClient = _blobServiceClient.GetBlobContainerClient(Environment.GetEnvironmentVariable("sourceContainerName"));
            _blobDestinationContainerClient = _blobServiceClient.GetBlobContainerClient(Environment.GetEnvironmentVariable("targetContainerName"));
        }

        [Function(nameof(LangBasedDocumentClassifier))]
        public async Task Run([BlobTrigger("skabsource/{name}", Source = BlobTriggerSource.LogsAndContainerScan, Connection = "blobConn")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob Trigger (using Event Grid) processed blob\n Name: {name} \n Data: {content}");

            var detectedLanguage = await _textAnalyticsClient.DetectLanguageAsync(content);
            var languageName = detectedLanguage.Value.Name;
            _logger.LogInformation($"Detected language: {languageName}");

            string targetBlobName = $"{languageName}/{name}";
            BlobClient blobClient = _blobDestinationContainerClient.GetBlobClient(targetBlobName);
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            await blobClient.UploadAsync(new MemoryStream(byteArray));

            _logger.LogInformation($"Upload blob {targetBlobName} in the {Environment.GetEnvironmentVariable("targetContainerName")} container.");

            await _blobSourceContainerClient.DeleteBlobIfExistsAsync(name);
            _logger.LogInformation($"Deleted blob {name} from {Environment.GetEnvironmentVariable("sourceContainerName")} container.");
        }
    }
}