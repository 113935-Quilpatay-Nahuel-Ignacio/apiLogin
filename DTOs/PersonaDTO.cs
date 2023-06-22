namespace ApiLogin.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Password { get; set; }
    }
}
