using SpaceEngineersRolodex.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceEngineersRolodex
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ObservableCollection<Coordinates> Collection { get; } = new ObservableCollection<Coordinates>();

        private Vector3 earthCoordinates;
        private float earthMinimum;
        private float earthMaximum;
        private Vector3 marsCoordinates;
        private float marsMinimum;
        private float marsMaximum;
        private Vector3 alienCoordinates;
        private float alienMinimum;
        private float alienMaximum;
        private Vector3 moonCoordinates;
        private float moonMinimum;
        private float moonMaximum;
        private Vector3 europaCoordinates;
        private float europaMinimum;
        private float europaMaximum;
        private Vector3 titanCoordinates;
        private float titanMinimum;
        private float titanMaximum;

        public MainPage()
        {

            this.InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var inputString = searchTextBox.Text;

            Vector3 toSearch = Vector3.Zero;

            if (inputString.ToLower() == "earth".ToLower())
            {

            } else if (inputString.ToLower() == "moon".ToLower())
            {

            }else if (inputString.ToLower() == "mars".ToLower())
            {

            }else if (inputString.ToLower() == "europa".ToLower())
            {

            }
            else if (inputString.ToLower() == "alien".ToLower())
            {

            }
            else if (inputString.ToLower() == "titan".ToLower())
            {

            }
            else
            {
                var validated = ValidateInputAsCoordinate(inputString);
                
                if (inputString.Split(':').Length > 4)
                {
                    var parts = inputString.Split(':');

                    toSearch = new Vector3(float.Parse(parts[2]), float.Parse(parts[3]), float.Parse(parts[4]));

                }
                else
                {
                    return;
                }

            }

            foreach (var place in Collection)
            {
                var against = new Vector3(place.X, place.Y, place.Z);
                

                var distance = Vector3.Distance(against, toSearch);

            }



        }

        private void InputNewButton_Click(object sender, RoutedEventArgs e)
        {
            var inputString = inputNewTextBox.Text;

            var validated = ValidateInputAsCoordinate(inputString);

            if (validated)
            {
                var newCoordinates = ParseInputs(inputString);

                if (newCoordinates == null)
                {
                    return;
                }
                else
                {
                    Collection.Add(newCoordinates);
                    outputTextArea.Text = outputTextArea.Text += System.Environment.NewLine
                                                                 + "Added new Coordinate: " + newCoordinates.Name + ":"
                                                                 + newCoordinates.X + "," + newCoordinates.Y + "," + newCoordinates.Z;
                }
            }
        }

        public void appendLineToConsole(string newLine)
        {
            outputTextArea.Text = outputTextArea.Text += System.Environment.NewLine
                                                         + newLine;
        }

        private Coordinates ParseInputs(string input)
        {
            
            var inputArray = input.Split(':');
            var name = inputArray[1];
            var xCoord = float.Parse(inputArray[2]);
            var yCoord = float.Parse(inputArray[3]);
            var zCoord = float.Parse(inputArray[4]);



            if (name.Contains(" - Station"))
            {
                var split = name.Split(' ');
                var newcoordinates = new Coordinates(split[0], split[0], xCoord, yCoord, zCoord);
                return newcoordinates;


            }
            else
            {
                var newcoordinates = new Coordinates(name, xCoord, yCoord, zCoord);
                return newcoordinates;
            }

        }

        private bool ValidateInputAsCoordinate(string input)
        {
            var inputArray = input.Split(':');

            if (inputArray.Length != 6)
            {
                displayInputError(inputErrorTypes.wrongLength);
                return false;
            }

            if (inputArray[0] != "GPS")
            {
                displayInputError(inputErrorTypes.notCoord);
                return false;
            }

            return true;
        }

        private void displayInputError(inputErrorTypes type)
        {
            switch (type)
            {
                case inputErrorTypes.noParse:
                    outputTextArea.Text = "The input coordinate string " +
                                          "has errors and could not be catalogged ---- Could not be parsed!";
                    break;
                case inputErrorTypes.notCoord:
                    outputTextArea.Text = "The input coordinate string has " +
                                          "errors and could not be catalogged ---- Not coordinate!";
                    break;
                case inputErrorTypes.wrongLength:
                    outputTextArea.Text = "The input coordinate string has" +
                                          " errors and could not be catalogged ---- WRONG LENGTH!";
                    break;
                case inputErrorTypes.notStation:
                    outputTextArea.Text = "The input coordinate string has " +
                                          "errors and could not be catalogged ---- No station tag!";
                    break;
            }
        }

        public enum inputErrorTypes
        {
            wrongLength, notCoord, noParse, notStation
        }

        private async void LoadRolodexButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".csv");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                ulong size = stream.Size;
                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                    {
                        uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                        string text = dataReader.ReadString(numBytesLoaded);
                        var lines = text.Split('\n');

                        foreach (var line in lines)
                        {
                            var components = line.Split(',');
                            if (components.Length == 6)
                            {
                                var newCoord = new Coordinates(components[0], components[1], components[2], float.Parse(components[3]), float.Parse(components[4]), float.Parse(components[5]));
                                Collection.Add(newCoord);
                            }
                        }
                    }
                }

                



                appendLineToConsole("Loaded File: " + file.Name);
            }
            else
            {
                appendLineToConsole("Operation Cancelled");
            }
        }

        private async void SaveRolodexButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".csv");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                    {
                        StringBuilder stringy = new StringBuilder();
                        foreach (var coord in Collection)
                        {
                            stringy.Append(coord.Name + "," + coord.FactionTag + "," + coord.Description + "," + coord.X + "," + coord.Y + "," +
                                           coord.Z + Environment.NewLine);
                        }
                        dataWriter.WriteString(stringy.ToString());
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                    }
                }
                stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.
                appendLineToConsole("Saved file to: " + file.Name);
            }
            else
            {
                appendLineToConsole("Operation Cancelled");
            }
        }
        
    }
}
