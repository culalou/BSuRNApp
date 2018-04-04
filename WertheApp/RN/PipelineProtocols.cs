﻿using System;
using CocosSharp;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace WertheApp.RN
{
    public class PipelineProtocols : ContentPage
    {
        //VARIABLES
        CCGameView cc_gameView;
        PipelineProtocolsScene gameScene;
        PipelineProtocolsScene2 gameScene2;

        public static int windowSize;
        public static String strategy;
        public static int timeoutTime;

        public static bool paused = false;
        public static CocosSharpView gameView;
        Button b_Pause;

        bool landscape = false; //indicates device orientation

		private double width = 0;
		private double height = 0;

        public static Label l_LastRecentInOrderAtReceiver;
        public static Label l_LastRecentAcknowlegement;
        public static Label l_Timeout;

        public const int gameviewWidth = 400;
        public const int gameviewHeight = 2000;

		//CONSTRUCTOR
		public PipelineProtocols(int a, String s, int t)
        {
            windowSize = a;
            strategy = s;
            timeoutTime = t;

            Title = "Pipeline Protocols" + strategy;

            //if orientation Horizontal
			if (Application.Current.MainPage.Width < Application.Current.MainPage.Height)
			{
                landscape = false;
				CreateContent();
			}
            //if orientation Landscape
			else
            {   
                landscape = true;
                CreateContent();
                this.Content.IsVisible = false;
				//this.Content = new Label { Text = "please rotate your device" };
			}
        }

		//METHODS
        /**********************************************************************
        *********************************************************************/
        //Gets called everytime the Page is not shown anymore. For example when clicking the back navigation
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (gameScene != null) { gameScene.Dispose(); }
            if (gameScene2 != null) { gameScene2.Dispose(); }
            PipelineProtocolsPack.stopEverything = true;
            PipelineProtocolsACK.stopEverything = true;
            PipelineProtocolsScene.stopEverything = true;
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
                    new RowDefinition{ Height = new GridLength(7, GridUnitType.Star)},
					new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)}
				};

			CreateTopHalf(grid);
			CreateBottomHalf(grid);
		}

		/**********************************************************************
        *********************************************************************/
		void CreateTopHalf(Grid grid)
		{
            var scrollview = new ScrollView();

			gameView = new CocosSharpView()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				// This gets called after CocosSharp starts up:
				ViewCreated = HandleViewCreated
			};
            double scaleFactor;
            if(landscape){
                scaleFactor = Application.Current.MainPage.Height / gameviewWidth;
            }else{
                scaleFactor = Application.Current.MainPage.Width / gameviewWidth;
            }
            gameView.HeightRequest = gameviewHeight * scaleFactor; // SCROLLING!!!!!!!!!!!!!!!!
            scrollview.Content = gameView;
			grid.Children.Add(scrollview, 0, 0);
		}

		/**********************************************************************
        *********************************************************************/
		void CreateBottomHalf(Grid grid)
		{
            //set the size of the elements in such a way, that they all fit on the screen
            //Screen Width is divided by the amount of elements (2)
            //Screen Width -20 because Margin is 10
            double StackChildSize;
            if (landscape)
            {
                StackChildSize = (Application.Current.MainPage.Height - 20) / 2;
            }
            else
            {
                StackChildSize = (Application.Current.MainPage.Width - 20) / 2;
            }

			//Using a Stacklayout to organize elements
			//with corresponding labels and String variables. 
			//For example l_Size, size
			var stackLayout = new StackLayout
			{
                Orientation = StackOrientation.Horizontal,
				Margin = new Thickness(10),

			};

            Button b_Send = new Button
            {
                Text = "Send Package",
                WidthRequest = StackChildSize,
                VerticalOptions = LayoutOptions.Center
            };
            b_Send.Clicked += B_Send_Clicked;
            stackLayout.Children.Add(b_Send);

            b_Pause = new Button
            {
                Text = "Pause",
                WidthRequest = StackChildSize,
                VerticalOptions = LayoutOptions.Center
            };
            b_Pause.Clicked += B_Stop_Clicked;
            stackLayout.Children.Add(b_Pause);

			grid.Children.Add(stackLayout, 0, 1);
		}

		/**********************************************************************
        *********************************************************************/
		async void B_Send_Clicked(object sender, EventArgs e)
        {
            if (strategy == "Selective Repeat")
            {
                int a1 = PipelineProtocolsScene.nextSeqnum;
                if (a1 == 29)
                {
                    await DisplayAlert("Alert", "You are done!", "OK");
                }
                else
                {
                    PipelineProtocolsScene.InvokeSender();
                }
            }
            else
            {
                int a1 = PipelineProtocolsScene2.nextSeqnum;
                if (a1 == 29)
                {
                    await DisplayAlert("Alert", "You are done!", "OK");
                }
                else
                {
                    PipelineProtocolsScene2.InvokeSender();
                }
            }

        }

		/**********************************************************************
        *********************************************************************/
		void B_Stop_Clicked(object sender, EventArgs e)
        {
			switch (paused)
			{
				case true:
                    if (strategy == "Selective Repeat"){ 
                        PipelineProtocolsScene.animationIsPaused = false;
                    }
                    else{
                        PipelineProtocolsScene2.animationIsPaused = false;
                    }
                    gameView.Paused = false;
                    paused = false;
                    b_Pause.Text = "Pause";
					break;
                case false:
                    if (strategy == "Selective Repeat"){
                        PipelineProtocolsScene.animationIsPaused = true;
                    }
                    else{
                        PipelineProtocolsScene2.animationIsPaused = true;
                    }
					b_Pause.Text = "Continue";
                    gameView.Paused = true;
                    paused = true;
					break;
			}
        }
		/**********************************************************************
        *********************************************************************/
		//sets up the scene 
		void HandleViewCreated(object sender, EventArgs e)
		{
			cc_gameView = sender as CCGameView;
			if (cc_gameView != null)
			{
				// This sets the game "world" resolution 
				//Attention: all drawn elements in the scene strongly depend ont he resolution! Better don't change it
				//###############################################################
                cc_gameView.DesignResolution = new CCSizeI(gameviewWidth, gameviewHeight); //CLIPPING
                                                                       //###############################################################

                //choose gamescene for GoBackN or Selective Repeat
                if(strategy == "Selective Repeat"){
                    gameScene = new PipelineProtocolsScene(cc_gameView); 
                    // Starts CocosSharp:
                    cc_gameView.RunWithScene(gameScene);
                }else{
                    gameScene2 = new PipelineProtocolsScene2(cc_gameView); 
                    // Starts CocosSharp:
                    cc_gameView.RunWithScene(gameScene2);
                }
				
			
			}
		}

		/**********************************************************************
        *********************************************************************/
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
                this.Content.IsVisible = false;
			}
			else if (height > width)
			{
                this.Content.IsVisible = true;
			}
		}


	}
}