﻿using System;
using Xamarin.Forms;


namespace WertheApp
{
    public partial class WertheAppPage : ContentPage
    {
		
        //CONSTRUCTOR
        public WertheAppPage()
        {
            InitializeComponent();

			ToolbarItem info = new ToolbarItem();
			info.Text = "Info";
			this.ToolbarItems.Add(info);
			info.Clicked += B_Info_Clicked;

			Title = "WertheApp";
			//Title = "Start Screen"

			// This is the top-level grid, which will split our page in half
			var grid = new Grid();
			var scrollView = new ScrollView();
			scrollView.Content = grid; //Wrap ScrollView around StackLayout to be able to scroll the content
			this.Content = scrollView;
			grid.RowDefinitions = new RowDefinitionCollection {
            // Bottom half will be twice as big as top half:
            new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
			new RowDefinition{ Height = new GridLength(2, GridUnitType.Star)},
            };
			CreateTopHalf(grid);
			CreateBottomHalf(grid);
        }


		//VARIABLES
		private double width = 0;
		private double height = 0;
        Image i_hsLogo = new Image { Aspect = Aspect.AspectFit, Margin = new Thickness(10)};


		//METHODS
		//this method is called everytime the device is rotated
		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height); //must be called
			if (this.width != width || this.height != height)
			{
				this.width = width;
				this.height = height;

				//reconfigure layout
				if (width < height)
				{
					i_hsLogo.Source = ImageSource.FromResource("AalenHS2.png");
				}
				else
				{
					i_hsLogo.Source = ImageSource.FromResource("AalenHS1.png");
				}
			}
		}

		void CreateTopHalf(Grid grid)
		{
            //add content to Toplevel grid
            grid.Children.Add(i_hsLogo, 0, 0);
		}
		void CreateBottomHalf(Grid grid)
		{
            //organize content in Stacklayout
            var stackLayout = new StackLayout
            {
                Margin = new Thickness(20)
			};
            var l_pick = new Label 
            { 
                Text = "Pick your course"
            };
            stackLayout.Children.Add(l_pick);

            var l_space = new Label();
			stackLayout.Children.Add(l_space);

			var b_bs = new Button
			{
				Text = "Operating Systems",
				BackgroundColor = App._buttonBackground,
				TextColor = App._buttonText,
				CornerRadius = App._buttonCornerRadius
			};
            b_bs.Clicked += B_Bs_Clicked;

            stackLayout.Children.Add(b_bs);

			var b_rn = new Button
			{
				Text = "Computer Networks",
				BackgroundColor = App._buttonBackground,
				TextColor = App._buttonText,
				CornerRadius = App._buttonCornerRadius
			};
            b_rn.Clicked += B_Rn_Clicked;
			stackLayout.Children.Add(b_rn);

            //add content to Toplevel grid
			grid.Children.Add(stackLayout, 0, 1);
		}

		async void B_Info_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Info());
		}

		async void B_Bs_Clicked(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new OperatingSystems());
		}

		async void B_Rn_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ComputerNetworks());
		}
    }
}