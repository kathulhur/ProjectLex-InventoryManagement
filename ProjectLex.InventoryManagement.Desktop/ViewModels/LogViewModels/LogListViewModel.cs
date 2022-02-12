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

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<LogViewModel> _logs;
        public IEnumerable<LogViewModel> Logs => _logs;


        public RelayCommand LoadLogsCommand { get; }

        public LogListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _logs = new ObservableCollection<LogViewModel>();

            LoadLogsCommand = new RelayCommand(LoadLogs);
        }


        private void LoadLogs()
        {
            _logs.Clear();
            foreach (Log s in _unitOfWork.LogRepository.Get(orderBy: l => l.OrderByDescending(l => l.DateTime), includeProperties: "Staff"))
            {
                _logs.Add(new LogViewModel(s));
            }
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
