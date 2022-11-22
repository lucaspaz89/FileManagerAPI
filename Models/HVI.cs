namespace FileManagerAPI.Models
{
    public class HVI
    {
        public int Id { get; set; }
        public decimal UHML { get; set; }
        public decimal UI { get; set; }
        public decimal STR { get; set; }
        public decimal ELONG { get; set; }
        public decimal SFI { get; set; }
    }

    public class ListHVI
    {
        public List<HVI> HVIList { get; set; }
    }
}
