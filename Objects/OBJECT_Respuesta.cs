using System;

namespace AldasKevin_Exm.Objects
{
    public class OBJECT_Respuesta
    {
        public @object[] objects { get; set; }
        public Metadata metadata { get; set; }


        public string requestId { get; set; }
        public string modelVersion { get; set; }
    }
}
