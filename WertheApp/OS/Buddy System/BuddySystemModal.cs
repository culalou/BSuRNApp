﻿/********************CLASS FOR START PROCESS POP UP****************************/
using System;
using System.Text.RegularExpressions; //Regex.IsMatch
using Xamarin.Forms;

namespace WertheApp.OS
{
    public class BuddySystemModal : ContentPage
    {
        //VARIABLES
        Picker p_ProcessNames;
        Entry e_ProcessSize;
        int minimumProcessSize;

        //CONSTRUCTOR
        public BuddySystemModal()
        {
            //Title = "";
            CreateContent();
        }


        //METHODS

        /**********************************************************************
        *********************************************************************/
        void CreateContent()
        {
            var scrollView = new ScrollView
            {
                Margin = new Thickness(10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            var stackLayout = new StackLayout();
            var stackLayout2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            var l_Available = new Label { Text = "Available process names:" , FontSize = App._textFontSize};
            p_ProcessNames = new Picker() { FontSize = App._textFontSize };
            p_ProcessNames.ItemsSource = BuddySystem.availableProcesses;
            p_ProcessNames.SelectedIndex = 0;
            var l_Space = new Label { Text = "  " };
            var l_ProcessSize = new Label { Text = "Process size:" , FontSize = App._textFontSize };
            e_ProcessSize = new Entry{ Keyboard = Keyboard.Numeric, FontSize = App._textFontSize };
            switch (BuddySystem.powerOfTwo)
            {
                case 1:
                    minimumProcessSize = 2;
                    break;
                case 2:
                    minimumProcessSize = 2;
                    break;
                case 3:
                    minimumProcessSize = 2;
                    break;
                case 4:
                    minimumProcessSize = 2;
                    break;
                case 5:
                    minimumProcessSize = 2;
                    break;
                case 6:
                    minimumProcessSize = 3;
                    break;
                case 7:
                    minimumProcessSize = 5;
                    break;
                case 8:
                    minimumProcessSize = 10;
                    break;
                case 9:
                    minimumProcessSize = 20;
                    break;
                case 10:
                    minimumProcessSize = 40;
                    break;
                case 11:
                    minimumProcessSize = 80;
                    break;
                case 12:
                    minimumProcessSize = 200;
                    break;
                case 13:
                    minimumProcessSize = 300;
                    break;
                case 14:
                    minimumProcessSize = 600;
                    break;
                case 15:
                    minimumProcessSize = 1100;
                    break;
                default:
                    minimumProcessSize = 2;
                    break;
            }
            var l_Min = new Label
            {
                FontSize = App._smallTextFontSize,
                Text = "Minimum process size: " + minimumProcessSize
            };

            var b_Start = new Button { Text = "Start process",
                BackgroundColor = App._buttonBackground,
                TextColor = App._buttonText,
                CornerRadius = App._buttonCornerRadius,
                FontSize = App._buttonFontSize

            };
            b_Start.Clicked += B_Start_Clicked;
            var l_Space2 = new Label { Text = "  " };
            var b_Cancel = new Button { Text = "Cancel",
                BackgroundColor = App._buttonBackground,
                TextColor = App._buttonText,
                CornerRadius = App._buttonCornerRadius,
                FontSize = App._buttonFontSize

            };
            b_Cancel.Clicked += B_Cancel_Clicked;

            stackLayout2.Children.Add(b_Start);
            stackLayout2.Children.Add(l_Space2);
            stackLayout2.Children.Add(b_Cancel);



            stackLayout.Children.Add(l_Available);
            stackLayout.Children.Add(p_ProcessNames);
            stackLayout.Children.Add(l_Space);
            stackLayout.Children.Add(l_ProcessSize);
            stackLayout.Children.Add(e_ProcessSize);
            stackLayout.Children.Add(l_Min);
            stackLayout.Children.Add(stackLayout2);

            this.Content = scrollView;
            scrollView.Content = stackLayout; //Wrap ScrollView around StackLayout to be able to scroll the content
        }

        /**********************************************************************
        *********************************************************************/
        //validates the string in e_ProcessSize. For example a request with size <2 is not allowed.
        //returns true if string is valid 
        bool ValidateMemoryRequestInput()
        {
            String s = e_ProcessSize.Text;
            if (BuddySystem.powerOfTwo < 8)
            {
                return Regex.IsMatch(s, "^(?:[" + minimumProcessSize + "-9]|[1-9]+[0-9]+)$"); //matches only numbers >= ...;
            }
            else
            {
                switch (BuddySystem.powerOfTwo)
                {
                    case 8:
                        return Regex.IsMatch(s, "^(?:[1-9]+[0-9]+)$"); //matches only numbers >= 10;
                    case 9:
                        return Regex.IsMatch(s, "^(?:[2-9]+[0-9]|[1-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 20;
                    case 10:
                        return Regex.IsMatch(s, "^(?:[4-9]+[0-9]|[1-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 40;
                    case 11:
                        return Regex.IsMatch(s, "^(?:[8-9]+[0-9]|[1-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 80;
                    case 12:
                        return Regex.IsMatch(s, "^(?:[2-9]+[0-9]+[0-9]|[1-9]+[0-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 200;
                    case 13:
                        return Regex.IsMatch(s, "^(?:[3-9]+[0-9]+[0-9]|[1-9]+[0-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 300;
                    case 14:
                        return Regex.IsMatch(s, "^(?:[6-9]+[0-9]+[0-9]|[1-9]+[0-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 600;
                    case 15:
                        return Regex.IsMatch(s, "^(?:[1-9]+[1-9]+[0-9]+[0-9]|[2-9]+[0-9]+[0-9]+[0-9]|[1-9]+[0-9]+[0-9]+[0-9]+[0-9]+)$"); //matches only numbers >= 1100;
                }

                return false;
            }
        }
            /**********************************************************************
            *********************************************************************/
            async void B_Start_Clicked(object sender, EventArgs e)
            {
                BuddySystem.endProcess = false;
                //check if there was a vailid input
                if (e_ProcessSize.Text != null && ValidateMemoryRequestInput())
                {
                    
                    bool bfound = BuddySystem.AllocateBlock(Int32.Parse(e_ProcessSize.Text), p_ProcessNames.SelectedItem.ToString());

                    //if fitting blocksize was found
                    if (bfound)
                    {
                   
                        BuddySystem.availableProcesses.Remove(p_ProcessNames.SelectedItem.ToString());
                        BuddySystem.activeProcesses.Add(p_ProcessNames.SelectedItem.ToString());
                        await Navigation.PopModalAsync(); // close Modal
                    }
                    else
                    {
                    
                        await DisplayAlert("Alert", "Process doesn't fit in memory", "OK");
                        await Navigation.PopModalAsync(); // close Modal
                    }

                }
                else if (e_ProcessSize.Text == null) { await DisplayAlert("Alert", "Please enter a process size", "OK"); }
                else if (!ValidateMemoryRequestInput()) { await DisplayAlert("Alert", "Please enter a valid process size (only integers >= " + minimumProcessSize + ")", "OK"); }
            }

        /**********************************************************************
        *********************************************************************/
        async void B_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(); // close Modal
        }
        }
    }
// return Regex.IsMatch(s, "^(?:[2-9]|[1-9]+[0-9]+)$"); //matches only numbers >= 2;