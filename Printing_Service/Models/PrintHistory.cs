namespace Printing_Service.Models
{
    public class PrintHistory
    {
        public DateTime Date { get; set; }
        
        public string FileName { get; set; }

        public string PageA4 { get; set; }

        public int Printer { get; set; }

        public string Side { get; set; }

        public string Scale{ get; set; }

        public string Orientation { get; set; }
    }
}
