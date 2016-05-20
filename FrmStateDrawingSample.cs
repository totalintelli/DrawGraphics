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
    public partial class FrmStateDrawingSample : Form
    {
        // 베어링 결과 타입
        public enum DiagResultFlag
        {
            /// <summary>
            /// 정상
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 불균형
            /// </summary>
            Unbalance = 2,

            /// <summary>
            /// 접촉
            /// </summary>
            Rubbing = 3,

            /// <summary>
            /// 오정렬
            /// </summary>
            Misalignment = 4,

            /// <summary>
            /// 오일휠
            /// </summary>
            OilWheel = 5,

            /// <summary>
            /// 알수없음.
            /// </summary>
            Unknown = 6
        }

        public FrmStateDrawingSample()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 500;
            this.Height = 300;
        }

        private void FrmStateDrawingSample_SizeChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Padding RectMargin = new System.Windows.Forms.Padding(30, 30, 30, 30);
            float WidthRate = (panel1.ClientRectangle.Width - (RectMargin.Right + RectMargin.Left)) / 3;
            float HeightRate = (panel1.ClientRectangle.Height - (RectMargin.Top + RectMargin.Bottom)) / 2;
            float HeightRateDouble = WidthRate * 2f;

            RectangleF Rect_Normal = new RectangleF(RectMargin.Left + 0, RectMargin.Top + 0, WidthRate, HeightRate);
            RectangleF Rect_Unbalance = new RectangleF(RectMargin.Left + WidthRate, RectMargin.Top + 0, WidthRate, HeightRate);
            RectangleF Rect_Rubbing = new RectangleF(RectMargin.Left + HeightRateDouble, RectMargin.Top + 0, WidthRate, HeightRate);

            RectangleF Rect_Misalignment = new RectangleF(RectMargin.Left + 0, RectMargin.Top + HeightRate, WidthRate, HeightRate);
            RectangleF Rect_OilWheel = new RectangleF(RectMargin.Left + WidthRate, RectMargin.Top + HeightRate, WidthRate, HeightRate);
            RectangleF Rect_Unknown = new RectangleF(RectMargin.Left + HeightRateDouble, RectMargin.Top + HeightRate, WidthRate, HeightRate);

            RectangleF[] Rects =
            {
                new RectangleF(RectMargin.Left + 0, RectMargin.Top + 0, WidthRate, HeightRate),
                new RectangleF(RectMargin.Left + WidthRate, RectMargin.Top + 0, WidthRate, HeightRate),
                new RectangleF(RectMargin.Left + HeightRateDouble, RectMargin.Top + 0, WidthRate, HeightRate),
                new RectangleF(RectMargin.Left + 0, RectMargin.Top + HeightRate, WidthRate, HeightRate),
                new RectangleF(RectMargin.Left + WidthRate, RectMargin.Top + HeightRate, WidthRate, HeightRate),
                new RectangleF(RectMargin.Left + HeightRateDouble, RectMargin.Top + HeightRate, WidthRate, HeightRate)
            };

            String NormalText = "정상";
            String UnbalanceText = "불균형";
            String RubbingText = "접촉";
            String MisalignmentText = "오정렬";
            String OilWheelText = "오일휠";
            String UnknownText = "모름";
            Font DrawFont = new Font("Arial", 12);
            SolidBrush DrawBrush = new SolidBrush(Color.Black);
            PointF DrawNormalTextPoint = new PointF(RectMargin.Left, RectMargin.Top + 5);
            PointF DrawUnbalanceTextPoint = new PointF(RectMargin.Left + WidthRate, RectMargin.Top + 5);
            PointF DrawRubbingTextPoint = new PointF(RectMargin.Left + HeightRateDouble, RectMargin.Top + 5);
            PointF DrawMisalignmentTextPoint = new PointF(RectMargin.Left, RectMargin.Top + HeightRate + 5);
            PointF DrawOilWheelTextPoint = new PointF(RectMargin.Left + WidthRate, RectMargin.Top + HeightRate + 5);
            PointF DrawUnknownTextPoint = new PointF(RectMargin.Left + HeightRateDouble, RectMargin.Top + HeightRate + 5);

            DrawStateType(e.Graphics, Rect_Normal, DiagResultFlag.Normal);
            e.Graphics.DrawRectangles(Pens.Black, Rects);
            e.Graphics.DrawString(NormalText, DrawFont, DrawBrush, DrawNormalTextPoint);

            DrawStateType(e.Graphics, Rect_Unbalance, DiagResultFlag.Unbalance);
            e.Graphics.DrawString(UnbalanceText, DrawFont, DrawBrush, DrawUnbalanceTextPoint);

            DrawStateType(e.Graphics, Rect_Rubbing, DiagResultFlag.Rubbing);
            e.Graphics.DrawString(RubbingText, DrawFont, DrawBrush, DrawRubbingTextPoint);

            DrawStateType(e.Graphics, Rect_Misalignment, DiagResultFlag.Misalignment);
            e.Graphics.DrawString(MisalignmentText, DrawFont, DrawBrush, DrawMisalignmentTextPoint);

            DrawStateType(e.Graphics, Rect_OilWheel, DiagResultFlag.OilWheel);
            e.Graphics.DrawString(OilWheelText, DrawFont, DrawBrush, DrawOilWheelTextPoint);

            DrawStateType(e.Graphics, Rect_Unknown, DiagResultFlag.Unknown);
            e.Graphics.DrawString(UnknownText, DrawFont, DrawBrush, DrawUnknownTextPoint);

        }


        // 타입에 다른 Drawing
        private void DrawStateType(Graphics gr, RectangleF DrawRect, DiagResultFlag Result)
        {
            // 실제 타입에 따른 그래프 그리는 구역
            bool isDrwaing = true;

            // 1. 유효성 검사 = NULL인 경우 체크
            if (gr == null && DrawRect == null)
                isDrwaing = false;
            // 2. 유효성 검사 = 높이 또는 너비값이 0 보다 작은 경우
            else if (DrawRect.Width <= 0 && DrawRect.Height <= 0)
                isDrwaing = false;

            // 실제 타입에 따른 그래프 그리는 구역
            if (isDrwaing && DrawRect.Width > 0 && DrawRect.Height > 0)
            {
                switch (Result)
                {
                    case DiagResultFlag.Normal:
                        {
                            #region 정상
                            // 직사각형의 개수
                            int RectCount = 12; // 입력하는 값은 변경 가능
                            // 직사각형의 높이에 대한 비율
                            float Rate_Height = 0.3f;
                            // 직사각형을 그리는 영역
                            // 1.1f는 직사각형의 너비를 보정하기 위한 값으로 고정값.
                            // 0.5f는 직사각형의 높이를 그리는 부분 높이의 반으로 정함.
                            // 0.3f는 처음에 그릴 직사각형을 그리는 부분의 높이의 3분의 1 지점에 오게 함.
                            // 0.05f는 첫 번째 직사각형의 위치를 정하는 값으로 고정값.
                            RectangleF Rect = new RectangleF(DrawRect.X + DrawRect.Width * 0.05f, DrawRect.Y + DrawRect.Height * Rate_Height,
                                                    DrawRect.Width / (RectCount * 1.1f), DrawRect.Height * 0.5f);

                            // 직사각형의 색상        
                            Brush ResultBackBr = Brushes.CornflowerBlue;
                            // 직사각형들 사이의 간격                                    
                            RectangleF HideRect = new RectangleF(Rect.X, Rect.Y, Rect.Width * 0.2f, Rect.Height); // 0.2f : 간격의 너비를 보정하는 값으로 고정값. 

                            // 직사각형의 개수 - 1 만큼 반복한다.
                            for (int i = 0; i < RectCount; i++)
                            {
                                // 곱셈 여부를 확인한다.
                                if (i % 2 == 0)
                                {
                                    Rate_Height = 0.3f;     // 0.3f는 직사각형의 중간 위치를 정하기 위한 값으로 고정값.
                                }
                                else
                                {
                                    if (i % 4 == 1)
                                    {
                                        Rate_Height = 0.2f; // 0.2f는 직사각형을 위로 올리기 위한 값으로 고정값.
                                    }
                                    else
                                    {
                                        Rate_Height = 0.4f; // 0.4f는 직사각형을 아래로 내리기 위한 값으로 고정값.
                                    }
                                }

                                if (i > 0)
                                {
                                    // 사각형의 왼쪽 위 모퉁이의 X좌표를 구한다.
                                    Rect.X += Rect.Width;
                                    HideRect.X = Rect.X;
                                }

                                // 직사각형의 Y값을 구한다.
                                Rect.Y = DrawRect.Y + DrawRect.Height * Rate_Height;
                                HideRect.Y = Rect.Y;

                                // 직사각형을 그린다.
                                gr.FillRectangle(ResultBackBr, Rect);

                                // 직사각형들 사이의 간격이 없어지는 현상을 해결하기 위한 코드
                                if (HideRect.Width < 1)
                                {
                                    HideRect.Width = 1.0f; // 1.0f는 직사각형들 사이의 간격의 최소값으로 고정값.
                                }
                                gr.FillRectangle(new SolidBrush(Color.White), HideRect);
                            }

                            break;

                            #endregion
                        }
                    case DiagResultFlag.Unbalance:
                        {
                            #region Unbalance

                            // 궤도
                            // 0.05f는 궤도의 위치에 대한 값으로 고정값.
                            // 0.9f는 궤도가 잘리지 않게 하기 위한 값으로 고정값.
                            RectangleF OrbitRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.05f, DrawRect.Y + DrawRect.Height * 0.05f,
                                                                DrawRect.Width * 0.9f, DrawRect.Height * 0.9f);
                            // 궤도의 두께
                            float OrbitPenWidth = 1 + DrawRect.Width * 0.01f;           // 1은 두께가 최소한 1이 되게 하기 위한 값으로 고정값임. 0.01f는 두께를 조절하기 위한 값으로 고정값.
                            // 궤도의 색상
                            Pen OrbitPen = new Pen(Color.Black, OrbitPenWidth);
                            // 중앙에 있는 원의 색상     
                            SolidBrush CenterCircleBrush = new SolidBrush(Color.Black);
                            // 중앙에 있는 원
                            // 0.43f는 중앙에 있는 원의 위치의 정하는 값으로 고정값.
                            // 0.15f는 오른쪽 원의 너비와 높이를 정하는 값으로 고정값.
                            RectangleF CenterCircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.43f, DrawRect.Y + DrawRect.Height * 0.43f,
                                                                DrawRect.Width * 0.15f, DrawRect.Height * 0.15f);
                            // 오른쪽 원의 색상
                            SolidBrush RightCircleBrush = new SolidBrush(Color.Red);
                            // 오른쪽에 있는 원
                            // 0.7f는 오른쪽 원의 위치의 X좌표를 정하는 값으로 고정값.
                            // 0.1f는 오른쪽 원의 위치의 Y좌표를 정하는 값으로 고정값.
                            // 0.2f는 오른쪽 원의 너비와 높이를 정하는 값으로 고정값.
                            RectangleF RightCircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.7f, DrawRect.Y + DrawRect.Height * 0.1f,
                                                                    DrawRect.Width * 0.2f, DrawRect.Height * 0.2f);
                            // 궤도를 그린다.
                            gr.DrawEllipse(OrbitPen, OrbitRect);

                            // 중앙에 있는 원을 그린다.
                            gr.FillEllipse(CenterCircleBrush, CenterCircleRect);

                            // 오른쪽 원을 그린다.
                            gr.FillEllipse(RightCircleBrush, RightCircleRect);

                            break;

                            #endregion
                        }
                    case DiagResultFlag.Rubbing:
                        {
                            #region 접촉

                            // 큰 원의 색상
                            Pen BigCirclePen = new Pen(Color.Pink, 3.0f);
                            // 큰 원
                            // 0.05f는 그림이 그리기 영역을 구분하는 선과 떼어놓기 위한 값으로 고정값.
                            // 0.9f는 그리기 영역의 너비에 대한 큰 원의 너비의 비율이나 그리기 영역의 높이에 대한 큰 원의 높이의 비율로 고정값.
                            RectangleF BigCircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.05f, DrawRect.Y + DrawRect.Height * 0.05f,
                                                                DrawRect.Width * 0.9f, DrawRect.Height * 0.9f);
                            // 부채꼴의 색상
                            SolidBrush PieBrush = new SolidBrush(Color.Red);
                            // 부채꼴
                            // 0.99f는 큰 원의 모서리가 보이게 하기 위한 값.
                            RectangleF PieRect = new RectangleF(BigCircleRect.X, BigCircleRect.Y, BigCircleRect.Width * 0.99f, BigCircleRect.Height * 0.99f);
                            // 부채꼴의 첫 번째 부분의 각도
                            float StartAngle = 10.0f;                                    // 10.0f는 고정값.
                            // 부채꼴의 두 번째 부분의 각도                             
                            float SweepAngle = 140.0f;                                   // 140.0f는 고정값.
                            // 부채꼴의 색상                                   
                            SolidBrush SmallCircleBrush = new SolidBrush(Color.Pink);
                            // 작은 원
                            // 0.25f는 작은 원의 위치를 정하기 위한 값으로 변경 불가.
                            // 0.6f는 그리기 영역의 너비에 대한 작은 원의 너비의 비율이면서 그리기 영역의 높이에 대한 작은 원의 높이의 비율이자 고정값.
                            RectangleF SmallCircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.25f, DrawRect.Y + DrawRect.Height * 0.25f,
                                                                        DrawRect.Width * 0.6f, DrawRect.Height * 0.6f);

                            // 큰 원을 그린다.
                            gr.DrawEllipse(BigCirclePen, BigCircleRect);

                            // 부채꼴을 큰 원에 겹쳐서 그린다.
                            if (PieRect.Width > 0 && PieRect.Height > 0)
                            {
                                gr.FillPie(PieBrush, PieRect.X, PieRect.Y, PieRect.Width, PieRect.Height, StartAngle, SweepAngle);
                            }

                            // 작은 원을 부채꼴에 겹쳐서 그린다.
                            gr.FillEllipse(SmallCircleBrush, SmallCircleRect);

                            break;

                            #endregion
                        }
                    case DiagResultFlag.Misalignment:
                        {
                            #region 오정렬

                            // 넓은 직사각형의 색상
                            SolidBrush RectBrush = new SolidBrush(Color.Red);
                            // 넓은 직사각형
                            // 0.1f는 넓은 직사각형을 오른쪽으로 옮기기 위한 값.
                            // 0.35f는 넓은 직사각형을 아래로 내리기 위한 값.
                            // 0.3f는 그리기 영역의 너비에 대한 비율이자 그리기 영역의 높이에 대한 비율로 고정값.
                            RectangleF WideRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.1f, DrawRect.Y + DrawRect.Height * 0.35f,
                                                                DrawRect.Width * 0.3f, DrawRect.Height * 0.3f);
                            // 좁은 직사각형
                            // 0.083f는 좁은 직사각형을 아래로 내리기 위한 값
                            // 0.05f는 그리기 영역의 너비와 좁은 직사각형의 너비에 대한 비율으로 고정값
                            // 0.83f는 그리기 영역의 높이와 좁은 직사각형의 높이에 대한 비율으로 고정값
                            RectangleF NarrowRect = new RectangleF(WideRect.X + WideRect.Width, DrawRect.Y + DrawRect.Height * 0.083f,
                                                                DrawRect.Width * 0.05f, DrawRect.Height * 0.83f);

                            // 넓은 직사각형을 그린다.
                            gr.FillRectangle(RectBrush, WideRect);

                            // 좁은 직사각형을 그린다.
                            gr.FillRectangle(RectBrush, NarrowRect);

                            // 기울어진 좁은 직사각형을 그린다. 190.0f는 좁은 직사각형을 기울이기 위한 값. 0.01f는 직사각형의 기울기를 조절하기 위한 값으로 고정값.
                            gr.RotateTransform(185.0f + DrawRect.Width * 0.01f - DrawRect.Height * 0.01f);
                            gr.TranslateTransform(NarrowRect.X + NarrowRect.Width, NarrowRect.Y + NarrowRect.Height, MatrixOrder.Append);
                            gr.FillRectangle(RectBrush, NarrowRect.Width - DrawRect.Width * 0.1f, 0, NarrowRect.Width, NarrowRect.Height);
                            gr.ResetTransform();

                            // 기울어진 넓은 직사각형을 그린다. 190.0f는 넓은 직사각형을 기울이기 위한 값. 0.01f는 직사각형의 기울기를 조절하기 위한 값으로 고정값.
                            gr.RotateTransform(185.0f + DrawRect.Width * 0.01f - DrawRect.Height * 0.01f);
                            gr.TranslateTransform(NarrowRect.X + NarrowRect.Width, NarrowRect.Y + NarrowRect.Height, MatrixOrder.Append);
                            // NarrowRectHeight * 0.3f는 기울어진 넓은 직사각형의 높이이고 고정값.
                            gr.FillRectangle(RectBrush, -WideRect.Width - NarrowRect.Width, NarrowRect.Height * 0.3f, WideRect.Width, WideRect.Height);
                            gr.ResetTransform();

                            break;

                            #endregion
                        }
                    case DiagResultFlag.OilWheel:
                        {
                            #region 오일휠

                            // 반원의 색상
                            SolidBrush HalfCircleBrush = new SolidBrush(Color.DarkRed);
                            // 반원
                            // 0.1f는 반원의 높이를 보정하기 위한 값으로 고정값.
                            // 0.8f는 반원의 너비를 보정하기 위한 값이고 반원의 높이를 보정하기 위한 값으로 고정값.
                            RectangleF HalfCircleRect = new RectangleF(DrawRect.X, DrawRect.Y + DrawRect.Height * 0.1f,
                                                                    DrawRect.Width * 0.8f, DrawRect.Height * 0.8f);
                            // 반원의 첫 번째 부분의 각도
                            float StartAngle = 270.0f;                                // 270.0f는 고정값임.
                            // 반원의 두 번재 부분의 각도
                            float SweepAngle = 180.0f;                                // 180.0f는 고정값임.
                            // 작은 원
                            // 0.2f는 작은 원의 위치와 너비를 보정하기 위한 값이자 고정값.
                            // 0.5f는 작은 원의 높이를 보정하기 위한 값이자 고정값.
                            // 0.4f는 작은 원의 위치를 보정하기 위한 값으로 고정값.
                            RectangleF SmallCircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.2f, DrawRect.Y + DrawRect.Height * 0.5f,
                                                                        DrawRect.Width * 0.4f, DrawRect.Height * 0.4f);
                            // 가리는 원의 색상
                            SolidBrush HideCircleBrush = new SolidBrush(Color.White);
                            // 가리는 원
                            // 0.2f는 가리는 원의 위치를 정하기 위한 값으로 고정값.
                            // 0.1f는 가리는 원의 위치를 정하기 위한 값으로 고정값.
                            // 0.4f는 가리는 원의 너비와 높이를 보정하기 위한 값으로 고정값.
                            RectangleF HideCircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.2f, DrawRect.Y + DrawRect.Height * 0.1f,
                                                                       DrawRect.Width * 0.4f, DrawRect.Height * 0.4f);
                            // 가리기 위한 GraphicsPath
                            GraphicsPath HideClipPath = new GraphicsPath();
                            HideClipPath.AddEllipse(HideCircleRect);

                            // 기포의 색상
                            SolidBrush BubbleBrush = new SolidBrush(Color.White);
                            // 기포 
                            // 0.4f와 0.7f는 기포의 위치를 정하기 위한 값으로 고정값. 
                            // 0.2f는 기포의 너비를 정하기 위한 값으로 고정값.
                            // 0.1f는 기포의 높이를 정하기 위한 값으로 고정값.
                            RectangleF BubbleRect
                                = new RectangleF(DrawRect.X + DrawRect.Width * 0.4f, DrawRect.Y + DrawRect.Height * 0.7f,
                                                            DrawRect.Width * 0.2f, DrawRect.Height * 0.1f);
                            // 기포를 넣기 위한 GraphicsPath
                            GraphicsPath BubbleClipPath = new GraphicsPath();
                            BubbleClipPath.AddEllipse(BubbleRect);
                            // 그래픽 컨테이너에 오일휠 그림을 넣는다.
                            GraphicsContainer ContainerState = gr.BeginContainer();

                            // 반원의 너비와 높이가 0보다 큰 지 확인한다.
                            if (HalfCircleRect.Width > 0 && HalfCircleRect.Height > 0)
                            {
                                // 물방울 꼬리를 만든다.
                                gr.SetClip(HideClipPath, CombineMode.Exclude);
                                // 기포를 넣는다.
                                gr.SetClip(BubbleClipPath, CombineMode.Exclude);
                                // 반원을 그린다.
                                gr.FillPie(HalfCircleBrush, HalfCircleRect.X, HalfCircleRect.Y, HalfCircleRect.Width, HalfCircleRect.Height, StartAngle, SweepAngle);
                            }

                            // 반원의 아랫 부분에 작은 원을 붙인다.
                            gr.FillEllipse(HalfCircleBrush, SmallCircleRect.X, SmallCircleRect.Y, SmallCircleRect.Width, SmallCircleRect.Height);

                            // 그래픽 컨테이너를 끝낸다.
                            gr.EndContainer(ContainerState);

                            break;

                            #endregion
                        }
                    case DiagResultFlag.Unknown:
                        {
                            #region 알수없음.

                            // 원의 색상
                            SolidBrush CircleBrush = new SolidBrush(Color.Black);
                            // 원
                            // 0.3f는 원의 위치를 정하기 위한 값으로 고정값.
                            // 0.5f는 원의 위치를 정하기 위한 값으로 고정값.
                            // 0.1f는 원의 너비와 높이를 정하기 위한 값으로 고정값.
                            RectangleF CircleRect = new RectangleF(DrawRect.X + DrawRect.Width * 0.3f, DrawRect.Y + DrawRect.Height * 0.5f,
                                                                    DrawRect.Width * 0.1f, DrawRect.Height * 0.1f);
                            // 원의 개수
                            int CircleCount = 3;                                        // 3은 고정값.
                            // 원의 간격
                            float Distance = DrawRect.Width / (float)(CircleCount * 2); // 2는 원의 간격을 정하기 위한 값으로 고정값.

                            // 원 세 개를 그린다.
                            float Value = CircleRect.X;

                            for (int i = 0; i < CircleCount; i++)
                            {
                                gr.FillEllipse(CircleBrush, Value, CircleRect.Y, CircleRect.Width, CircleRect.Height);
                                Value += Distance;
                            }

                            break;

                            #endregion
                        }
                    default:
                        break;
                }
            }
        }
    }
}
