﻿using CryptoInfoApp.Core;
using System.Windows;

namespace CryptoInfoApp.ViewModel
{
    public class ConvertViewModel : ObservableObject
    {
        public Visibility LoadingVisibility { get; set; } = Visibility.Visible;
        public Visibility CircleLoaderVisibility { get; set; } = Visibility.Visible;
        public Visibility ErrorVisibility { get; set; } = Visibility.Hidden;
        public string ErroeMessage { get; set; } = string.Empty;
    }
}