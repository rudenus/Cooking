using Dal.Enums;

namespace Dal.Entities
{
    public class File
    {
        public Guid FileId { get; set; }

        public byte[] Content { get; set; }

        public FileType Type { get; set; }

        public string Name { get; set; }
    }
}
