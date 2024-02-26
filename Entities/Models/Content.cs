using DataLayer.Models.AbstractEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Content : Entity
{
    public Content()
    {
    }

    // TO-DO: add file

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
    
    public bool Active { get; set; }
    
    public string Description { get; set; }
    
    public string Link { get; set; }

    public DateTime ShowDate { get; set; }
}