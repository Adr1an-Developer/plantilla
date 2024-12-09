using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQL.Migrations.Database.DTOs
{
    public class PermissionDTO
    {
        public string id
        {
            get; set;
        }

        public string menu_id
        {
            get; set;
        }

        public string profile_id
        {
            get; set;
        }

        public bool can_view
        {
            get; set;
        }

        public bool can_add
        {
            get; set;
        }

        public bool can_edit
        {
            get; set;
        }

        public bool can_delete
        {
            get; set;
        }

        public bool can_export
        {
            get; set;
        }

        public bool can_authorize
        {
            get; set;
        }

        public bool is_active
        {
            get; set;
        }

        public bool is_deleted
        {
            get; set;
        }

        public string create_by_user
        {
            get; set;
        }
    }
}