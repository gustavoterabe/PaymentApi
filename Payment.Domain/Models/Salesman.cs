using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Domain.Models;

public class Salesman : BaseEntity
{
    public string Document { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    
    public List<Sale>? Sales { get; set; }
}