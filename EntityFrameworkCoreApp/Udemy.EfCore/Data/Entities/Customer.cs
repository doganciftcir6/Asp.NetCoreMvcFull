using System.ComponentModel.DataAnnotations;

namespace Udemy.EfCore.Data.Entities
{
    public class Customer
    {
        //[Key]
        public int Number { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
