﻿using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class BorderlessEntryCodePageViewModel : CodePageViewModelBase
    {
        public BorderlessEntryCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.BorderlessEntryPage.xaml");
        }
    }
}
