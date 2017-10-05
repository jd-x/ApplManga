using System.ComponentModel.DataAnnotations;

namespace ApplManga.Models {
    public class MangaList {
        [Key]
        public string Title { get; set; }
        public string Site { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public string PubStatus { get; set; }
    }
}
