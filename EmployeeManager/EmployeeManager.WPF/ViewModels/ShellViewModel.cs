using Caliburn.Micro;
using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using EmployeeManager.Binaries.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager.WPF.ViewModels
{
    public class ShellViewModel : Screen, IShellViewModel
    {
        private IEmployeeService _employeeService;
        private IDataValidator _dataValidator;
        private ISerializeService _serializeService;

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; NotifyOfPropertyChange(); }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; NotifyOfPropertyChange(); }
        }

        private int _totalPages;
        public int totalPages
        {
            get { return _totalPages; }
            set
            {
                _totalPages = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("nextPagePossible");
            }
        }

        private int _currentpage;
        public int currentPage
        {
            get { return _currentpage; }
            set
            {
                _currentpage = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("nextPagePossible");
                NotifyOfPropertyChange("previousPagePossible");
            }
        }

        public bool nextPagePossible
        {
            get
            {
                return currentPage < totalPages;
            }
        }

        public bool previousPagePossible
        {
            get
            {
                return currentPage > 1;
            }
        }

        public bool isSearch { get; set; }
        public string searchText { get; set; }
        public string searchGender { get; set; }
        public string searchStatus { get; set; }

        public ShellViewModel(IEmployeeService employeeService, IDataValidator dataValidator, ISerializeService serializeService)
        {
            _employeeService = employeeService;
            _dataValidator = dataValidator;
            _serializeService = serializeService;

            Populate();
        }

        public async void Populate()
        {
            currentPage = 1;
            GetEmployeeResponse response = new GetEmployeeResponse();
            try
            {
                response = await _employeeService.GetEmployees(currentPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

            if (response.Success)
            {
                totalPages = response.meta.pagination.pages;
                Employees = new ObservableCollection<Employee>(response.data);

                if (Employees.Count > 0)
                    SelectedEmployee = Employees[0];
            }
            else
                MessageBox.Show("Load Employees failed: " + response.FailureReason);
        }

        public async void Save()
        {
            if (!_dataValidator.isValidEmail(SelectedEmployee.email))
            {
                MessageBox.Show("Email Validation Failed: Entered email is not in the correct format");
                return;
            }

            SaveEmployeeResponse response = new SaveEmployeeResponse();

            try
            {
                if (SelectedEmployee.isExisting)
                    response = await _employeeService.UpdateEmployee(SelectedEmployee);
                else
                    response = await _employeeService.AddEmployee(SelectedEmployee);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

            if (response.Success)
            {
                if (!SelectedEmployee.isExisting)
                    Employees.Add(response.data);
                New();
            }
            else
                MessageBox.Show("Save Employee failed: " + response.FailureReason);
        }

        public void New()
        {
            SelectedEmployee = new Employee();
        }

        public async void Remove()
        {
            RemoveEmployeeResponse response = new RemoveEmployeeResponse();

            try
            {
                response = await _employeeService.RemoveEmployee(SelectedEmployee.id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

            if (response.Success)
            {
                Employees.Remove(SelectedEmployee);
                New();
            }
            else
                MessageBox.Show("Remove Employee failed: " + response.FailureReason);
        }

        public async void Search()
        {
            if (!_dataValidator.isValidSearchCriteria(searchGender, searchStatus))
            {
                MessageBox.Show("Validation Failed: Please select both the filter dropdowns.");
                return;
            }
            currentPage = 1;
            GetEmployeeResponse response = new GetEmployeeResponse();

            try
            {
                response = await _employeeService.SearchEmployees(0, searchText, searchGender, searchStatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

            if (response.Success)
            {
                isSearch = true;
                totalPages = response.meta.pagination.pages;
                Employees = new ObservableCollection<Employee>(response.data);

                if (Employees.Count > 0)
                    SelectedEmployee = Employees[0];
            }
            else
                MessageBox.Show("Load Employees failed: " + response.FailureReason);
        }

        public void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Employees";
            saveFileDialog.Filter = "Comma Delimited File|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, GetExportData());
            }
        }

        private string GetExportData()
        {
            return _serializeService.SerializeEmployeeListToCSV(Employees.ToList());
        }

        public void Previous()
        {
            currentPage--;
            doPaging();
        }

        public void Next()
        {
            currentPage++;
            doPaging();
        }

        private async void doPaging()
        {
            GetEmployeeResponse response = new GetEmployeeResponse();

            try
            {
                if (isSearch)
                    response = await _employeeService.SearchEmployees(currentPage, searchText, searchGender, searchStatus);
                else
                    response = await _employeeService.GetEmployees(currentPage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

            if (response.Success)
            {
                totalPages = response.meta.pagination.pages;
                Employees = new ObservableCollection<Employee>(response.data);

                if (Employees.Count > 0)
                    SelectedEmployee = Employees[0];
            }
            else
                MessageBox.Show("Paging Employees failed: " + response.FailureReason);
        }
    }
}
