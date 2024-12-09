using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.DTOs.SystemMenu
{
    public class AddSystemMenu
    {
        public string Name
        {
            get; set;
        } // Menu name

        public string Url
        {
            get; set;
        } // URL

        public string Title
        {
            get; set;
        } // Menu title

        public string Icon
        {
            get; set;
        } // Menu icon

        public bool Group
        {
            get; set;
        }

        public string? ParentMenuId
        {
            get; set;
        } // Parent menu id (null if root menu)

        public long Order
        {
            get; set;
        }
    }
}