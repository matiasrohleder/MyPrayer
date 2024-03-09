using DataLayer.Helpers;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.AbstractEntities;

public abstract class UserEntity : IdentityUser<Guid>, IEntity
{
    public bool Deleted { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = nameof(UserEntity);
    public DateTime CreatedDate { get; set; }
    public DateTime LastEditedDate { get; set; }
    public Guid CreatorId { get; set; }
    public Guid LastEditorId { get; set; }

    public virtual string ToJson()
    {
        // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
        // Tracking code: 3.0.0-issue1
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
        {
            MaxDepth = 1,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }

    public virtual bool Compare(IEntity entity)
    {
        return Id == entity.Id;
    }

    public virtual void CopyTo<TEntity>(TEntity target, params string[] excludeFields) where TEntity : class, IEntity
    {
        // Exclude properties that are not allowed to be modified by the user.
        excludeFields =
        [
            .. excludeFields,
            nameof(NormalizedUserName),
            nameof(NormalizedEmail),
            nameof(PasswordHash),
            nameof(SecurityStamp),
            nameof(LockoutEnd),
            nameof(EmailConfirmed),
            nameof(ConcurrencyStamp),
        ];

#pragma warning disable CS8604 // Possible null reference argument. // TODO
        EntityCopy<TEntity>.Copy(this as TEntity, target, excludeFields);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}