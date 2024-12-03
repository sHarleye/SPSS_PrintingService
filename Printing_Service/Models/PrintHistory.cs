namespace Printing_Service.Models
{
    public class PrintHistory
    {
        public DateTime Date { get; set; }
        
        public string FileName { get; set; }

        public int PageA4 { get; set; }

        public int Printer { get; set; }

        public string Color { get; set; }
    }
}
