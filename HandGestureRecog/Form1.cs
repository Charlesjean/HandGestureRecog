using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace HandGestureRecog
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        //vide capture devicet

        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        //background images
        Image<Bgr, byte> mBackGround;
        Image<Gray, byte> mBackGroundGray;
        //use to construct background model
        int mFrameCount = 0;

        //frames to detect motion
        Image<Bgr, byte> previousFrame = null;
        Image<Bgr,byte> newFrame = null;
        Image<Bgr,byte> nextFrame = null;

        //face detection
        HaarCascade harrcasc;
        int[,] colorHis = new int[64, 64];
        int[,] bkColorHis = new int[256, 256];
        double totalFacePixels = 0;
        double totalBKPixels = 0;
        Boolean needDetectFace = true;
        int[] regionSize = {120/2,140/2,160/2,180/2};

        float scaleRatio = 0.25f;
        Image<Bgr, byte> drawFrame;

        int frameCount = 0;

        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (videoSource != null)
                videoSource.SignalToStop();
        }

        private void init()
        {
            AllocConsole();
            System.Console.WriteLine("Hello Console");
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            
            videoSource.NewFrame += new NewFrameEventHandler(processNewFrame);
            videoSource.Start();
            harrcasc = new HaarCascade(@"E:\haarcascade_frontalface_alt_tree.xml");
        }


        private void processNewFrame(object sender, NewFrameEventArgs e)// get called when get one new frame
        {
            mFrameCount++;
            if (mFrameCount < 10)// first we need to contruct background image model
            {
                mBackGround = new Image<Bgr, byte>((Bitmap)e.Frame.Clone()).Resize(scaleRatio, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                mBackGroundGray = mBackGround.Convert<Gray, byte>().PyrUp().PyrDown();
                this.pictureBoxBackGround.Image = mBackGround.ToBitmap() as Image;
            }
            else//when we get background image, we use back ground substraction to get segment
            {
                Image<Bgr, byte> newImg = new Image<Bgr, byte>((Bitmap)e.Frame.Clone()).Resize(scaleRatio, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                previousFrame = newFrame;
                newFrame = nextFrame;//new Image<Bgr, byte>((Bitmap)e.Frame.Clone()).Resize(scaleRatio, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                nextFrame = newImg;
                if(previousFrame != null && newFrame != null && nextFrame != null)
                {
                    processFrame();
                    
                }

            }

        }
        private void processFrame()//perform frame process here
        {
            System.Console.WriteLine("Frame {0}:===============================================", frameCount);
            Stopwatch myWatch = new Stopwatch();
            myWatch.Start();
            Image<Gray, byte> grayFrame = newFrame.Convert<Gray, byte>().PyrUp().PyrDown();
            //Image<Gray, float> subTractImg = GradientSubstraction(newFrame);
            Image<Gray, float> motionImg = motionDetection();
            //pictureBoxSubstraction.Image = subTractImg.ToBitmap() as Image;
            //Image<Gray, float> gradientThreshold = getThreshold(subTractImg);
            //Image<Gray, float> motionThreshold = getThreshold(motionImg);
            //Image<Gray, float> motionAndGradientMask = gradientThreshold.And(motionThreshold);
            drawFrame = newFrame.Clone();
            myWatch.Stop();
            System.Console.WriteLine("Init time {0} seconds", myWatch.ElapsedMilliseconds);
            //face detection and update color model
            myWatch.Reset();
            myWatch.Start();
            if (frameCount > int.MaxValue)
                frameCount = 0;
            if(needDetectFace)
            {
                var faces = grayFrame.DetectHaarCascade(harrcasc, 1.1, 2, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING
                                            , new Size(grayFrame.Width / 12, grayFrame.Height / 12))[0];
                if (faces.Length > 0)
                {
                    //needDetectFace = false;
                    Image<Ycc, byte> yuvImg = newFrame.Convert<Ycc, byte>();
                    foreach (var face in faces)
                    {
                        drawFrame.Draw(face.rect, new Bgr(255, 0, 0), 3);
                        this.constructColorHis(face.rect, yuvImg);
                    }
                }
            }
            frameCount++;
            myWatch.Stop();
            System.Console.WriteLine("Face Detection and Color Init Time: {0}", myWatch.ElapsedMilliseconds);
            myWatch.Reset();
            myWatch.Start();
            Image<Gray, double> colorProb = this.getColorLikelyHood(newFrame);
            Image<Gray, double> motionProb = motionImg.Clone().Convert<Gray, double>();
            Image<Gray, double> colorIntergal = new Image<Gray, double>(colorProb.Width, colorProb.Height);
            Image<Gray, double> motionIntergal = new Image<Gray, double>(motionProb.Width, motionProb.Height);

            colorIntergal = colorProb.Integral();
            motionIntergal = motionProb.Integral();
            List<HandRegion> handRegions = this.getHandRect(colorIntergal, motionIntergal);

            myWatch.Stop();
            System.Console.WriteLine("Hand Region Detection time: {0}", myWatch.ElapsedMilliseconds);

            for (int i = 0; i < handRegions.Count; ++i )
            {
                drawFrame.Draw(handRegions[i].rect, new Bgr(0, 0, 255), 1);
                drawFrame.Draw(handRegions[i].innerRect, new Bgr(0, 0, 255), 1);
            }
           //display image 
           // this.pictureBoxGradientAndMotion.Image = motionThreshold.ToBitmap() as Image;
           // this.pictureBoxThreshold.Image = gradientThreshold.ToBitmap() as Image;
            this.pictureBoxSegmentation.Image = motionImg.ToBitmap() as Image;
            this.pictureBoxFinalSegmentation.Image = colorProb.ToBitmap() as Image;//motionAndGradientMask.ToBitmap() as Image;
            //this.pictureBox1.Image = dialateImg.ToBitmap() as Image;
            //this.pictureBoxBackGround.Image = contourImg.ToBitmap() as Image;
            pictureBox2.Image = drawFrame.ToBitmap() as Image;

        }

        //simple background substraction with gray image
        private Image<Gray,byte> GrayColorSubstraction(Image<Gray,byte> bkImg, Image<Gray,byte> frame)
        {
            Gray gray = frame.GetAverage(null);
            Image<Gray, byte> dif = frame.AbsDiff(bkImg);
            Image<Gray,byte> threshold = dif.ThresholdBinary(gray, new Gray(255));
            return threshold;
        }

        //simple background substraction on HSV space
       private Image<Gray,byte> HsvColorSubstraction(Image<Bgr,byte> frame)
        {
            Image<Hsv, byte> hsvFrame = frame.Convert<Hsv, byte>();
            Image<Hsv, byte> hsvBackground = mBackGround.Convert<Hsv, byte>();
            Image<Gray, double> hImg = new Image<Gray, double>(hsvFrame.Width, hsvFrame.Height);

            for (int i = 0; i < hImg.Width; ++i)
                for (int j = 0; j < hImg.Height; ++j )
                {
                    hImg.Data[j, i, 0] =System.Math.Abs( hsvFrame.Data[j, i, 2] - hsvBackground.Data[j, i, 2]);
                }

            return hImg.Convert<Gray, byte>();
        }

        //substraction using gradient origination
        private Image<Gray,float> GradientSubstraction(Image<Bgr,byte> frame)
       {
           Image<Gray, float> gFrame = getGradient(frame);
           Image<Gray, float> gBackground = getGradient(mBackGround);

           return gFrame.AbsDiff(gBackground);
       }

        private Image<Gray,float> getGradient(Image<Bgr,byte> frame)
        {
            Image<Gray, byte> grayFrame = frame.Convert<Gray, byte>();
            Image<Gray,float> xg = grayFrame.Sobel(1, 0, 3);
            Image<Gray, float> yg = grayFrame.Sobel(0, 1, 3);

            Image<Gray, float> gradient = new Image<Gray, float>(xg.Width, xg.Height);
            for(int j = 0; j < grayFrame.Height; ++j)
                for(int i = 0; i < grayFrame.Width; ++i)
                {
                    float xgp = xg.Data[j,i,0];
                    float ygp = yg.Data[j,i,0];
                    gradient.Data[j,i,0] =(float) System.Math.Sqrt(xgp*xgp + ygp* ygp);
                }
            return gradient;
        }

        //use two pass threshold to avoid much noise point
        private Image<Gray,float> getThreshold(Image<Gray,float> toBeThreshold)
        {
            Gray avg = toBeThreshold.GetAverage(null);
            Image<Gray, float> mask = toBeThreshold.ThresholdBinary(avg, new Gray(255));
            Gray avg2 = toBeThreshold.GetAverage(mask.Convert<Gray,byte>());
            return toBeThreshold.ThresholdBinary(avg2, new Gray(255));
        }

        private Image<Gray,float> motionDetection()
        {
            //Image<Gray, float> preFrameGray = previousFrame.Convert<Gray, float>();
            //Image<Gray, float> currentFrameGray = newFrame.Convert<Gray, float>();
            //Image<Gray, float> nextFrameGray = nextFrame.Convert<Gray, float>();

            //Image<Gray, float> diffImg1 = currentFrameGray.AbsDiff(preFrameGray);
            //Image<Gray, float> differImg2 = nextFrameGray.AbsDiff(currentFrameGray);
            Image<Gray, float> diffImg1 = newFrame.AbsDiff(previousFrame).Convert<Gray,float>();
            Image<Gray, float> differImg2 = nextFrame.AbsDiff(newFrame).Convert<Gray,float>();

             return differImg2.And(diffImg1);
        }

        private Image<Gray,float> combineMotionAndGradient(Image<Gray,float> gradient, Image<Gray,float> motion)
        {
            return gradient.And(motion);
        }

        //construct color histagram with cr and cb component of Ycc space
        private void constructColorHis(Rectangle rect, Image<Ycc,byte> frame)
        {
            //construct foreground color histogram
            totalFacePixels = 0;
            totalBKPixels = 0;
            for(int i = 0; i < colorHis.GetLength(0); ++i)
                for(int j = 0; j < colorHis.GetLength(1); ++j)
                {
                    colorHis[i, j] = 0;
                }
            int width = rect.Width;
            int height = rect.Height;
            int padding = width / 8;// use padding to make color distribution more accurate
            width -= 2 * padding;
            height -= 2 * padding;
            Rectangle rect2 = new Rectangle();
            rect2.X = rect.X + padding;
            rect2.Y = rect.Y + padding;
            rect2.Width = width;
            rect2.Height = height;
            for(int row = rect2.Y; row < height + rect2.Y; ++row)
                for(int col = rect2.X; col < width + rect2.X; ++col)
                {
                    Ycc color = frame[row, col];
                    colorHis[(int)color.Cb / 4,(int)color.Cr / 4] += 1;
                    totalFacePixels++;
                }
            //construct background color Histogram
            int bkSize = rect.Width / 3;
            int xs = rect.X - bkSize > 0 ? (rect.X - bkSize) : 0;
            int ys = rect.Y - bkSize > 0 ? (rect.Y - bkSize) : 0;
            int bkWidth = rect.Width + 2 * bkSize < newFrame.Width ? (rect.Width + 2 * bkSize) : newFrame.Width;
            int bkHeight = rect.Height + 2 * bkSize < newFrame.Height ? (rect.Height + 2 * bkSize) : newFrame.Height;

            for(int row = ys; row < rect.Y + rect.Height && row < frame.Height; ++row)
                for(int col = xs; col < xs + bkWidth && col < frame.Width; ++col)
                {
                    if (rect.Contains(new Point(col, row)))
                        continue;
                    Ycc color = frame[row, col];
                    bkColorHis[(int)color.Cb, (int)color.Cr] += 1;
                    totalBKPixels++;
                }
        }
        //get prob map using background and foreground color
        private Image<Gray,double> getColorLikelyHood(Image<Bgr,byte> frame)
        {
            Image<Ycc, byte> yccImg = frame.Convert<Ycc, byte>();
            Image<Gray, double> proImg = new Image<Gray, double>(yccImg.Width, yccImg.Height);
            for (int i = 0; i < yccImg.Height; ++i)
                for (int j = 0; j < yccImg.Width; ++j)
                {
                    Ycc color = yccImg[i, j];
                    double foreProb = colorHis[(int)color.Cb / 4, (int)color.Cr / 4] / totalFacePixels;
                    double bkProb = bkColorHis[(int)color.Cb, (int)color.Cr] / totalBKPixels;
                    if (foreProb + bkProb == 0.0)
                        proImg[i, j] = new Gray(0);
                    else
                        proImg[i, j] = new Gray(foreProb / (foreProb + bkProb) * 255);
                }

            return proImg;
        }

        private List<HandRegion> getHandRect(Image<Gray,double> colProb, Image<Gray,double> motionProb)
        {
            List<HandRegion> handRegions = new List<HandRegion>();
            for (int i = 0; i < 3; ++i)
            {
                HandRegion region = new HandRegion(new Rectangle(0, 0, 0, 0), new Rectangle(0, 0, 0, 0), 0, 0);
                handRegions.Add(region);
            }

            double colorRatio = 1 / 2.0;
            double motionRatio = 1 / 2.0;
            int largestSize = regionSize[0] / 4;
            HandRegion biggest = null;
            double sum = 0;
            for (int row = largestSize; row < colProb.Height - largestSize; ++row)
                for (int col = largestSize; col < colProb.Width - largestSize; ++col)
                {
                    for (int i = 0; i < regionSize.Length; ++i)
                    {
                        int size = regionSize[i];
                        int innerColorSize = (int)(size * colorRatio);
                        int innerMotionSize = (int)(size * motionRatio);
                        Rectangle innerColorRect = new Rectangle(col - innerColorSize / 2, row - innerColorSize / 2, innerColorSize, innerColorSize);
                        Rectangle innerMotionRect = new Rectangle(col - innerMotionSize / 2, row - innerMotionSize / 2, innerMotionSize, innerMotionSize);
                        double innerColorSum = getSum(colProb, innerColorRect);
                        double innerMotionSum = getSum(motionProb, innerMotionRect);

                        Rectangle rect = new Rectangle(col - size / 2, row - size / 2, size, size);
                        double outColorSum = getSum(colProb, rect);
                        double outMotionSum = getSum(motionProb, rect);
                        double colorEnergy = 2 * innerColorSum - outColorSum;
                        double motionEnergy = 2 * innerMotionSum - outMotionSum;

                        if(colorEnergy + motionEnergy > handRegions[0].GetEnergy())
                        {
                            handRegions[0].Copy(new HandRegion(rect, innerColorRect, colorEnergy, motionEnergy));
                            handRegions.Sort();
                        }

                        //test
                       // if(colorEnergy > 0 && motionEnergy > 0)
                        //{
                        //    double energy = colorEnergy + motionEnergy;

                        //    HandRegion newRegion = new HandRegion(rect, innerColorRect, 2 * innerColorSum - outColorSum, 2 * innerMotionSum - outMotionSum);
                        //    int index = this.addNewHandRegion2(newRegion, handRegions);
                        //    if (index != -1)
                        //        handRegions[index].Copy(newRegion);
                        //    handRegions.Sort();
                        //}
                        //end of test

                    }
                }
            return handRegions;      
        }

        private double getSum(Image<Gray,double> integralImg, Rectangle rect)
        {
            Rectangle rectIntegral = new Rectangle();
            rectIntegral.X = rect.X + 1;
            rectIntegral.Y = rect.Y + 1;
            int xend = rectIntegral.X + rect.Width;
            int yend = rectIntegral.Y + rect.Height;

            rectIntegral.X = (xend - rect.Width - 1) > 0 ? (xend - rect.Width - 1) : 0;
            rectIntegral.Y = yend - rect.Height - 1 > 0 ? (yend - rect.Height - 1) : 0;
            rectIntegral.Width = rect.Width + 1 < integralImg.Width ? (rect.Width + 1) : integralImg.Width - 1;
            rectIntegral.Height = rect.Height + 1 < integralImg.Height ? (rect.Height + 1) : integralImg.Height - 1;

            int BRrow = rectIntegral.Y + rectIntegral.Height - 1 < integralImg.Height ? rectIntegral.Y + rectIntegral.Height - 1 : integralImg.Height - 1;
            int BRcol = rectIntegral.X + rectIntegral.Width - 1 < integralImg.Width ? rectIntegral.X + rectIntegral.Width - 1 : integralImg.Width - 1;

            int TLrow = rectIntegral.Y;
            int TLcol = rectIntegral.X;

            int TRrow = rectIntegral.Y;
            int TRcol = rectIntegral.X + rectIntegral.Width - 1 < integralImg.Width ? rectIntegral.X + rectIntegral.Width - 1 : integralImg.Width - 1;

            int BLrow = rectIntegral.Y + rectIntegral.Height - 1 < integralImg.Height ? rectIntegral.Y + rectIntegral.Height - 1 : integralImg.Height - 1;
            int BLcol = rectIntegral.X;

            double n1 = integralImg.Data[BRrow, BRcol, 0];
            double n2 = integralImg.Data[TLrow, TLcol, 0];
            double n3 = integralImg.Data[TRrow, TRcol, 0];
            double n4 = integralImg.Data[BLrow, BLcol, 0];
            return integralImg.Data[BRrow, BRcol, 0] + integralImg.Data[TLrow, TLcol, 0] - integralImg.Data[TRrow, TRcol,0] - integralImg.Data[BLrow, BLcol,0];


        }
        private int addNewHandRegion(HandRegion newRegion, List<HandRegion> regionList)
        {
            if( newRegion.GetEnergy() < 0 )
                return -1;
            if (regionList.Count < 1)
            {
                regionList.Add(newRegion);
                return -1;
            }

            if (regionList.Count >= 3 && newRegion.GetEnergy() < regionList[0].GetEnergy())
                return -1;
            for(int i = 0; i < regionList.Count; ++i)
            {
                if(newRegion.rect.IntersectsWith(regionList[i].rect))
                {
                    Rectangle intersectRec = Rectangle.Intersect(newRegion.rect, regionList[i].rect);
                    int AreaInter = intersectRec.Width * intersectRec.Height;
                    int AreaNew = newRegion.rect.Width * newRegion.rect.Height;
                    int AreaList = regionList[i].rect.Width * regionList[i].rect.Height;
                    if(AreaInter > 0.5 * AreaNew || AreaInter > 0.5 * AreaList)
                    {
                        if (newRegion.GetEnergy() > regionList[i].GetEnergy())
                            return i;
                        else
                            return -1;
                    }
                    
                }
            }
            if(regionList.Count < 3)
            {
                regionList.Add(newRegion);
                return -1;
            }

            return 0;
        }

         private int addNewHandRegion2(HandRegion newRegion, List<HandRegion> regionList)
        {
            if (newRegion.GetEnergy() <= 0)
                return -1;
             if(regionList.Count < 1)
             {
                 regionList.Add(newRegion);
                 return -1;
             }
             if(regionList.Count >= 3)
             {
                 if (newRegion.GetEnergy() < regionList[0].GetEnergy())
                     return -1;
                 //
                 for (int i = 0; i < regionList.Count; ++i)
                 {
                     if (newRegion.rect.IntersectsWith(regionList[i].rect))
                     {
                         Rectangle intersectRec = Rectangle.Intersect(newRegion.rect, regionList[i].rect);
                         int AreaInter = intersectRec.Width * intersectRec.Height;
                         int AreaNew = newRegion.rect.Width * newRegion.rect.Height;
                         int AreaList = regionList[i].rect.Width * regionList[i].rect.Height;
                         if (AreaInter > 0.5 * AreaNew || AreaInter > 0.5 * AreaList)
                         {
                             if (newRegion.GetEnergy() > regionList[i].GetEnergy())
                                 return i;
                             else
                                 return -1;
                         }
                     }
                 }
                 return 0;
                 //
             }
             else
             {
                 //
                 for (int i = 0; i < regionList.Count; ++i)
                 {
                     if (newRegion.rect.IntersectsWith(regionList[i].rect))
                     {
                         Rectangle intersectRec = Rectangle.Intersect(newRegion.rect, regionList[i].rect);
                         int AreaInter = intersectRec.Width * intersectRec.Height;
                         int AreaNew = newRegion.rect.Width * newRegion.rect.Height;
                         int AreaList = regionList[i].rect.Width * regionList[i].rect.Height;
                         if (AreaInter > 0.5 * AreaNew || AreaInter > 0.5 * AreaList)
                         {
                             if (newRegion.GetEnergy() > regionList[i].GetEnergy())
                                 return i;
                             else
                                 return -1;
                         }
                     }
                 }
                 regionList.Add(newRegion);
                 return -1;
                 //

             }
        }
    }
}
