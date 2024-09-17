namespace user_listing.Dto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public bool Situacao { get; set; }
        public string CPF { get; set; }
    }
}
