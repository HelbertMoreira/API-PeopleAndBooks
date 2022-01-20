using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleAndBooks.Model
{
    [Table("UserSystem")]
    public class UserSystem
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("User_name")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Column("Password")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Column("Full_name")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Column("Refresh_token")]
        public string Token { get; set; }

        [Column("Refresh_token_expiry_time")]
        public DateTime RefreshToken { get; set; }
    }
}
