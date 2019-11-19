﻿using System;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Diagnostics; //Debug.WriteLine("");
using System.Collections.Generic;

namespace WertheApp.RN
{
    public class DijkstraSettingsDraw
    {
        //VARIABLES
        private SKCanvasView skiaview;
        private int id;

        private static List<DijkstraSettingsDraw> networkList;
        private static float xe, ye;

        private static SKPaint sk_blackText, sk_RouterText, sk_RouterContour, sk_RouterFill, sk_test;
        private static float textSize;
        private SKPoint routerZ, routerU, routerV, routerX, routerW, routerY;

        //CONSTRUCTOR
        public DijkstraSettingsDraw(int id)
        {
            this.id = id;
            networkList = new List<DijkstraSettingsDraw>();
            networkList.Add(this);


            // crate the canvas
            this.skiaview = new SKCanvasView();
            this.skiaview.PaintSurface += PaintSurface;

            textSize = 5;
            //strokeWidth = 0.2f;
        }

        //METHODS
        /**********************************************************************
        *********************************************************************/
        // do the drawing
        void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            //canvas object
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            //Important! Otherwise the drawing will look messed up in iOS
            if (canvas != null){ canvas.Clear();}

            //calculate some stuff and make the paint
            CalculateNeededNumbers(canvas);
            MakeSKPaint(); //depends on xe and ye and therfore has to be called after they were initialized

            /*********************HERE GOES THE DRAWING************************/
            SKRect sk_rBackground = new SKRect(00 * xe, 0 * ye, 100 * xe, 100 * ye); //left , top, right, bottom
            canvas.DrawRect(sk_rBackground, sk_test); //left, top, right, bottom, color

            // define center point for router
            routerZ = new SKPoint(90 * xe, 50 * ye);
            routerU = new SKPoint(10 * xe, 50 * ye);
            routerV = new SKPoint(35 * xe, 25 * ye);
            routerW = new SKPoint(65 * xe, 25 * ye);
            routerX = new SKPoint(35 * xe, 75 * ye);
            routerY = new SKPoint(65 * xe, 75 * ye);

            //draw connections
            switch (this.id)
            {
                case 1: DrawNetwork1(canvas);
                    break;
                case 2: DrawNetwork2(canvas);
                    break;
                case 3: DrawNetwork3(canvas);
                    break;
                case 4: DrawNetwork4(canvas);
                    break;
            }
       
            //draw router
            DrawRouter(canvas, routerZ, "z");
            DrawRouter(canvas, routerU, "u");
            DrawRouter(canvas, routerV, "v");
            DrawRouter(canvas, routerW, "w");
            DrawRouter(canvas, routerX, "x");
            DrawRouter(canvas, routerY, "y");

            

