using PeopleAndBooks.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace PeopleAndBooks.Model
{    
    public class Person : BaseEntity
    {
        [Display(Name = "Primeniro nome")]
        public string First_Name { get; set; }

        [Display(Name = "Sobrenome")]
        public string Last_Name { get; set; }

        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [Display(Name = "Gênero")]
        public string Gender { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}
