namespace WebAssemblyKERP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string GoogleId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Role { get; set; } = "User"; // Domyślna rola to User
    }
}
