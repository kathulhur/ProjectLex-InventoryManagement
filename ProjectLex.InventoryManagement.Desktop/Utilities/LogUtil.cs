using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.Utilities
{
    public static class LogUtil
    {
        public static Log CreateLog(LogCategory logCategory, ActionType actionType, string details)
        {
            Log newLog = new Log
            {
                LogID = Guid.NewGuid(),
                StaffID = ((MainViewModel)Application.Current.MainWindow.DataContext).AuthenticationStore.CurrentStaff.StaffID,
                LogCategory = logCategory.ToString(),
                ActionType = actionType.ToString(),
                LogDetails = details,
                DateTime = DateTime.Now
            };

            return newLog;
        }
    }
}
