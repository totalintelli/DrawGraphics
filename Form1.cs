using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawGraphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //DrawBezierPointF(e);
            //DrawArcRectangle(e);
            //DrawClosedCurvePoint(e);
            //FillPathEllipse(e);
            //DrawClosedCurvePoint(e);
            //FillEllipseInt(e);
            //FillClosedCurvePoint(e);
            //FillHalfEllipseInt(e);
            //DrawNormalStateImage(e.Graphics, new RectangleF(0, 0, panel1.Width, panel1.Height));
            DrawMisArrangementImage(e);
            //DrawUnbalanceImage(e);
            //DrawUnKnownImage(e);

            /*
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw 50x50 pixels.
            e.Graphics.ScaleTransform(50, 50);
            e.Graphics.TranslateTransform(60, 60, MatrixOrder.Append);
            DrawSmiley(e.Graphics);
            e.Graphics.ResetTransform();

            // Draw 50x100 pixels.
            e.Graphics.ScaleTransform(50, 100);
            e.Graphics.TranslateTransform(170, 110, MatrixOrder.Append);
            DrawSmiley(e.Graphics);
            e.Graphics.ResetTransform();

            // Draw 50x30 pixels rotated and flipped vertically.
            e.Graphics.RotateTransform(45, MatrixOrder.Append);
            e.Graphics.ScaleTransform(50, -30, MatrixOrder.Append);
            e.Graphics.TranslateTransform(60, 170, MatrixOrder.Append);
            DrawSmiley(e.Graphics);
            e.Graphics.ResetTransform();
            */
        }

       

        private void DrawBezierPointF(PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points for curve.
            PointF start = new PointF(0.0F, 0.0F);
            PointF control1 = new PointF(0.0F, 0.0F);
            PointF control2 = new PointF(50.0F, 50.0F);
            PointF end = new PointF(100.0F, 0.0F);

            // Draw arc to screen
            e.Graphics.DrawBezier(blackPen, start, control1, control2, end);
        }

        private void DrawArcRectangle(PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create rectangle to bound ellipse.
            Rectangle rect = new Rectangle(0, 0, 100, 200);

            // Create start and sweep angles on ellipse.
            float startAngle = 45.0F;
            float sweepAngle = 270.0F;

            // Draw arc to screen.
            e.Graphics.DrawArc(blackPen, rect, startAngle, sweepAngle);
        }

        private void DrawClosedCurvePoint(PaintEventArgs e)
        {
            // Create pens.
            Pen redPen = new Pen(Color.Red, 3);
            Pen greenPen = new Pen(Color.Green, 3);

            // Create points that define curve.
            Point point1 = new Point(1, 1);
            Point point2 = new Point(10, 2);
            Point point3 = new Point(15, 3);
            Point point4 = new Point(20, 4);
            Point point5 = new Point(25, 5);
            Point[] curvePoints =
             {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
             };

            // Draw lines between original points to screen.
            e.Graphics.DrawLines(redPen, curvePoints);

            // Draw closed curve to screen.
            e.Graphics.DrawClosedCurve(greenPen, curvePoints);

        }

        public void FillPathEllipse(PaintEventArgs e)
        {

            // Create solid brush.
            SolidBrush redBrush = new SolidBrush(Color.Red);

            // Create graphics path object and add ellipse.
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddEllipse(0, 0, 200, 100);

            // Fill graphics path to screen.
            e.Graphics.FillPath(redBrush, graphPath);
        }

        public void FillEllipseInt(PaintEventArgs e)
        {

            // Create solid brush.
            SolidBrush redBrush = new SolidBrush(Color.Red);

            // Create location and size of ellipse.
            int x = 10;
            int y = 10;
            int width = 20;
            int height = 20;

            // Fill ellipse on screen.
            e.Graphics.FillEllipse(redBrush, x, y, width, height);
        }

        public void FillClosedCurvePoint(PaintEventArgs e)
        {

            // Create solid brush.
            SolidBrush redBrush = new SolidBrush(Color.Red);

            //Create array of points for curve.
            Point point1 = new Point(0, 0);
            Point point2 = new Point(10, 20);
            Point point3 = new Point(30, 30);
            Point point4 = new Point(60, 0);
            Point point5 = new Point(50, 0);
            Point point6 = new Point(30, 20);
            Point point7 = new Point(20, 20);
            Point point8 = new Point(10, 0);
            Point point9 = new Point(0, 0);
            Point[] points = { point1, point2, point3, point4, point5, point6, point7, point8, point9 };

            // Fill curve on screen.
            e.Graphics.FillClosedCurve(redBrush, points);
        }

        public void FillHalfEllipseInt(PaintEventArgs e)
        {
            // Create solid brush.
            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Pen bluePen = new Pen(Color.Blue);

            // Create rectangle for clipping region.
            Rectangle clipRect = new Rectangle(0, 50, 200, 100);

            // Set clipping region of graphics to rectangle.
            e.Graphics.SetClip(clipRect);

            // Create location and size of ellipse.
            int x = 10;
            int y = 10;
            int width = 100;
            int height = 100;
            int x2 = 25;
            int y2 = 25;
            int width2 = 80;
            int height2 = 80;

            // Fill ellipse on screen.
            e.Graphics.FillEllipse(redBrush, x, y, width, height);
            e.Graphics.FillEllipse(whiteBrush, x2, y2, width2, height2);
            e.Graphics.DrawRectangle(bluePen, x, y, width, height);

            // Release graphics object.
            e.Graphics.Dispose();
        }

        public void DrawNormalStateImage(Graphics gr, RectangleF DrawRect)
        {
            if (gr != null && DrawRect != null && DrawRect.Width > 0 && DrawRect.Height > 0)
            {
                float RectWidth = DrawRect.Width * 0.04f;                  // 사각형의 너비
                float RectHeight = DrawRect.Height * 0.6f;                 // 사각형의 높이
                float RectX = RectWidth;                                   // 사각형의 왼쪽 위 모퉁이의 X좌표
                float RectY = DrawRect.Height * 0.2f;                      // 사각형의 왼쪽 위 모퉁이의 Y좌표
                RectangleF RectangleF
                    = new RectangleF(RectX, RectY, RectWidth, RectHeight); // 사각형 정보
                int RectCount = 12;                                        // 사각형의 개수
                bool MultiplierIsZero = false;                             // 영을 곱할 것인지 여부
                float Multiplier = 0.2f;                                   // 곱하는 수 
                Brush CfbBrush = Brushes.CornflowerBlue;                   // 정상상태 직사각형의 색상

                // 직사각형의 개수 - 1 만큼 반복한다.
                for (int i = 0; i < RectCount; i++)
                {
                    // 곱셈 여부를 확인한다.
                    if (i % 2 == 0)
                    {
                        Multiplier = 0.2f;
                    }
                    else
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            // 곱하는 수를 구한다.
                            Multiplier = 0;
                            // 곱셈 여부를 구한다. 
                            MultiplierIsZero = false;
                        }
                        else
                        {
                            // 곱하는 수를 구한다.
                            Multiplier = 0.4f;

                            // 곱셈 여부를 구한다.
                            MultiplierIsZero = true;
                        }

                    }

                    //직사각형 왼쪽 위 꼭지점의 좌표를 구한다.
                    RectangleF.Y = DrawRect.Height * Multiplier;

                    // 직사각형을 그린다.
                    gr.FillRectangle(CfbBrush, RectangleF);

                    // 사각형의 왼쪽 위 모퉁이의 X좌표를 구한다.
                    RectangleF.X += RectWidth * 2;

                    // 사각형의 왼쪽 위 모퉁이의 Y좌표를 구한다.
                    RectangleF.Y = DrawRect.Height;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void DrawMisArrangementImage(PaintEventArgs e)
        {
            SolidBrush RectBrush = new SolidBrush(Color.Red);
            float WideRectX = panel1.Location.X;                         // 넓은 직사각형의 X좌표
            float WideRectY = panel1.Location.Y + panel1.Height / 70.0f; // 넓은 직사각형의 Y좌표, 30.0f는 패널의 높이에 대한 넓은 직사각형의 Y축 값의 비율로 수정 가능
            float WideRectWidth = panel1.Width / 30.0f;                  // 넓은 직사각형의 너비, 30.0f는 패널의 너비와 넓은 직사각형의 너비에 대한 비율으로 수정 가능
            float WideRectHeight = panel1.Height / 20.0f;                // 넓은 직사각형의 높이, 20.0f는 패널의 높이와 넓은 직사각형의 높이에 대한 비율으로 수정 가능
            float NarrowRectX = WideRectX + WideRectWidth;               // 좁은 직사각형의 X좌표
            float NarrowRectY = panel1.Location.Y;                       // 좁은 직사각형의 Y좌표
            float NarrowRectWidth = panel1.Width / 100.0f;               // 좁은 직사각형의 너비, 100.0f는 패널의 너비와 좁은 직사각형의 너비에 대한 비율으로 수정 가능
            float NarrowRectHeight = panel1.Height / 12.0f;              // 좁은 직사각형의 높이, 12.0f는 패널의 높이와 좁은 직사각형의 높이에 대한 비율으로 수정 가능

            // 넓은 직사각형을 그린다.
            e.Graphics.FillRectangle(RectBrush, WideRectX, WideRectY, WideRectWidth, WideRectHeight);

            // 좁은 직사각형을 그린다.
            e.Graphics.FillRectangle(RectBrush, NarrowRectX, NarrowRectY, NarrowRectWidth, NarrowRectHeight);

            // 200도가 기울어진 좁은 직사각형을 그린다.
            e.Graphics.RotateTransform(200.0F);
            e.Graphics.TranslateTransform(NarrowRectX + NarrowRectWidth, NarrowRectY + NarrowRectHeight, MatrixOrder.Append);
            e.Graphics.FillRectangle(RectBrush, -NarrowRectWidth, 0, NarrowRectWidth, NarrowRectHeight);

            // 200도가 기울어진 넓은 직사각형을 그린다.
            e.Graphics.RotateTransform(190.0F, MatrixOrder.Append);
            e.Graphics.TranslateTransform(WideRectX + WideRectWidth, WideRectY + WideRectHeight, MatrixOrder.Append);
            e.Graphics.FillRectangle(RectBrush, panel1.Width / 9.5f, panel1.Height / 7.5f, WideRectWidth, WideRectHeight); 
            e.Graphics.ResetTransform();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void DrawUnbalanceImage(PaintEventArgs e)
        {
            Pen OrbitPen = new Pen(Color.Black);                        // 궤도의 색상
            float OrbitX = 10.0f;                                       // 궤도의 X좌표, 10.0f는 고정값
            float OrbitY = 10.0f;                                       // 궤도의 Y좌표, 10.0f는 고정값
            float OrbitWidth = panel1.Width * 0.95f -10;                // 궤도의 너비, 0.95f는 궤도가 잘리지 않게 하기 위한 값으로 고정값임.
            float OrbitHeight = panel1.Height * 0.95f -10;              // 궤도의 높이, 0.95f는 궤도가 잘리지 않게 하기 위한 값으로 고정값임.
            SolidBrush CenterCircleBrush = new SolidBrush(Color.Black); // 중앙에 있는 원의 색상
            float CenterCircleX = panel1.Width * 0.45f;                 // 중앙에 있는 원의 X좌표, 0.45f는 중앙에 있는 원의 위치의 X좌표를 정하는 값으로 고정값임.
            float CenterCircleY = panel1.Height * 0.45f;                // 중앙에 있는 원의 Y좌표, 0.45f는 중앙에 있는 원의 위치의 Y좌표를 정하는 값으로 고정값임.
            float CenterCircleWidth = panel1.Width * 0.06f;             // 중앙에 있는 원의 너비, 0.06f는 오른쪽 원의 너비를 정하는 값으로 고정값임.
            float CenterCircleHeight = panel1.Height * 0.1f;            // 중앙에 있는 원의 높이, 0.1f는 오른쪽 원의 너비를 정하는 값으로 고정값임.
            SolidBrush RightCircleBrush = new SolidBrush(Color.Red);    // 오른쪽 원의 색상
            float RightCircleX = panel1.Width * 0.8f;                   // 오른쪽 원의 X좌표, 0.8f는 오른쪽 원의 위치의 X좌표를 정하는 값으로 고정값임.
            float RightCircleY = panel1.Height * 0.15f;                  // 오른쪽 원의 Y좌표, 0.15f는 오른쪽 원의 위치의 Y좌표를 정하는 값으로 고정값임.
            float RightCircleWidth = panel1.Width * 0.12f;              // 오른쪽 원의 너비, 0.12f는 오른쪽 원의 너비를 정하는 값으로 고정값임.
            float RightCircleHeight = panel1.Height * 0.2f;             // 오른쪽 원의 높이, 0.2f는 오른쪽 원의 너비를 정하는 값으로 고정값임.

            // 궤도를 그린다.
            e.Graphics.DrawEllipse(OrbitPen, OrbitX, OrbitY, OrbitWidth, OrbitHeight);

            // 중앙에 있는 원을 그린다.
            e.Graphics.FillEllipse(CenterCircleBrush, CenterCircleX, CenterCircleY, CenterCircleWidth, CenterCircleHeight);

            // 오른쪽 원을 그린다.
            e.Graphics.FillEllipse(RightCircleBrush, RightCircleX, RightCircleY, RightCircleWidth, RightCircleHeight);
        }

        private void DrawUnKnownImage(PaintEventArgs e)
        {
            // 원의 색상
            SolidBrush CircleBrush = new SolidBrush(Color.Black);
            // 원의 X좌표
            float CircleX = panel1.Width * 0.01f;
            // 원의 Y좌표
            float CircleY = panel1.Height * 0.5f;
            // 원의 너비
            float CircleWidth = panel1.Width * 0.1f;
            // 원의 높이
            float CircleHeight = panel1.Height * 0.1f;
            // 원의 개수
            int CircleCount = 3;
            // 원의 간격
            float Distance = panel1.Width / (float)(CircleCount * 2);

            // 원 세 개를 그린다.
            for(int i = 0; i < CircleCount; i++)
            {
                e.Graphics.FillEllipse(CircleBrush, CircleX, CircleY, CircleWidth, CircleHeight);
                CircleX += Distance;
            }
        }

        // Draw a smiley face in -1 <= x <= 1, -1 <= y <= 1.
        private void DrawSmiley(Graphics gr)
        {
            using (Pen thin_pen = new Pen(Color.Blue, 0))
            {
                gr.FillEllipse(Brushes.Yellow, -1, -1, 2, 2);
                gr.DrawEllipse(thin_pen, -1, -1, 2, 2);
                gr.DrawArc(thin_pen, -0.75f, -0.75f, 1.5f, 1.5f, 0, 180);
                gr.FillEllipse(Brushes.Red, -0.2f, -0.2f, 0.4f, 0.6f);
                gr.FillEllipse(Brushes.White, -0.5f, -0.6f, 0.3f, 0.5f);
                gr.DrawEllipse(thin_pen, -0.5f, -0.6f, 0.3f, 0.5f);
                gr.FillEllipse(Brushes.Black, -0.4f, -0.5f, 0.2f, 0.3f);
                gr.FillEllipse(Brushes.White, 0.2f, -0.6f, 0.3f, 0.5f);
                gr.DrawEllipse(thin_pen, 0.2f, -0.6f, 0.3f, 0.5f);
                gr.FillEllipse(Brushes.Black, 0.3f, -0.5f, 0.2f, 0.3f);
            }
        }
    }
}
