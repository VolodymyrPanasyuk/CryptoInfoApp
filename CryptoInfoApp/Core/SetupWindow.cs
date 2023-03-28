﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoInfoApp.Core
{
    public static class SetupWindow
    {
        public static void Setup(Window window)
        {
            window.MinHeight = SystemParameters.WorkArea.Height / 2.5;
            window.MinWidth = SystemParameters.WorkArea.Width / 2.5;
            window.MaxHeight = SystemParameters.WorkArea.Height + 7;
            ThemeSwitcher.SetDefault(window);
        }
    }
}