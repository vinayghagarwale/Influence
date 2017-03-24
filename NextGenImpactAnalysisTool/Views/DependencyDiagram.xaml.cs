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
using NextGenImpactAnalysisTool.Engine;
using NextGenImpactAnalysisTool.Model;
using System.Collections.ObjectModel;

namespace NextGenImpactAnalysisTool.Views
{
    /// <summary>
    /// Interaction logic for DependencyDiagram.xaml
    /// </summary>
    public partial class DependencyDiagram
    {
        public DependencyDiagram()
        {
            InitializeComponent();
        }
        public DependencyDiagram(System.Xml.XmlNode node, Products _product)
        {
            InitializeComponent();
            ProcessDependencyDiagram pdd = new ProcessDependencyDiagram();

            pdd.ConstructProductList(node, _product);
            ConstructDiagram(_product);
            UpdateCoordinate();
        }

        private void UpdateCoordinate()
        {
            //foreach(Type childgrid in mycanvas.Children)
            //{
            //    foreach(DataGridRow row in childgrid.Items)
            //    {
            //        DrawLine(mycanvas, 30, 200, 700, 150);
            //    }

            //}
        }

        private void ConstructDiagram(Products _product)
        {
            ScrollViewer sv = new ScrollViewer();
            sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            //Canvas mycanvas = new Canvas();
            int x = 20, y = 20;
            int x1 = 50, y1 = 500;
            foreach (Product prod in _product)
            {
                foreach(Modules mods in prod.lstModules)
                {
                    if (mods.Count > 0)
                    {
                        DrawTable(mycanvas, mods, prod.ProductName + " - "+ mods.ModuleName, x, y,x1,y1);
                        x = x + 210;
                        x1 = x1 + 50;
                    }
                }
            }
            
        }

        private void DrawTable(Canvas CanvasPanel, List<Module> lst, string HeaderName, int x,int y, int xchild, int ychild)
        {
           
            DataGrid dt = new DataGrid();

            //dt.Name = HeaderName;
            dt.Width = 180;
            dt.Height = 400;

            dt.MouseDown += Dt_MouseDown1;
            dt.MouseMove += Dt_MouseMove1;
            dt.MouseLeave += Dt_MouseLeave1;

            Border b = new Border();

            Style s = new Style();

            dt.AutoGenerateColumns = false;
            dt.IsSynchronizedWithCurrentItem = false;
            DataGridTextColumn dc1 = new DataGridTextColumn();
            dc1.Header = HeaderName;
            dc1.CanUserSort = false;
            dc1.CanUserResize = false;
            dc1.CanUserReorder = false;
            dc1.IsReadOnly = true;
            dc1.Binding = new Binding("ModuleName");

            //dc1.HeaderStyle = s;          
            dt.Columns.Add(dc1);

            DataGridTextColumn dc2 = new DataGridTextColumn();
            dc2.Header = HeaderName;
            dc2.Visibility = Visibility.Hidden;
            dc2.Binding = new Binding("product");
            dt.Columns.Add(dc2);

            DataGridTextColumn dc3 = new DataGridTextColumn();
            dc3.Header = HeaderName;
            dc3.Visibility = Visibility.Hidden;
            dc3.Binding = new Binding("dependentmodule");
            dt.Columns.Add(dc3);

            DataGridTextColumn dc4 = new DataGridTextColumn();
            dc4.Header = "Bind";
            //dc4.Visibility = Visibility.Hidden;
            dc4.Binding = new Binding("coordinate");
            dt.Columns.Add(dc4);
            Canvas.SetLeft(dt, x);
            Canvas.SetTop(dt, y);
            int cellheiht = 20;

            foreach (Module m in lst)
            {
                if(m.dependentmodule.Length>0)
                {
                    double x1= Canvas.GetTop(dt);
                    double y1 = Canvas.GetLeft(dt);
                    List<string> childlist = new List<string>();
                    y1 = Convert.ToInt16(dt.Width) + y1;
                    m.coordinate = new System.Drawing.Point(Convert.ToInt16(x1) + cellheiht, Convert.ToInt16(y1));// Canvas.GetTop(dt); 

                    DataGrid dtchild = new DataGrid();
                    dtchild.AutoGenerateColumns = false;
                    DataGridTextColumn dchild1 = new DataGridTextColumn();
                    dchild1.Header = m.product + " --> " + m.dependentmodule;
                    dtchild.Columns.Add(dchild1);

                    Canvas.SetLeft(dtchild, ychild);
                    Canvas.SetTop(dtchild, xchild);
                    CanvasPanel.Children.Add(dtchild);

                    DrawLine(mycanvas, m.coordinate.Y, m.coordinate.X, ychild, xchild);

                    dtchild.ItemsSource = childlist;
                    xchild = xchild + 50;
                }
                cellheiht += 30;
            }
            dt.ItemsSource = lst;

            CanvasPanel.Children.Add(b);
            CanvasPanel.Children.Add(dt);
        }

