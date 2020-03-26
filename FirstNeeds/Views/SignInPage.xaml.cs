using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace FirstNeeds.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        string mobileNumber = string.Empty;
        bool isMobileNumberValid = false;
        string randomNumber;
        public SignInPage()
        {
            InitializeComponent();
        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            mobileNumber = mobileNumberEntry.Text;
            bool isNumberValid = ValidateMobileNumber(this.mobileNumber);

            if (isNumberValid)
            {
                string OTP = GetOTP();


                Navigation.PushAsync(new SMSVerificationPage(OTP));

                //Navigation.PushAsync(new SMSVerificationPage("123456"));

            }
        }



        public string GetOTP()
        {
            String result;
            string apiKey = "lB951zfxkKY-s3ZK3b5MHsKwBE6pjxpQq3bPrABV0X";
            string numbers = mobileNumberEntry.Text; // in a comma seperated list
            string sender = "TXTLCL";

            Random rnd = new Random();
            randomNumber = (rnd.Next(100000, 999999)).ToString();
            string message = "Hey FirstNeeds user, Your OTP is " + randomNumber;
                       
            String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
            //refer to parameters to complete correct url string

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception ex)
            {
                //return e.Message;
                DisplayAlert("Error", "Erros is " + ex, "OK");
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }

            DisplayAlert("Success", "OTP sent successfully", "OK");

            return randomNumber;
        }

        private Boolean ValidateMobileNumber(string mobileNumber)
        {
            bool isNumberEntered = string.IsNullOrEmpty(mobileNumber);

            if (isNumberEntered)
            {
                DisplayAlert("Alert", "Please enter a number", "OK");
                return false;
            }

            else
            {
                long MobileNumberConverted = 0;
                sbyte lengthOfMobNo = 10;
                //bool isConversionSuccessful = long.TryParse(mobileNumber, out MobileNumberConverted);
                bool isDigitsOnly = IsDigitsOnly(mobileNumber);
                if (isDigitsOnly)
                { 
                    bool isMobileLengthValid = lengthOfMobNo == mobileNumber.ToString().Length;

                    if (isMobileLengthValid)
                    {
                        //DisplayAlert("Success", "Valid number", "OK");
                        return true;

                    }
                    else
                    {
                        DisplayAlert("Alert", "Please enter 10 digit number", "OK");
                        return false;

                    }
                }
                else
                {
                    DisplayAlert("Alert", "Please enter numbers only", "OK");
                    return false;

                }

            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}