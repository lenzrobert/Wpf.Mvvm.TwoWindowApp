using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wpf.Mvvm.TwoWindowApp.Annotations;
using Wpf.Mvvm.TwoWindowApp.Model;
using Wpf.Mvvm.TwoWindowApp.View;

namespace Wpf.Mvvm.TwoWindowApp.ViewModel
{
    public class TwoWindowViewModel :INotifyPropertyChanged
    {
        Werte _w = new Werte();
        private ViewSecondWindow _secondWindow;


        private ICommand _actionButtonCommand;

        #region Button Commands
        public ICommand ActionButtonCommand
        {
            get => _actionButtonCommand;
            set => _actionButtonCommand = value;
        }

        private ICommand _setButtonCommand;

        public ICommand SetButtonCommand
        {
            get => _setButtonCommand;
            set => _setButtonCommand = value;
        }

        private ICommand _returnButtonCommand;

        public ICommand ReturnButtonCommand
        {
            get => _returnButtonCommand;
            set => _returnButtonCommand = value;
        }

        private bool _canExecute = true;

        public bool CanExecute
        {
            get => this._canExecute;
            set
            {
                if (this._canExecute == value)
                {
                    return;
                }

                this._canExecute = value;
            }
        }

        #endregion


        #region ListBox
        private string _wertListBox;

        public string WertListBox
        {
            get => _wertListBox;
            set
            {
                _wertListBox = value; 
                OnPropertyChanged("WertListBox");
            }
        }

        private int _selectedIndexListBox;

        public int SelectedIndexListBox
        {
            get => _selectedIndexListBox;
            set
            {
                _selectedIndexListBox = value;
                OnPropertyChanged("SelectedIndexListBox");
            }
        }
 
        #endregion

        #region ComboBox

        private string _wertComboBox;

        public string WertComboBox
        {
            get => _wertComboBox;
            set
            {
                _wertComboBox = value; 
                OnPropertyChanged("WertComboBox");
            }
        }

        private int _selectedIndexComboBox;

        public int SelectedItemComboBox
        {
            get => _selectedIndexComboBox;
            set
            {
                _selectedIndexComboBox = value;
                OnPropertyChanged("SelectedItemComboBox");
            }
        }

        #endregion

        #region CheckBox
        private bool _valueCheckBox;

        public bool ValueCheck
        {
            get => _valueCheckBox; 
            set
            {
                _valueCheckBox = value;
                OnPropertyChanged("ValueCheck");
            }
        }

        #endregion

        #region DataSet
        private DataRowView _selectedArtikel;

        public DataRowView SelectedArtikel
        {
            get => _selectedArtikel;
            set
            {
                _selectedArtikel = value;
                _w.WertDataSet = "";
                if (_selectedArtikel != null)
                {
                    _w.WertDataSet = _selectedArtikel.Row[0].ToString();
                }
                OnPropertyChanged("SelectedArtikel");
            }
        }

        public DataView ViewTabelleArtikel
        {
            get => _w.GetListeArtikel().Tables["Artikel"].DefaultView;
        }

        private int _selectedIndexDataSet;

        public int SelectedIndexDataSet
        {
            get => _selectedIndexDataSet;
            set
            {
                _selectedIndexDataSet = value;
                OnPropertyChanged("SelectedIndexDataSet");
            }
        }

        private string _result;

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        #endregion

        public TwoWindowViewModel()
        {
            _actionButtonCommand = new RelayCommand(Action, param => this._canExecute);
            _setButtonCommand = new RelayCommand(Set, param => this._canExecute);
            _returnButtonCommand = new RelayCommand(ReturnAndReset, param => this._canExecute);
            Result = "Das steht drin";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Action(object obj)
        {
            string hilf = "true";
            _secondWindow = new ViewSecondWindow();
            if (ValueCheck == false)
            {
                hilf = "false";
            }

            Result =
                $"gewählt wurde [ListBox]: {WertListBox}, [ComboBox]: {WertComboBox}, {Environment.NewLine}" +
                $"[DataSet]: {_w.WertDataSet}, [CheckBox]: {hilf}";
            _secondWindow.DataContext = this;
            _secondWindow.ShowDialog();
        }

        private void Set(object obj)
        {
            ValueCheck = true;
            SelectedIndexListBox = 2;
            SelectedItemComboBox = 1;
            SelectedIndexDataSet = 3;
        }

        private void ReturnAndReset(object obj)
        {
            ValueCheck = false;
            SelectedIndexListBox = 0;
            SelectedItemComboBox = -1;
            SelectedIndexDataSet = -1;
            _secondWindow.Close();
        }
    }
}
