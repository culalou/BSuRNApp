﻿using System;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Collections.Generic;
using System.Text.RegularExpressions; //Regex.IsMatch

/*PLEASE EXCUSE THIS HORRIBLE MESS... I wish I had time to redo everything...*/
namespace WertheApp.CN
{
    public class DijkstraSettingsDraw
    {
        //VARIABLES
        private SKCanvasView skiaview;
        private SKCanvas canvas;
        private int id;
        private static DijkstraSettings dS;
        private String weightUV, weightUX, weightUW,
        weightUY, weightZW, weightZY,
        weightZV, weightZX, weightVX,
        weightVY, weightVW, weightXW,
        weightXY, weightYW;
        private String action;

        private static List<DijkstraSettingsDraw> networkList = new List<DijkstraSettingsDraw>();
        private static SKPaint sk_WeightsText,
            sk_RouterText, sk_RouterContour, sk_RouterFill, sk_test;

        private static SKPoint routerZ, routerU, routerV, routerX, routerW, routerY;
        private static SKPoint wUV, wUX, wUW,
            wUY, wZW, wZY,
            wZV, wZX, wVX,
            wVY, wVW, wXW,
            wXY, wYW;
        private static SKRect rect_wUV, rect_wUX, rect_wUW,
        rect_wUY, rect_wZW, rect_wZY,
        rect_wZV, rect_wZX, rect_wVX,
        rect_wVY, rect_wVW, rect_wXW,
        rect_wXY, rect_wYW;



        //CONSTRUCTOR
        public DijkstraSettingsDraw(int id, DijkstraSettings dijkstraSettings)
        {
            this.id = id;
            networkList.Add(this);
            dS = dijkstraSettings; //TODO

            // crate the canvas
            this.skiaview = new SKCanvasView();
            this.skiaview.PaintSurface += PaintSurface;
            this.skiaview.Touch += OnTouch;
            skiaview.EnableTouchEvents = true;

            SetDefaultWeights();
           
        }

        //METHODS
        public String[] GetAllWeights()
        {
            String[] a = {weightUV, weightUX, weightUW,
        weightUY, weightZW, weightZY,
        weightZV, weightZX, weightVX,
        weightVY, weightVW, weightXW,
        weightXY, weightYW };

            return a;
        }

        /**********************************************************************
        *********************************************************************/
        private async void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:

                    if (rect_wUV.Contains(e.Location))
                    {
                        //Debug.WriteLine("UV clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightUV, action);
                    }
                    else if (rect_wUX.Contains(e.Location))
                    {
                        //Debug.WriteLine("UX clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightUX, action);
                    }
                    else if (rect_wUW.Contains(e.Location)
                        && ( this.id == 2 || this.id == 4))
                    {
                        //Debug.WriteLine("UW clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightUW, action);
                    }
                    else if (rect_wUY.Contains(e.Location)
                        && (this.id == 3 || this.id == 4))
                    {
                        //Debug.WriteLine("UY clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightUY, action);
                    }
                    else if (rect_wZW.Contains(e.Location))
                    {
                        //Debug.WriteLine("ZW clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightZW, action);
                    }
                    else if (rect_wZY.Contains(e.Location))
                    {
                        //Debug.WriteLine("ZY clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightZY, action);
                    }
                    else if (rect_wZV.Contains(e.Location) && this.id == 4)
                    {
                        //Debug.WriteLine("ZV clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightZV, action);
                    }
                    else if (rect_wZX.Contains(e.Location) && this.id == 4)
                    {
                        //Debug.WriteLine("ZX clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightZX, action);
                    }
                    else if (rect_wVX.Contains(e.Location)
                        && (this.id == 2 || this.id == 3 || this.id == 4))
                    {
                        //Debug.WriteLine("VX clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightVX, action);
                    }
                    else if (rect_wVY.Contains(e.Location) && this.id == 1)
                    {
                        //Debug.WriteLine("VY clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightVY, action);
                    }
                    else if (rect_wVW.Contains(e.Location))
                    {
                        //Debug.WriteLine("VW clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightVW, action);
                    }
                    else if (rect_wXW.Contains(e.Location)
                        && (this.id == 1 || this.id == 2 || this.id == 3))
                    {
                        //Debug.WriteLine("XW clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightXW, action);
                    }
                    else if (rect_wXY.Contains(e.Location))
                    {
                        //Debug.WriteLine("XY clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightXY, action);
                    }
                    else if (rect_wYW.Contains(e.Location)
                        && (this.id == 2 || this.id == 3 || this.id == 4))
                    {
                        //Debug.WriteLine("YW clicked");
                        await dS.OpenPickerPopUp();
                        SetWeight(ref weightYW, action);
                    }
                    break;
            }

