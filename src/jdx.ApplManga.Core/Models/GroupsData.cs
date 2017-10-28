namespace jdx.ApplManga.Core.Models {
    public class GroupsData {
        public string GroupName { get; set; }
        public string MangaTitle { get; set; }

        public GroupsData(string name, string title) {
            GroupName = name;
            MangaTitle = title;
        }
    }
}
