using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cidades.Domain.Model.Base;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public long id { get; set; }

}