using Sample.Mobile.Models;
using Sample.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        readonly FirebaseService _service = new FirebaseService();

        public MainPage()
        {
            InitializeComponent();
            Task.Run(async () => await ReloadPeopleList());
        }

        private async void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            ToggleIndicator();
            if (!FormValidationSuccess())
            {
                await DisplayAlert("Error", "Name and person are required fields", "OK");
                return;
            }

            var person = await _service.GetPerson(TxtName.Text);

            if (person != null)
            {
                await DisplayAlert("Error", "A person with that name already exist", "OK");
                return;
            }

            await _service.AddPerson(TxtName.Text, TxtPhone.Text);

            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;

            await DisplayAlert("Success", "Person Added Successfully", "OK");

            await ReloadPeopleList();
            ToggleIndicator();
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            ToggleIndicator();
            if (SelectedPerson == null)
            {
                await DisplayAlert("Error", "A person must be selected to proceed", "OK");
                return;
            }

            await _service.DeletePerson(SelectedPerson.PersonId);

            await DisplayAlert("Success", "Person Deleted Successfully", "OK");

            await ReloadPeopleList();
            ToggleIndicator();
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            ToggleIndicator();
            if (SelectedPerson == null)
            {
                await DisplayAlert("Error", "A person must be selected to proceed", "OK");
                return;
            }

            if (!FormValidationSuccess())
            {
                await DisplayAlert("Error", "Name and person are required fields", "OK");
                return;
            }

            var person = await _service.GetPerson(TxtName.Text);

            if (person != null && person.PersonId != SelectedPerson.PersonId)
            {
                await DisplayAlert("Error", "A person with that name already exist", "OK");
                return;
            }

            await _service.UpdatePerson(SelectedPerson.PersonId, TxtName.Text, TxtPhone.Text);

            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;

            await DisplayAlert("Success", "Person Updated Successfully", "OK");

            await ReloadPeopleList();
            ToggleIndicator();
        }

        private async void ListPeople_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var person = await _service.GetPerson(SelectedPerson.PersonId);

            TxtName.Text = person.Name;
            TxtPhone.Text = person.Phone;
        }

        private Person SelectedPerson => (Person)ListPeople.SelectedItem;

        private void ToggleIndicator() => StackIndicator.IsVisible = !StackIndicator.IsVisible;

        private async Task ReloadPeopleList()
        {
            var allPersons = await _service.GetAllPersons();
            ListPeople.ItemsSource = allPersons;
        }

        /// <summary>
        /// Confirms whether entries are valid
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        private bool FormValidationSuccess()
        {
            return
                !string.IsNullOrWhiteSpace(TxtName.Text) &&
                !string.IsNullOrWhiteSpace(TxtPhone.Text);
        }
    }
}
