namespace AldasKevin_Exm.layer
{
    public class OCR_Respuesta
    {
       
        public string orientation { get; set; }
        public string modelVersion { get; set; }
        public string language { get; set; }
        public decimal textAngle { get; set; }

        // Array
        public region[] regions { get; set; }
    }
}
