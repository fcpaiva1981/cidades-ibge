using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cidades.Domain.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace Cidades.Domain.Model;

[Table("cidades")]
[Index(nameof(IBGE), IsUnique = true)]
public class Cidades : BaseEntity
{
    [Column("ibge")]
    [Required]
    public long IBGE { get; set; }

    [Column("ibge7")]
    [Required]
    public long IBGE7 { get; set; }

    [Column("uf")]
    [Required]
    [StringLength(2)]
    public string UF { get; set; }

    [Column("municipio")]
    [Required]
    [StringLength(255)]
    public string Municipio { get; set; }

    [Column("regiao")]
    [Required]
    [StringLength(30)]
    public string Regiao { get; set; }

    [Column("porte")]
    [Required]
    [StringLength(30)]
    public string Porte { get; set; }

    [Column("populacao")]
    [Required]
    public long Populacao { get; set; }


}