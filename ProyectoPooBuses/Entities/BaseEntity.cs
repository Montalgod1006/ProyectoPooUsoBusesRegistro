using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPooBuses.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
    }
}