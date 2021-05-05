﻿using System.Collections.Generic;
using WertheApp.CN;
using Xamarin.Forms;

namespace WertheApp
{
    public class ComputerNetworks : ContentPage
    {
        //CONSTRUCTOR
        public ComputerNetworks()
        {
            Title = "Computer Networks";
            var scrollView = new ScrollView
            {
                Margin = new Thickness(10)
            };
            var stackLayout = new StackLayout();
            this.Content = scrollView;
            scrollView.Content = stackLayout; //Wrap ScrollView around StackLayout to be able to scroll the content
            CreateContent(stackLayout);

        }

        //METHODS
        void CreateContent(StackLayout stackLayout)
		{
            var l_choose = new Label 
            { 
                Text="Choose an App",
                FontSize = App._labelFontSize

            };
            stackLayout.Children.Add(l_choose);

			var l_space = new Label();
			stackLayout.Children.Add(l_space);

            //add buttons for apps
            List<string> appNameList = new List<string>()    {
                        "Ack Generation",
                        "Congestion Control",
                        "Dijkstra",
                        "Pipeline Protocols",
                        "Reno Fast Recovery"
                    };
            foreach(string appName in appNameList)
            {
                var b_button = new Button
                {
                    Text = appName,
                    BackgroundColor = App._buttonBackground,
                    TextColor = App._buttonText,
                    CornerRadius = App._buttonCornerRadius,
                    FontSize = App._buttonFontSize
                };
                b_button.Clicked += Button_Clicked;
                stackLayout.Children.Add(b_button);
            }

        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            var button = (sender as Button);
            string appName = button.Text;

                switch (appName)
            {
                case "Congestion Control":
                    await Navigation.PushAsync(new CongestionControlSettings());
                    break;
                case "Pipeline Protocols":
                    await Navigation.PushAsync(new PipelineProtocolsSettings());
                    break;
                case "Reno Fast Recovery":
                    await Navigation.PushAsync(new RenoFastRecovery());
                    break;
                case "Ack Generation":
                    await Navigation.PushAsync(new AckGeneration());
                    break;
                case "Dijkstra":
                    DijkstraSettings.ClearNetworkList();
                    await Navigation.PushAsync(new DijkstraSettings());
                    break;
            }
        }

  


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send<object>(this, "Unspecified");
        }
    }
}

/*
 
            //creates a list with all Apps of the course
			var listView = new ListView();
            listView.BackgroundColor = Color.Transparent;
            listView.ItemsSource = new string[]
			{
                //Reno Fast Retransmit/Recovery
				"Congestion Control", "Pipeline Protocols", "Reno Fast Recovery", "Ack Generation", "Dijkstra"
            };
            //after an item was clicked, open the respective app 
			listView.ItemTapped += async (sender, e) =>
            {
                var appName = e.Item.ToString ();

                switch(appName)
                {
                    case "Congestion Control":
                        await Navigation.PushAsync(new CongestionControlSettings());
                        break;
                    case "Pipeline Protocols":
                        await Navigation.PushAsync(new PipelineProtocolsSettings());
                        break;
                    case "Reno Fast Recovery":                        
                        await Navigation.PushAsync(new RenoFastRecovery());
                        break;
                    case "Ack Generation":
                        await Navigation.PushAsync(new AckGeneration());
                        break;
                    case "Dijkstra":
                        DijkstraSettings.ClearNetworkList();
                        await Navigation.PushAsync(new DijkstraSettings());
                        break;
                }

            };

			stackLayout.Children.Add(listView);
 */