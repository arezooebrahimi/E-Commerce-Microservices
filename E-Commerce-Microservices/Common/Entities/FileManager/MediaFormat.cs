using MongoDB.Bson.Serialization.Attributes;

namespace Common.Entities.FileManager
{
    public class MediaFormat
    {
        [BsonElement("file_name")]
        public required string FileName { get; set; }

        [BsonElement("file_path")]
        public required string FilePath { get; set; }

        [BsonElement("format")]
        public required string Format { get; set; }

        [BsonElement("ext")]
        public required string Ext { get; set; }

        [BsonElement("width")]
        public int Width { get; set; }

        [BsonElement("height")]
        public int Height { get; set; }
    }
}
