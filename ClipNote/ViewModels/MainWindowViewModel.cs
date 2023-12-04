using System;
using System.Collections.ObjectModel;
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
        private string title = "Prism Application";
        private SortType currentSortType;
        private string lastCopiedText = string.Empty;

        private DatabaseContext DatabaseContext { get; set; } = new ();

        private ObservableCollection<Text> texts = new ();

        public string Title { get => title; set => SetProperty(ref title, value); }

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

        public DelegateCommand<object> SortCommand => new ((param) =>
        {
            var sortType = (SortType)param;
            currentSortType = sortType;

            switch (sortType)
            {
                case SortType.Type:
                    Texts = new ObservableCollection<Text>(Texts.OrderBy(t => t.Type));
                    break;
                case SortType.DateTime:
                    Texts = new ObservableCollection<Text>(Texts.OrderBy(t => t.CreatedAt));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        public DelegateCommand<string> AddTextCommand => new ((param) =>
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
    }
}