using ProjectLottery.V1.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.System
{
    [Table("system_menu")]
    public class SystemMenu : AuditEntity
    {
        [Column("id")]
        public string Id
        {
            get; set;
        } // Primary key

        [Column("name")]
        public string Name
        {
            get; set;
        } // Menu name

        [Column("menu_url")]
        public string Url
        {
            get; set;
        } // URL

        [Column("menu_title")]
        public string Title
        {
            get; set;
        } // Menu title

        [Column("menu_icon")]
        public string Icon
        {
            get; set;
        } // Menu icon

        [Column("menu_group")]
        public bool Group
        {
            get; set;
        }

        [Column("parent_menu_id")]
        public string? ParentMenuId
        {
            get; set;
        } // Parent menu id (null if root menu)

        [Column("menu_order")]
        public long Order
        {
            get; set;
        } // Menu display order

        [NotMapped]
        public string ParentMenu
        {
            get; set;
        }
    }
}