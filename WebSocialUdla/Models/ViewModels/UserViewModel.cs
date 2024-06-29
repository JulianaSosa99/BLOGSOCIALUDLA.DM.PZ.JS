namespace WebSocialUdla.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public bool AdminRoleCheckBox { get; set; }
    }
}
