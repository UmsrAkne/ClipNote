using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClipNote.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace ClipNote.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private ObservableCollection<Text> texts = new ObservableCollection<Text>();

        public string Title { get => title; set => SetProperty(ref title, value); }

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

        public DelegateCommand<object> SortCommand => new DelegateCommand<object>((param) =>
        {
            var sortType = (SortType)param;

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
    }
}