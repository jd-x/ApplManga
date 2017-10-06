namespace jdx.ApplManga.Core.Models {
    public class GroupsData {
        public string groupName { get; set; }
        public string mangaTitle { get; set; }

        public GroupsData(string name, string title) {
            groupName = name;
            mangaTitle = title;
        }
    }
}
