using PeopleAndBooks.Model.Base;

namespace PeopleAndBooks.Model
{    
    public class Person : BaseEntity
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
}
