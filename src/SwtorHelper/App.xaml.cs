﻿using Microsoft.Maui.Controls;

namespace SwtorHelper;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
    }
}