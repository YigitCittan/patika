using System.ComponentModel.DataAnnotations.Schema;

namespace patikaodev;

public class Product
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int GenreID { get; set; }
    
    public string? Genre{get; set;}

    public string? Name { get; set; }

    public string? Color { get; set; }

    public decimal Price { get; set; }


}