        MouseDevice ellipseControlTouchDevice;
        Point lastPoint;

        private void Dt_MouseLeave1(object sender, MouseEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.MouseDevice == ellipseControlTouchDevice)
            {
                // Forget about this contact.
                ellipseControlTouchDevice = null;
            }

            // Mark this event as handled.  
            e.Handled = true;
        }

        private void Dt_MouseMove1(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice == ellipseControlTouchDevice)
            {
                // Get the current position of the contact.  
                Point currentTouchPoint = ellipseControlTouchDevice.GetPosition(mycanvas);

                // Get the change between the controlling contact point and
                // the changed contact point.  
                double deltaX = currentTouchPoint.X - lastPoint.X;
                double deltaY = currentTouchPoint.Y - lastPoint.Y;

                Point currPoint = ellipseControlTouchDevice.GetPosition(mycanvas);
                // Get and then set a new top position and a new left position for the ellipse.  
                double newTop = currPoint.Y + deltaY;
                double newLeft = currPoint.X + deltaX;

                Canvas.SetTop(((DataGrid)sender), newTop);
                Canvas.SetLeft(((DataGrid)sender), newLeft);

                // Forget the old contact point, and remember the new contact point.  
                lastPoint = currentTouchPoint;

                // Mark this event as handled.  
                e.Handled = true;
            }
        }

        private void Dt_MouseDown1(object sender, MouseButtonEventArgs e)
        {
            // Capture to the ellipse.  
            e.MouseDevice.Capture(((DataGrid)sender));

            // Remember this contact if a contact has not been remembered already.  
            // This contact is then used to move the ellipse around.
            ellipseControlTouchDevice = null;
            if (ellipseControlTouchDevice == null)
            {
                ellipseControlTouchDevice = e.MouseDevice;

                // Remember where this contact took place.  
                lastPoint = ellipseControlTouchDevice.GetPosition(mycanvas);
            }

            // Mark this event as handled.  
            e.Handled = true;
        }




        private void DrawLine(Canvas CanvasPanel,int x1, int y1, int x2, int y2)
        {
            Line line = new Line();
            line = new Line();
            UserControl child;
            line.Stroke = Brushes.BlueViolet;
            line.StrokeThickness = 4;

            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            line.BringIntoView();
            line.StrokeThickness = 2;
            mycanvas.Children.Add(line);

            child = mycanvas.Children[mycanvas.Children.Count-1] as UserControl;
            Canvas.SetZIndex(line, 100);
        }
        private void DataGrid_MouseMove(object sender, MouseEventArgs e)
        {
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        DataGridRow row = UIHelper.FindVisualParent<DataGridRow>(e.OriginalSource as FrameworkElement);
        //        if (row != null && row.IsSelected)
        //        {
        //            _source = UIHelper.FindVisualParent<DataGrid>(row).ItemsSource;
        //            _itemToMove = row.Item;
        //            DragDropEffects finalEffects = DragDrop.DoDragDrop(row, _itemToMove, AllowedEffects);
        //            DataGridDragDropEventArgs args = new DataGridDragDropEventArgs()
        //            {
        //                Destination = _destination,
        //                Direction = _direction,
        //                DroppedObject = _itemToMove,
        //                Effects = finalEffects,
        //                Source = _source,
        //                TargetObject = _dropTarget
        //            };

        //            if (_dropTarget != null && Command != null && Command.CanExecute(args))
        //            {
        //                Command.Execute(args);

        //                _itemToMove = null;
        //                _dropTarget = null;
        //                _source = null;
        //                _destination = null;
        //                _direction = DataGridDragDropDirection.Indeterminate;
        //                _lastIndex = -1;
        //            }
        //        }
        //    }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void mycanvas_MouseMove(object sender, MouseEventArgs e)
        {
            showcoord.Text = e.MouseDevice.GetPosition(mycanvas).ToString();
        }
    }
}
