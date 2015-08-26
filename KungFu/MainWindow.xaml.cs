using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Kinect;

namespace KungFu
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly int bytesPerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        private KinectSensor kinectSensor = null;

        private CoordinateMapper coordinateMapper = null;

        private MultiSourceFrameReader multiFrameSourceReader = null;

        private WriteableBitmap bitmap = null; //这个是摄像头

        private WriteableBitmap bitmapTarget = null;//这个是示例图片

        private uint bitmapBackBufferSize = 0;

        private DepthSpacePoint[] colorMappedToDepthPoints = null;

        public MainWindow() {

            this.kinectSensor = KinectSensor.GetDefault();

            this.multiFrameSourceReader = this.kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth | FrameSourceTypes.Color | FrameSourceTypes.BodyIndex);

            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            FrameDescription depthFrameDescreption = this.kinectSensor.DepthFrameSource.FrameDescription;

            int depthWidth = depthFrameDescreption.Width;
            int depthHeight = depthFrameDescreption.Height;

            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;

            int colorWidth = colorFrameDescription.Width;
            int colorHeight = colorFrameDescription.Height;

            this.colorMappedToDepthPoints = new DepthSpacePoint[colorWidth * colorHeight];

            this.bitmap = new WriteableBitmap(colorWidth, colorHeight, 96, 96, PixelFormats.Bgra32, null);

            this.bitmapBackBufferSize = (uint)((this.bitmap.BackBufferStride * (this.bitmap.PixelHeight - 1)) + (this.bitmap.PixelWidth * this.bytesPerPixel));

            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;


            InitializeComponent();
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e) {
            this.StatusText=this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText : 
        }
    }
}
