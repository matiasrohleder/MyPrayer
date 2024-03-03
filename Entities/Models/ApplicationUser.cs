using System.ComponentModel.DataAnnotations;
using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

public class ApplicationUser : UserEntity
{
    #region Properties

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    #endregion
}