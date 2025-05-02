using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities;

public class ProductFeature
{
    public Guid ProductId { get; set; }
    public Guid FeatureId { get; set; }
    public Guid? DefaultFeatureOptionId { get; set; }
    public int Order { get; set; }
    public bool IsVisible { get; set; }
    public bool IsVariable { get; set; }

    // Navigation properties
    public virtual Product? Product { get; set; }
    public virtual Feature? Feature { get; set; }
    public virtual FeatureOption? DefaultFeature { get; set; }
} 