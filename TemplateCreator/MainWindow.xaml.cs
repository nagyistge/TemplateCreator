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
    #region Tool

    public enum Tool
    {
        None,
        Line,
        Polyline,
        Path,
        Rect,
        Circle,
        Text,
        Move,
        Scale,
        Rotate,
        Duplicate
    }

    #endregion

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

        public Tool Tool
        {
            get { return (Tool)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
        }

        public static readonly DependencyProperty ToolProperty =
            DependencyProperty.Register("Tool", typeof(Tool), typeof(LineGuidesAdorner),
                new FrameworkPropertyMetadata(Tool.None,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Constructor

        public LineGuidesAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        #endregion

        #region Pens & Brushes

        Brush BrushRect = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

        Pen PenRect = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)), 0.0)
        {
            StartLineCap = PenLineCap.Flat,
            EndLineCap = PenLineCap.Flat,
            LineJoin = PenLineJoin.Miter
        };

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

                if (Tool == Tool.Line || Tool == Tool.Polyline)
                {
                    var p0 = new Point(X1, Y1);
                    var p1 = new Point(X2, Y2);
                    drawingContext.DrawLine(PenElement, p0, p1);
                }
                else if (Tool == Tool.Rect)
                {
                    var p0 = new Point(X1, Y1);
                    var p1 = new Point(X2, Y2);
                    var rect = new Rect(p0, p1);

                    drawingContext.DrawRectangle(BrushRect, PenRect, rect);
                }
                else if (Tool == Tool.Circle)
                {
                    var p0 = new Point(X1, Y1);
                    var p1 = new Point(X2, Y2);
                    var rect = new Rect(p0, p1);

                    double radiusX = (rect.Right - rect.Left) / 2.0;
                    double radiusY = (rect.Bottom - rect.Top) / 2.0;

                    double centerX = rect.Left + radiusX;
                    double centerY = rect.Top + radiusY;

                    var center = new Point(centerX, centerY);

                    drawingContext.DrawEllipse(BrushRect, PenRect, center, radiusX, radiusY);
                }
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

        private Tool tool = Tool.Line;

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
                    // line
                    case Key.L:
                        {
                            tool = Tool.Line;
                        }
                        break;

                    // polyline
                    case Key.P:
                        {
                            tool = Tool.Polyline;
                        }
                        break;

                    // rect
                    case Key.R:
                        {
                            tool = Tool.Rect;
                        }
                        break;

                    // circle
                    case Key.C:
                        {
                            tool = Tool.Circle;
                        }
                        break;

                    // text
                    case Key.T:
                        {
                            tool = Tool.Text;
                        }
                        break;

                    // move
                    case Key.V:
                        {
                            tool = Tool.Move;
                        }
                        break;

                    // scale
                    case Key.S:
                        {
                            tool = Tool.Scale;
                        }
                        break;

                    // rotate
                    case Key.E:
                        {
                            tool = Tool.Rotate;
                        }
                        break;

                    // duplicate
                    case Key.D:
                        {
                            tool = Tool.Duplicate;
                        }
                        break;

                    // snap
                    case Key.M:
                        {
                            snapWhenCreating = !snapWhenCreating;
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
                if (OriginaLine != null || tool != Tool.Polyline)
                    canvas.ReleaseMouseCapture();

                var p = e.GetPosition(canvas);

                PushModel();

                double x = snapWhenCreating == true ? Snap(p.X, snapX, snapOffsetX) : p.X;
                double y = snapWhenCreating == true ? Snap(p.Y, snapY, snapOffsetY) : p.Y;

                if (tool == Tool.Line || 
                    tool == Tool.Polyline ||
                    (tool == Tool.Scale && OriginaLine != null))
                {
                    if (((adorner.X1 == adorner.X2) && (adorner.Y1 == adorner.Y2)) == false)
                    {
                        var line = CreateLine(adorner.X1, adorner.Y1, x, y);
                        canvas.Children.Add(line);
                    }
                }
                else if (tool == Tool.Rect)
                {
                    if (((adorner.X1 == adorner.X2) || (adorner.Y1 == adorner.Y2)) == false)
                    {
                        var rect = CreateRect(adorner.X1, adorner.Y1, x, y);
                        canvas.Children.Add(rect);
                    }
                }
                else if (tool == Tool.Circle)
                {
                    if (((adorner.X1 == adorner.X2) || (adorner.Y1 == adorner.Y2)) == false)
                    {
                        var circle = CreateCircle(adorner.X1, adorner.Y1, x, y);
                        canvas.Children.Add(circle);
                    }
                }

                if (OriginaLine == null && tool == Tool.Polyline)
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

                if (tool == Tool.Scale)
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
                            AddAdorner(x1, y1, x, x, Tool.Line);
                            UpdateTitle(x1, y1, x, x);
                        }
                        else
                        {
                            AddAdorner(x2, y2, x, x, Tool.Line);
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

                    AddAdorner(x, y, x, y, tool);

                    UpdateTitle(x, y, x, y);

                    canvas.CaptureMouse();
                }
            }
        }

        #endregion

        #region Adorner

        private void AddAdorner(double x1, double y1, double x2, double y2, Tool tool)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
            adorner = new LineGuidesAdorner(canvas);

            RenderOptions.SetEdgeMode(adorner, EdgeMode.Aliased);

            adorner.Tool = tool;
            adorner.CanvasWidth = canvas.Width;
            adorner.CanvasHeight = canvas.Height;
            adorner.X = x2;
            adorner.Y = y2;
            adorner.X1 = x1;
            adorner.Y1 = y1;
            adorner.X2 = x2;
            adorner.Y2 = y2;

            adornerLayer.Add(adorner);

            //var setZOrderMethodInfo = adornerLayer.GetType().GetMethod("SetAdornerZOrder", 
            //    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //setZOrderMethodInfo.Invoke(adornerLayer, new object[] { adorner, 5 });
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

        private Line CreateLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 5.0,
                StrokeLineJoin = PenLineJoin.Miter,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square,
                Fill = Brushes.Transparent,
                SnapsToDevicePixels = false
            };

            Panel.SetZIndex(line, 3);

            RenderOptions.SetEdgeMode(line, EdgeMode.Aliased);

            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            return line;
        }

        #endregion

        #region Rect

        private Rectangle CreateRect(double x1, double y1, double x2, double y2)
        {
            var rect = new Rectangle()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 0.0,
                StrokeLineJoin = PenLineJoin.Miter,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square,
                Fill = Brushes.Red,
                SnapsToDevicePixels = false,
            };

            Panel.SetZIndex(rect, 3);

            RenderOptions.SetEdgeMode(rect, EdgeMode.Aliased);

            var p0 = new Point(x1, y1);
            var p1 = new Point(x2, y2);
            var r = new Rect(p0, p1);

            rect.Width = r.Width;
            rect.Height = r.Height;

            Canvas.SetLeft(rect, r.Left);
            Canvas.SetTop(rect, r.Top);

            return rect;
        }

        #endregion

        #region Circle

        private Ellipse CreateCircle(double x1, double y1, double x2, double y2)
        {
            var ellipse = new Ellipse()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 0.0,
                StrokeLineJoin = PenLineJoin.Miter,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square,
                Fill = Brushes.Red,
                SnapsToDevicePixels = false,
            };

            Panel.SetZIndex(ellipse, 3);

            RenderOptions.SetEdgeMode(ellipse, EdgeMode.Aliased);

            var p0 = new Point(x1, y1);
            var p1 = new Point(x2, y2);
            var r = new Rect(p0, p1);

            ellipse.Width = r.Width;
            ellipse.Height = r.Height;

            Canvas.SetLeft(ellipse, r.Left);
            Canvas.SetTop(ellipse, r.Top);

            return ellipse;
        }

        #endregion

        #region Model

        private string DoubleToString(double value)
        {
            return value.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-GB"));
        }

        private string GenerateModel()
        {
            var children = canvas.Children;
            var sb = new StringBuilder();

            foreach (var child in children)
            {
                if (child is Line)
                {
                    var line = child as Line;

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
                else if (child is Rectangle)
                {
                    var rect = child as Rectangle;

                    double x1 = Canvas.GetLeft(rect);
                    double y1 = Canvas.GetTop(rect);
                    double x2 = x1 + rect.Width;
                    double y2 = y1 + rect.Height;

                    sb.AppendFormat("{0};{1};{2};{3};{4}{5}",
                        "rect",
                        DoubleToString(x1),
                        DoubleToString(y1),
                        DoubleToString(x2),
                        DoubleToString(y2),
                        Environment.NewLine);
                }
                else if (child is Ellipse)
                {
                    var circle = child as Ellipse;

                    double x1 = Canvas.GetLeft(circle);
                    double y1 = Canvas.GetTop(circle);
                    double x2 = x1 + circle.Width;
                    double y2 = y1 + circle.Height;
                    
                    sb.AppendFormat("{0};{1};{2};{3};{4}{5}",
                        "circle",
                        DoubleToString(x1),
                        DoubleToString(y1),
                        DoubleToString(x2),
                        DoubleToString(y2),
                        Environment.NewLine);
                }
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
                else if (args.Length == 5 &&
                    string.Compare(args[0], "rect", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    double x1 = double.Parse(args[1]);
                    double y1 = double.Parse(args[2]);
                    double x2 = double.Parse(args[3]);
                    double y2 = double.Parse(args[4]);

                    var r = CreateRect(x1, y1, x2, y2);
                    canvas.Children.Add(r);
                }
                else if (args.Length == 5 &&
                    string.Compare(args[0], "circle", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    double x1 = double.Parse(args[1]);
                    double y1 = double.Parse(args[2]);
                    double x2 = double.Parse(args[3]);
                    double y2 = double.Parse(args[4]);

                    var c = CreateCircle(x1, y1, x2, y2);
                    canvas.Children.Add(c);
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