            e.Handled = true;
        }
        /**********************************************************************
        *********************************************************************/
        public void DrawWeights()
        {
            switch (this.id)
            {
                case 1:
                    DrawWeightsNetwork1();
                    break;
                case 2:
                    DrawWeightsNetwork2();
                    break;
                case 3:
                    DrawWeightsNetwork3();
                    break;
                case 4:
                    DrawWeightsNetwork4();
                    break;
            }
        }

        /**********************************************************************
        *********************************************************************/
        public void DrawRouters()
        {
            DrawRouter(routerZ, "Z");
            DrawRouter(routerU, "U");
            DrawRouter(routerV, "V");
            DrawRouter(routerW, "W");
            DrawRouter(routerX, "X");
            DrawRouter(routerY, "Y");
        }

        /**********************************************************************
        *********************************************************************/
        public void DrawConnections()
        {
            switch (this.id)
            {
                case 1:
                    DrawConnectionsNetwork1();
                    break;
                case 2:
                    DrawConnectionsNetwork2();
                    break;
                case 3:
                    DrawConnectionsNetwork3();
                    break;
                case 4:
                    DrawConnectionsNetwork4();
                    break;
            }
        }

        /**********************************************************************
        *********************************************************************/
        void DrawWeightsNetwork1()
        {
            this.canvas.DrawText(weightUV, wUV, sk_WeightsText);
            this.canvas.DrawText(weightUX, wUX, sk_WeightsText);
            this.canvas.DrawText(weightZW, wZW, sk_WeightsText);
            this.canvas.DrawText(weightZY, wZY, sk_WeightsText);
            this.canvas.DrawText(weightXY, wXY, sk_WeightsText);
            this.canvas.DrawText(weightVW, wVW, sk_WeightsText);
            this.canvas.DrawText(weightVY, wVY, sk_WeightsText);
            this.canvas.DrawText(weightXW, wXW, sk_WeightsText);

        }

        /**********************************************************************
        *********************************************************************/
        void DrawWeightsNetwork2()
        {
            this.canvas.DrawText(weightUV, wUV, sk_WeightsText);
            this.canvas.DrawText(weightUX, wUX, sk_WeightsText);
            this.canvas.DrawText(weightZW, wZW, sk_WeightsText);
            this.canvas.DrawText(weightZY, wZY, sk_WeightsText);
            this.canvas.DrawText(weightXY, wXY, sk_WeightsText);
            this.canvas.DrawText(weightVW, wVW, sk_WeightsText);
            this.canvas.DrawText(weightXW, wXW, sk_WeightsText);
            this.canvas.DrawText(weightVX, wVX, sk_WeightsText);
            this.canvas.DrawText(weightYW, wYW, sk_WeightsText);
            this.canvas.DrawText(weightUW, wUW, sk_WeightsText);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawWeightsNetwork3()
        {
            this.canvas.DrawText(weightUV, wUV, sk_WeightsText);
            this.canvas.DrawText(weightUX, wUX, sk_WeightsText);
            this.canvas.DrawText(weightZW, wZW, sk_WeightsText);
            this.canvas.DrawText(weightZY, wZY, sk_WeightsText);
            this.canvas.DrawText(weightXY, wXY, sk_WeightsText);
            this.canvas.DrawText(weightVW, wVW, sk_WeightsText);
            this.canvas.DrawText(weightXW, wXW, sk_WeightsText);
            this.canvas.DrawText(weightVX, wVX, sk_WeightsText);
            this.canvas.DrawText(weightYW, wYW, sk_WeightsText);
            this.canvas.DrawText(weightUY, wUY, sk_WeightsText);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawWeightsNetwork4()
        {
            this.canvas.DrawText(weightUV, wUV, sk_WeightsText);
            this.canvas.DrawText(weightUX, wUX, sk_WeightsText);
            this.canvas.DrawText(weightZW, wZW, sk_WeightsText);
            this.canvas.DrawText(weightZY, wZY, sk_WeightsText);
            this.canvas.DrawText(weightXY, wXY, sk_WeightsText);
            this.canvas.DrawText(weightVW, wVW, sk_WeightsText);
            this.canvas.DrawText(weightVX, wVX, sk_WeightsText);
            this.canvas.DrawText(weightYW, wYW, sk_WeightsText);
            this.canvas.DrawText(weightUW, wUW, sk_WeightsText);
            this.canvas.DrawText(weightUY, wUY, sk_WeightsText);
            this.canvas.DrawText(weightZV, wZV, sk_WeightsText);
            this.canvas.DrawText(weightZX, wZX, sk_WeightsText);

        }

        /**********************************************************************
        *********************************************************************/
        void DrawConnectionsNetwork1()
        {
            DrawConnection(routerX, routerU);
            DrawConnection(routerX, routerW);
            DrawConnection(routerX, routerY);
            DrawConnection(routerU, routerV);
            //DrawConnections(canvas, routerV, routerX);
            DrawConnection(routerV, routerW);
            DrawConnection(routerV, routerY);
            //DrawConnections(canvas, routerW, routerY);
            DrawConnection(routerW, routerZ);
            DrawConnection(routerZ, routerY);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawConnectionsNetwork2()
        {
            DrawConnection(routerX, routerU);
            DrawConnection(routerX, routerW);
            DrawConnection(routerX, routerY);
            DrawConnection(routerU, routerV);
            DrawConnection(routerV, routerX);
            DrawConnection(routerV, routerW);
            //DrawConnections(canvas, routerV, routerY);
            DrawConnection(routerW, routerY);
            DrawConnection(routerW, routerZ);
            DrawConnection(routerZ, routerY);
            SKPoint p = new SKPoint(xPercent(0.15f), -yPercent(0.3f));
            SKPath curveUW = new SKPath();
            curveUW.MoveTo(routerU);
            curveUW.CubicTo(routerU, p, routerW);
            canvas.DrawPath(curveUW, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawConnectionsNetwork3()
        {
            DrawConnection(routerX, routerU);
            DrawConnection(routerX, routerW);
            DrawConnection(routerX, routerY);
            DrawConnection(routerU, routerV);
            DrawConnection(routerV, routerX);
            DrawConnection(routerV, routerW);
            //DrawConnections(canvas, routerV, routerY);
            DrawConnection(routerW, routerY);
            DrawConnection(routerW, routerZ);
            DrawConnection(routerZ, routerY);
            SKPoint p = new SKPoint(xPercent(0.15f), yPercent(1.30f));
            SKPath curveUY = new SKPath();
            curveUY.MoveTo(routerU);
            curveUY.CubicTo(routerU, p, routerY);
            canvas.DrawPath(curveUY, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawConnectionsNetwork4()
        {
            DrawConnection(routerX, routerU);
            //DrawConnections(canvas, routerX, routerW);
            DrawConnection(routerX, routerY);
            DrawConnection(routerU, routerV);
            DrawConnection(routerV, routerX);
            DrawConnection(routerV, routerW);
            //DrawConnections(canvas, routerV, routerY);
            DrawConnection(routerW, routerY);
            DrawConnection(routerW, routerZ);
            DrawConnection(routerZ, routerY);

            SKPoint p = new SKPoint(xPercent(0.15f), -yPercent(0.3f));
            SKPath curveUW = new SKPath();
            curveUW.MoveTo(routerU);
            curveUW.CubicTo(routerU, p, routerW);
            canvas.DrawPath(curveUW, sk_RouterContour);

            SKPoint p2 = new SKPoint(xPercent(0.15f), yPercent(1.30f));
            SKPath curveUY = new SKPath();
            curveUY.MoveTo(routerU);
            curveUY.CubicTo(routerU, p2, routerY);
            canvas.DrawPath(curveUY, sk_RouterContour);

            SKPoint p3 = new SKPoint(xPercent(0.85f), -yPercent(0.3f));
            SKPath curveZV = new SKPath();
            curveZV.MoveTo(routerZ);
            curveZV.CubicTo(routerZ, p3, routerV);
            canvas.DrawPath(curveZV, sk_RouterContour);

            SKPoint p4 = new SKPoint(xPercent(0.85f), yPercent(1.30f));
            SKPath curveZX = new SKPath();
            curveZX.MoveTo(routerZ);
            curveZX.CubicTo(routerZ, p4, routerX);
            canvas.DrawPath(curveZX, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawRouter(SKPoint router, String name)
        {

            float radius = xPercent(0.07f);
            float crossLength = radius / 3;
            float crossWidth = crossLength / 2;
      
            //router
            this.canvas.DrawCircle(router, radius, sk_RouterFill);
            this.canvas.DrawCircle(router, radius, sk_RouterContour);

            //x on router
            SKPath cross = new SKPath();
            cross.MoveTo(router.X, router.Y -yPercent(0.01f));
            cross.RLineTo(-crossLength, -crossLength);
            cross.RLineTo(-crossWidth, +crossWidth);
            cross.RLineTo(+crossLength, +crossLength);
            cross.RLineTo(-crossLength, +crossLength);
            cross.RLineTo(+crossWidth, +crossWidth);
            cross.RLineTo(+crossLength, -crossLength);
            cross.RLineTo(+crossLength, +crossLength);
            cross.RLineTo(+crossWidth, -crossWidth);
            cross.RLineTo(-crossLength, -crossLength);
            cross.RLineTo(+crossLength, -crossLength);
            cross.RLineTo(-crossWidth, -crossWidth);
            cross.RLineTo(-crossLength, +crossLength);
            canvas.DrawPath(cross, sk_RouterContour);

            //letter on router
            switch (name)
            {
                case "U": this.canvas.DrawText(name, router.X + xPercent(0.1f), router.Y + xPercent(0.02f), sk_RouterText);
                    break;
                case "V":
                    this.canvas.DrawText(name, router.X, router.Y - xPercent(0.1f), sk_RouterText);
                    break;
                case "W":
                    this.canvas.DrawText(name, router.X, router.Y - xPercent(0.1f), sk_RouterText);
                    break;
                case "X":
                    this.canvas.DrawText(name, router.X, router.Y + xPercent(0.15f), sk_RouterText);
                    break;
                case "Y":
                    this.canvas.DrawText(name, router.X, router.Y + xPercent(0.15f), sk_RouterText);
                    break;
                case "Z":
                    this.canvas.DrawText(name, router.X - xPercent(0.1f), router.Y + xPercent(0.02f), sk_RouterText);
                    break;

            }
            

        }

        /**********************************************************************
        *********************************************************************/
        void DrawConnection(SKPoint a, SKPoint b)
        {
            this.canvas.DrawLine(a, b, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        private void SetWeight(ref String o, String w)
        {

            if (w !=  null && Regex.IsMatch(w, "[1-9]"))
            {
               
                o = w;
                this.Paint();
            }

        }

        /**********************************************************************
        *********************************************************************/
        public void SetAction(String a)
        {
            this.action = a;
        }
      

        /**********************************************************************
        *********************************************************************/
        public SKCanvasView ReturnCanvas()
        {
            return this.skiaview;
        }

        /**********************************************************************
        *********************************************************************/
        public static SKCanvasView GetCanvasByID(int id)
        {
            foreach(DijkstraSettingsDraw network in networkList)
            {
                int networkId = network.GetId();
                if(networkId == id)
                {
                    return network.ReturnCanvas();
                }
            }
            return null; //not found
        }

        /**********************************************************************
        *********************************************************************/
        public static void ClearNetworkList()
        {
            networkList.Clear();
        }
        /**********************************************************************
        *********************************************************************/
        public static DijkstraSettingsDraw GetNetworkByID(int id)
        {
            int count = 0;
            foreach (DijkstraSettingsDraw network in networkList)
            {
                count++;
          
                int networkId = network.GetId();
                if (networkId == id)
                {
                    
                    return network;
                }
            }
            
            
            return null; //not found
        }

        /**********************************************************************
        *********************************************************************/
        public int GetId()
        {
            return this.id;
        }

        /**********************************************************************
        *********************************************************************/
        //redraws the canvas
        public void Paint()
        {
            // update the canvas when the data changes
            this.skiaview.InvalidateSurface();

        }

        /**********************************************************************
        *********************************************************************/
        public void SetDefaultWeights()
        {
            switch (this.id)
            {
                case 1:
                    weightUV = "1";
                    weightUX = "1";
                    weightZW = "1";
                    weightZY = "1";
                    weightXY = "1";
                    weightVW = "1";

                    weightVY = "1";
                    weightXW = "1";
                    weightVX = "0";
                    weightYW = "0";
                    weightUW = "0";
                    weightUY = "0";

                    weightZV = "0";
                    weightZX = "0";
                    break;
                case 2:
                    weightUV = "1";
                    weightUX = "1";
                    weightZW = "1";
                    weightZY = "1";
                    weightXY = "1";
                    weightVW = "1";

                    weightVY = "0";
                    weightXW = "1";
                    weightVX = "1";
                    weightYW = "1";
                    weightUW = "1";
                    weightUY = "0";

                    weightZV = "0";
                    weightZX = "0";
                    break;
                case 3:
                    weightUV = "1";
                    weightUX = "1";
                    weightZW = "1";
                    weightZY = "1";
                    weightXY = "1";
                    weightVW = "1";

                    weightVY = "0";
                    weightXW = "1";
                    weightVX = "1";
                    weightYW = "1";
                    weightUW = "0";
                    weightUY = "1";

                    weightZV = "0";
                    weightZX = "0";
                    break;
                case 4:
                    weightUV = "1";
                    weightUX = "1";
                    weightZW = "1";
                    weightZY = "1";
                    weightXY = "1";
                    weightVW = "1";

                    weightVY = "0";
                    weightXW = "0";
                    weightVX = "1";
                    weightYW = "1";
                    weightUW = "1";
                    weightUY = "1";

                    weightZV = "1";
                    weightZX = "1";
                    break;
            }
            this.Paint();
        }


        /**********************************************************************
        *********************************************************************/
        public void SetRandomWeights()
        {
            Random rnd = new Random();
            switch (this.id)
            {
                case 1:
                    weightUV = rnd.Next(1,9).ToString();
                    weightUX = rnd.Next(1, 9).ToString();
                    weightZW = rnd.Next(1, 9).ToString();
                    weightZY = rnd.Next(1, 9).ToString();
                    weightXY = rnd.Next(1, 9).ToString();
                    weightVW = rnd.Next(1, 9).ToString();
                    weightVY = rnd.Next(1, 9).ToString();
                    weightXW = rnd.Next(1, 9).ToString();
                    break;
                case 2:
                    weightUV = rnd.Next(1, 9).ToString();
                    weightUX = rnd.Next(1, 9).ToString();
                    weightZW = rnd.Next(1, 9).ToString();
                    weightZY = rnd.Next(1, 9).ToString();
                    weightXY = rnd.Next(1, 9).ToString();
                    weightVW = rnd.Next(1, 9).ToString();
                    weightXW = rnd.Next(1, 9).ToString();
                    weightVX = rnd.Next(1, 9).ToString();
                    weightYW = rnd.Next(1, 9).ToString();
                    weightUW = rnd.Next(1, 9).ToString();
                    break;
                case 3:
                    weightUV = rnd.Next(1, 9).ToString();
                    weightUX = rnd.Next(1, 9).ToString();
                    weightZW = rnd.Next(1, 9).ToString();
                    weightZY = rnd.Next(1, 9).ToString();
                    weightXY = rnd.Next(1, 9).ToString();
                    weightVW = rnd.Next(1, 9).ToString();
                    weightXW = rnd.Next(1, 9).ToString();
                    weightVX = rnd.Next(1, 9).ToString();
                    weightYW = rnd.Next(1, 9).ToString();
                    weightUY = rnd.Next(1, 9).ToString();
                    break;
                case 4:
                    weightUV = rnd.Next(1, 9).ToString();
                    weightUX = rnd.Next(1, 9).ToString();
                    weightZW = rnd.Next(1, 9).ToString();
                    weightZY = rnd.Next(1, 9).ToString();
                    weightXY = rnd.Next(1, 9).ToString();
                    weightVW = rnd.Next(1, 9).ToString();
                    weightVX = rnd.Next(1, 9).ToString();
                    weightYW = rnd.Next(1, 9).ToString();
                    weightUW = rnd.Next(1, 9).ToString();
                    weightUY = rnd.Next(1, 9).ToString();
                    weightZV = rnd.Next(1, 9).ToString();
                    weightZX = rnd.Next(1, 9).ToString();
                    break;
            }
            this.Paint();
        }

        /**********************************************************************
        *********************************************************************/
        public void SetPresetsWeights()
        {
            switch (this.id)
            {
                case 1:
                    weightUV = "2";
                    weightUX = "1";
                    weightZW = "5";
                    weightZY = "2";
                    weightXY = "1";
                    weightVW = "3";

                    weightVY = "1";
                    weightXW = "3";
                    weightVX = "0";
                    weightYW = "0";
                    weightUW = "0";
                    weightUY = "0";

                    weightZV = "0";
                    weightZX = "0";
                    break;
                case 2:
                    weightUV = "2";
                    weightUX = "1";
                    weightZW = "5";
                    weightZY = "2";
                    weightXY = "1";
                    weightVW = "3";

                    weightVY = "0";
                    weightXW = "3";
                    weightVX = "2";
                    weightYW = "1";
                    weightUW = "5";
                    weightUY = "0";

                    weightZV = "0";
                    weightZX = "0";
                    break;
                case 3:
                    weightUV = "2";
                    weightUX = "1";
                    weightZW = "5";
                    weightZY = "2";
                    weightXY = "1";
                    weightVW = "3";

                    weightVY = "0";
                    weightXW = "3";
                    weightVX = "2";
                    weightYW = "1";
                    weightUW = "0";
                    weightUY = "1";

                    weightZV = "0";
                    weightZX = "0";
                    break;
                case 4:
                    weightUV = "2";
                    weightUX = "1";
                    weightZW = "5";
                    weightZY = "2";
                    weightXY = "1";
                    weightVW = "3";

                    weightVY = "0";
                    weightXW = "0";
                    weightVX = "2";
                    weightYW = "1";
                    weightUW = "5";
                    weightUY = "1";

                    weightZV = "1";
                    weightZX = "1";
                    break;
            }
            this.Paint();
        }

        /**********************************************************************
        *********************************************************************/
        static private void MakeSKPaint()
        {
            sk_test = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = strokeWidth,
                IsAntialias = true,
                Color = new SKColor(67, 110, 238).WithAlpha(30)
            };

            sk_RouterText = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.StrokeAndFill,
                TextSize = xPercent(0.07f),
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
            };
            

            sk_RouterFill = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = strokeWidth,
                IsAntialias = true,
                Color = new SKColor(170, 170, 170)
            };

            sk_RouterContour = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                IsAntialias = true,
                Color = new SKColor(100, 100, 100)
            };

            sk_WeightsText = new SKPaint
            {
                Color = SKColors.BlueViolet,
                Style = SKPaintStyle.StrokeAndFill,
                TextSize = xPercent(0.07f),
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
            };
        }

        /**********************************************************************
       *********************************************************************/
        static void DefineStuff(SKCanvas canvas)
        {

            // define center point for router
            routerZ = new SKPoint(xPercent(0.9f), yPercent(0.5f));
            routerU = new SKPoint(xPercent(0.1f), yPercent(0.5f));
            routerV = new SKPoint(xPercent(0.35f), yPercent(0.25f));
            routerW = new SKPoint(xPercent(0.65f), yPercent(0.25f));
            routerX = new SKPoint(xPercent(0.35f), yPercent(0.75f));
            routerY = new SKPoint(xPercent(0.65f), yPercent(0.75f));

            wUV = new SKPoint(xPercent(0.2f), yPercent(0.375f));
            wUX = new SKPoint(xPercent(0.2f), yPercent(0.675f));
            wZW = new SKPoint(xPercent(0.8f), yPercent(0.375f));
            wZY = new SKPoint(xPercent(0.8f), yPercent(0.675f));
            wVW = new SKPoint(xPercent(0.5f), yPercent(0.235f));
            wXY = new SKPoint(xPercent(0.5f), yPercent(0.81f));
            wVY = new SKPoint(xPercent(0.415f), yPercent(0.435f));
            wXW = new SKPoint(xPercent(0.585f), yPercent(0.435f));
            wVX = new SKPoint(xPercent(0.315f), yPercent(0.5f));
            wYW = new SKPoint(xPercent(0.685f), yPercent(0.5f));
            wUW = new SKPoint(xPercent(0.1f), yPercent(0.235f));
            wUY = new SKPoint(xPercent(0.1f), yPercent(0.8f));
            wZX = new SKPoint(xPercent(0.9f), yPercent(0.8f));
            wZV = new SKPoint(xPercent(0.9f), yPercent(0.235f));

            //define rectangles for touch
            rect_wUV = new SKRect(wUV.X - 50, wUV.Y - 70, wUV.X + 50, wUV.Y + 30);
            rect_wUX = new SKRect(wUX.X - 50, wUX.Y - 70, wUX.X + 50, wUX.Y + 30);
            rect_wUW = new SKRect(wUW.X - 50, wUW.Y - 70, wUW.X + 50, wUW.Y + 30);
            rect_wUY = new SKRect(wUY.X - 50, wUY.Y - 70, wUY.X + 50, wUY.Y + 30);
            rect_wZW = new SKRect(wZW.X - 50, wZW.Y - 70, wZW.X + 50, wZW.Y + 30);
            rect_wZY = new SKRect(wZY.X - 50, wZY.Y - 70, wZY.X + 50, wZY.Y + 30);
            rect_wZV = new SKRect(wZV.X - 50, wZV.Y - 70, wZV.X + 50, wZV.Y + 30);
            rect_wZX = new SKRect(wZX.X - 50, wZX.Y - 70, wZX.X + 50, wZX.Y + 30);
            rect_wVX = new SKRect(wVX.X - 50, wVX.Y - 70, wVX.X + 50, wVX.Y + 30);
            rect_wVY = new SKRect(wVY.X - 50, wVY.Y - 70, wVY.X + 50, wVY.Y + 30);
            rect_wVW = new SKRect(wVW.X - 50, wVW.Y - 70, wVW.X + 50, wVW.Y + 30);
            rect_wXW = new SKRect(wXW.X - 50, wXW.Y - 70, wXW.X + 50, wXW.Y + 30);
            rect_wXY = new SKRect(wXY.X - 50, wXY.Y - 70, wXY.X + 50, wXY.Y + 30);
            rect_wYW = new SKRect(wYW.X - 50, wYW.Y - 70, wYW.X + 50, wYW.Y + 30);
        }
        /**********************************************************************
        *********************************************************************/
        // canvas
        private static SKImageInfo info; // canvas info
        private static float centerX, centerY, x1, x2, y1, y2; // canvas coordinates

        // painting tools
        private static float strokeWidth; // stroke Width for paint colors
        private static void CalculateNeededVariables()
        {
            /*important: the coordinate system starts in the upper left corner*/
            strokeWidth = 5;
            centerX = info.Width / 2;
            centerY = info.Height / 2;
            x1 = strokeWidth / 2;
            y1 = strokeWidth / 2;
            x2 = info.Width - strokeWidth / 2;
            y2 = info.Height - strokeWidth / 2;

            //Debug.WriteLine(string.Format($" centerX: {centerX}, centerY {centerY}, x1: {x1}, x2: {x2}, y1: {y1}, y2: {y2}"));
        }
        static float xPercent(float p)
        {
            float percent = (info.Width - strokeWidth / 2) * p;
            return percent;
        }
        static float yPercent(float p)
        {
            float percent = (info.Height - strokeWidth / 2) * p;
            return percent;
        }

        /**********************************************************************
        *********************************************************************/
        // do the drawing
        void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            info = e.Info; //
            //canvas object
            SKSurface surface = e.Surface;
            canvas = surface.Canvas;

            //Important! Otherwise the drawing will look messed up in iOS
            if (canvas != null) { canvas.Clear(); }

            //calculate some stuff and make the paint
            CalculateNeededVariables();
            DefineStuff(canvas);
            MakeSKPaint(); //depends on xe and ye and therfore has to be called after they were initialized

            /*********************HERE GOES THE DRAWING************************/
            SKRect sk_rBackground = new SKRect(x1, y1, x2, y2); //left , top, right, bottom
            canvas.DrawRect(sk_rBackground, sk_test); //left, top, right, bottom, color

            //draw Network
            DrawConnections();
            DrawRouters();
            DrawWeights();

            //execute all drawing actions
            canvas.Flush();
        }



    }
}
