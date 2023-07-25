using System.Xml.Serialization;

namespace My_Books.Data.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public int publisherId { get; set; }
        public List<int>? AuthorsIds { get; set; }
    }

    public class BookWithAuthorsVM
    {
        public int Id { get; set; }
        [XmlElement("Title")]
        public string Title { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("IsRead")]
        public bool IsRead { get; set; }
        [XmlElement("DateRead")]
        public DateTime? DateRead { get; set; }

        [XmlElement("Rate")]
        public int? Rate { get; set; }
        [XmlElement("Genre")]
        public string Genre { get; set; }
        [XmlElement("CoverUrl")]
        public string CoverUrl { get; set; }
        [XmlElement("publisherName")]
        public string? publisherName { get; set; }
        [XmlElement("AuthorsName")]
        public List<string>? AuthorsName { get; set; }
        [XmlElement("bookFile")]
        public List<string> bookFile { get; set; }
    }
}
