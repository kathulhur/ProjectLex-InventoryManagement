using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.Services
{
    public class AuthenticationService
    {
        private readonly UserCollection _userCollection;
        private readonly List<User> _users;

        private readonly ICommand LoadUsersCommand;
        public AuthenticationService(UserCollection userCollection)
        {
            _userCollection = userCollection;
            _users = new List<User>();
            LoadUsersCommand = new LoadDataCommand<User>(userCollection, OnUsersLoaded);
        }

        public User Authenticate(string username, string password)
        {
            return _users.Where(u => u.UserUsername == username && u.UserPassword == password).FirstOrDefault();
        }

        private void OnUsersLoaded()
        {
            _users.AddRange(_userCollection.DataList);
        }

        public static AuthenticationService LoadAuthenticationService(UserCollection userCollection)
        {
            AuthenticationService authenticationService = new AuthenticationService(userCollection);
            authenticationService.LoadUsersCommand.Execute(null);
            return authenticationService;
        }

    }
}
