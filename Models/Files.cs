namespace FileManagerAPI.Models
{
    public class Files
    {
        public int Id { get; set; }
        public string Ruta { get; set; } = string.Empty;
        public string ArchivoNombre { get; set; } = string.Empty;
        public IFormFile Archivo { get; set; }


    }
}
