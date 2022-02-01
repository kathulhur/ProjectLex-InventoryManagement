using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private Staff _staff;
        public Staff Staff => _staff;


        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Staff Login(string username, string password)
        {
            Staff storedStaff = _unitOfWork.StaffRepository.Get(s => s.StaffUsername == username && s.StaffPassword == password).SingleOrDefault();

            return storedStaff;
        }

    }

}
