namespace Printing_Service.Models
{
    public class Document
    {
        private string Document_ID { get; set; }
        private bool A3page { get; set; }
        private int Page { get; set; }
        private string Type { get; set; }
        private float Ratio { get; set; }
        private string Name { get; set; }

    }
}
