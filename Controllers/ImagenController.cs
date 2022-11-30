using AldasKevin_Exm.layer;
using AldasKevin_Exm.models;
using AldasKevin_Exm.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AldasKevin_Exm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<imagen>> PostIMG (string ruta_img)
        {

            var api_objecto = new System.Net.WebClient();
            api_objecto.Headers.Add("Content-type", "application/octet-stream");
            api_objecto.Headers.Add("Ocp-Apim-Subscription-Key", "d2b6384bd9f1453ba1b819a6b8737911");
            var qs_objecto = "model-version=latest";
            var url_objeto = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/detect";


            var resp_objeto = api_objecto.UploadFile(url_objeto + "?" + qs_objecto, "POST", ruta_img);
            var json_objeto = Encoding.UTF8.GetString(resp_objeto);
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OBJECT_Respuesta>(json_objeto);

            var api_texto = new System.Net.WebClient();
            api_texto.Headers.Add("Content-type", "application/octet-stream");
            api_texto.Headers.Add("Ocp-Apim-Subscription-Key", "d2b6384bd9f1453ba1b819a6b8737911");

            var qs_texto = "language=es&language=true&model-version=latest";
            var url_texto = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/ocr";


            var resp_texto = api_texto.UploadFile(url_texto + "?" + qs_texto, "POST", ruta_img);
            var json_texto = System.Text.Encoding.UTF8.GetString(resp_texto);
            var texto = Newtonsoft.Json.JsonConvert.DeserializeObject<OCR_Respuesta>(json_texto);


            

            return Ok("TEXTO DE IMAGEN: \n" + respuesta_TXT_OCR(texto) + "\n" + "OBJETOS DE IMAGEN: \n" + respuesta_Objectos_OCR(obj));
        }
        private static string respuesta_Objectos_OCR(OBJECT_Respuesta resp)
        {
            var contador = 1;
            var respuesta = "";

            foreach (var @object in resp.objects)
            {
                respuesta += "Objecto: " + @object.Object + "\n";
                var formato = "";
                var parent = @object.parent;
                while (parent != null)
                {
                    formato += " ";
                    respuesta += formato + "Padre " + contador + ": " + parent.Object + "\n";
                    parent = parent.parent;
                    contador++;
                }
                formato += "";
                contador = 1;
            }
            return respuesta;
        }
        private static string respuesta_TXT_OCR(OCR_Respuesta resp)
        {
            var respuesta = "";


            foreach (var region in resp.regions)
            {
                foreach (var line in region.lines)
                {
                    foreach (var word in line.words)
                    {
                        respuesta += word.text + " ";
                    }
                    respuesta += "\n";
                }
                respuesta += "\n";
            }
            return respuesta;
        }

        
    }
}
