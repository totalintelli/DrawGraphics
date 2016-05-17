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
            if (isDrwaing)
            {
                switch (Result)
                {
                    case DiagResultFlag.Normal:
                        {
                            #region 정상
                            int RectCount = 12;                                                              // 직사각형의 개수, 입력하는 값은 변경 가능
                            float RectWidth = DrawRect.Width / (float)Math.Round((double)RectCount * 2);     // 직사각형의 너비 
                            float RectHeight = DrawRect.Height * 0.5f;                                       // 직사각형의 높이, 0.5f : 직사각형의 높이를 그리는 부분 높이의 반으로 정함.
                            float Rate_Height = 0.3f;                                                        // 직사각형의 높이의 비율, 0.3f: 처음에 그릴 직사각형을 그리는 부분의 높이의 3분의 1 지점에 오게 함 
                            float RectX = DrawRect.X + DrawRect.Width * 0.1f;                                // 직사각형의 왼쪽 위 모퉁이의 X좌표
                            float RectY = DrawRect.Y + DrawRect.Height * Rate_Height;                        // 직사각형의 왼쪽 위 모퉁이의 Y좌표
                            RectangleF LineRect = new RectangleF(RectX, RectY, RectWidth, RectHeight);       // 직사각형의 정보
                            Brush ResultBackBr = Brushes.CornflowerBlue;                                     // 직사각형의 색상
                            float Distance = 0.0f;                                                           // 직사각형 꼭지점들 사이의 간격 

                            // 직사각형의 개수 - 1 만큼 반복한다.
                            for (int i = 0; i < RectCount; i++)
                            {
                                // 곱셈 여부를 확인한다.
                                if (i % 2 == 0)
                                {
                                    Rate_Height = 0.3f;
                                }
                                else
                                {
                                    if (i % 4 == 1)
                                    {
                                        Rate_Height = 0.2f;
                                    }
                                    else
                                    {
                                        Rate_Height = 0.4f;
                                    }
                                }

                                // 사각형의 왼쪽 위 모퉁이의 X좌표를 구한다.
                                LineRect.X += Distance;

                                // 직사각형의 Y값을 구한다.
                                LineRect.Y = DrawRect.Y + DrawRect.Height * Rate_Height;

                                // 직사각형을 그린다.
                                gr.FillRectangle(ResultBackBr, LineRect);

                                // 직사각형의 꼭지점 사이의 거리를 구한다.
                                Distance = (float)Math.Round(RectWidth * 1.5f);
                            }

                            break;

                            #endregion
                        }
                    case DiagResultFlag.Unbalance:
                        {
                            // Unbalance
                            float OrbitX = DrawRect.X + DrawRect.Width * 0.01f + 2;     // 궤도의 X좌표, 0.01f는 고정값
                            float OrbitY = DrawRect.Y + DrawRect.Height * 0.01f + 2;    // 궤도의 Y좌표, 0.01f는 고정값
                            float OrbitWidth = DrawRect.Width * 0.9f;                   // 궤도의 너비, 0.9f는 궤도가 잘리지 않게 하기 위한 값으로 고정값임.
                            float OrbitHeight = DrawRect.Height * 0.9f;                 // 궤도의 높이, 0.9f는 궤도가 잘리지 않게 하기 위한 값으로 고정값임.
                            float OrbitPenWidth = 1 + DrawRect.Width * 0.01f;           // 궤도의 두께
                            Pen OrbitPen = new Pen(Color.Black, OrbitPenWidth);         // 궤도의 색상
                            SolidBrush CenterCircleBrush = new SolidBrush(Color.Black); // 중앙에 있는 원의 색상
                            float CenterCircleX = DrawRect.X + DrawRect.Width * 0.4f;   // 중앙에 있는 원의 X좌표, 0.4f는 중앙에 있는 원의 위치의 X좌표를 정하는 값으로 고정값임.
                            float CenterCircleY = DrawRect.Y + DrawRect.Height * 0.4f;  // 중앙에 있는 원의 Y좌표, 0.4f는 중앙에 있는 원의 위치의 Y좌표를 정하는 값으로 고정값임.
                            float CenterCircleWidth = DrawRect.Width * 0.15f;           // 중앙에 있는 원의 너비, 0.15f는 오른쪽 원의 너비를 정하는 값으로 고정값임.
                            float CenterCircleHeight = DrawRect.Height * 0.15f;         // 중앙에 있는 원의 높이, 0.15f는 오른쪽 원의 너비를 정하는 값으로 고정값임.
                            SolidBrush RightCircleBrush = new SolidBrush(Color.Red);    // 오른쪽 원의 색상
                            float RightCircleX = DrawRect.X + DrawRect.Width * 0.7f;    // 오른쪽 원의 X좌표, 0.7f는 오른쪽 원의 위치의 X좌표를 정하는 값으로 고정값임.
                            float RightCircleY = DrawRect.Y + DrawRect.Height * 0.1f;   // 오른쪽 원의 Y좌표, 0.1f는 오른쪽 원의 위치의 Y좌표를 정하는 값으로 고정값임.
                            float RightCircleWidth = DrawRect.Width * 0.3f;             // 오른쪽 원의 너비, 0.3f는 오른쪽 원의 너비를 정하는 값으로 고정값임.
                            float RightCircleHeight = DrawRect.Height * 0.3f;           // 오른쪽 원의 높이, 0.3f는 오른쪽 원의 너비를 정하는 값으로 고정값임.

                            // 궤도를 그린다.
                            gr.DrawEllipse(OrbitPen, OrbitX, OrbitY, OrbitWidth, OrbitHeight);

                            // 중앙에 있는 원을 그린다.
                            gr.FillEllipse(CenterCircleBrush, CenterCircleX, CenterCircleY, CenterCircleWidth, CenterCircleHeight);

                            // 오른쪽 원을 그린다.
                            gr.FillEllipse(RightCircleBrush, RightCircleX, RightCircleY, RightCircleWidth, RightCircleHeight);
                            break;
                        }
                    case DiagResultFlag.Rubbing:
                        {
                            //  접촉
                            Pen BigCirclePen = new Pen(Color.Pink, 3.0f);                               // 큰 원의 색상
                            float BigCircleX = DrawRect.X + DrawRect.Width * 0.25f;                     // 큰 원의 X 좌표
                            float BigCircleY = DrawRect.Y + DrawRect.Height * 0.25f;                    // 큰 원의 Y 좌표
                            float BigCircleWidth = DrawRect.Width * 0.5f;                               // 큰 원의 너비
                            float BigCircleHeight = DrawRect.Height * 0.5f;                             // 큰 원의 높이
                            SolidBrush PieBrush = new SolidBrush(Color.Red);                            // 부채꼴의 색상
                            float PieX = DrawRect.X + DrawRect.Width * 0.3f;                           // 부채꼴의 X좌표
                            float PieY = DrawRect.Y + DrawRect.Height * 0.25f; // 부채꼴의 Y좌표
                            float PieWidth = DrawRect.Width * 0.5f;                                    // 부채꼴의 너비
                            float PieHeight = DrawRect.Height * 0.5f;                                  // 부채꼴의 높이

                            // 큰 원을 그린다.
                            gr.DrawEllipse(BigCirclePen, BigCircleX, BigCircleY, BigCircleWidth, BigCircleHeight);

                            
                            // 부채꼴을 큰 원에 겹쳐서 그린다.
                            gr.FillPie(PieBrush, PieX, PieY, PieWidth, PieHeight, 45.0f, 120.0f);

                            // 작은 원을 부채꼴에 겹쳐서 그린다.
                            break;
                        }
                    case DiagResultFlag.Misalignment:
                        {
                            // 오정렬
                            SolidBrush RectBrush = new SolidBrush(Color.Red);
                            float WideRectX = DrawRect.X + DrawRect.Width * 0.1f;      // 넓은 직사각형의 X좌표, 0.1f는 넓은 직사각형을 오른쪽으로 옮기기 위한 값
                            float WideRectY = DrawRect.Y + DrawRect.Height * 0.35f;    // 넓은 직사각형의 Y좌표, 0.35f는 넓은 직사각형을 아래로 내리기 위한 값
                            float WideRectWidth = DrawRect.Width * 0.3f;               // 넓은 직사각형의 너비, 0.3f는 그리기 영역의 너비에 대한 비율으로 수정 가능
                            float WideRectHeight = DrawRect.Height * 0.3f;             // 넓은 직사각형의 높이, 0.3f는 그리기 영역의 높이에 대한 비율으로 수정 가능
                            float NarrowRectX = WideRectX + WideRectWidth;             // 좁은 직사각형의 X좌표
                            float NarrowRectY = DrawRect.Y + DrawRect.Height * 0.083f; // 좁은 직사각형의 Y좌표, 0.083f는 좁은 직사각형을 아래로 내리기 위한 값
                            float NarrowRectWidth = DrawRect.Width * 0.05f;            // 좁은 직사각형의 너비, 0.05f는 그리기 영역의 너비와 좁은 직사각형의 너비에 대한 비율으로 수정 가능
                            float NarrowRectHeight = DrawRect.Height * 0.83f;          // 좁은 직사각형의 높이, 0.83f는 그리기 영역의 높이와 좁은 직사각형의 높이에 대한 비율으로 수정 가능

                            // 넓은 직사각형을 그린다.
                            gr.FillRectangle(RectBrush, WideRectX, WideRectY, WideRectWidth, WideRectHeight);

                            // 좁은 직사각형을 그린다.
                            gr.FillRectangle(RectBrush, NarrowRectX, NarrowRectY, NarrowRectWidth, NarrowRectHeight);

                            // 200도가 기울어진 좁은 직사각형을 그린다.
                            gr.RotateTransform(190.0F);
                            gr.TranslateTransform(NarrowRectX + NarrowRectWidth, NarrowRectY + NarrowRectHeight, MatrixOrder.Append);
                            gr.FillRectangle(RectBrush, -NarrowRectWidth, 0, NarrowRectWidth, NarrowRectHeight);
                            gr.ResetTransform();

                            // 200도가 기울어진 넓은 직사각형을 그린다.
                            gr.RotateTransform(190.0F);
                            gr.TranslateTransform(NarrowRectX + NarrowRectWidth, NarrowRectY + NarrowRectHeight, MatrixOrder.Append);
                            gr.FillRectangle(RectBrush, -WideRectWidth - NarrowRectWidth, NarrowRectHeight * 0.3f, WideRectWidth, WideRectHeight);
                            gr.ResetTransform();
                            break;
                        }
                    case DiagResultFlag.OilWheel:
                        {
                            // 오일휠
                            break;
                        }
                    case DiagResultFlag.Unknown:
                        {
                            // 알수없음.
                            // 원의 색상
                            SolidBrush CircleBrush = new SolidBrush(Color.Black);
                            // 원의 X좌표
                            float CircleX = DrawRect.X + DrawRect.Width * 0.3f;
                            // 원의 Y좌표
                            float CircleY = DrawRect.Y + DrawRect.Height * 0.5f;
                            // 원의 너비
                            float CircleWidth = DrawRect.Width * 0.1f;
                            // 원의 높이
                            float CircleHeight = DrawRect.Height * 0.1f;
                            // 원의 개수
                            int CircleCount = 3;
                            // 원의 간격
                            float Distance = DrawRect.Width / (float)(CircleCount * 2);

                            // 원 세 개를 그린다.
                            for (int i = 0; i < CircleCount; i++)
                            {
                                gr.FillEllipse(CircleBrush, CircleX, CircleY, CircleWidth, CircleHeight);
                                CircleX += Distance;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
