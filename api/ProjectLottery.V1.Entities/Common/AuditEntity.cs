using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Common
{
    public class AuditEntity
    {
        [Column("is_active")]
        public bool? IsActive
        {
            get; set;
        }

        [Column("is_deleted")]
        public bool? IsDeleted
        {
            get; set;
        }

        [Column("creation_date")]
        public DateTimeOffset? CreationDate
        {
            get; set;
        }

        [Column("create_by_user")]
        public string? CreateByUser
        {
            get; set;
        }

        [Column("modification_date")]
        public DateTimeOffset? ModificationDate
        {
            get; set;
        }

        [Column("update_by_user")]
        public string? UpdateByUser
        {
            get; set;
        }

        /// <summary>
        /// Para marcar como eliminado el registro.
        /// </summary>
        /// <param name="deletedByUserId"></param>
        public void Delete(string deletedByUserId)
        {
            UpdateByUser = deletedByUserId;
            ModificationDate = DateTime.Now;
            IsActive = false;
            IsDeleted = true;
        }

        /// <summary>
        /// Actualiza los campos de auditoria para la edición
        /// </summary>
        /// <param name="updateByUserId"></param>
        public void UpdateAuditInfo(string updateByUserId)
        {
            UpdateByUser = updateByUserId;
            ModificationDate = DateTime.Now;
        }
    }

    public class AuditEntityWithID
    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("is_active")]
        public bool? IsActive
        {
            get; set;
        }

        [Column("is_deleted")]
        public bool? IsDeleted
        {
            get; set;
        }

        [Column("creation_date")]
        public DateTimeOffset? CreationDate
        {
            get; set;
        }

        [Column("create_by_user")]
        public string? CreateByUser
        {
            get; set;
        }

        [Column("modification_date")]
        public DateTimeOffset? ModificationDate
        {
            get; set;
        }

        [Column("update_by_user")]
        public string? UpdateByUser
        {
            get; set;
        }
    }

    public class AuditEntityNotUserFields
    {
        [Column("is_active")]
        public bool? IsActive
        {
            get; set;
        }

        [Column("is_deleted")]
        public bool? IsDeleted
        {
            get; set;
        }

        [Column("creation_date")]
        public DateTimeOffset? CreationDate
        {
            get; set;
        }
    }
}