using EmployeeManager.Binaries.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.WPF.ViewModels
{
    public interface IShellViewModel
    {
        ObservableCollection<Employee> Employees { get; set; }
        Employee SelectedEmployee { get; set; }
        void Populate();
        void Save();
        void New();
        void Remove();
        void Search();

        void Export();
        void Previous();
        void Next();
    }
}
