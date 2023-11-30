﻿using System.Collections.ObjectModel;
using ClipNote.Models;
using Prism.Mvvm;

namespace ClipNote.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<Text> Texts { get; set; } = new ObservableCollection<Text>();
    }
}