#region References

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

#endregion

namespace TemplateCreator
{
    #region LineGuidesAdorner

    public class LineGuidesAdorner : Adorner
    {
        #region Properties

        public double CanvasWidth
        {
            get { return (double)GetValue(CanvasWidthProperty); }
            set { SetValue(CanvasWidthProperty, value); }
        }

        public static readonly DependencyProperty CanvasWidthProperty =
            DependencyProperty.Register("CanvasWidth", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double CanvasHeight
        {
            get { return (double)GetValue(CanvasHeightProperty); }
            set { SetValue(CanvasHeightProperty, value); }
        }

        public static readonly DependencyProperty CanvasHeightProperty =
            DependencyProperty.Register("CanvasHeight", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        public static readonly DependencyProperty X1Property =
            DependencyProperty.Register("X1", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2", typeof(double), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Constructor

        public LineGuidesAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        #endregion

        #region Pens

        Pen PenGuides = new Pen(new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)), 1.0)
        {
            StartLineCap = PenLineCap.Flat,
            EndLineCap = PenLineCap.Flat,
            LineJoin = PenLineJoin.Miter
        };

        Pen PenElement = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)), 5.0)
        {
            StartLineCap = PenLineCap.Square,
            EndLineCap = PenLineCap.Square,
            LineJoin = PenLineJoin.Miter
        };

        #endregion

        #region OnRender

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (IsEnabled == true)
            {
                double x = X;
                double y = Y;
                double width = CanvasWidth;
                double height = CanvasHeight;
                double offsetX = 0.5;
                double offsetY = -0.5;

                if (x >= 0 && x <= width)
                {
                    var verticalPoint0 = new Point(x + offsetX, 0);
                    var verticalPoint1 = new Point(x + offsetX, height);
                    drawingContext.DrawLine(PenGuides, verticalPoint0, verticalPoint1);
                }

                if (y >= 0 && y <= height)
                {
                    var horizontalPoint0 = new Point(0, y + offsetY);
                    var horizontalPoint1 = new Point(width, y + offsetY);
                    drawingContext.DrawLine(PenGuides, horizontalPoint0, horizontalPoint1);
                }

                var p0 = new Point(X1, Y1);
                var p1 = new Point(X2, Y2);
                drawingContext.DrawLine(PenElement, p0, p1);
            }
        }

        #endregion
    }

    #endregion

    #region MainWindow

    public partial class MainWindow : Window
    {
        #region Fields

        private string appTitle = "Template Creator";

        private bool snapWhenCreating = true;
        private bool snapWhenMoving = true;
        private bool polyLineMode = false;
        private bool scaleMode = false;

        private Line OriginaLine = null;

        private double snapX = 15;
        private double snapY = 15;
        private double snapOffsetX = 0;
        private double snapOffsetY = 5;

        private Stack<string> undo = new Stack<string>();
        private Stack<string> redo = new Stack<string>();

        private LineGuidesAdorner adorner = null;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            canvas.MouseRightButtonDown += Canvas_MouseRightButtonDown;
            canvas.MouseMove += Canvas_MouseMove;

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

            //GridGenerate(PathGrid, 330, 35, 600, 750, 30);
            GridGenerate(PathGrid, 0, snapOffsetY, 1260, 891 - snapOffsetY - 16, 30);
        }

        #endregion

        #region Grid

        public static string GridGenerate(double originX, double originY, double width, double height, double size)
        {
            var sb = new StringBuilder();

            double sizeX = size;
            double sizeY = size;

            // horizontal lines
            for (double y = sizeY + originY; y < height + originY; y += size)
            {
                sb.AppendFormat("M{0},{1}", originX, y);
                sb.AppendFormat("L{0},{1}", width + originX, y);
            }

            // vertical lines
            for (double x = sizeX + originX; x < width + originX; x += size)
            {
                sb.AppendFormat("M{0},{1}", x, originY);
                sb.AppendFormat("L{0},{1}", x, height + originY);
            }

            return sb.ToString();
        }

        public static void GridGenerate(Path path, double originX, double originY, double width, double height, double size)
        {
            string grid = GridGenerate(originX, originY, width, height, size);
            path.Data = Geometry.Parse(grid);
        }

        #endregion

        #region Keyboard Events

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox)
                return;

            HanleKey(e.Key);
        }

        private void HanleKey(Key key)
        {
            bool isControl = (Keyboard.Modifiers & ModifierKeys.Control) > 0;

            if (isControl)
            {
                switch (key)
                {
                    // open
                    case Key.O:
                        {
                            Open();
                        }
                        break;

                    // save
                    case Key.S:
                        {
                            Save();
                        }
                        break;

                    // undo
                    case Key.Z:
                        if (canvas.IsMouseCaptured == false)
                        {
                            PopModel();
                        }
                        break;

                    // redo
                    case Key.Y:
                        if (canvas.IsMouseCaptured == false)
                        {
                            RollbackModel();
                        }
                        break;

                    // reset
                    case Key.R:
                        if (canvas.IsMouseCaptured == false)
                        {
                            PushModel();
                            canvas.Children.Clear();
                        }
                        break;
                }
            }
            else
            {
                switch (key)
                {
                    // scale
                    case Key.S:
                        {
                            scaleMode = !scaleMode;
                        }
                        break;

                    // snap
                    case Key.M:
                        {
                            snapWhenCreating = !snapWhenCreating;
                        }
                        break;

                    // line
                    case Key.L:
                        {
                            polyLineMode = false;
                        }
                        break;

                    // polyline
                    case Key.P:
                        {
                            polyLineMode = true;
                        }
                        break;
                }
            }
        }

        #endregion

        #region Canvas Events

        private void UpdateTitle(double x1, double y1, double x2, double y2)
        {
            double dy = y2 - y1;
            double dx = x2 - x1;
            double theta = Math.Atan2(dy, dx);
            theta *= 180 / Math.PI;

            this.Title = string.Format("{0} [x: {1}, y: {2}, dx: {3}, dy: {4}, theta: {5}]",
                appTitle,
                DoubleToString(Math.Round(x2, 1)),
                DoubleToString(Math.Round(y2, 1)),
                DoubleToString(Math.Round(dx, 1)),
                DoubleToString(Math.Round(dy, 1)),
                DoubleToString(Math.Round(theta, 1)));
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (canvas.IsMouseCaptured)
            {
                this.Title = appTitle;

                canvas.ReleaseMouseCapture();

                if (OriginaLine != null)
                {
                    canvas.Children.Add(OriginaLine);
                    OriginaLine = null;
                }

                RemoveAdorner();
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (canvas.IsMouseCaptured)
            {
                var p = e.GetPosition(canvas);

                double x = (snapWhenCreating == true && snapWhenMoving == true) ? Snap(p.X, snapX, snapOffsetX) : p.X;
                double y = (snapWhenCreating == true && snapWhenMoving == true) ? Snap(p.Y, snapY, snapOffsetY) : p.Y;

                UpdateTitle(adorner.X1, adorner.Y1, x, y);

                if (adorner.X != x)
                {
                    adorner.X = x;
                    adorner.X2 = x;
                }

                if (adorner.Y != y)
                {
                    adorner.Y = y;
                    adorner.Y2 = y;
                }
            }
            else
            {
                var p = e.GetPosition(canvas);

                double x = snapWhenCreating == true ? Snap(p.X, snapX, snapOffsetX) : p.X;
                double y = snapWhenCreating == true ? Snap(p.Y, snapY, snapOffsetY) : p.Y;

                UpdateTitle(x, y, x, y);
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (canvas.IsMouseCaptured)
            {
                if (OriginaLine != null || polyLineMode == false)
                    canvas.ReleaseMouseCapture();

                var p = e.GetPosition(canvas);

                PushModel();

                double x = snapWhenCreating == true ? Snap(p.X, snapX, snapOffsetX) : p.X;
                double y = snapWhenCreating == true ? Snap(p.Y, snapY, snapOffsetY) : p.Y;

                if (((adorner.X1 == adorner.X2) && (adorner.Y1 == adorner.Y2)) == false)
                {
                    var l = CreateLine(adorner.X1, adorner.Y1, x, y);
                    canvas.Children.Add(l);
                }

                if (OriginaLine == null && polyLineMode == true)
                {
                    // create next line
                    adorner.X = x;
                    adorner.Y = y;
                    adorner.X1 = x;
                    adorner.Y1 = y;
                    adorner.X2 = x;
                    adorner.Y2 = y;
                }
                else
                {
                    this.Title = appTitle;

                    RemoveAdorner();
                }

                OriginaLine = null;
            }
            else
            {
                System.Diagnostics.Debug.Print("{0}", e.OriginalSource.GetType());

                if (scaleMode == true)
                {
                    if (e.OriginalSource is Line)
                    {
                        OriginaLine = e.OriginalSource as Line;

                        canvas.Children.Remove(OriginaLine);

                        var p = e.GetPosition(canvas);
                        double x = snapWhenCreating == true ? Snap(p.X, snapX, snapOffsetX) : p.X;
                        double y = snapWhenCreating == true ? Snap(p.Y, snapY, snapOffsetY) : p.Y;

                        double x1 = OriginaLine.X1;
                        double y1 = OriginaLine.Y1;
                        double x2 = OriginaLine.X2;
                        double y2 = OriginaLine.Y2;

                        double dx1 = x1 - x;
                        double dy1 = y1 - y;

                        double dx2 = x2 - x;
                        double dy2 = y2 - y;

                        double d1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);
                        double d2 = Math.Sqrt(dx2 * dx2 + dy2 * dy2);

                        if (d1 > d2)
                        {
                            AddAdorner(x1, y1, x, x);
                            UpdateTitle(x1, y1, x, x);
                        }
                        else
                        {
                            AddAdorner(x2, y2, x, x);
                            UpdateTitle(x2, y2, x, x);
                        }

                        canvas.CaptureMouse();
                    }
                }
                else
                {
                    var p = e.GetPosition(canvas);
                    double x = snapWhenCreating == true ? Snap(p.X, snapX, snapOffsetX) : p.X;
                    double y = snapWhenCreating == true ? Snap(p.Y, snapY, snapOffsetY) : p.Y;

                    AddAdorner(x, y, x, y);

                    UpdateTitle(x, y, x, y);

                    canvas.CaptureMouse();
                }
            }
        }

        #endregion

        #region Adorner

        private void AddAdorner(double x1, double y1, double x2, double y2)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
            adorner = new LineGuidesAdorner(canvas);

            RenderOptions.SetEdgeMode(adorner, EdgeMode.Aliased);

            adorner.CanvasWidth = canvas.Width;
            adorner.CanvasHeight = canvas.Height;
            adorner.X = x2;
            adorner.Y = y2;
            adorner.X1 = x1;
            adorner.Y1 = y1;
            adorner.X2 = x2;
            adorner.Y2 = y2;

            adornerLayer.Add(adorner);

            var setZOrderMethodInfo = adornerLayer.GetType().GetMethod("SetAdornerZOrder", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            setZOrderMethodInfo.Invoke(adornerLayer, new object[] { adorner, 10 });
        }

        private void RemoveAdorner()
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
            adornerLayer.Remove(adorner);
            adorner = null;
        }

        #endregion

        #region Snap

        public double Snap(double original, double snap, double offset)
        {
            return Snap(original - offset, snap) + offset;
        }

        public double Snap(double original, double snap)
        {
            return original + ((Math.Round(original / snap) - original / snap) * snap);
        }

        #endregion

        #region Line

        private void SetLineStart(Line line, double x, double y, bool snap)
        {
            line.X1 = snap == true ? Snap(x, snapX, snapOffsetX) : x;
            line.Y1 = snap == true ? Snap(y, snapY, snapOffsetY) : y;
        }

        private void SetLineEnd(Line line, double x, double y, bool snap)
        {
            line.X2 = snap == true ? Snap(x, snapX, snapOffsetX) : x;
            line.Y2 = snap == true ? Snap(y, snapY, snapOffsetY) : y;
        }

        private Line CreateLine()
        {
            var l = new Line()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 5.0,
                StrokeLineJoin = PenLineJoin.Miter,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square,
                SnapsToDevicePixels = false,
            };

            Panel.SetZIndex(l, 3);

            RenderOptions.SetEdgeMode(l, EdgeMode.Aliased);

            return l;
        }

        private Line CreateLine(double x1, double y1, double x2, double y2)
        {
            var l = CreateLine();

            l.X1 = x1;
            l.Y1 = y1;
            l.X2 = x2;
            l.Y2 = y2;

            return l;
        }

        #endregion

        #region Model

        private string DoubleToString(double value)
        {
            return value.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-GB"));
        }

        private string GenerateModel()
        {
            var lines = canvas.Children.OfType<Line>();
            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                double x1 = line.X1;
                double y1 = line.Y1;
                double x2 = line.X2;
                double y2 = line.Y2;

                sb.AppendFormat("{0};{1};{2};{3};{4}{5}",
                    "line",
                    DoubleToString(x1),
                    DoubleToString(y1),
                    DoubleToString(x2),
                    DoubleToString(y2),
                    Environment.NewLine);
            }

            return sb.ToString();
        }

        private void ParseModel(string model)
        {
            var lines = model.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var args = line.Split(';');

                if (args.Length == 5 &&
                    string.Compare(args[0], "line", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    double x1 = double.Parse(args[1]);
                    double y1 = double.Parse(args[2]);
                    double x2 = double.Parse(args[3]);
                    double y2 = double.Parse(args[4]);

                    var l = CreateLine(x1, y1, x2, y2);
                    canvas.Children.Add(l);
                }
            }
        }

        private void PushModel()
        {
            undo.Push(GenerateModel());
        }

        private void PopModel()
        {
            if (undo.Count <= 0)
                return;

            redo.Push(GenerateModel());
            canvas.Children.Clear();
            ParseModel(undo.Pop());
        }

        private void RollbackModel()
        {
            if (redo.Count <= 0)
                return;

            undo.Push(GenerateModel());
            canvas.Children.Clear();
            ParseModel(redo.Pop());
        }

        public void Open()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Open Model",
            };

            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                using (var reader = new System.IO.StreamReader(dlg.FileName))
                {
                    var model = reader.ReadToEnd();

                    PushModel();
                    ParseModel(model);
                }
            }
        }

        public void Save()
        {
            var dlg = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Save Model",
                FileName = "model"
            };

            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                using (var writer = new System.IO.StreamWriter(dlg.FileName))
                {
                    var model = GenerateModel();
                    writer.Write(model);
                }
            }
        }

        #endregion
    }

    #endregion
}
