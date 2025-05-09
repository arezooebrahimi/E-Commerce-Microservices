namespace FileManager.Models.Mongodb
{
    public class MongoDbSettings
    {
        public  required string ConnectionURI { get; set; }
        public required string DatabaseName { get; set; }
    }
}
