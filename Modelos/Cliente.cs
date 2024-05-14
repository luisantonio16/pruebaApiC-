using System.ComponentModel.DataAnnotations;

namespace pruebaApiC_.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "el campo Nombre debe de tener maximo 50 caracteres")]
        public string Nombre { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "el campo Nombre debe de tener maximo 50 caracteres")]
        public string Apellido { get; set; } = null!;
        public int êdad { get; set; }
    }
}
