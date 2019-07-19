using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Models;
using DouApp.Interfaces;
using System.Threading;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressPage : ContentPage
    {
        public UserRecipe ConvertedRecipe { get; set; }
        public UserRecipe CommandRecipe { get; set; }

        public ProgressPage()
        {
            InitializeComponent();
        }

        public async void LetsDoh(ContentPage page)
        {
            // The amount the progress bar advances each time
            // (It should b 1/ 8 but Container3 is missing from the machine).
            double progressChunk = 1.0 / 7.0;
            uint progressTime = 250;

            // Create command string and send it via bluetooth
            string command = CreateCommandString();

            // Send command via bluetooth
            if (DependencyService.Get<IBluetoothHelper>().IsConnected())
                DependencyService.Get<IBluetoothHelper>().WriteStringToDevice(command);
            else
            {
                await page.DisplayAlert("Error!", "Bluetooth device is not connected! Going back to recipe page", "Ok");
                await Navigation.PopAsync();
            }

            // This loop will run untill the command is finished, bowl is removed or user sends stop
            while (true)
            {
                // Read string from bluetooth (with a 60 seconds time limit)
                string received = await DependencyService.Get<IBluetoothHelper>().ReadStringFromDevice(60);

                // Check recieved commands
                if (received.Contains("bowl"))
                {
                    // Show Yes/No message.
                    bool toContinue = await page.DisplayAlert("Continue?", "Bowl was removed from scale! Please place it on the scale and press Yes to continue, or press No to stop.", "Yes", "No");
                    if (toContinue)
                    {
                        // If user chooses yes, send 'y' through bluetooth and continue
                        DependencyService.Get<IBluetoothHelper>().WriteStringToDevice("y");
                        // Wait 10 seconds
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        // else, send 'n' through bluettoth and return false
                        DependencyService.Get<IBluetoothHelper>().WriteStringToDevice("n");
                        // Wait 10 seconds
                        Thread.Sleep(10000);
                    }
                }
                else if (received.StartsWith("1"))
                {
                    // Remove given amount from the appropriate container
                    UpdateLargeContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("2"))
                {
                    // Remove given amount from the appropriate container
                    UpdateLargeContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("3"))
                {
                    // Remove given amount from the appropriate container
                    UpdateLargeContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("4"))
                {
                    // Remove given amount from the appropriate container
                    UpdateSmallOrLiquidContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("5"))
                {
                    // Remove given amount from the appropriate container
                    UpdateSmallOrLiquidContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("6"))
                {
                    // Remove given amount from the appropriate container
                    UpdateSmallOrLiquidContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("7"))
                {
                    // Remove given amount from the appropriate container
                    UpdateSmallOrLiquidContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.StartsWith("8"))
                {
                    // Remove given amount from the appropriate container
                    UpdateSmallOrLiquidContainer(received);

                    await UpdateProgressBar(progressChunk, progressTime, Easing.Linear);
                }
                else if (received.Contains("done"))
                {
                    // Save the amounts in the containers
                    App.Containers.SaveContainers();

                    // Show a message and return to recipe page
                    await page.DisplayAlert("Done!", "Dough making is complete! please remove the bowl and make delicious baked goods", "Ok");
                    await Navigation.PopAsync();
                }
                else if (received.Contains("stop"))
                {
                    // Save the amounts in the containers
                    App.Containers.SaveContainers();
                    
                    // Show a message and return to recipe page
                    await page.DisplayAlert("Stopped!", "Dough making wasn't completed! Going back to recipe page", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    // Nothing was read, continue to next iteration
                    continue;
                }
            }
        }

        private string CreateCommandString()
        {
            string command = "";

            // Division by 0.25 is done to containers that release 0.25 cup/spoon each time
            // order of containers on the machine is 4 -> 1 -> 5 -> 2 -> 6 -> 7 -> 8
            command += "f4$" + ((int)(CommandRecipe.Amount4 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f1$" + ((int)(CommandRecipe.Amount1)).ToString().PadLeft(3, '0') + ";";
            command += "f5$" + ((int)(CommandRecipe.Amount5 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f2$" + ((int)(CommandRecipe.Amount2)).ToString().PadLeft(3, '0') + ";";
            command += "f6$" + ((int)(CommandRecipe.Amount6 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f7$" + ((int)(CommandRecipe.Amount7 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f8$" + ((int)(CommandRecipe.Amount8 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            //command += "f3$" + ((int)(commandRecipe.Amount3)).ToString().PadLeft(3, '0') + ";";
            command += "b;^";

            return command;
        }

        private void UpdateLargeContainer(string received)
        {
            int container = int.Parse(received.Substring(0, 1));

            int startIndex = 2;
            int i = startIndex;
            while (received[i] != ';')
                i++;
            int length = (i - 1) - startIndex + 1;

            decimal weight = decimal.Parse(received.Substring(startIndex, length));
            App.Containers.RemoveFromContainer(container, weight);
        }

        private void UpdateSmallOrLiquidContainer(string received)
        {
            int container = int.Parse(received.Substring(0, 1));
            App.Containers.RemoveFromContainer(container, ConvertedRecipe.Amount4);
        }

        private async Task<bool> UpdateProgressBar(double progressChunk, uint time, Easing easing)
        {
            double currentProgress = progressBar.Progress;
            bool result = await progressBar.ProgressTo(currentProgress + progressChunk, time, easing);
            return result;
        }
    }
}