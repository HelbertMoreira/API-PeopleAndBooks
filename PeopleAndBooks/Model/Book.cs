using PeopleAndBooks.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace PeopleAndBooks.Model
{
    public class Book : BaseEntity
    {
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Preço")]
        public decimal Price { get; set; }

        [Display(Name = "Data de lançamento")]
        public DateTime LaunchDate { get; set; }
    }
}
