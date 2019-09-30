﻿using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class MaterialEntryPageViewModel : ReactiveViewModelBase
    {
        private DelegateCommand _showCodeBehind;
        public DelegateCommand ShowCodeBehind => _showCodeBehind ?? (_showCodeBehind = new DelegateCommand(ExecuteShowCodeBehind));

        public MaterialEntryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ConsoleLoggingService logger)
            : base(navigationService, pageDialogService, logger)
        {
        }

        async void ExecuteShowCodeBehind()
        {
            await NavigationService.NavigateAsync("MaterialEntryCodePage");
        }
    }
}
