using Caliburn.Micro;
using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using EmployeeManager.Binaries.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public int totalPages { get; set; }
        public int currentPage { get; set; }

        public bool isSearch { get; set; }
        public string searchText { get; set; }
        public string searchGender { get; set; }
        public string searchStatus { get; set; }

        public ShellViewModel(IEmployeeService employeeService, IDataValidator dataValidator)
        {
            _employeeService = employeeService;
            _dataValidator = dataValidator;

            Populate();
        }

        public async void Populate()
        {
            currentPage = 1;

            GetUserResponse response = await _employeeService.GetEmployees(currentPage);

            if (response.Success)
            {
                totalPages = response.meta.pagination.total;
                Employees = new ObservableCollection<Employee>(response.data);
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

            SaveUserResponse response;
            if (SelectedEmployee.isExisting)
                response = await _employeeService.UpdateEmployee(SelectedEmployee);
            else
                response = await _employeeService.AddEmployee(SelectedEmployee);

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
            RemoveUserResponse response = await _employeeService.RemoveEmployee(SelectedEmployee.id);
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

            GetUserResponse response = await _employeeService.SearchEmployees(currentPage, searchText, searchGender, searchStatus);

            if (response.Success)
            {
                isSearch = true;
                totalPages = response.meta.pagination.total;
                Employees = new ObservableCollection<Employee>(response.data);
                SelectedEmployee = Employees[0];
            }
            else
                MessageBox.Show("Load Employees failed: " + response.FailureReason);
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
