﻿using System;
using CocosSharp;
using Xamarin.Forms;
using System.Collections.Generic; 
using System.Linq; //fragmentList.ElementAt(i);
using System.Diagnostics;

namespace WertheApp.OS
{
    //This class splits the Content in 2 halfs. 
    //Top half is the animated scene, which is actually defined in AllocationStrategiesScene
    //Bottom half are buttons and labels that interact with the scene
    public class OldAllocationStrategies : ContentPage
    {
        //VARIABLES
        public static List<int> fragmentList { get; set; } //List of fragments. From Settings Page passed to the constructor and later accesed from Scene
        public static String strategy { get; set; } //Choosen strategy (Firts Fit, ...) from Settings Page passed to the constructor and later accesed from Scene
        public static Button b_Restart;

        public static int memoryRequest; //gets its value from the modal page

		private double width = 0;
		private double height = 0;

		public const int gameviewWidth = 330;
		public const int gameviewHeight = 100;

        bool isContentCreated; //indicates weather the Content of the page was already created

        public enum MyEnum
        {
            newRequest = 0, //new request was entered after clicking th next button
            searchingForBlock = 1, // currently searching for a memory block which is big enough 
            successfull = 2, //free memory found 
            unsuccessfull = 3, //no memory block found which is big enough to fit in the request
            noRequestYet = 4,   //the application just started an no memory has been requested yet
            memoryFull = 5 //no free space at all. Every block is full
        }
        public static MyEnum memoryRequestState;

        //Labels. Values change
        public static Label l_Size;
        public static Label l_Free;
        public static Label l_Diff;
        public static Label l_Best;

        //values for labels
        public static String size = "-";
        public static String free = "-";
        public static String diff = "-";
        public static String best = "-";


        //CONSTRUCTOR
        public OldAllocationStrategies(List<int> l, String s)
        {
            ToolbarItem info = new ToolbarItem();
            info.Text = App._sHelpInfoHint;
            this.ToolbarItems.Add(info);
            info.Clicked += B_Info_Clicked;

            fragmentList = l;
            strategy = s;
            memoryRequestState = MyEnum.noRequestYet;

            Title = "Allocation Strategies: " + strategy;

            isContentCreated = false;

            if (Application.Current.MainPage.Width > Application.Current.MainPage.Height)
            {
                this.isContentCreated = true;
                CreateContent();
                this.Content.IsVisible = true;
            }
            else
            {
                CreateContent();
                this.isContentCreated = true;
                this.Content.IsVisible = false;
            }

            //EVENT LISTENER
			//subscribe to Message in order to know if a new memory request was made
            MessagingCenter.Subscribe<OldAllocationStrategiesModal>(this, "new memory request", (args) =>{
                l_Size.Text = memoryRequest.ToString();
                l_Diff.Text = diff;
                l_Free.Text = free;
				if (OldAllocationStrategiesScene.CheckIfFull())
				{
					memoryRequestState = MyEnum.memoryFull;
                }
                else
                {
                    memoryRequestState = MyEnum.newRequest;
                }


            });
        }

