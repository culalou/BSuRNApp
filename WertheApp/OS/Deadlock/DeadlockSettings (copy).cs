﻿using System;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace WertheApp.OS
{
    public class DeadlockSettingsCopy : ContentPage
    {
        //uhm....you should do sth about this mess ...
        private String dvd, usb, bluRay, printer, ijPrinter, printer3D;
        private String resourceVectorE, busyResourceVectorB, freeResourceVectorA, upcomingVectorC;
        private Xamarin.Forms.Picker p_dvd, p_usb, p_bluRay, p_printer, p_ijPrinter, p_runningprocesses, p_printer3D;

        private Label l_busyP1, l_busyP2, l_busyP3, l_busyP4, l_busyP5;
        private Label l_upcomingP1, l_upcomingP2, l_upcomingP3, l_upcomingP4, l_upcomingP5;

        private Xamarin.Forms.Picker p_p1_dvd, p_p2_dvd, p_p3_dvd, p_p4_dvd, p_p5_dvd;
        private Xamarin.Forms.Picker p_p1_usb, p_p2_usb, p_p3_usb, p_p4_usb, p_p5_usb;
        private Xamarin.Forms.Picker p_p1_bluRay, p_p2_bluRay, p_p3_bluRay, p_p4_bluRay, p_p5_bluRay;
        private Xamarin.Forms.Picker p_p1_printer, p_p2_printer, p_p3_printer, p_p4_printer, p_p5_printer;
        private Xamarin.Forms.Picker p_p1_ijprinter, p_p2_ijprinter, p_p3_ijprinter, p_p4_ijprinter, p_p5_ijprinter;
        private Xamarin.Forms.Picker p_p1_printer3D, p_p2_printer3D, p_p3_printer3D, p_p4_printer3D, p_p5_printer3D;
        // private int busy_dvd, busy_usb, busy_bluRay, busy_printer, busy_ijprinter, busy_printer3D;


        private Xamarin.Forms.Picker p_p1_upcoming_dvd, p_p2_upcoming_dvd, p_p3_upcoming_dvd, p_p4_upcoming_dvd, p_p5_upcoming_dvd;
        private Xamarin.Forms.Picker p_p1_upcoming_usb, p_p2_upcoming_usb, p_p3_upcoming_usb, p_p4_upcoming_usb, p_p5_upcoming_usb;
        private Xamarin.Forms.Picker p_p1_upcoming_bluRay, p_p2_upcoming_bluRay, p_p3_upcoming_bluRay, p_p4_upcoming_bluRay, p_p5_upcoming_bluRay;
        private Xamarin.Forms.Picker p_p1_upcoming_printer, p_p2_upcoming_printer, p_p3_upcoming_printer, p_p4_upcoming_printer, p_p5_upcoming_printer;
        private Xamarin.Forms.Picker p_p1_upcoming_ijprinter, p_p2_upcoming_ijprinter, p_p3_upcoming_ijprinter, p_p4_upcoming_ijprinter, p_p5_upcoming_ijprinter;
        private Xamarin.Forms.Picker p_p1_upcoming_printer3D, p_p2_upcoming_printer3D, p_p3_upcoming_printer3D, p_p4_upcoming_printer3D, p_p5_upcoming_printer3D;
        //private int upcoming_dvd, upcoming_usb, upcoming_bluRay, upcoming_printer, upcoming_ijprinter;

        private Label l_resourceVectorE, l_busyResourceVectorB, l_freeResourceVectorA, l_upcomingVectorC;
        private StackLayout sl_busyResources, sl_upcomingRequests;
        private StackLayout sl_busyProcess1, sl_busyProcess2, sl_busyProcess3, sl_busyProcess4, sl_busyProcess5;
        private StackLayout sl_upcomingProcess1, sl_upcomingProcess2, sl_upcomingProcess3, sl_upcomingProcess4, sl_upcomingProcess5;

        private List<Xamarin.Forms.Picker> busyResPickerList, upcomingResPickerList, resPickerList;
        //private int preset;
        private static Dictionary<int, String> vectorBProcesses, vectorCProcesses;


        public DeadlockSettingsCopy()
        {

            busyResPickerList = new List<Xamarin.Forms.Picker>();
            upcomingResPickerList = new List<Xamarin.Forms.Picker>();
            resPickerList = new List<Xamarin.Forms.Picker>();
            //preset = 0;
        }

        //METHODS
        /**********************************************************************
        *********************************************************************/
        void CreateContent()
        {
            CreateExistingResourcesUI(stackLayout);


            // BUSY

            sl_busyResources = new StackLayout() { HorizontalOptions = LayoutOptions.Start };
            CreateBusyResourcesUI(sl_busyResources);
            stackLayout.Children.Add(sl_busyResources);


            //upcomingVectorC REQUESTS
            StackLayout stackLayoutUpcoming = new StackLayout { Orientation = StackOrientation.Horizontal };
           
          
            sl_upcomingRequests = new StackLayout() { };
            CreateUpcomingRequestsUI(sl_upcomingRequests);
            stackLayout.Children.Add(sl_upcomingRequests);


          
        }


        /**********************************************************************
        *********************************************************************/
        void CreateBusyResourcesUI(StackLayout sl_busyResources)
        {
            // stacklayouts for every process (contain the pickers)
            sl_busyProcess1 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_busyProcess2 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_busyProcess3 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_busyProcess4 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_busyProcess5 = new StackLayout() { Orientation = StackOrientation.Horizontal };

            l_busyP1 = new Label { Text = "P1:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_busyP2 = new Label { Text = "P2:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_busyP3 = new Label { Text = "P3:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_busyP4 = new Label { Text = "P4:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_busyP5 = new Label { Text = "P5:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };

            p_p1_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            busyResPickerList.Add(p_p1_dvd);
            busyResPickerList.Add(p_p2_dvd);
            busyResPickerList.Add(p_p3_dvd);
            busyResPickerList.Add(p_p4_dvd);
            busyResPickerList.Add(p_p5_dvd);

            busyResPickerList.Add(p_p1_usb);
            busyResPickerList.Add(p_p2_usb);
            busyResPickerList.Add(p_p3_usb);
            busyResPickerList.Add(p_p4_usb);
            busyResPickerList.Add(p_p5_usb);

            busyResPickerList.Add(p_p1_bluRay);
            busyResPickerList.Add(p_p2_bluRay);
            busyResPickerList.Add(p_p3_bluRay);
            busyResPickerList.Add(p_p4_bluRay);
            busyResPickerList.Add(p_p5_bluRay);

            busyResPickerList.Add(p_p1_printer);
            busyResPickerList.Add(p_p2_printer);
            busyResPickerList.Add(p_p3_printer);
            busyResPickerList.Add(p_p4_printer);
            busyResPickerList.Add(p_p5_printer);

            busyResPickerList.Add(p_p1_ijprinter);
            busyResPickerList.Add(p_p2_ijprinter);
            busyResPickerList.Add(p_p3_ijprinter);
            busyResPickerList.Add(p_p4_ijprinter);
            busyResPickerList.Add(p_p5_ijprinter);

            busyResPickerList.Add(p_p1_printer3D);
            busyResPickerList.Add(p_p2_printer3D);
            busyResPickerList.Add(p_p3_printer3D);
            busyResPickerList.Add(p_p4_printer3D);
            busyResPickerList.Add(p_p5_printer3D);

            AddItemsToBusyResPickers();
            SetBusyResPickersToZero();
            AddPickersToBusyRes();

            sl_busyResources.Children.Add(sl_busyProcess1);
            sl_busyResources.Children.Add(sl_busyProcess2);

        }
        /**********************************************************************
        *********************************************************************/
        void CreateUpcomingRequestsUI(StackLayout stackLayout)
        {
            // stacklayouts for every process (contain the pickers)
            sl_upcomingProcess1 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_upcomingProcess2 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_upcomingProcess3 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_upcomingProcess4 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            sl_upcomingProcess5 = new StackLayout() { Orientation = StackOrientation.Horizontal };

            l_upcomingP1 = new Label { Text = "P1:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40 };
            l_upcomingP2 = new Label { Text = "P2:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_upcomingP3 = new Label { Text = "P3:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_upcomingP4 = new Label { Text = "P4:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };
            l_upcomingP5 = new Label { Text = "P5:  ",
                FontSize = App._textFontSize,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40
            };

            //goddamit I know you can do better
            //pickers for every resource /process combination
            p_p1_upcoming_dvd = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_upcoming_dvd = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_upcoming_dvd = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_upcoming_dvd = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_upcoming_dvd = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            p_p1_upcoming_usb = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_upcoming_usb = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_upcoming_usb = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_upcoming_usb = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_upcoming_usb = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            p_p1_upcoming_bluRay = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_upcoming_bluRay = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_upcoming_bluRay = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_upcoming_bluRay = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_upcoming_bluRay = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            p_p1_upcoming_printer = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_upcoming_printer = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_upcoming_printer = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_upcoming_printer = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_upcoming_printer = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            p_p1_upcoming_ijprinter = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_upcoming_ijprinter = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_upcoming_ijprinter = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_upcoming_ijprinter = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_upcoming_ijprinter = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            p_p1_upcoming_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p2_upcoming_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p3_upcoming_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p4_upcoming_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };
            p_p5_upcoming_printer3D = new Xamarin.Forms.Picker() { WidthRequest = 40, FontSize = App._textFontSize };

            upcomingResPickerList.Add(p_p1_upcoming_dvd);
            upcomingResPickerList.Add(p_p2_upcoming_dvd);
            upcomingResPickerList.Add(p_p3_upcoming_dvd);
            upcomingResPickerList.Add(p_p4_upcoming_dvd);
            upcomingResPickerList.Add(p_p5_upcoming_dvd);

            upcomingResPickerList.Add(p_p1_upcoming_usb);
            upcomingResPickerList.Add(p_p2_upcoming_usb);
            upcomingResPickerList.Add(p_p3_upcoming_usb);
            upcomingResPickerList.Add(p_p4_upcoming_usb);
            upcomingResPickerList.Add(p_p5_upcoming_usb);

            upcomingResPickerList.Add(p_p1_upcoming_bluRay);
            upcomingResPickerList.Add(p_p2_upcoming_bluRay);
            upcomingResPickerList.Add(p_p3_upcoming_bluRay);
            upcomingResPickerList.Add(p_p4_upcoming_bluRay);
            upcomingResPickerList.Add(p_p5_upcoming_bluRay);

            upcomingResPickerList.Add(p_p1_upcoming_printer);
            upcomingResPickerList.Add(p_p2_upcoming_printer);
            upcomingResPickerList.Add(p_p3_upcoming_printer);
            upcomingResPickerList.Add(p_p4_upcoming_printer);
            upcomingResPickerList.Add(p_p5_upcoming_printer);

            upcomingResPickerList.Add(p_p1_upcoming_ijprinter);
            upcomingResPickerList.Add(p_p2_upcoming_ijprinter);
            upcomingResPickerList.Add(p_p3_upcoming_ijprinter);
            upcomingResPickerList.Add(p_p4_upcoming_ijprinter);
            upcomingResPickerList.Add(p_p5_upcoming_ijprinter);

            upcomingResPickerList.Add(p_p1_upcoming_printer3D);
            upcomingResPickerList.Add(p_p2_upcoming_printer3D);
            upcomingResPickerList.Add(p_p3_upcoming_printer3D);
            upcomingResPickerList.Add(p_p4_upcoming_printer3D);
            upcomingResPickerList.Add(p_p5_upcoming_printer3D);

            AddItemsToUpcomingResPickers();
            SetVectorChangedEvents();
            SetUpcomingResPickersToZero();
            AddPickersToUpcomingRes();

            sl_upcomingRequests.Children.Add(sl_upcomingProcess1);
            sl_upcomingRequests.Children.Add(sl_upcomingProcess2);
        }


        /**********************************************************************
        *********************************************************************/
        void B_ClearBusyClicked(object sender, EventArgs e)
        {
            SetBusyResPickersToZero();
        }

        void B_ClearUpcoming_Clicked(object sender, EventArgs e)
        {
            SetUpcomingResPickersToZero();
        }

        void B_ClearResources_Clicked(object sender, EventArgs e)
        {
            SetResPickersToZero();
            p_dvd.SelectedIndex = 2;
            p_printer.SelectedIndex = 2;
            p_usb.SelectedIndex = 3;
        }

        /**********************************************************************
        *********************************************************************/

        void SetPreset1()
        {
            //dvd, printer, usb, bluRay, ijPrinter, printer3D
            // set resources
            p_dvd.SelectedIndex = 6;
            p_printer.SelectedIndex = 3;
            p_usb.SelectedIndex = 4;
            p_bluRay.SelectedIndex = 2;
            p_ijPrinter.SelectedIndex = 0;
            p_printer3D.SelectedIndex = 0;

            //set process total
            p_runningprocesses.SelectedIndex = 5;

            
            //set busy resources p1
            p_p1_dvd.SelectedIndex = 3;
            p_p1_usb.SelectedIndex = 1;
            p_p1_bluRay.SelectedIndex = 1;

            //set busy resources p2
            p_p2_printer.SelectedIndex = 1;

            //set busy resources p3
            p_p3_dvd.SelectedIndex = 1;
            p_p3_printer.SelectedIndex = 1;
            p_p3_usb.SelectedIndex = 1;

            //set busy resources p4
            p_p4_dvd.SelectedIndex = 1;
            p_p4_printer.SelectedIndex = 1;
            p_p4_bluRay.SelectedIndex = 1;

            //set busy resources p5
            //none


            //set future requests p1
            p_p1_upcoming_dvd.SelectedIndex = 1;
            p_p1_upcoming_printer.SelectedIndex = 1;

            //set future requests  p2
            p_p2_upcoming_printer.SelectedIndex = 1;
            p_p2_upcoming_usb.SelectedIndex = 1;
            p_p2_upcoming_bluRay.SelectedIndex = 2;

            //set future requests p3
            p_p3_upcoming_dvd.SelectedIndex = 3;
            p_p3_upcoming_printer.SelectedIndex = 1;

            //set future requests p4
            p_p4_upcoming_usb.SelectedIndex = 1;

            //set future requests p5
            p_p5_upcoming_dvd.SelectedIndex = 2;
            p_p5_upcoming_printer.SelectedIndex = 1;
            p_p5_upcoming_usb.SelectedIndex = 1;
            
           
        }

        void SetPreset2()
        {
            //dvd, printer, usb, bluRay, ijPrinter, printer3D
            // set resources
            p_dvd.SelectedIndex = 2;
            p_printer.SelectedIndex = 2;
            p_usb.SelectedIndex = 6;
            p_bluRay.SelectedIndex = 0;
            p_ijPrinter.SelectedIndex = 0;
            p_printer3D.SelectedIndex = 3;

            //set process total
            p_runningprocesses.SelectedIndex = 5;

            //set busy resources p1
            p_p1_usb.SelectedIndex = 3;

            //set busy resources p2
            //none

            //set busy resources p3
            p_p3_dvd.SelectedIndex = 1;
            p_p3_printer.SelectedIndex = 1;
            p_p3_usb.SelectedIndex = 2;

            //set busy resources p4
            p_p4_dvd.SelectedIndex = 1;
            p_p4_usb.SelectedIndex = 1;
            p_p4_printer3D.SelectedIndex = 1;

            //set busy resources p5
            p_p5_printer.SelectedIndex = 1;
            p_p5_printer3D.SelectedIndex = 1;


            //set future requests p1
            p_p1_upcoming_printer3D.SelectedIndex = 1;

            //set future requests  p2
            p_p2_upcoming_usb.SelectedIndex = 2;
            p_p2_upcoming_printer3D.SelectedIndex = 1;

            //set future requests p3
            p_p3_upcoming_printer.SelectedIndex = 2;
            p_p3_upcoming_usb.SelectedIndex = 2;
            p_p3_upcoming_printer3D.SelectedIndex = 2;

            //set future requests p4
            p_p4_upcoming_dvd.SelectedIndex = 1;
            p_p4_upcoming_printer3D.SelectedIndex = 1;

            //set future requests p5
            p_p5_upcoming_printer.SelectedIndex = 1;
            p_p5_upcoming_printer3D.SelectedIndex = 1;

        }

        void SetPreset3()
        {
            //dvd, printer, usb, bluRay, ijPrinter, printer3D
            // set resources
            p_dvd.SelectedIndex = 1;
            p_printer.SelectedIndex = 1;
            p_usb.SelectedIndex = 1;
            p_bluRay.SelectedIndex = 0;
            p_ijPrinter.SelectedIndex = 0;
            p_printer3D.SelectedIndex = 1;

            //set process total
            p_runningprocesses.SelectedIndex = 5;

            //set busy resources p1
            //none

            //set busy resources p2
            //none

            //set busy resources p3
            p_p3_dvd.SelectedIndex = 1;
            p_p3_printer.SelectedIndex = 1;

            //set busy resources p4
            p_p4_usb.SelectedIndex = 1;

            //set busy resources p5
            p_p5_printer3D.SelectedIndex = 1;


            //set future requests p1
            p_p1_upcoming_dvd.SelectedIndex = 1;
            p_p1_upcoming_printer.SelectedIndex = 1;
            p_p1_upcoming_usb.SelectedIndex = 1;
            p_p1_upcoming_printer3D.SelectedIndex = 1;

            //set future requests  p2
            p_p2_upcoming_usb.SelectedIndex = 1;

            //set future requests p3
            p_p3_upcoming_usb.SelectedIndex = 1;
            p_p3_upcoming_printer3D.SelectedIndex = 1;

            //set future requests p4
            //none

            //set future requests p5
            p_p5_upcoming_usb.SelectedIndex = 1;

        }

        /**********************************************************************
        *********************************************************************/
        void ProcessesChanged(object sender, EventArgs e)
        {
            sl_busyResources.Children.Clear();
            sl_upcomingRequests.Children.Clear();
            var total = Int16.Parse(p_runningprocesses.SelectedItem.ToString());

            if (total >= 2)
            {
                sl_busyResources.Children.Add(sl_busyProcess1);
                sl_busyResources.Children.Add(sl_busyProcess2);

                sl_upcomingRequests.Children.Add(sl_upcomingProcess1);
                sl_upcomingRequests.Children.Add(sl_upcomingProcess2);

            }
            if (total >= 3)
            {
                sl_busyResources.Children.Add(sl_busyProcess3);
                sl_upcomingRequests.Children.Add(sl_upcomingProcess3);

            }
            if (total >= 4)
            {
                sl_busyResources.Children.Add(sl_busyProcess4);
                sl_upcomingRequests.Children.Add(sl_upcomingProcess4);
            }
            if (total >= 5)
            {
                sl_busyResources.Children.Add(sl_busyProcess5);
                sl_upcomingRequests.Children.Add(sl_upcomingProcess5);

            }
            scrollView.ForceLayout();

        }

        //Vector changed für Vector E berechnen
        /**********************************************************************
        *********************************************************************/
        void VectorChanged(object sender, EventArgs e)
        {
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();
            int total = 0;

            resourceVectorE = "";
            if (dvd != "0") { resourceVectorE = "" + resourceVectorE + "    " + dvd; total++; }
            if (printer != "0") { resourceVectorE = "" + resourceVectorE + "    " + printer; total++; }
            if (usb != "0") { resourceVectorE = "" + resourceVectorE + "    " + usb; total++; }
            if (bluRay != "0") { resourceVectorE = "" + resourceVectorE + "    " + bluRay; total++; }
            if (ijPrinter != "0") { resourceVectorE = "" + resourceVectorE + "    " + ijPrinter; total++; }
            if (printer3D != "0") { resourceVectorE = "" + resourceVectorE + "    " + printer3D; total++; }
            String vectorEText = "E = (" + resourceVectorE + "    )";

            string attention = "\nPick between 2 and 5 resources!";
            if (total < 2 || total > 5)
            {
                vectorEText = vectorEText + " " + attention;
            }
            vectorEText = vectorEText.Replace("\n", System.Environment.NewLine);
            l_resourceVectorE.Text = vectorEText;
        }

        //calculate Vector for busy ressources (VECTOR B) 
        void SetVectorB()
        {
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();

            //Get number of processes
            var total = Int16.Parse(p_runningprocesses.SelectedItem.ToString());

            //calculate vector B
            int busy_dvd = 0;
            int busy_printer = 0;
            int busy_usb = 0;
            int busy_bluRay = 0;
            int busy_ijprinter = 0;
            int busy_printer3D = 0;

            if (total >= 2)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p1_dvd.SelectedItem.ToString())
                    + Int32.Parse(p_p2_dvd.SelectedItem.ToString());

                busy_printer = busy_printer + Int32.Parse(p_p1_printer.SelectedItem.ToString())
                    + Int32.Parse(p_p2_printer.SelectedItem.ToString());

                busy_usb = busy_usb + Int32.Parse(p_p1_usb.SelectedItem.ToString())
                  + Int32.Parse(p_p2_usb.SelectedItem.ToString());

                busy_bluRay = busy_bluRay + Int32.Parse(p_p1_bluRay.SelectedItem.ToString())
                  + Int32.Parse(p_p2_bluRay.SelectedItem.ToString());

                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p1_ijprinter.SelectedItem.ToString())
                  + Int32.Parse(p_p2_ijprinter.SelectedItem.ToString());

                busy_printer3D = busy_printer3D + Int32.Parse(p_p1_printer3D.SelectedItem.ToString())
                    + Int32.Parse(p_p2_printer3D.SelectedItem.ToString());

            }
            if (total >= 3)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p3_dvd.SelectedItem.ToString());
                busy_printer = busy_printer + Int32.Parse(p_p3_printer.SelectedItem.ToString());
                busy_usb = busy_usb + Int32.Parse(p_p3_usb.SelectedItem.ToString());
                busy_bluRay = busy_bluRay + Int32.Parse(p_p3_bluRay.SelectedItem.ToString());
                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p3_ijprinter.SelectedItem.ToString());
                busy_printer3D = busy_printer3D + Int32.Parse(p_p3_printer3D.SelectedItem.ToString());
            }
            if (total >= 4)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p4_dvd.SelectedItem.ToString());
                busy_printer = busy_printer + Int32.Parse(p_p4_printer.SelectedItem.ToString());
                busy_usb = busy_usb + Int32.Parse(p_p4_usb.SelectedItem.ToString());
                busy_bluRay = busy_bluRay + Int32.Parse(p_p4_bluRay.SelectedItem.ToString());
                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p4_ijprinter.SelectedItem.ToString());
                busy_printer3D = busy_printer3D + Int32.Parse(p_p4_printer3D.SelectedItem.ToString());
            }
            if (total >= 5)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p5_dvd.SelectedItem.ToString());
                busy_printer = busy_printer + Int32.Parse(p_p5_printer.SelectedItem.ToString());
                busy_usb = busy_usb + Int32.Parse(p_p5_usb.SelectedItem.ToString());
                busy_bluRay = busy_bluRay + Int32.Parse(p_p5_bluRay.SelectedItem.ToString());
                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p5_ijprinter.SelectedItem.ToString());
                busy_printer3D = busy_printer3D + Int32.Parse(p_p5_printer3D.SelectedItem.ToString());

            }

            busyResourceVectorB = "";
            if (dvd != "0") { busyResourceVectorB = "" + busyResourceVectorB + "    " + busy_dvd; }
            if (printer != "0") { busyResourceVectorB = "" + busyResourceVectorB + "    " + busy_printer; }
            if (usb != "0") { busyResourceVectorB = "" + busyResourceVectorB + "    " + busy_usb; }
            if (bluRay != "0") { busyResourceVectorB = "" + busyResourceVectorB + "    " + busy_bluRay; }
            if (ijPrinter != "0") { busyResourceVectorB = "" + busyResourceVectorB + "    " + busy_ijprinter; }
            if (printer3D != "0") { busyResourceVectorB = "" + busyResourceVectorB + "    " + busy_printer3D; }
            String vectorBText = "B = (" + busyResourceVectorB + "    )";

            string attention = "\nCannot occupy more resources than available!";
            if (busy_dvd > Int32.Parse(dvd) ||
                busy_printer > Int32.Parse(printer) ||
                busy_usb > Int32.Parse(usb) ||
                busy_bluRay > Int32.Parse(bluRay) ||
                busy_printer3D > Int32.Parse(printer3D) ||
                busy_ijprinter > Int32.Parse(ijPrinter)
                )
            {
                vectorBText = vectorBText + " " + attention;
            }

            vectorBText = vectorBText.Replace("\n", System.Environment.NewLine);
            l_busyResourceVectorB.Text = vectorBText;

        }

        //calculate Vector for busy ressources (VECTOR C) 
        void SetVectorC()
        {
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();

            //Get number of processes
            var total = Int16.Parse(p_runningprocesses.SelectedItem.ToString());

            //calculate vector C
            int upcoming_dvd = 0;
            int upcoming_printer = 0;
            int upcoming_usb = 0;
            int upcoming_bluRay = 0;
            int upcoming_ijprinter = 0;
            int upcoming_printer3D = 0;

            if (total >= 2)
            {
                upcoming_dvd = upcoming_dvd + Int32.Parse(p_p1_upcoming_dvd.SelectedItem.ToString())
                    + Int32.Parse(p_p2_upcoming_dvd.SelectedItem.ToString());

                upcoming_printer = upcoming_printer + Int32.Parse(p_p1_upcoming_printer.SelectedItem.ToString())
                    + Int32.Parse(p_p2_upcoming_printer.SelectedItem.ToString());

                upcoming_usb = upcoming_usb + Int32.Parse(p_p1_upcoming_usb.SelectedItem.ToString())
                  + Int32.Parse(p_p2_upcoming_usb.SelectedItem.ToString());

                upcoming_bluRay = upcoming_bluRay + Int32.Parse(p_p1_upcoming_bluRay.SelectedItem.ToString())
                  + Int32.Parse(p_p2_upcoming_bluRay.SelectedItem.ToString());

                upcoming_ijprinter = upcoming_ijprinter + Int32.Parse(p_p1_upcoming_ijprinter.SelectedItem.ToString())
                  + Int32.Parse(p_p2_upcoming_ijprinter.SelectedItem.ToString());

                upcoming_printer3D = upcoming_printer3D + Int32.Parse(p_p1_upcoming_printer3D.SelectedItem.ToString())
                    + Int32.Parse(p_p2_upcoming_printer3D.SelectedItem.ToString());

            }
            if (total >= 3)
            {
                upcoming_dvd = upcoming_dvd + Int32.Parse(p_p3_upcoming_dvd.SelectedItem.ToString());
                upcoming_printer = upcoming_printer + Int32.Parse(p_p3_upcoming_printer.SelectedItem.ToString());
                upcoming_usb = upcoming_usb + Int32.Parse(p_p3_upcoming_usb.SelectedItem.ToString());
                upcoming_bluRay = upcoming_bluRay + Int32.Parse(p_p3_upcoming_bluRay.SelectedItem.ToString());
                upcoming_ijprinter = upcoming_ijprinter + Int32.Parse(p_p3_upcoming_ijprinter.SelectedItem.ToString());
                upcoming_printer3D = upcoming_printer3D + Int32.Parse(p_p3_upcoming_printer3D.SelectedItem.ToString());
            }
            if (total >= 4)
            {
                upcoming_dvd = upcoming_dvd + Int32.Parse(p_p4_upcoming_dvd.SelectedItem.ToString());
                upcoming_printer = upcoming_printer + Int32.Parse(p_p4_upcoming_printer.SelectedItem.ToString());
                upcoming_usb = upcoming_usb + Int32.Parse(p_p4_upcoming_usb.SelectedItem.ToString());
                upcoming_bluRay = upcoming_bluRay + Int32.Parse(p_p4_upcoming_bluRay.SelectedItem.ToString());
                upcoming_ijprinter = upcoming_ijprinter + Int32.Parse(p_p4_upcoming_ijprinter.SelectedItem.ToString());
                upcoming_printer3D = upcoming_printer3D + Int32.Parse(p_p4_upcoming_printer3D.SelectedItem.ToString());
            }
            if (total >= 5)
            {
                upcoming_dvd = upcoming_dvd + Int32.Parse(p_p5_upcoming_dvd.SelectedItem.ToString());
                upcoming_printer = upcoming_printer + Int32.Parse(p_p5_upcoming_printer.SelectedItem.ToString());
                upcoming_usb = upcoming_usb + Int32.Parse(p_p5_upcoming_usb.SelectedItem.ToString());
                upcoming_bluRay = upcoming_bluRay + Int32.Parse(p_p5_upcoming_bluRay.SelectedItem.ToString());
                upcoming_ijprinter = upcoming_ijprinter + Int32.Parse(p_p5_upcoming_ijprinter.SelectedItem.ToString());
                upcoming_printer3D = upcoming_printer3D + Int32.Parse(p_p5_upcoming_printer3D.SelectedItem.ToString());

            }

            upcomingVectorC = ""; 
            if (dvd != "0") { upcomingVectorC = "" + upcomingVectorC + "    " + upcoming_dvd; }
            if (printer != "0") { upcomingVectorC = "" + upcomingVectorC + "    " + upcoming_printer; }
            if (usb != "0") { upcomingVectorC = "" + upcomingVectorC + "    " + upcoming_usb; }
            if (bluRay != "0") { upcomingVectorC = "" + upcomingVectorC + "    " + upcoming_bluRay; }
            if (ijPrinter != "0") { upcomingVectorC = "" + upcomingVectorC + "    " + upcoming_ijprinter; }
            if (printer3D != "0") { upcomingVectorC = "" + upcomingVectorC + "    " + upcoming_printer3D; }
            String vectorCText = "C = (" + upcomingVectorC + "    )";

            l_upcomingVectorC.Text = vectorCText;

        }
        //calculate Vector for busy ressources (VECTOR C) 
        /**********************************************************************
        *********************************************************************/
        void VectorCChangedEvent(object sender, EventArgs e)
        {
            try
            {
                SetVectorC();
            }
            catch (System.NullReferenceException ex)
            {
                //Debug.WriteLine(ex);
                //Exception is thrown because VectorChanged2 Event is also invoked when items are removed from picker  
            }

        }

        //calculate Vector for busy ressources (VECTOR B) 
        /**********************************************************************
        *********************************************************************/
        void VectorBChangedEvent(object sender, EventArgs e)
        {
            try
            {
                SetVectorB();
            }
            catch (System.NullReferenceException ex) {
                //Debug.WriteLine(ex);
                //Exception is thrown because VectorChanged2 Event is also invoked when items are removed from picker  
            }

        }

        //calculate Vector for free ressources (VECTOR A) 
        /**********************************************************************
        *********************************************************************/
        void VectorAChangedEvent(object sender, EventArgs e)
        {
            try
            {
                SetVectorA();
            }
            catch (System.NullReferenceException ex)
            {
                //Debug.WriteLine(ex);
                //Exception is thrown because VectorChanged3 Event is also invoked when items are removed from picker  
            }
        }

        void SetVectorA()
        {
            //get number of ressources
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();

            //Get number of processes
            var total = Int16.Parse(p_runningprocesses.SelectedItem.ToString());

            //calculate vector B
            int busy_dvd = 0;
            int busy_printer = 0;
            int busy_usb = 0;
            int busy_bluRay = 0;
            int busy_ijprinter = 0;
            int busy_printer3D = 0;

            if (total >= 2)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p1_dvd.SelectedItem.ToString())
                    + Int32.Parse(p_p2_dvd.SelectedItem.ToString());

                busy_printer = busy_printer + Int32.Parse(p_p1_printer.SelectedItem.ToString())
                    + Int32.Parse(p_p2_printer.SelectedItem.ToString());

                busy_usb = busy_usb + Int32.Parse(p_p1_usb.SelectedItem.ToString())
                  + Int32.Parse(p_p2_usb.SelectedItem.ToString());

                busy_bluRay = busy_bluRay + Int32.Parse(p_p1_bluRay.SelectedItem.ToString())
                  + Int32.Parse(p_p2_bluRay.SelectedItem.ToString());

                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p1_ijprinter.SelectedItem.ToString())
                  + Int32.Parse(p_p2_ijprinter.SelectedItem.ToString());

                busy_printer3D = busy_printer3D + Int32.Parse(p_p1_printer3D.SelectedItem.ToString())
                    + Int32.Parse(p_p2_printer3D.SelectedItem.ToString());

            }
            if (total >= 3)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p3_dvd.SelectedItem.ToString());
                busy_printer = busy_printer + Int32.Parse(p_p3_printer.SelectedItem.ToString());
                busy_usb = busy_usb + Int32.Parse(p_p3_usb.SelectedItem.ToString());
                busy_bluRay = busy_bluRay + Int32.Parse(p_p3_bluRay.SelectedItem.ToString());
                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p3_ijprinter.SelectedItem.ToString());
                busy_printer3D = busy_printer3D + Int32.Parse(p_p3_printer3D.SelectedItem.ToString());
            }
            if (total >= 4)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p4_dvd.SelectedItem.ToString());
                busy_printer = busy_printer + Int32.Parse(p_p4_printer.SelectedItem.ToString());
                busy_usb = busy_usb + Int32.Parse(p_p4_usb.SelectedItem.ToString());
                busy_bluRay = busy_bluRay + Int32.Parse(p_p4_bluRay.SelectedItem.ToString());
                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p4_ijprinter.SelectedItem.ToString());
                busy_printer3D = busy_printer3D + Int32.Parse(p_p4_printer3D.SelectedItem.ToString());

            }
            if (total >= 5)
            {
                busy_dvd = busy_dvd + Int32.Parse(p_p5_dvd.SelectedItem.ToString());
                busy_printer = busy_printer + Int32.Parse(p_p5_printer.SelectedItem.ToString());
                busy_usb = busy_usb + Int32.Parse(p_p5_usb.SelectedItem.ToString());
                busy_bluRay = busy_bluRay + Int32.Parse(p_p5_bluRay.SelectedItem.ToString());
                busy_ijprinter = busy_ijprinter + Int32.Parse(p_p5_ijprinter.SelectedItem.ToString());
                busy_printer3D = busy_printer3D + Int32.Parse(p_p5_printer3D.SelectedItem.ToString());

            }

            //calculate free ressources vector
            int free_dvd = Int32.Parse(dvd) - busy_dvd;
            int free_printer = Int32.Parse(printer) - busy_printer;
            int free_usb = Int32.Parse(usb) - busy_usb;
            int free_bluRay = Int32.Parse(bluRay) - busy_bluRay;
            int free_ijPrinter = Int32.Parse(ijPrinter) - busy_ijprinter;
            int free_printer3D = Int32.Parse(printer3D) - busy_printer3D;


            freeResourceVectorA = "";
            if (dvd != "0") { freeResourceVectorA = "" + freeResourceVectorA + "    " + free_dvd; }
            if (printer != "0") { freeResourceVectorA = "" + freeResourceVectorA + "    " + free_printer; }
            if (usb != "0") { freeResourceVectorA = "" + freeResourceVectorA + "    " + free_usb; }
            if (bluRay != "0") { freeResourceVectorA = "" + freeResourceVectorA + "    " + free_bluRay; }
            if (ijPrinter != "0") { freeResourceVectorA = "" + freeResourceVectorA + "    " + free_ijPrinter; }
            if (printer3D != "0") { freeResourceVectorA = "" + freeResourceVectorA + "    " + free_printer3D; }

            l_freeResourceVectorA.Text = "A = (" + freeResourceVectorA + "    )";

        }

        //show Ocuupied and Requested Ressources Picker
        void VectorChanged4(object sender, EventArgs e)
        {
            AddItemsToBusyResPickers();
            AddItemsToUpcomingResPickers();
            SetUpcomingResPickersToZero();
            SetBusyResPickersToZero();
            SetVectorB();
            SetVectorC();
            SetVectorA();
            AddPickersToBusyRes();
            AddPickersToUpcomingRes();

        }

        void SetVectorChangedEvents()
        {
            foreach (Xamarin.Forms.Picker picker in busyResPickerList)
            {
                picker.SelectedIndexChanged += VectorBChangedEvent;
                picker.SelectedIndexChanged += VectorAChangedEvent;
            }
            foreach (Xamarin.Forms.Picker picker in upcomingResPickerList)
            {
                picker.SelectedIndexChanged += VectorCChangedEvent;
            }
        }

        void SetResPickersToZero()
        {
            foreach (Xamarin.Forms.Picker picker in resPickerList)
            {
                picker.SelectedIndex = 0;
            }

        }

        void SetUpcomingResPickersToZero()
        {
            foreach (Xamarin.Forms.Picker picker in upcomingResPickerList)
            {
                picker.SelectedIndex = 0;
            }
               
        }

        void SetBusyResPickersToZero()
        {
            foreach (Xamarin.Forms.Picker picker in busyResPickerList)
            {
                picker.SelectedIndex = 0;
            }
        }

        void AddItemsToUpcomingResPickers()
        {
            //get number of ressources
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();


            //first remove all items
            foreach (Xamarin.Forms.Picker picker in upcomingResPickerList)
            {
                picker.Items.Clear();
            }

            //add item for every ressource
            for (int i = 0; i <= UInt32.Parse(dvd); i++)
            {
                p_p1_upcoming_dvd.Items.Add(i.ToString());
                p_p2_upcoming_dvd.Items.Add(i.ToString());
                p_p3_upcoming_dvd.Items.Add(i.ToString());
                p_p4_upcoming_dvd.Items.Add(i.ToString());
                p_p5_upcoming_dvd.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(printer); i++)
            {
                p_p1_upcoming_printer.Items.Add(i.ToString());
                p_p2_upcoming_printer.Items.Add(i.ToString());
                p_p3_upcoming_printer.Items.Add(i.ToString());
                p_p4_upcoming_printer.Items.Add(i.ToString());
                p_p5_upcoming_printer.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(usb); i++)
            {
                p_p1_upcoming_usb.Items.Add(i.ToString());
                p_p2_upcoming_usb.Items.Add(i.ToString());
                p_p3_upcoming_usb.Items.Add(i.ToString());
                p_p4_upcoming_usb.Items.Add(i.ToString());
                p_p5_upcoming_usb.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(bluRay); i++)
            {
                p_p1_upcoming_bluRay.Items.Add(i.ToString());
                p_p2_upcoming_bluRay.Items.Add(i.ToString());
                p_p3_upcoming_bluRay.Items.Add(i.ToString());
                p_p4_upcoming_bluRay.Items.Add(i.ToString());
                p_p5_upcoming_bluRay.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(ijPrinter); i++)
            {
                p_p1_upcoming_ijprinter.Items.Add(i.ToString());
                p_p2_upcoming_ijprinter.Items.Add(i.ToString());
                p_p3_upcoming_ijprinter.Items.Add(i.ToString());
                p_p4_upcoming_ijprinter.Items.Add(i.ToString());
                p_p5_upcoming_ijprinter.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(printer3D); i++)
            {
                p_p1_upcoming_printer3D.Items.Add(i.ToString());
                p_p2_upcoming_printer3D.Items.Add(i.ToString());
                p_p3_upcoming_printer3D.Items.Add(i.ToString());
                p_p4_upcoming_printer3D.Items.Add(i.ToString());
                p_p5_upcoming_printer3D.Items.Add(i.ToString());
            }

        }
        void AddItemsToBusyResPickers()
        {
            //get number of ressources
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();

            //first remove all items
            foreach (Xamarin.Forms.Picker picker in busyResPickerList)
            {
                picker.Items.Clear();
            }

            //add item for every ressource
            for (int i = 0; i <= UInt32.Parse(dvd); i++)
            {
                p_p1_dvd.Items.Add(i.ToString());
                p_p2_dvd.Items.Add(i.ToString());
                p_p3_dvd.Items.Add(i.ToString());
                p_p4_dvd.Items.Add(i.ToString());
                p_p5_dvd.Items.Add(i.ToString());

            }
            for (int i = 0; i <= UInt32.Parse(printer); i++)
            {
                p_p1_printer.Items.Add(i.ToString());
                p_p2_printer.Items.Add(i.ToString());
                p_p3_printer.Items.Add(i.ToString());
                p_p4_printer.Items.Add(i.ToString());
                p_p5_printer.Items.Add(i.ToString());

            }
            for (int i = 0; i <= UInt32.Parse(usb); i++)
            {
                p_p1_usb.Items.Add(i.ToString());
                p_p2_usb.Items.Add(i.ToString());
                p_p3_usb.Items.Add(i.ToString());
                p_p4_usb.Items.Add(i.ToString());
                p_p5_usb.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(bluRay); i++)
            {
                p_p1_bluRay.Items.Add(i.ToString());
                p_p2_bluRay.Items.Add(i.ToString());
                p_p3_bluRay.Items.Add(i.ToString());
                p_p4_bluRay.Items.Add(i.ToString());
                p_p5_bluRay.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(ijPrinter); i++)
            {
                p_p1_ijprinter.Items.Add(i.ToString());
                p_p2_ijprinter.Items.Add(i.ToString());
                p_p3_ijprinter.Items.Add(i.ToString());
                p_p4_ijprinter.Items.Add(i.ToString());
                p_p5_ijprinter.Items.Add(i.ToString());
            }
            for (int i = 0; i <= UInt32.Parse(printer3D); i++)
            {
                p_p1_printer3D.Items.Add(i.ToString());
                p_p2_printer3D.Items.Add(i.ToString());
                p_p3_printer3D.Items.Add(i.ToString());
                p_p4_printer3D.Items.Add(i.ToString());
                p_p5_printer3D.Items.Add(i.ToString());

            }
        }


        void AddPickersToBusyRes()
        {
            // get values from ressource pickers
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();

            //remove all process pickers 
            sl_busyProcess1.Children.Clear();
            sl_busyProcess2.Children.Clear();
            sl_busyProcess3.Children.Clear();
            sl_busyProcess4.Children.Clear();
            sl_busyProcess5.Children.Clear();

            // add labels to process layouts
            sl_busyProcess1.Children.Add(l_busyP1);
            sl_busyProcess2.Children.Add(l_busyP2);
            sl_busyProcess3.Children.Add(l_busyP3);
            sl_busyProcess4.Children.Add(l_busyP4);
            sl_busyProcess5.Children.Add(l_busyP5);
            
            if (dvd != "0")
            {
                sl_busyProcess1.Children.Add(p_p1_dvd);
                sl_busyProcess2.Children.Add(p_p2_dvd);
                sl_busyProcess3.Children.Add(p_p3_dvd);
                sl_busyProcess4.Children.Add(p_p4_dvd);
                sl_busyProcess5.Children.Add(p_p5_dvd);
            }
            if (printer != "0")
            {
                sl_busyProcess1.Children.Add(p_p1_printer);
                sl_busyProcess2.Children.Add(p_p2_printer);
                sl_busyProcess3.Children.Add(p_p3_printer);
                sl_busyProcess4.Children.Add(p_p4_printer);
                sl_busyProcess5.Children.Add(p_p5_printer);
            }
            if (usb != "0")
            {
                sl_busyProcess1.Children.Add(p_p1_usb);
                sl_busyProcess2.Children.Add(p_p2_usb);
                sl_busyProcess3.Children.Add(p_p3_usb);
                sl_busyProcess4.Children.Add(p_p4_usb);
                sl_busyProcess5.Children.Add(p_p5_usb);
            }
            if (bluRay != "0")
            {
                sl_busyProcess1.Children.Add(p_p1_bluRay);
                sl_busyProcess2.Children.Add(p_p2_bluRay);
                sl_busyProcess3.Children.Add(p_p3_bluRay);
                sl_busyProcess4.Children.Add(p_p4_bluRay);
                sl_busyProcess5.Children.Add(p_p5_bluRay);
            }
            if (ijPrinter != "0")
            {
                sl_busyProcess1.Children.Add(p_p1_ijprinter);
                sl_busyProcess2.Children.Add(p_p2_ijprinter);
                sl_busyProcess3.Children.Add(p_p3_ijprinter);
                sl_busyProcess4.Children.Add(p_p4_ijprinter);
                sl_busyProcess5.Children.Add(p_p5_ijprinter);
            }
            if (printer3D != "0")
            {
                sl_busyProcess1.Children.Add(p_p1_printer3D);
                sl_busyProcess2.Children.Add(p_p2_printer3D);
                sl_busyProcess3.Children.Add(p_p3_printer3D);
                sl_busyProcess4.Children.Add(p_p4_printer3D);
                sl_busyProcess5.Children.Add(p_p5_printer3D);
            }
        }
        void AddPickersToUpcomingRes()
        {
            // get values from ressource pickers
            dvd = p_dvd.SelectedItem.ToString();
            printer = p_printer.SelectedItem.ToString();
            usb = p_usb.SelectedItem.ToString();
            bluRay = p_bluRay.SelectedItem.ToString();
            ijPrinter = p_ijPrinter.SelectedItem.ToString();
            printer3D = p_printer3D.SelectedItem.ToString();


            //remove all process pickers
            sl_upcomingProcess1.Children.Clear();
            sl_upcomingProcess2.Children.Clear();
            sl_upcomingProcess3.Children.Clear();
            sl_upcomingProcess4.Children.Clear();
            sl_upcomingProcess5.Children.Clear();

            // add labels to process layouts
            sl_upcomingProcess1.Children.Add(l_upcomingP1);
            sl_upcomingProcess2.Children.Add(l_upcomingP2);
            sl_upcomingProcess3.Children.Add(l_upcomingP3);
            sl_upcomingProcess4.Children.Add(l_upcomingP4);
            sl_upcomingProcess5.Children.Add(l_upcomingP5);

            if (dvd != "0")
            {
                sl_upcomingProcess1.Children.Add(p_p1_upcoming_dvd);
                sl_upcomingProcess2.Children.Add(p_p2_upcoming_dvd);
                sl_upcomingProcess3.Children.Add(p_p3_upcoming_dvd);
                sl_upcomingProcess4.Children.Add(p_p4_upcoming_dvd);
                sl_upcomingProcess5.Children.Add(p_p5_upcoming_dvd);

            }
            if (printer != "0")
            {
                sl_upcomingProcess1.Children.Add(p_p1_upcoming_printer);
                sl_upcomingProcess2.Children.Add(p_p2_upcoming_printer);
                sl_upcomingProcess3.Children.Add(p_p3_upcoming_printer);
                sl_upcomingProcess4.Children.Add(p_p4_upcoming_printer);
                sl_upcomingProcess5.Children.Add(p_p5_upcoming_printer);
            }
            if (usb != "0")
            {
                sl_upcomingProcess1.Children.Add(p_p1_upcoming_usb);
                sl_upcomingProcess2.Children.Add(p_p2_upcoming_usb);
                sl_upcomingProcess3.Children.Add(p_p3_upcoming_usb);
                sl_upcomingProcess4.Children.Add(p_p4_upcoming_usb);
                sl_upcomingProcess5.Children.Add(p_p5_upcoming_usb);
            }
            if (bluRay != "0")
            {
                sl_upcomingProcess1.Children.Add(p_p1_upcoming_bluRay);
                sl_upcomingProcess2.Children.Add(p_p2_upcoming_bluRay);
                sl_upcomingProcess3.Children.Add(p_p3_upcoming_bluRay);
                sl_upcomingProcess4.Children.Add(p_p4_upcoming_bluRay);
                sl_upcomingProcess5.Children.Add(p_p5_upcoming_bluRay);
            }

            if (ijPrinter != "0")
            {
                sl_upcomingProcess1.Children.Add(p_p1_upcoming_ijprinter);
                sl_upcomingProcess2.Children.Add(p_p2_upcoming_ijprinter);
                sl_upcomingProcess3.Children.Add(p_p3_upcoming_ijprinter);
                sl_upcomingProcess4.Children.Add(p_p4_upcoming_ijprinter);
                sl_upcomingProcess5.Children.Add(p_p5_upcoming_ijprinter);
            }
            if (printer3D != "0")
            {
                sl_upcomingProcess1.Children.Add(p_p1_upcoming_printer3D);
                sl_upcomingProcess2.Children.Add(p_p2_upcoming_printer3D);
                sl_upcomingProcess3.Children.Add(p_p3_upcoming_printer3D);
                sl_upcomingProcess4.Children.Add(p_p4_upcoming_printer3D);
                sl_upcomingProcess5.Children.Add(p_p5_upcoming_printer3D);
            }
        }

        /**********************************************************************
        *********************************************************************/
        //If Button Start is clicked
        async void B_Start_Clicked(object sender, EventArgs e)
        {
            //check if resources between 2 and 5
            var countE = 0;
            foreach (Char c in resourceVectorE){
                if (Char.IsDigit(c))
                {
                    countE++;
                }
            }
            if (countE < 3) {
                await DisplayAlert("Alert", "Please define at least 3 resources", "OK");
            }
            else if (countE > 5)
            {
                await DisplayAlert("Alert", "Please define no more than 5 resources", "OK");
            }
            //check busy resources can't be higher than available ressources
            else if (l_busyResourceVectorB.Text.Contains("\nCannot occupy more resources than available!"))
            {
                await DisplayAlert("Alert", "Busy resources can't be higher than available ressources", "OK");
            }
            else
            {
                var exResDict = GetExistingResDict();
                //todo: only use the numbers
                String VE = GetOnlyDigitsInString(l_resourceVectorE.Text);
                String VB = GetOnlyDigitsInString(l_busyResourceVectorB.Text);
                String VC = GetOnlyDigitsInString(l_upcomingVectorC.Text);
                String VA = GetOnlyDigitsInString(l_freeResourceVectorA.Text);
                int totalProcesses = Int16.Parse(p_runningprocesses.SelectedItem.ToString());
                await Navigation.PushAsync(new Deadlock(exResDict, VE, VB, VC, VA, totalProcesses, GetVectorBProcesses(), GetVectorCProcesses()));
            }

        }

        /**********************************************************************
        *********************************************************************/
        String GetOnlyDigitsInString(String givenString)
        {
            String digits = "";
            foreach (Char c in givenString)
            {
                if (Char.IsDigit(c))
                {
                    digits += c;
                }
            }
            return digits;
        }

        Dictionary<String, int> GetExistingResDict()
        {
            var exResDict = new Dictionary<string, int>(){};
            exResDict.Add("dvd", Int16.Parse(p_dvd.SelectedItem.ToString()));
            exResDict.Add("printer", Int16.Parse(p_printer.SelectedItem.ToString()));
            exResDict.Add("usb", Int16.Parse(p_usb.SelectedItem.ToString()));
            exResDict.Add("bluRay", Int16.Parse(p_bluRay.SelectedItem.ToString()));
            exResDict.Add("ijPrinter", Int16.Parse(p_ijPrinter.SelectedItem.ToString()));
            exResDict.Add("printer3D", Int16.Parse(p_printer3D.SelectedItem.ToString()));

            return exResDict;
        }

        public Dictionary<int, String> GetVectorBProcesses()
        {
         
            // create dictionary (keys= numbers 1 to 5 for processes 1 to 5)
            vectorBProcesses = new Dictionary<int, String>() { };

            // Vector values
            String textP1 = ""
                + p_p1_dvd.SelectedItem.ToString()
                + p_p1_printer.SelectedItem.ToString()
                + p_p1_usb.SelectedItem.ToString()
                + p_p1_bluRay.SelectedItem.ToString()
                + p_p1_ijprinter.SelectedItem.ToString()
                + p_p1_printer3D.SelectedItem.ToString();
            String textP2 = ""
                + p_p2_dvd.SelectedItem.ToString()
                + p_p2_printer.SelectedItem.ToString()
                + p_p2_usb.SelectedItem.ToString()
                + p_p2_bluRay.SelectedItem.ToString()
                + p_p2_ijprinter.SelectedItem.ToString()
                + p_p2_printer3D.SelectedItem.ToString();
            String textP3 = ""
                + p_p3_dvd.SelectedItem.ToString()
                + p_p3_printer.SelectedItem.ToString()
                + p_p3_usb.SelectedItem.ToString()
                + p_p3_bluRay.SelectedItem.ToString()
                + p_p3_ijprinter.SelectedItem.ToString()
                + p_p3_printer3D.SelectedItem.ToString();
            String textP4 = ""
                + p_p4_dvd.SelectedItem.ToString()
                + p_p4_printer.SelectedItem.ToString()
                + p_p4_usb.SelectedItem.ToString()
                + p_p4_bluRay.SelectedItem.ToString()
                + p_p4_ijprinter.SelectedItem.ToString()
                + p_p4_printer3D.SelectedItem.ToString();
            String textP5 = ""
                + p_p5_dvd.SelectedItem.ToString()
                + p_p5_printer.SelectedItem.ToString()
                + p_p5_usb.SelectedItem.ToString()
                + p_p5_bluRay.SelectedItem.ToString()
                + p_p5_ijprinter.SelectedItem.ToString()
                + p_p5_printer3D.SelectedItem.ToString();

            // get values from ressource pickers
            dvd = p_dvd.SelectedItem.ToString(); //index 0
            printer = p_printer.SelectedItem.ToString(); //index 1
            usb = p_usb.SelectedItem.ToString(); //index 2
            bluRay = p_bluRay.SelectedItem.ToString(); //index 3
            ijPrinter = p_ijPrinter.SelectedItem.ToString(); //index 4
            printer3D = p_printer3D.SelectedItem.ToString(); //index 5
            // remove unavailable ressources (starting from the last one)
            if (printer3D == "0") {
                textP1 = textP1.Remove(4, 1);
                textP2 = textP2.Remove(5, 1); textP3 = textP3.Remove(5, 1);
                textP4 = textP4.Remove(5, 1); textP5 = textP5.Remove(5, 1);
            }
            if (ijPrinter == "0") {
                textP1 = textP1.Remove(4, 1);
                textP2 = textP2.Remove(4, 1); textP3 = textP3.Remove(4, 1);
                textP4 = textP4.Remove(4, 1); textP5 = textP5.Remove(4, 1);
            }
            if (bluRay == "0") {
                textP1 = textP1.Remove(3, 1);
                textP2 = textP2.Remove(3, 1); textP3 = textP3.Remove(3, 1);
                textP4 = textP4.Remove(3, 1); textP5 = textP5.Remove(3, 1);
            }
            if (usb == "0") {
                textP1 = textP1.Remove(2, 1);
                textP2 = textP2.Remove(2, 1); textP3 = textP3.Remove(2, 1);
                textP4 = textP4.Remove(2, 1); textP5 = textP5.Remove(2, 1);
            }
            if (printer == "0") {
                textP1 = textP1.Remove(1, 1);
                textP2 = textP2.Remove(1, 1); textP3 = textP3.Remove(1, 1);
                textP4 = textP4.Remove(1, 1); textP5 = textP5.Remove(1, 1);
            }
            if (dvd == "0") {
                textP1 = textP1.Remove(0, 1);
                textP2 = textP2.Remove(0, 1); textP3 = textP3.Remove(0, 1);
                textP4 = textP4.Remove(0, 1); textP5 = textP5.Remove(0, 1);
            }

            // add key value pairs to dictionary
            int total = Int16.Parse(p_runningprocesses.SelectedItem.ToString());
            if(total >= 2)
            {
                vectorBProcesses.Add(1, textP1);
                vectorBProcesses.Add(2, textP2);
            }
            if(total >= 3){vectorBProcesses.Add(3, textP3);}
            if(total >= 4){vectorBProcesses.Add(4, textP4);}
            if(total >= 5){vectorBProcesses.Add(5, textP5);}

            return vectorBProcesses;
        }
        public Dictionary<int, String> GetVectorCProcesses()
        {
            // create dictionary (keys= numbers 1 to 5 for processes 1 to 5)
            vectorCProcesses = new Dictionary<int, String>() { };

            // Vector values
            String textP1 = ""
                + p_p1_upcoming_dvd.SelectedItem.ToString()
                + p_p1_upcoming_printer.SelectedItem.ToString()
                + p_p1_upcoming_usb.SelectedItem.ToString()
                + p_p1_upcoming_bluRay.SelectedItem.ToString()
                + p_p1_upcoming_ijprinter.SelectedItem.ToString()
                + p_p1_upcoming_printer3D.SelectedItem.ToString();
            String textP2 = ""
                + p_p2_upcoming_dvd.SelectedItem.ToString()
                + p_p2_upcoming_printer.SelectedItem.ToString()
                + p_p2_upcoming_usb.SelectedItem.ToString()
                + p_p2_upcoming_bluRay.SelectedItem.ToString()
                + p_p2_upcoming_ijprinter.SelectedItem.ToString()
                + p_p2_upcoming_printer3D.SelectedItem.ToString();
            String textP3 = ""
                + p_p3_upcoming_dvd.SelectedItem.ToString()
                + p_p3_upcoming_printer.SelectedItem.ToString()
                + p_p3_upcoming_usb.SelectedItem.ToString()
                + p_p3_upcoming_bluRay.SelectedItem.ToString()
                + p_p3_upcoming_ijprinter.SelectedItem.ToString()
                + p_p3_upcoming_printer3D.SelectedItem.ToString();
            String textP4 = ""
                + p_p4_upcoming_dvd.SelectedItem.ToString()
                + p_p4_upcoming_printer.SelectedItem.ToString()
                + p_p4_upcoming_usb.SelectedItem.ToString()
                + p_p4_upcoming_bluRay.SelectedItem.ToString()
                + p_p4_upcoming_ijprinter.SelectedItem.ToString()
                + p_p4_upcoming_printer3D.SelectedItem.ToString();
            String textP5 = ""
                + p_p5_upcoming_dvd.SelectedItem.ToString()
                + p_p5_upcoming_printer.SelectedItem.ToString()
                + p_p5_upcoming_usb.SelectedItem.ToString()
                + p_p5_upcoming_bluRay.SelectedItem.ToString()
                + p_p5_upcoming_ijprinter.SelectedItem.ToString()
                + p_p5_upcoming_printer3D.SelectedItem.ToString();

            // get values from ressource pickers
            dvd = p_dvd.SelectedItem.ToString(); //index 0
            printer = p_printer.SelectedItem.ToString(); //index 1
            usb = p_usb.SelectedItem.ToString(); //index 2
            bluRay = p_bluRay.SelectedItem.ToString(); //index 3
            ijPrinter = p_ijPrinter.SelectedItem.ToString(); //index 4
            printer3D = p_printer3D.SelectedItem.ToString(); //index 5

            // remove unavailable ressources (starting from the last one)
            if (printer3D == "0")
            {
                textP1 = textP1.Remove(5, 1);
                textP2 = textP2.Remove(5, 1); textP3 = textP3.Remove(5, 1);
                textP4 = textP4.Remove(5, 1); textP5 = textP5.Remove(5, 1);
            }
            if (ijPrinter == "0")
            {
                textP1 = textP1.Remove(4, 1);
                textP2 = textP2.Remove(4, 1); textP3 = textP3.Remove(4, 1);
                textP4 = textP4.Remove(4, 1); textP5 = textP5.Remove(4, 1);
            }
            if (bluRay == "0")
            {
                textP1 = textP1.Remove(3, 1);
                textP2 = textP2.Remove(3, 1); textP3 = textP3.Remove(3, 1);
                textP4 = textP4.Remove(3, 1); textP5 = textP5.Remove(3, 1);
            }
            if (usb == "0")
            {
                textP1 = textP1.Remove(2, 1);
                textP2 = textP2.Remove(2, 1); textP3 = textP3.Remove(2, 1);
                textP4 = textP4.Remove(2, 1); textP5 = textP5.Remove(2, 1);
            }
            if (printer == "0")
            {
                textP1 = textP1.Remove(1, 1);
                textP2 = textP2.Remove(1, 1); textP3 = textP3.Remove(1, 1);
                textP4 = textP4.Remove(1, 1); textP5 = textP5.Remove(1, 1);
            }
            if (dvd == "0")
            {
                textP1 = textP1.Remove(0, 1);
                textP2 = textP2.Remove(0, 1); textP3 = textP3.Remove(0, 1);
                textP4 = textP4.Remove(0, 1); textP5 = textP5.Remove(0, 1);
            }

            // add key value pairs to dictionary
            int total = Int16.Parse(p_runningprocesses.SelectedItem.ToString());
            if (total >= 2)
            {
                vectorCProcesses.Add(1, textP1);
                vectorCProcesses.Add(2, textP2);
            }
            if (total >= 3) { vectorCProcesses.Add(3, textP3); }
            if (total >= 4) { vectorCProcesses.Add(4, textP4); }
            if (total >= 5) { vectorCProcesses.Add(5, textP5); }

            return vectorCProcesses;
        }

        /**********************************************************************
        *********************************************************************/
        async void B_Info_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeadlockHelp());
        }

        /**********************************************************************
        *********************************************************************/
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GC.Collect();
        }

    }
}
