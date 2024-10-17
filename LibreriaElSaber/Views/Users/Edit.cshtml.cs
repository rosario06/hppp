using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibreriaElSaber.Views.Users
{
    public class EditModel : PageModel
    {

        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string TipoUsuario { get; set; }
        public string Imagen { get; set; }

        public void OnGet()
        {
        }
    }
}
