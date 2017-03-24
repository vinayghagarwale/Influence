using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NextGenImpactAnalysisTool.ViewModels
{
    public class ImpactAnalysisToolViewModel : ViewModelBase
    {

        #region properties

        private string _treepath = @"/ Nextgen / Node / Node / Node / Items / Functional";

        public string Treepath
        {
            get
            {
                return _treepath;
            }
            set
            {
                _treepath = value;
                RaisePropertyChanged("TreePath");
            }
        }
        #endregion

        public string CurrentColor
        {
            get { return _currentColor; }
            set
            {
                if (value != _currentColor)
                {
                    _currentColor = value;
                    RaisePropertyChanged("CurrentColor");
                }
            }
        }
        private string _currentColor;

        //private RelayCommand _openEHR;
        //public RelayCommand OpenEHR
        //{
        //    get
        //    {
        //        //return _openEHR ?? (_openEHR = new RelayCommand(param => Utility.ExecuteCommandShow(_kbmUpgradeModel.NextgenRootFolderpath + strEHR, ""), param => true));
        //    }
        //}
    }
}
