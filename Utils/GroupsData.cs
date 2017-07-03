using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplManga.ViewModels {
    public class GroupsData {
        public string groupName { get; set; }
        public string mangaTitle { get; set; }

        public GroupsData(string name, string title) {
            groupName = name;
            mangaTitle = title;
        }
    }
}
