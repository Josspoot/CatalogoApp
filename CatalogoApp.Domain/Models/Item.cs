namespace CatalogoApp.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Consola { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public List<ComentarioItem> Comentarios { get; set; } = new();
    }

    public class ComentarioItem
    {
        // Genera un Id único automáticamente para cada comentario nuevo
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string Usuario { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public int Estrellas { get; set; } 
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        // Propiedad para contar los apoyos recibidos
        public int Apoyos { get; set; } = 0; 
    }
}