		//METHODS
        /**********************************************************************
        *********************************************************************/
        //Gets called everytime the Page is not shown anymore. For example when clicking the back navigation
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        /**********************************************************************
        *********************************************************************/
		void CreateContent()
		{  
            // This is the top-level grid, which will split our page in half
			var grid = new Grid();
			this.Content = grid;
			grid.RowDefinitions = new RowDefinitionCollection {
                    // Each half will be the same size:
                    new RowDefinition{ Height = new GridLength(4, GridUnitType.Star)},
					new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)}
				};
			CreateTopHalf(grid);
			CreateBottomHalf(grid);
		}

        /**********************************************************************
        *********************************************************************/
        //sets up the scene 
		void HandleViewCreated(object sender, EventArgs e)
		{
			OldAllocationStrategiesScene gameScene;

			var gameView = sender as CCGameView;
			if (gameView != null)
			{
				// This sets the game "world" resolution to 330x100:
                //Attention: all drawn elements in the scene strongly depend ont he resolution! Better don't change it
                gameView.DesignResolution = new CCSizeI(gameviewWidth, gameviewHeight);
               
				// GameScene is the root of the CocosSharp rendering hierarchy:
				gameScene = new OldAllocationStrategiesScene(gameView);

				// Starts CocosSharp:
				gameView.RunWithScene(gameScene);

			}
		}

        /**********************************************************************
        *********************************************************************/
		void CreateTopHalf(Grid grid)
		{
            var gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White, /*Android bug: Doesn't work for Android*/
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreated
			};

            gameView.ResolutionPolicy = CocosSharpView.ViewResolutionPolicy.ExactFit; //gameview fits the screen
            grid.Children.Add(gameView, 0, 0);
		}

        /**********************************************************************
        *********************************************************************/
		void CreateBottomHalf(Grid grid)
		{

            //Using a Stacklayout to organize elements
            //with corresponding labels and String variables. 
            //For example l_Size, size
            var stackLayout = new StackLayout{
                Orientation = StackOrientation.Horizontal,
				Margin = new Thickness(10),

            };

            b_Restart = new Button
            {
                Text = "Restart",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = App._buttonBackground,
                TextColor = App._buttonText,
                CornerRadius = App._buttonCornerRadius
            };
            b_Restart.Clicked += B_Restart_Clicked;
            b_Restart.IsEnabled = false;
            stackLayout.Children.Add(b_Restart);

            var l_1 = new Label { Text = "Size:",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			l_Size = new Label { Text = size,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			stackLayout.Children.Add(l_1);
            stackLayout.Children.Add(l_Size);

            var l_2 = new Label { Text = "Free:",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            l_Free = new Label { Text = free,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			stackLayout.Children.Add(l_2);
            stackLayout.Children.Add(l_Free);

            var l_3 = new Label { Text = "Diff.:",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            l_Diff = new Label { Text = diff,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            stackLayout.Children.Add(l_3);
            stackLayout.Children.Add(l_Diff);

            var l_4 = new Label { Text = "Best:",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            l_Best = new Label { Text = best,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			stackLayout.Children.Add(l_4);
            stackLayout.Children.Add(l_Best);

			var b_Next = new Button{ Text = "Next",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = App._buttonBackground,
                TextColor = App._buttonText,
                CornerRadius = App._buttonCornerRadius
            };
            b_Next.Clicked += B_Next_Clicked;
            stackLayout.Children.Add(b_Next);

			grid.Children.Add(stackLayout, 0, 1);
		}

        /**********************************************************************
        *********************************************************************/
        void B_Restart_Clicked(object sender, EventArgs e)
        {

            memoryRequestState = MyEnum.noRequestYet;
            CreateContent();
            b_Restart.IsEnabled = false;
            OldAllocationStrategiesScene.indexCFLastAllocated = 0;
            OldAllocationStrategiesScene.pos = 0;
            OldAllocationStrategiesScene.suc = 0;  //the very first request...
            OldAllocationStrategiesScene.indexCFLastAllocated = 0;
            OldAllocationStrategiesScene.besValue = 0;
            OldAllocationStrategiesScene.besPos = -1;


        }
        /**********************************************************************
        *********************************************************************/
        async void B_Next_Clicked(object sender, EventArgs e)
        {
  
            switch (memoryRequestState)
			{
                case MyEnum.newRequest: //new request
                    memoryRequestState = MyEnum.searchingForBlock;
                    OldAllocationStrategiesScene.RequestNew(memoryRequest);
					break;
                case MyEnum.searchingForBlock: //searching for block
					switch (strategy)
					{
						case "First Fit":
							OldAllocationStrategiesScene.pos++;
							OldAllocationStrategiesScene.FirstFit(memoryRequest);
							break;
						case "Next Fit":
                            OldAllocationStrategiesScene.pos++;
                            OldAllocationStrategiesScene.NextFit(memoryRequest);
							break;
						case "Best Fit":
                            OldAllocationStrategiesScene.pos++;
                            OldAllocationStrategiesScene.BestFit(memoryRequest);
							break;
						case "Worst Fit":
                            OldAllocationStrategiesScene.pos++;
                            OldAllocationStrategiesScene.WorstFit(memoryRequest);
							break;
						case "Tailoring Best Fit":
                            OldAllocationStrategiesScene.pos++;
                            OldAllocationStrategiesScene.TailoringBestFit(memoryRequest);
							break;
                        case "Combined Fit":
                            OldAllocationStrategiesScene.pos++;
// Debug.WriteLine("next clicked pos = " + OldAllocationStrategiesScene.pos);
                            OldAllocationStrategiesScene.CombinedFit(memoryRequest);
                            break;
                    }
					break;
                case MyEnum.successfull: //successfull
					OldAllocationStrategiesScene.ClearRedArrow();
					OldAllocationStrategiesScene.ClearGrayArrow();
                    OldAllocationStrategiesScene.DrawFill();
                    memoryRequestState = MyEnum.noRequestYet;
                    b_Restart.IsEnabled = true;

                    //clear information bar
                    l_Size.Text = size;
					l_Diff.Text = diff;
					l_Best.Text = best;
					l_Free.Text = free;
                   
                    break;
                case MyEnum.unsuccessfull: //unsucessfull
					OldAllocationStrategiesScene.ClearGrayArrow();
					OldAllocationStrategiesScene.ClearRedArrow();
					await DisplayAlert("Alert", "No free space has been found", "OK");
					memoryRequestState = MyEnum.noRequestYet; //ready for a new request
															  
                    //clear information bar
					l_Size.Text = size;
					l_Diff.Text = diff;
					l_Best.Text = best;
					l_Free.Text = free;
                   
                    break;
                case MyEnum.noRequestYet: //no requst yet
                    
                    if (OldAllocationStrategiesScene.CheckIfFull())
                    {
                        memoryRequestState = MyEnum.memoryFull;
                        await DisplayAlert("Alert", "Out of memory! The app will close now", "OK");
                        await Navigation.PopAsync();
                        break;
                    }else{
                        await Navigation.PushModalAsync(new OldAllocationStrategiesModal(), true);
                        break;
                    }
                case MyEnum.memoryFull: //memory is full
                    await DisplayAlert("Alert", "Out of memory! The app will close now", "OK");
                    await Navigation.PopAsync();
                    break;
			}

        }
        /**********************************************************************
        *********************************************************************/
        void B_Info_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AllocationStrategiesHelp());
        }
        //this method is called everytime the device is rotated
        protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height); //must be called
			if (this.width != width || this.height != height)
			{
				this.width = width;
				this.height = height;
			}

            //reconfigure layout
            if (width > height)
			{
                //isContentCreated = true;
                this.Content.IsVisible = true;
			}
            else if (height > width && isContentCreated)
            {
                //isContentCreated = false;
                this.Content.IsVisible = false;

                //DisplayAlert("Alert", "Please rotate the device", "OK");
			}
		}
	}
}