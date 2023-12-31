﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ClipNote.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace ClipNote.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private SortType currentSortType;
        private string lastCopiedText = string.Empty;
        private ObservableCollection<Text> texts = new ();

        public MainWindowViewModel()
        {
            Texts = new ObservableCollection<Text>(DatabaseContext.Texts);
            SortCommand.Execute(SortType.DateTime);
        }

        public string Title => "Prism Application";

        public string PostText { get; set; }

        public ObservableCollection<Text> Texts
        {
            get => texts;
            private set
            {
                if (SetProperty(ref texts, value))
                {
                    RaisePropertyChanged(nameof(SortCommand));
                }
            }
        }

        public DelegateCommand<object> SortCommand => new (param =>
        {
            var sortType = (SortType)param;
            currentSortType = sortType;

            Texts = sortType switch
            {
                SortType.Type => new ObservableCollection<Text>(Texts.OrderBy(t => t.Type)),
                SortType.DateTime => new ObservableCollection<Text>(Texts.OrderBy(t => t.CreatedAt)),
                _ => Texts,
            };
        });

        public DelegateCommand<string> AddTextCommand => new (param =>
        {
            var text = new Text(param);
            Texts.Add(text);
            DatabaseContext.Texts.Add(text);
            DatabaseContext.SaveChanges();

            SortCommand.Execute(currentSortType);
            PostText = string.Empty;
            RaisePropertyChanged(nameof(PostText));
        });

        public DelegateCommand ReadClipboardCommand => new (() =>
        {
            if (!Clipboard.ContainsText())
            {
                return;
            }

            var c = Clipboard.GetText();

            if (c == lastCopiedText || string.IsNullOrWhiteSpace(c))
            {
                return;
            }

            lastCopiedText = c;
            AddTextCommand.Execute(c);
        });

        public DelegateCommand<Text> SendClipboardCommand => new ((param) =>
        {
            if (param == null)
            {
                return;
            }

            Clipboard.SetText(param.Value);
        });

        private DatabaseContext DatabaseContext { get; } = new ();
    }
}