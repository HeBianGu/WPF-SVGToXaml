using HeBianGu.Product.SvgBase;
using Microsoft.Win32;
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

namespace HeBianGu.Product.SvgConvertTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            var result = open.ShowDialog();

            if (result == null || !result.Value) return;

            if (open.FileName == null) return;

            if (!open.FileName.ToLower().EndsWith(".svg")) return;

            this.list_svgsource.Items.Add(new FileEntity(open.FileName));

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void list_svgsource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.list_svgsource.SelectedValue == null) return;

            FileEntity file = this.list_svgsource.SelectedValue as FileEntity;

            this.ConvertToImage(file);
            this.ConvertToData(file);
            this.ConvertToPathDrawing(file);
            this.ConvertToPathXaml(file);
        }

        void ConvertToImage(FileEntity file)
        {

            SVG svg = SVGReader.Read(file.FilePath);

            this.image_svg.Source = new DrawingImage(svg.GetDrawing());
        }

        void ConvertToData(FileEntity file)
        {
            SVG svg = SVGReader.Read(file.FilePath);

            this.cv_data.Width = svg.Width;
            this.cv_data.Height = svg.Height;

            System.Windows.Shapes.Path p = new System.Windows.Shapes.Path();

            p.Data = svg.GetGeometry();
            p.Stroke = Brushes.Red;
            this.cv_data.Children.Clear();
            this.cv_data.Children.Add(p);
        }

        void ConvertToPathDrawing(FileEntity file)
        {
            SVG svg = SVGReader.Read(file.FilePath);

            Drawing drawing = svg.GetDrawing();

            DrawingGroup group = drawing as DrawingGroup;

            Action<DrawingGroup> drawingAction = null;

            this.tx_code.Text = null;

            this.cv_pathdraw.Children.Clear();

            this.cv_pathdraw.Width = svg.Width;
            this.cv_pathdraw.Height = svg.Height;

            drawingAction = l =>
            {
                foreach (var item in l.Children)
                {
                    if (item is GeometryDrawing)
                    {
                        GeometryDrawing gd = item as GeometryDrawing;

                        System.Windows.Shapes.Path p = new System.Windows.Shapes.Path();

                        p.Data = gd.Geometry;
                        p.Fill = gd.Brush;
                        p.Stroke = gd.Pen?.Brush;

                        this.tx_code.Text += gd.Geometry.ToString() + Environment.NewLine;
                        this.cv_pathdraw.Children.Add(p);
                    }
                    else if (item is DrawingGroup)
                    {
                        drawingAction(item as DrawingGroup);
                    }
                }

            };

            drawingAction(group);
        }

        void ConvertToPathXaml(FileEntity file)
        {
            SVG svg = SVGReader.Read(file.FilePath);

            Drawing drawing = svg.GetDrawing();

            DrawingGroup group = drawing as DrawingGroup;

            Action<DrawingGroup> drawingAction = null;

            this.tx_code.Text = null;

            drawingAction = l =>
            {
                foreach (var item in l.Children)
                {
                    if (item is GeometryDrawing)
                    {
                        GeometryDrawing gd = item as GeometryDrawing;

                        string line = $"<Path Stroke=\"{gd.Pen?.Brush}\" Fill=\"{gd.Brush}\" Data=\"{gd.Geometry.ToString()}\"/>";

                        this.tx_code.Text += line.Replace($"Stroke =\"\"","").Replace($"Fill =\"\"", "") + Environment.NewLine;
                    }
                    else if (item is DrawingGroup)
                    {
                        drawingAction(item as DrawingGroup);
                    }
                }
            };

            drawingAction(group);
        }

    }

    class FileEntity
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }
        public FileEntity(string filePath)
        {
            this.FilePath = filePath;

            this.FileName = System.IO.Path.GetFileName(filePath);
        }
    }
}
