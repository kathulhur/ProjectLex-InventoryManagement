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

namespace ProjectLex.InventoryManagement.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for PaginationControl.xaml
    /// </summary>
    public partial class PaginationControl : UserControl
    {



        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(PaginationControl), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public int NumberOfPages
        {
            get { return (int)GetValue(NumberOfPagesProperty); }
            set { SetValue(NumberOfPagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberOfPages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberOfPagesProperty =
            DependencyProperty.Register("NumberOfPages", typeof(int), typeof(PaginationControl), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));





        public List<int> RecordsPerPage
        {
            get { return (List<int>)GetValue(RecordsPerPageProperty); }
            set { SetValue(RecordsPerPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecordsPerPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecordsPerPageProperty =
            DependencyProperty.Register("RecordsPerPage", typeof(List<int>), typeof(PaginationControl), new PropertyMetadata(new List<int>() { 10, 20, 30 }));



        public int SelectedRecordsPerPage
        {
            get { return (int)GetValue(SelectedRecordsPerPageProperty); }
            set { SetValue(SelectedRecordsPerPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedRecordsPerPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRecordsPerPageProperty =
            DependencyProperty.Register("SelectedRecordsPerPage", typeof(int), typeof(PaginationControl), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private static void OnSelectedRecordsPerPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is PaginationControl)
            MessageBox.Show("hey");
        }

        public ICommand FirstPageCommand
        {
            get { return (ICommand)GetValue(FirstPageCommandProperty); }
            set { SetValue(FirstPageCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstPageCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstPageCommandProperty =
            DependencyProperty.Register("FirstPageCommand", typeof(ICommand), typeof(PaginationControl), new PropertyMetadata(null));




        public ICommand LastPageCommand
        {
            get { return (ICommand)GetValue(LastPageCommandProperty); }
            set { SetValue(LastPageCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastPageCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastPageCommandProperty =
            DependencyProperty.Register("LastPageCommand", typeof(ICommand), typeof(PaginationControl), new PropertyMetadata(null));




        public ICommand PreviousPageCommand
        {
            get { return (ICommand)GetValue(PreviousPageCommandProperty); }
            set { SetValue(PreviousPageCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviousPageCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousPageCommandProperty =
            DependencyProperty.Register("PreviousPageCommand", typeof(ICommand), typeof(PaginationControl), new PropertyMetadata(null));




        public ICommand NextPageCommand
        {
            get { return (ICommand)GetValue(NextPageCommandProperty); }
            set { SetValue(NextPageCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NextPageCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextPageCommandProperty =
            DependencyProperty.Register("NextPageCommand", typeof(ICommand), typeof(PaginationControl), new PropertyMetadata(null));




        public PaginationControl()
        {
            InitializeComponent();
        }

        
    }
}
