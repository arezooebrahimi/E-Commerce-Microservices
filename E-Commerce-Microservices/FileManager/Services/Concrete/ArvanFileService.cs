using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using FileManager.Services.Abstract;


namespace FileManager.Services.Concrete
{
    public class ArvanFileService:IArvanFileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string? _bucketName;

        public ArvanFileService(IConfiguration configuration)
        {
            var accessKey = configuration["ArvanStorage:AccessKey"];
            var secretKey = configuration["ArvanStorage:SecretKey"];
            var serviceUrl = configuration["ArvanStorage:ServiceURL"];
            var region = configuration["ArvanStorage:Region"];

            _bucketName = configuration["ArvanStorage:BucketName"];

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(_bucketName))
            {
                throw new ArgumentException("Arvan Storage configuration is missing.");
            }

            var config = new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                SignatureVersion = "2",
                ForcePathStyle = true,
            };

            var awsCredentials = new BasicAWSCredentials(accessKey,secretKey);

            _s3Client = new AmazonS3Client(awsCredentials, config);
        }

        public async Task<string> UploadFileAsync(string fileName,string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("فایل پیدا نشد.", filePath);

            string folderName = DateTime.Now.ToString("MM-yyyy");
            string key = $"{folderName}/{fileName}";
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key ,
                FilePath = filePath,
                CannedACL = S3CannedACL.PublicRead,
                AutoCloseStream = true
            };

            var response = await _s3Client.PutObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return key;
            }
            else
            {
                throw new Exception("File upload failed.");
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            var response = await _s3Client.DeleteObjectAsync(request);
        }

        //return $"https://{_bucketName}.s3.ir-thr-at1.arvanstorage.ir/{fileName}";
    }
}
