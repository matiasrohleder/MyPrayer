using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

[Table("Categories")]
public class Category : Entity
{
    public Category()
    {
        Active = true;
    }

    public bool Active { get; set; }

    public int Order { get; set; }
}