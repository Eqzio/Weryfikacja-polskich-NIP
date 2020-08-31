using PUM.Models;
using PUM.Utilities;
using PUM.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUM.ViewModels
{
    public class MainPageViewModel : NotificationObject
    {
        VerificationDetails verificationDetails { get; set; }

        public List<VerificationDetails.VerifiedNIP> VerifiedNIPs { get; } = new List<VerificationDetails.VerifiedNIP>();

        private readonly INavigation _navigation;

        public MainPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            verificationDetails = new VerificationDetails();
            VerifyNipCommand = new Command(execute: () => VerifyNip());
        }


        private string _nip;
        public string Nip
        {
            get { return _nip; }
            set
            {
                if (_nip == value) return;
                _nip = value;
                if (_nip.Length == 10)
                {
                    IsNipNotValid = false;
                }
                else
                {
                    IsNipNotValid = true;
                }
                RaisePropertyChanged(() => Nip);
            }
        }

        private bool _isNipNotValid = true;
        public bool IsNipNotValid
        {
            get { return _isNipNotValid; }
            set
            {
                if (_isNipNotValid == value) return;
                _isNipNotValid = value;
                RaisePropertyChanged(() => IsNipNotValid);
            }
        }

        public ICommand VerifyNipCommand { get; private set; }

        public async void VerifyNip()
        {
            VerificationDetails.Rootobject ro = await verificationDetails.VerifyNIP(Nip);

            VerificationDetails.VerifiedNIP item = new VerificationDetails.VerifiedNIP
            {
                name = ro.result.subject.name,
                nip = ro.result.subject.nip,
                statusVat = ro.result.subject.statusVat,
                regon = ro.result.subject.regon,
                residenceAddress = ro.result.subject.residenceAddress,
                accountNumbers = ArrayToString(ro.result.subject.accountNumbers),
                registrationLegalDate = ro.result.subject.registrationLegalDate,
                requestDateTime = ro.result.requestDateTime,
                requestId = ro.result.requestId
            };

            App.Database.SaveVerifiedNIPAsync(item);
            await _navigation.PushAsync(new DetailsPage
            {
                BindingContext = item
            });
        }



        private string ArrayToString(string[] array)
        {
            string temp = "";

            for(int i=0; i < array.Length; i++)
            {
                temp += array[i];
                if (i < array.Length - 1)
                {
                    temp += ",\n";
                }
            }

            return temp;
        }

        private string[] StringToArray(string value)
        {
            string[] array = value.Split(',');
            return array;
        }

    }
}
