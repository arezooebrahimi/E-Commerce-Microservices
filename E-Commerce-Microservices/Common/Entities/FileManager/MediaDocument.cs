using Common.Attributes;
using Common.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Entities.FileManager
{
    public class MediaDocument:ICreatedAtEntity, IFilePathEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("file_name")]
        [MongoIndex(Unique = false, Name = "file_name")]
        public required string FileName { get; set; }

        [BsonElement("file_path")]
        [MongoIndex(Unique = true, Name = "file_path")]
        public required string FilePath { get; set; }

        [BsonElement("mime_type")]
        public required string MimeType { get; set; }

        [BsonElement("size")]
        public long Size { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("formats")]
        public List<MediaFormat>? Formats { get; set; }
    }
}
