namespace Common.Dtos.Common
{
    public class FilterOptions
    {
        public List<FilterMode> FilterModes { get; set; } = new();
        public string Operator { get; set; } = "and";
    }
}
