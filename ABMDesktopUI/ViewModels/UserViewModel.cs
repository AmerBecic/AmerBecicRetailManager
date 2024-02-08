using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ABMDesktopUI.Library.Api;
using ABMDesktopUI.Library.Models;
using Caliburn.Micro;

namespace ABMDesktopUI.ViewModels
{
    public class UserViewModel : Screen
    {
        public BindingList<ApplicationUserModel> _users;
        private readonly IUserApi _userApi;
        private readonly StatusInfoViewModel _statusInfo;
        private readonly IWindowManager _window;

        public UserViewModel(StatusInfoViewModel statusInfo , IWindowManager window, IUserApi userApi)
        {
            _userApi = userApi;
            _statusInfo = statusInfo;
            _window = window;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _statusInfo.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the Sales Form");
                    _window.ShowDialog(_statusInfo, null, settings);
                }
                else
                {
                    _statusInfo.UpdateMessage("Fatal Exception", ex.Message);
                    _window.ShowDialog(_statusInfo, null, settings);
                }
                TryClose();
            }
        }
        private async Task LoadUsers()
        {
            var usersList = await _userApi.GetAll();
            Users = new BindingList<ApplicationUserModel>(usersList);
        }

        public BindingList<ApplicationUserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }
    }
}
