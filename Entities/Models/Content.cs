using DataLayer.Models.AbstractEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Content : Entity
{
    public Content()
    {
        Description = string.Empty;
        Link = string.Empty;
    }

    // TO-DO: add file

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }
    
    public bool Active { get; set; }

    [StringLength(2000)]
    public string Description { get; set; }

    [StringLength(100)]
    public string Link { get; set; }

    public DateTime ShowDate { get; set; }
}