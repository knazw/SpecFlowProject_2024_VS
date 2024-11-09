using OpenQA.Selenium;
using SpecFlowProject_2024.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.PageObjects
{
    internal class RegisterPage : BasePage
    {
        public IWebElement radioMale => webDriver.FindElement(By.XPath("//*[@type='radio' and @id='gender-male']"));
        public IWebElement radioFemale => webDriver.FindElement(By.XPath("//*[@type='radio' and @id='gender-female']"));
        public IWebElement inputFirstName => webDriver.FindElement(By.Id("FirstName"));
        public IWebElement inputLastName => webDriver.FindElement(By.Id("LastName"));
        public IWebElement selectDateOfBirthDay => webDriver.FindElement(By.CssSelector("select[name='DateOfBirthDay']"));
        public IWebElement selectDateOfBirthMonth => webDriver.FindElement(By.CssSelector("select[name='DateOfBirthMonth']"));
        public IWebElement selectDateOfBirthYear => webDriver.FindElement(By.CssSelector("select[name='DateOfBirthYear']"));
        public IWebElement inputEmail => webDriver.FindElement(By.Id("Email"));
        public IWebElement inputPassword => webDriver.FindElement(By.Id("Password"));
        public IWebElement inputConfirmPassword => webDriver.FindElement(By.Id("ConfirmPassword"));
        public IWebElement checkboxNewsletter => webDriver.FindElement(By.Id("Newsletter"));
        public IWebElement buttonRegister => webDriver.FindElement(By.Id("register-button"));
        //        public IWebElement iconRegister => webDriver.FindElement(By.CssSelector("a[class='ico-register']"));

        public RegisterPage(IWebDriver webDriver) : base(webDriver)
        {

        }

        public RegisterPage FillFields(User user)
        {
            SelectMaleOrFemale(user.Gender);
            SetFirstName(user.FirstName);
            SetLastName(user.LastName);
            SetDayOfBirth(user.DateOfBirth.Day);
            SetMonthOfBirth(user.DateOfBirth.Month);
            SetYearOfBirth(user.DateOfBirth.Year);
            SetEmail(user.Email);
            SetInputPassword(user.Password);
            SetInputConfirmPassword(user.Password);
            SetCheckboxNewsletter(user.Newsletter);

                /*
            this.stepsData.registerPage.setFirstName(this.stepsData.currentUser.firstName);
            this.stepsData.registerPage.setLastName(this.stepsData.currentUser.lastName);

            this.stepsData.registerPage.setDayOfBirth(this.stepsData.currentUser.dateOfBirth.getDayOfMonth());
            this.stepsData.registerPage.setMonthOfBirth(this.stepsData.currentUser.dateOfBirth.getMonth().toString());
            this.stepsData.registerPage.setYearOfBirth(this.stepsData.currentUser.dateOfBirth.getYear());
            this.stepsData.registerPage.setEmail(this.stepsData.currentUser.email);

            this.stepsData.registerPage.setInputPassword(this.stepsData.currentUser.password);
            this.stepsData.registerPage.setInputConfirmPassword(this.stepsData.currentUser.password);
            this.stepsData.registerPage.setCheckboxNewsletter(this.stepsData.currentUser.newsletter);
                */

            return this;
        }

        public RegisterResult ClickRegister()
        {
            Click(buttonRegister);

            return new RegisterResult(webDriver);
        }

        private void SetCheckboxNewsletter(bool newsletter)
        {
            if(!newsletter)
            {
                Click(checkboxNewsletter); 
            }            
        }

        private void SetInputConfirmPassword(string password)
        {
            type(inputConfirmPassword, password);
        }

        private void SetInputPassword(string password)
        {
            type(inputPassword, password);
        }

        private void SetEmail(string email)
        {
            type(inputEmail, email);            
        }

        private void SetYearOfBirth(int year)
        {
            type(selectDateOfBirthYear, year + "");
        }

        private void SetMonthOfBirth(int month)
        {
            string monthString;
            switch(month)
            {
                case 1:
                    monthString = "January";
                    break;
                case 2:
                    monthString = "February";
                    break;
                case 3:
                    monthString = "March";
                    break;
                case 4:
                    monthString = "April";
                    break;
                case 5:
                    monthString = "May";
                    break;
                case 6:
                    monthString = "June";
                    break;
                case 7:
                    monthString = "July";
                    break;
                case 8:
                    monthString = "August";
                    break;
                case 9:
                    monthString = "September";
                    break;
                case 10:
                    monthString = "October";
                    break;
                case 11:
                    monthString = "November";
                    break;
                case 12:
                    monthString = "December";
                    break;
                default:
                    monthString = "January";
                    break;
            }
            type(selectDateOfBirthMonth, monthString);

        }

        private void SetDayOfBirth(int day)
        {
            type(selectDateOfBirthDay, day + "");

            //throw new NotImplementedException();
        }

        private void SetLastName(string lastName)
        {
            type(inputLastName, lastName);
        }

        private void SetFirstName(string firstName)
        {
            type(inputFirstName, firstName);            
        }

        public void SelectMaleOrFemale(string gender)
        {
            if (gender.Equals("female"))
            {
                Click(radioFemale);
            }
            else if (gender.Equals("male"))
            {
                Click(radioMale);
            }
        }
    }
}
