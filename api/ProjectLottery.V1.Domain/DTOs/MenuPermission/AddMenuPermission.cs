using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.DTOs.MenuPermission
{
    public class AddMenuPermission
    {
        public string MenuId
        {
            get; set;
        } // Foreign key: refers to system_menu

        public string ProfileId
        {
            get; set;
        } // Foreign key: refers to profiles

        public bool CanView { get; set; } = true; // Can view the menu

        public bool CanAdd { get; set; } = false; // Can add new items

        public bool CanEdit { get; set; } = false; // Can edit items

        public bool CanDelete { get; set; } = false; // Can delete items

        public bool CanExport { get; set; } = false; // Can export items

        public bool CanAuthorize { get; set; } = false; // Can authorize actions
    }
}