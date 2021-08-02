using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Amazon.DynamoDBv2.DataModel;
using FluentValidation;

namespace FTS.Precatorio.Domain.Core
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity()
        {
            ValidationResult = new FluentValidation.Results.ValidationResult();
        }

        [DynamoDBHashKey]
        public Guid Id { get; protected set; }

        [Required]
        public Guid Control_GrupoId { get; set; }

        public DateTimeOffset Control_DataInc { get; set; }

        [Required]
        public DateTimeOffset Control_DataAlter { get; set; }

        [Required]
        [MaxLength(50), StringLength(50)]
        public string Control_UsuInc { get; set; }

        [Required]
        [MaxLength(50), StringLength(50)]
        public string Control_UsuAlter { get; set; }

        [NotMapped]
        [DynamoDBIgnore]
        public FluentValidation.Results.ValidationResult ValidationResult { get; protected set; }
    }

    public interface IEntityControlGroup { }
}