using ProjectLottery.V1.Entities.Common;
using ProjectLottery.V1.Entities.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.System
{
    [Table("menu_permissions")]
    public class MenuPermission : AuditEntity
    {
        [Column("id")]
        public string Id
        {
            get; set;
        } // Primary key

        [Column("menu_id")]
        public string MenuId
        {
            get; set;
        } // Foreign key: refers to system_menu

        [Column("profile_id")]
        public string ProfileId
        {
            get; set;
        } // Foreign key: refers to profiles

        [Column("can_view")]
        public bool CanView { get; set; } = true; // Can view the menu

        [Column("can_add")]
        public bool CanAdd { get; set; } = false; // Can add new items

        [Column("can_edit")]
        public bool CanEdit { get; set; } = false; // Can edit items

        [Column("can_delete")]
        public bool CanDelete { get; set; } = false; // Can delete items

        [Column("can_export")]
        public bool CanExport { get; set; } = false; // Can export items

        [Column("can_authorize")]
        public bool CanAuthorize { get; set; } = false; // Can authorize actions

        [NotMapped]
        public string? MenuName
        {
            get; set;
        }

        [NotMapped]
        public string? ProfileName
        {
            get; set;
        }

        [NotMapped]
        public string? ParentMenuId
        {
            get; set;
        }

        [NotMapped]
        public long? MenuOrder
        {
            get; set;
        }

        [NotMapped]
        public bool? isChild
        {
            get
            {
                return ParentMenuId != null;
            }
        }
    }
}