namespace Printing_Service.Models
{
    public class Printing
    {
        public string Printer_ID {  get; set; }
	    public string Student_ID { get; set; }
	    public List<string> Document_ID {get; set;}
    }
}
