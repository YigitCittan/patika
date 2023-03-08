using System.ComponentModel.DataAnnotations.Schema;

namespace patikaodev;

public class Product
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }


}
