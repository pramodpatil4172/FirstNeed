using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstNeeds.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SMSVerificationPage : ContentPage
    {
        string OTP;
        public SMSVerificationPage(string OTP)
        {
            InitializeComponent();
            this.OTP = OTP;
         
        }

        private void CONFIRM_Clicked(object sender, EventArgs e)
        {
            bool isOTPCorrect = VerifyOTP(OTP);
        }

        
        //private void RESENDCODE_Clicked(object sender, EventArgs e)
        //{
        //    SignInPage sp = new SignInPage();
        //    string resentOTP = sp.GetOTP();
        //    bool isResentOTPCorrect = VerifyOTP(resentOTP);

        //}

        private bool VerifyOTP(string OTP)
        {
            if (OTP == verificationCodeEntry.Text)
            {
                DisplayAlert("Success", "Entered OTP is correct", "OK");
                return true;
            }

            else
            {
                DisplayAlert("Fail", "Incorrect OTP", "OK");
                return false;
            }
        }

    }
}