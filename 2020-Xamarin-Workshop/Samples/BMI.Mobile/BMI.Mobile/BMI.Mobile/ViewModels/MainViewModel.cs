using BMI.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BMI.Mobile.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //VARIABLE: WEIGHT
        private double _weight;

        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        //VARIABLE: HEIGHT
        private double _height;

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        //VARIABLE: NAME
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        //VARIABLE: DISPLAYS MESSAGE(BMI)
        private string _message;

        public string Message
        {
            get { return _message; }

            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        //VARIABLE: LIST OF HUMANS (SEE MODELS FOLDER)
        public ObservableCollection<Human> ListHumans { get; set; }

        //COMMANDS
        public ICommand CalculateBMI
        {
            get { return new Command(Run); }
        }

        //COMMAND FUNCTIONS (IF ANY)
        public void Run()
        {
            double BMI = Weight / (Height * Height);

            //TOSTRING() CONVERTS THE DOUBLE TO STRING TYPE
            //THE ARG FORMATS THE VALUE TO 2 DECIMAL PLACES
            Message = Name + "'S BMI: " + BMI.ToString("0.00");

            //CREATING AN OBJECT OF HUMAN
            //SAVING IT TO A COLLECTION FOR VIEWING LATER
            ListHumans.Add(new Human(Name, Weight, Height));

            //RESETTING ALL FIELDS
            Name = null;
            Weight = 0;
            Height = 0;
        }

        public MainViewModel()
        {
            //INITIALIZING UI AND VARIABLES
            Message = "HELLO XAMARIN FROM MVVM!";
            ListHumans = new ObservableCollection<Human>();
        }

        //THIS ACTS AS A LISTENER WAITING FOR SOMETHING TO CHANGE VALUES
        public event PropertyChangedEventHandler PropertyChanged;

        //WHEN A VALUE CHANGES THE PROPERTYCHANGED VARIABLE
        //IS INITIALIZED WITH THE PARAMETER THAT CHANGED
        //IT BECOMES THE RESPONSIBILITY OF THE VARIABLE TO
        //ENSURE THE UI IS UPDATED IF NEEDED.
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
