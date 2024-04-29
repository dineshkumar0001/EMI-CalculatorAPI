namespace EmiCalculator.Models
{
    using System;

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Description { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public bool ActiveFlag { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
