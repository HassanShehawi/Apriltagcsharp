using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using OpenCvSharp;

namespace artags_localizing_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //size of tag is in meters and camera parameters are obtained from calibration
            float tag_size = 0.1F;
            float fx = 1200F;
            float fy = 1400F;
            double px = 817.143;
            double py = 387.159;

            //array of floats to carry values of image points (x and y * 4 points)
            float[] ptsry = new float[8];

            //initialize video capture from camera
            var capt = new OpenCvSharp.VideoCapture();
            capt.Open(0);

            //window for displaying video
            Window window = new Window("capture");

            //main task; display video and find tags
            using (Mat frame = new Mat())
            {
                //looping untill key is pressed
                while (true)
                {
                    //read from camera and show it
                    capt.Read(frame);
                    window.ShowImage(frame);

                    //detect tags and find how many and print the number
                    Apriltag ap = new Apriltag("canny", true, "tag16h5");
                    var current_tags = ap.detect(frame);
                    Console.WriteLine("Number of tags = " + current_tags.Count);
                    Console.WriteLine();
                    
                    //sleep for 10 msec
                    System.Threading.Thread.Sleep(10);

                    //if a key is pressed, close the window and exit
                    if (Cv2.WaitKey(1) >= 0)
                    {
                        capt.Release();
                        Cv2.DestroyAllWindows();
                        break;
                    }
                }
            }
        }
    }
}
