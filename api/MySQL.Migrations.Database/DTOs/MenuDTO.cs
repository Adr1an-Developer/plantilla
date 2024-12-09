using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQL.Migrations.Database.DTOs
{
    public class MenuDTO
    {
        public string id
        {
            get; set;
        }

        public string name
        {
            get; set;
        }

        public string menu_title
        {
            get; set;
        }

        public string menu_url
        {
            get; set;
        }

        public string menu_icon
        {
            get; set;
        }

        public int menu_group
        {
            get; set;
        }

        public string parent_menu_id
        {
            get; set;
        }

        public int menu_order
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

        public DateTime creation_date
        {
            get; set;
        }
    }
}