            //execute all drawing actions
            canvas.Flush();
        }
        /**********************************************************************
        *********************************************************************/
        void DrawNetwork1(SKCanvas canvas)
        {
            DrawConnections(canvas, routerX, routerU);
            DrawConnections(canvas, routerX, routerW);
            DrawConnections(canvas, routerX, routerY);
            DrawConnections(canvas, routerU, routerV);
            //DrawConnections(canvas, routerV, routerX);
            DrawConnections(canvas, routerV, routerW);
            DrawConnections(canvas, routerV, routerY);
            //DrawConnections(canvas, routerW, routerY);
            DrawConnections(canvas, routerW, routerZ);
            DrawConnections(canvas, routerZ, routerY);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawNetwork2(SKCanvas canvas)
        {
            DrawConnections(canvas, routerX, routerU);
            DrawConnections(canvas, routerX, routerW);
            DrawConnections(canvas, routerX, routerY);
            DrawConnections(canvas, routerU, routerV);
            DrawConnections(canvas, routerV, routerX);
            DrawConnections(canvas, routerV, routerW);
            //DrawConnections(canvas, routerV, routerY);
            DrawConnections(canvas, routerW, routerY);
            DrawConnections(canvas, routerW, routerZ);
            DrawConnections(canvas, routerZ, routerY);
            SKPoint p = new SKPoint(35 * xe, -35* ye);
            SKPath curveUW = new SKPath();
            curveUW.MoveTo(routerU);
            curveUW.CubicTo(routerU, p, routerW);
            canvas.DrawPath(curveUW, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawNetwork3(SKCanvas canvas)
        {
            DrawConnections(canvas, routerX, routerU);
            DrawConnections(canvas, routerX, routerW);
            DrawConnections(canvas, routerX, routerY);
            DrawConnections(canvas, routerU, routerV);
            DrawConnections(canvas, routerV, routerX);
            DrawConnections(canvas, routerV, routerW);
            //DrawConnections(canvas, routerV, routerY);
            DrawConnections(canvas, routerW, routerY);
            DrawConnections(canvas, routerW, routerZ);
            DrawConnections(canvas, routerZ, routerY);
            SKPoint p = new SKPoint(35 * xe, 135 * ye);
            SKPath curveUY = new SKPath();
            curveUY.MoveTo(routerU);
            curveUY.CubicTo(routerU, p, routerY);
            canvas.DrawPath(curveUY, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawNetwork4(SKCanvas canvas)
        {
            DrawConnections(canvas, routerX, routerU);
            //DrawConnections(canvas, routerX, routerW);
            DrawConnections(canvas, routerX, routerY);
            DrawConnections(canvas, routerU, routerV);
            DrawConnections(canvas, routerV, routerX);
            DrawConnections(canvas, routerV, routerW);
            //DrawConnections(canvas, routerV, routerY);
            DrawConnections(canvas, routerW, routerY);
            DrawConnections(canvas, routerW, routerZ);
            DrawConnections(canvas, routerZ, routerY);

            SKPoint p = new SKPoint(35 * xe, -35 * ye);
            SKPath curveUW = new SKPath();
            curveUW.MoveTo(routerU);
            curveUW.CubicTo(routerU, p, routerW);
            canvas.DrawPath(curveUW, sk_RouterContour);

            SKPoint p2 = new SKPoint(35 * xe, 135 * ye);
            SKPath curveUY = new SKPath();
            curveUY.MoveTo(routerU);
            curveUY.CubicTo(routerU, p2, routerY);
            canvas.DrawPath(curveUY, sk_RouterContour);

            SKPoint p3 = new SKPoint(65 * xe, -35 * ye);
            SKPath curveZV = new SKPath();
            curveZV.MoveTo(routerZ);
            curveZV.CubicTo(routerZ, p3, routerV);
            canvas.DrawPath(curveZV, sk_RouterContour);

            SKPoint p4 = new SKPoint(65 * xe, 135 * ye);
            SKPath curveZX = new SKPath();
            curveZX.MoveTo(routerZ);
            curveZX.CubicTo(routerZ, p4, routerX);
            canvas.DrawPath(curveZX, sk_RouterContour);
        }

        /**********************************************************************
        *********************************************************************/
        void DrawRouter(SKCanvas canvas, SKPoint router, String name)
        {
            float radius = 60;
            float crosslength = radius / 2;
      
            //router
            canvas.DrawCircle(router, radius, sk_RouterFill);
            canvas.DrawCircle(router, radius, sk_RouterContour);

            //x on router
            SKPath cross = new SKPath();
            cross.MoveTo(router.X, router.Y -5);
            cross.RLineTo(-crosslength, -crosslength);
            cross.RLineTo(-crosslength / 5, +crosslength / 5);
            cross.RLineTo(+crosslength, +crosslength);
            cross.RLineTo(-crosslength, +crosslength);
            cross.RLineTo(+crosslength / 5, +crosslength / 5);
            cross.RLineTo(+crosslength, -crosslength);
            cross.RLineTo(+crosslength, +crosslength);
            cross.RLineTo(+crosslength / 5, -crosslength / 5);
            cross.RLineTo(-crosslength, -crosslength);
            cross.RLineTo(+crosslength, -crosslength);
            cross.RLineTo(-crosslength / 5, -crosslength / 5);
            cross.RLineTo(-crosslength, +crosslength);
            canvas.DrawPath(cross, sk_RouterContour);

            //letter on router
            canvas.DrawText(name, router.X, router.Y + 50, sk_RouterText);

        }

        /**********************************************************************
        *********************************************************************/
        void DrawConnections(SKCanvas canvas, SKPoint a, SKPoint b)
        {
            canvas.DrawLine(a, b, sk_RouterContour);
            //make all connections and parameters will be connections for special networks
            //connection v - x

            //connection w - y

        }

        /**********************************************************************
        *********************************************************************/
        static void CalculateNeededNumbers(SKCanvas canvas)
        {
            /*important: the coordinate system starts in the upper left corner*/
            float lborder = canvas.LocalClipBounds.Left;
            float tborder = canvas.LocalClipBounds.Top;
            float rborder = canvas.LocalClipBounds.Right;
            float bborder = canvas.LocalClipBounds.Bottom;

            xe = rborder / 100; //using the variable surfacewidth instead would mess everything up
            ye = bborder / 100;
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
        static private void MakeSKPaint()
        {
            //For Text
            sk_blackText = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = ye * textSize / 1.8f,
                IsAntialias = true,
                IsStroke = false, //TODO: somehow since the newest update this doesnt work anymore for ios
                TextAlign = SKTextAlign.Center,
                IsVerticalText = false
            };

            sk_test = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = 5,
                IsAntialias = true,
                Color = new SKColor(67, 110, 238).WithAlpha(30)
            };

            sk_RouterText = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill,
                TextSize = 45,
                IsAntialias = true,
                //IsStroke = false, //TODO: somehow since the newest update this doesnt work anymore for ios
                TextAlign = SKTextAlign.Center,
                IsVerticalText = false
            };
            

            sk_RouterFill = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = 5,
                IsAntialias = true,
                Color = new SKColor(170, 170, 170)
            };
            sk_RouterContour = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5,
                IsAntialias = true,
                Color = new SKColor(100, 100, 100)
            };
        }

    }
}
