using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class LogListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private int _currentPage = 1;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }

        private int _numberOfPages = 10;

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set
            {
                SetProperty(ref _numberOfPages, value);
            }
        }

        private int _selectedRecordsPerPage = 10;

        public int SelectedRecordsPerPage
        {
            get { return _selectedRecordsPerPage; }
            set
            {
                SetProperty(ref _selectedRecordsPerPage, value);
                UpdateRecordsPerPage();
                
            }
        }

        private void UpdateRecordsPerPage()
        {
            NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_logs.Count) / SelectedRecordsPerPage));
            NumberOfPages = NumberOfPages == 0 ? 1 : NumberOfPages;
            FirstPage();
        }

        private IEnumerable<int> _recordsPerPage = new List<int> { 10, 20, 30 };
        public IEnumerable<int> RecordsPerPage => _recordsPerPage;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private readonly ObservableCollection<LogViewModel> _logs;
        public ObservableCollection<LogViewModel> Logs { get; }


        public RelayCommand LoadLogsCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }

        public LogListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _logs = new ObservableCollection<LogViewModel>();
            Logs = new ObservableCollection<LogViewModel>();

            LoadLogsCommand = new RelayCommand(LoadLogs);
            NextPageCommand = new RelayCommand(NextPage, () => CurrentPage < NumberOfPages);
            PreviousPageCommand = new RelayCommand(PreviousPage, () => CurrentPage > 1);
            FirstPageCommand = new RelayCommand(FirstPage, () => CurrentPage > 1);
            LastPageCommand = new RelayCommand(LastPage, () => CurrentPage < NumberOfPages);
        }

        private void UpdateCollection(IEnumerable<LogViewModel> logs)
        {
            Logs.Clear();
            foreach(LogViewModel l in logs)
            {
                Logs.Add(l);
            }
        }

        private void UpdateButtonEnableStates()
        {
            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
            FirstPageCommand.NotifyCanExecuteChanged();
            LastPageCommand.NotifyCanExecuteChanged();
        }

        private void NextPage()
        {
            CurrentPage++;
            int offset = (CurrentPage-1) * SelectedRecordsPerPage;
            UpdateCollection(_logs.Skip(offset).Take(SelectedRecordsPerPage));
            UpdateButtonEnableStates();
        }

        private void PreviousPage()
        {
            CurrentPage--;
            int offset = (CurrentPage - 1) * SelectedRecordsPerPage;
            UpdateCollection(_logs.Skip(offset).Take(SelectedRecordsPerPage));
            UpdateButtonEnableStates();
        }

        private void FirstPage()
        {
            UpdateCollection(_logs.Take(SelectedRecordsPerPage));
            CurrentPage = 1;
            UpdateButtonEnableStates();
        }

        private void LastPage()
        {
            int offset = (NumberOfPages-1) * SelectedRecordsPerPage;
            UpdateCollection(_logs.Skip(offset).Take(SelectedRecordsPerPage));
            CurrentPage = NumberOfPages;
            UpdateButtonEnableStates();
        }



        private void LoadLogs()
        {
            _logs.Clear();
            foreach (Log s in _unitOfWork.LogRepository.Get(orderBy: l => l.OrderByDescending(l => l.DateTime), includeProperties: "Staff"))
            {
                _logs.Add(new LogViewModel(s));
            }
            UpdateRecordsPerPage();
            FirstPage();
            
        }

        public static LogListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            LogListViewModel viewModel = new LogListViewModel(navigationStore);
            viewModel.LoadLogsCommand.Execute(null);
            return viewModel;
        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _unitOfWork.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
