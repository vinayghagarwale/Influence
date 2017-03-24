using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NextGenImpactAnalysisTool.Model;
namespace NextGenImpactAnalysisTool.ViewModels
{
    public class InsertNodeViewModel : ViewModelBase
    {

        private List<string> _moduleList = new List<string> () {"PM", "EHR", "Pateint Portal" };
        public InsertNodeViewModel() { }
        #region properties


        public List<string> ModuleList
        {
            get
            {
                return _moduleList;
            }
            set
            {
                _moduleList = value;
                RaisePropertyChanged("ModuleList");
            }
        }
        #endregion

        private RelayCommand _AddRow;
        /// <summary>
        /// Disable a single sql job.
        /// </summary>
        public RelayCommand AddRow
        {
            get { return _AddRow ?? (_AddRow = new RelayCommand(param => AddRow1(), param => true)); }
        }

        private void AddRow1()
        {
           
        }

        private RelayCommand _DeleteRow;
        /// <summary>
        /// Disable a single sql job.
        /// </summary>
        public RelayCommand DeleteRow
        {
            get { return _DeleteRow ?? (_DeleteRow = new RelayCommand(param => DeleteRow1(), param => true)); }
        }

        private void DeleteRow1()
        {


        }

        private ObservableCollection<FunctionalDetails> _sqlJobs;
        /// <summary>
        /// Collection of active sql jobs.
        /// </summary>
        public ObservableCollection<FunctionalDetails> SqlJobs
        {
           // get { return _sqlJobs ?? (_sqlJobs = _dataContext.Manager.GetActiveSqlJobs(1)); }
            set
            {
                _sqlJobs = value;
                 RaisePropertyChanged("SqlJobs");
            }
        }

        private FunctionalDetails _selectedJob;

        public FunctionalDetails SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                RaisePropertyChanged("SelectedJob");
            }
        }
    }
}
