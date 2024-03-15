namespace PrimeraPracticaNetCore.Models
{
    public class ModelZapasImagen
    {
        public Zapatilla Zapatilla { get; set; }
        public List<ImagenesZapa> ImagenesZapa { get; set; }
        public ModelZapasImagen()
        {
            this.ImagenesZapa= new List<ImagenesZapa>();
        }
    }
}
