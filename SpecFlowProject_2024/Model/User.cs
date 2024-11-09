using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Model
{
    internal class User
    {
        private string firstName;
        private string lastName;

        private string email;
        //    @JsonFormat
        //            (shape = JsonFormat.Shape.STRING, pattern = "dd-MM-yyyy hh:mm:ss")
        private DateTimeOffset dateOfBirth;

        private string gender;

        private string password;
        private string companyName;

        private bool newsletter;

        private string city;

        private string address;

        private string postalCode;

        private string creditCard;

        private string expirationOfCreditCard;

        private string verificationCode;
        private string country;
        private string phoneNumber;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public DateTimeOffset DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Password { get => password; set => password = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public bool Newsletter { get => newsletter; set => newsletter = value; }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public string CreditCard { get => creditCard; set => creditCard = value; }
        public string ExpirationOfCreditCard { get => expirationOfCreditCard; set => expirationOfCreditCard = value; }
        public string VerificationCode { get => verificationCode; set => verificationCode = value; }
        public string Country { get => country; set => country = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public User()
        {

        }
        public User(string firstName, string lastName, string email, DateTimeOffset dateOfBirth, string gender, string password, string companyName, bool newsletter, string city, string address, string postalCode, string creditCard, string expirationOfCreditCard, string verificationCode, string country, string phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Password = password;
            this.CompanyName = companyName;
            this.Newsletter = newsletter;
            this.City = city;
            this.Address = address;
            this.PostalCode = postalCode;
            this.CreditCard = creditCard;
            this.ExpirationOfCreditCard = expirationOfCreditCard;
            this.VerificationCode = verificationCode;
            this.Country = country;
            this.PhoneNumber = phoneNumber;
        }
    }
}
