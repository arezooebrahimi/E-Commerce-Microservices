namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MongoIndexAttribute : Attribute
    {
        public bool Unique { get; set; } = false;
        public bool Ascending { get; set; } = true;
        public string? Name { get; set; }
    }
}
