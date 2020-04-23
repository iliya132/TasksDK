using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TasksDK.Interfaces;
using TasksDK.Model;
using TasksDK.Model.Entities;
using TasksDK.Model.Entities.DTO;
using TasksDK.View;

namespace TasksDK.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private IEmployeeProvider _employees;
        private ITaskProvider _tasks;

        #region currentValues
        private EmployeeTask _copiedTask;
        private bool isCut = false;
        /// <summary>
        /// Текущие задачи. Загрузка из ITaskProvider.
        /// </summary>
        private ObservableCollection<EmployeeTask> _currentTasks = new ObservableCollection<EmployeeTask>();
        public ObservableCollection<EmployeeTask> CurrentTasks { get => _currentTasks; set => _currentTasks = value; }
        /// <summary>
        /// Древо подчиненных аналитиков
        /// </summary>
        private ObservableCollection<Node> nodes = new ObservableCollection<Node>();
        public ObservableCollection<Node> NodesCollection { get => nodes; set => nodes = value; }
        private ObservableCollection<Analytic> _subordinateEmployees = new ObservableCollection<Analytic>();
        public ObservableCollection<Analytic> SubordinateEmployees
        {
            get { return _subordinateEmployees; }
            set { _subordinateEmployees = value; }
        }
        private ObservableCollection<AnalyticOrdered> subordinatedOrdered = new ObservableCollection<AnalyticOrdered>();
        public ObservableCollection<AnalyticOrdered> SubordinatedOrdered
        {
            get { return subordinatedOrdered; }
            set { subordinatedOrdered = value; }
        }
        private ObservableCollection<Analytic> _selectedAnalytics = new ObservableCollection<Analytic>();
        public ObservableCollection<Analytic> SelectedAnalytics { get => _selectedAnalytics; set => _selectedAnalytics = value; }

        EmployeeTask _mainTask = new EmployeeTask();
        EmployeeTask _currentTask = new EmployeeTask();
        private List<Process> _processes;
        public List<Process> Processes { get => _processes; set => _processes = value; }

        private Stack<EmployeeTask> _taskStack = new Stack<EmployeeTask>();
        public Stack<EmployeeTask> TaskStack { get => _taskStack; set => _taskStack = value; }
        private ObservableCollection<Employee> _analytics = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Analytics { get => _analytics; set => _analytics = value; }
        private ObservableCollection<string> _analytics_str = new ObservableCollection<string>();
        public ObservableCollection<string> Analytics_str { get => _analytics_str; set => _analytics_str = value; }
        private Analytic _currentAnalytic;
        private Employee _currentEmployee;

        public string EditedAssigneeFIO { get; set; }
        public string EditedOwnerFIO { get; set; }

        /// <summary>
        /// Новая задача. Привязка из окна AddTask
        /// </summary>
        private EmployeeTask newTask = new EmployeeTask();
        public EmployeeTask NewTask { get => newTask; set => newTask = value; }
        private EmployeeTask _selectedTask;
        public EmployeeTask SelectedTask { get => _selectedTask; set => _selectedTask = value; }
        private ObservableCollection<Process> _selectedProcesses = new ObservableCollection<Process>();
        public ObservableCollection<Process> SelectedProcesses { get => _selectedProcesses; set => _selectedProcesses = value; }

        #endregion

        #region Commands
        public RelayCommand AddNewTaskCommand { get; set; }
        public RelayCommand AddNewSubTaskCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand FilterTasks_My { get; set; }
        public RelayCommand FilterTasks_AssignedOnMe { get; set; }
        public RelayCommand FilterTasks_All { get; set; }
        public RelayCommand<EmployeeTask> EditTaskCommand { get; set; }
        public RelayCommand<EmployeeTask> CutTaskCommand { get; set; }
        public RelayCommand<EmployeeTask> CopyTaskCommand { get; set; }
        public RelayCommand<EmployeeTask> PasteAsChildTaskCommand { get; set; }
        public RelayCommand PasteTask { get; set; }
        public RelayCommand<EmployeeTask> DeleteTask { get; set; }
        public RelayCommand<EmployeeTask> SelectParentCommand { get; set; }
        public RelayCommand<EmployeeTask> AddResult { get; set; }
        public RelayCommand<EmployeeTask> SelectTaskCommand { get; set; }
        public RelayCommand<ICollection<object>> StoreProcessSelection { get; set; }
        public RelayCommand<AnalyticOrdered> SelectAnalytic { get; set; }
        public RelayCommand<AnalyticOrdered> UnselectAnalytic { get; set; }

        #endregion

        public MainViewModel(IEmployeeProvider employeeProvider, ITaskProvider taskProvider)
        {

            _employees = employeeProvider;
            _tasks = taskProvider;

            InitializeData();
            InitializeCommands();
        }

        private void AddTaskMethod()
        {
            List<ProcessProxy> procProxies = new List<ProcessProxy>();
            foreach (ProcessProxy proxy in newTask.Processes)
            {
                procProxies.Add(new ProcessProxy
                {
                    Id = proxy.Id,
                    ProcessId = proxy.ProcessId
                });
            }

            EmployeeTask addMainTask = new EmployeeTask
            {
                Assignee = newTask.Assignee,
                Weight = newTask.Weight,
                SupervisorDonePercent = newTask.SupervisorDonePercent,
                SupervisorComment = newTask.SupervisorComment,
                AwaitedResult = newTask.AwaitedResult,
                Comment = newTask.Comment,
                CreationDate = newTask.CreationDate,
                DueDate = newTask.DueDate,
                EmployeeComment = newTask.EmployeeComment,
                EmployeeDonePercent = newTask.EmployeeDonePercent,
                Meter = newTask.Meter,
                Name = newTask.Name,
                Processes = procProxies,
                Owner = newTask.Owner,
                ParentTask = newTask.ParentTask,
                Reporter = newTask.Reporter
            };
            addMainTask.ChildTasks = new List<EmployeeTask>();

            foreach (Analytic analytic in SelectedAnalytics)
            {
                string analyticFIO = $"{analytic.LastName} {analytic.FirstName} {analytic.FatherName}";
                newTask.Assignee = Analytics.FirstOrDefault(i => i.FIO.Equals(analyticFIO));

                List<ProcessProxy> procProxies2 = new List<ProcessProxy>();
                foreach (ProcessProxy proxy in newTask.Processes)
                {
                    procProxies2.Add(new ProcessProxy
                    {
                        Id = proxy.Id,
                        ProcessId = proxy.ProcessId
                    });
                }

                addMainTask.ChildTasks.Add(new EmployeeTask
                {
                    Assignee = newTask.Assignee,
                    Weight = newTask.Weight,
                    SupervisorDonePercent = newTask.SupervisorDonePercent,
                    SupervisorComment = newTask.SupervisorComment,
                    AwaitedResult = newTask.AwaitedResult,
                    Comment = newTask.Comment,
                    CreationDate = newTask.CreationDate,
                    DueDate = newTask.DueDate,
                    EmployeeComment = newTask.EmployeeComment,
                    EmployeeDonePercent = newTask.EmployeeDonePercent,
                    Meter = newTask.Meter,
                    Name = newTask.Name,
                    Processes = procProxies2,
                    Owner = newTask.Owner,
                    ParentTask = newTask.ParentTask,
                    Reporter = newTask.Reporter
                });
            }
            if (addMainTask.ChildTasks.Count == 1)
            {
                _tasks.AddTask(addMainTask.ChildTasks[0]);
                _currentTask.ChildTasks.Add(addMainTask.ChildTasks[0]);
                CurrentTasks.Add(addMainTask.ChildTasks[0]);
            }
            _tasks.AddTask(addMainTask);
            _currentTask.ChildTasks.Add(addMainTask);
            CurrentTasks.Add(addMainTask);
            _tasks.Commit();
        }

        private void AddNewTask()
        {
            AddTask addTaskWindow = new AddTask();

            EditedOwnerFIO = _currentEmployee.FIO;
            addTaskWindow.Reporter.IgnoreTextChange = true;
            addTaskWindow.Reporter.Text = EditedOwnerFIO;
            addTaskWindow.Reporter.IgnoreTextChange = false;
            newTask.Owner = newTask.Reporter = Analytics.FirstOrDefault(i => i.FIO.Equals(EditedOwnerFIO));
            if (addTaskWindow.ShowDialog() == true)
            {
                AddTaskMethod();
            }
            newTask = new EmployeeTask();
        }

        private void AddSubTask()
        {

            if (SelectedTask != null)
            {
                AddTask addTaskWindow = new AddTask();
                EditedOwnerFIO = _currentEmployee.FIO;
                newTask.Owner = newTask.Reporter = Analytics.FirstOrDefault(i => i.FIO.Equals(EditedOwnerFIO));
                addTaskWindow.Reporter.IgnoreTextChange = true;
                addTaskWindow.Reporter.Text = _currentEmployee.FIO;
                addTaskWindow.Reporter.IgnoreTextChange = false;
                if (addTaskWindow.ShowDialog() == true)
                {
                    List<ProcessProxy> procProxies = new List<ProcessProxy>();
                    foreach (ProcessProxy proxy in newTask.Processes)
                    {
                        procProxies.Add(new ProcessProxy
                        {
                            Id = proxy.Id,
                            ProcessId = proxy.ProcessId
                        });
                    }

                    EmployeeTask addMainTask = new EmployeeTask
                    {
                        Assignee = newTask.Assignee,
                        Weight = newTask.Weight,
                        SupervisorDonePercent = newTask.SupervisorDonePercent,
                        SupervisorComment = newTask.SupervisorComment,
                        AwaitedResult = newTask.AwaitedResult,
                        Comment = newTask.Comment,
                        CreationDate = newTask.CreationDate,
                        DueDate = newTask.DueDate,
                        EmployeeComment = newTask.EmployeeComment,
                        EmployeeDonePercent = newTask.EmployeeDonePercent,
                        Meter = newTask.Meter,
                        Name = newTask.Name,
                        Processes = procProxies,
                        Owner = newTask.Owner,
                        ParentTask = newTask.ParentTask,
                        Reporter = newTask.Reporter
                    };
                    addMainTask.ChildTasks = new List<EmployeeTask>();

                    foreach (Analytic analytic in SelectedAnalytics)
                    {
                        string analyticFIO = $"{analytic.LastName} {analytic.FirstName} {analytic.FatherName}";
                        newTask.Assignee = Analytics.FirstOrDefault(i => i.FIO.Equals(analyticFIO));

                        List<ProcessProxy> procProxies2 = new List<ProcessProxy>();
                        foreach (ProcessProxy proxy in newTask.Processes)
                        {
                            procProxies2.Add(new ProcessProxy
                            {
                                Id = proxy.Id,
                                ProcessId = proxy.ProcessId
                            });
                        }

                        addMainTask.ChildTasks.Add(new EmployeeTask
                        {
                            Assignee = newTask.Assignee,
                            Weight = newTask.Weight,
                            SupervisorDonePercent = newTask.SupervisorDonePercent,
                            SupervisorComment = newTask.SupervisorComment,
                            AwaitedResult = newTask.AwaitedResult,
                            Comment = newTask.Comment,
                            CreationDate = newTask.CreationDate,
                            DueDate = newTask.DueDate,
                            EmployeeComment = newTask.EmployeeComment,
                            EmployeeDonePercent = newTask.EmployeeDonePercent,
                            Meter = newTask.Meter,
                            Name = newTask.Name,
                            Processes = procProxies2,
                            Owner = newTask.Owner,
                            ParentTask = newTask.ParentTask,
                            Reporter = newTask.Reporter
                        });

                    }
                    _tasks.AddTask(addMainTask);
                    SelectedTask.ChildTasks.Add(addMainTask);
                    CurrentTasks.Add(addMainTask);
                    _tasks.Commit();
                }
                newTask = new EmployeeTask();

                UpdateTasks();

            }

        }

        private void InitializeData()
        {
            Processes = _tasks.GetProcesses();
            _mainTask.ChildTasks = new List<EmployeeTask>(_tasks.GetTasks(null));
            _currentAnalytic = _employees.GetCurrentAnalytic();
            _currentEmployee = new Employee
            {
                FIO = $"{_currentAnalytic.LastName} {_currentAnalytic.FirstName} {_currentAnalytic.FatherName}",
                AnalyticId = _currentAnalytic.Id
            };
            foreach (Employee empl in _employees.GetEmployees())
            {
                Analytics.Add(empl);
                Analytics_str.Add(empl.FIO);
            }
            _currentTask = _mainTask;
            TaskStack.Push(_mainTask);
            CurrentTasks = new ObservableCollection<EmployeeTask>(_mainTask.ChildTasks);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
            SubordinateEmployees = _employees.GetMyAnalyticsData(_currentAnalytic);
            SubordinatedOrdered = GetAnalyticOrdereds(SubordinateEmployees);
            GenerateNodes();
        }

        private ObservableCollection<AnalyticOrdered> GetAnalyticOrdereds(IEnumerable<Analytic> analytics)
        {
            ObservableCollection<AnalyticOrdered> exportVal = new ObservableCollection<AnalyticOrdered>();
            foreach (Analytic analytic in analytics)
            {
                AnalyticOrdered ordered = new AnalyticOrdered(analytic);
                ordered.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Ordered_PropertyChanged);
                exportVal.Add(ordered);
            }
            return exportVal;
        }

        private void Ordered_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Selected"))
            {
                ReportSelectionUpdate();
            }
        }

        private void ReportSelectionUpdate()
        {
            SelectedAnalytics.Clear();
            foreach (AnalyticOrdered analytic in SubordinatedOrdered)
            {
                if (analytic.Selected)
                {
                    SelectedAnalytics.Add(analytic.Analytic);
                }
            }
        }

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            AddNewSubTaskCommand = new RelayCommand(AddSubTask);
            EditTaskCommand = new RelayCommand<EmployeeTask>(EditTask);
            CutTaskCommand = new RelayCommand<EmployeeTask>(CutTask);
            CopyTaskCommand = new RelayCommand<EmployeeTask>(CopyTask);
            PasteTask = new RelayCommand(PasteTaskMethod);
            PasteAsChildTaskCommand = new RelayCommand<EmployeeTask>(PasteAsChildTask);
            SelectParentCommand = new RelayCommand<EmployeeTask>(SelectParent);
            BackCommand = new RelayCommand(Back);
            SelectTaskCommand = new RelayCommand<EmployeeTask>(SelectTask);
            DeleteTask = new RelayCommand<EmployeeTask>(DeleteTaskMethod);
            StoreProcessSelection = new RelayCommand<ICollection<object>>(StoreProcesses);
            FilterTasks_My = new RelayCommand(ShowMyTasks);
            FilterTasks_AssignedOnMe = new RelayCommand(ShowTasksWhereIAssigned);
            FilterTasks_All = new RelayCommand(ShowAllTAsks);
            AddResult = new RelayCommand<EmployeeTask>(AddResultMethod);
            SelectAnalytic = new RelayCommand<AnalyticOrdered>(SelectAnalyticMethod);
            UnselectAnalytic = new RelayCommand<AnalyticOrdered>(UnselectAnalyticMethod);
        }

        private void PasteTaskMethod() => PasteAsChildTask(_currentTask);

        private void PasteAsChildTask(EmployeeTask obj)
        {
            if (_copiedTask != null)
            {
                if (isCut)
                {
                    _copiedTask.ParentTask = obj;
                }
                else
                { 
                    EmployeeTask _task = _copiedTask.AsCopy();
                    _task.ParentTask = obj;
                    obj.ChildTasks.Add(_task);
                }
                _tasks.Commit();
                UpdateTasks();
            }

        }

        private void CopyTask(EmployeeTask obj)
        {
            isCut = false;
            _copiedTask = obj;
        }

        private void CutTask(EmployeeTask obj)
        {
            isCut = true;
            _copiedTask = obj;
        }

        private void SelectAnalyticMethod(AnalyticOrdered analytic)
        {
            analytic.Selected = true;
            ReportSelectionUpdate();
        }
        private void UnselectAnalyticMethod(AnalyticOrdered analytic)
        {
            analytic.Selected = false;
            ReportSelectionUpdate();
        }

        private void AddResultMethod(EmployeeTask task)
        {
            ResultsWindow dialog = new ResultsWindow();
            int participationIndex;
            if (task.Reporter.FIO.Equals(_currentEmployee.FIO))//пользователь инициатор задачи
            {
                participationIndex = 1;
            }
            else if (task.Assignee.FIO.Equals(_currentEmployee.FIO)) //пользователь как ответственное лицо
            {
                participationIndex = 2;
            }
            else //задача не принадлежит пользователю
            {
                MessageBox.Show("Вы не являетесь участником данной задачи и не можете заявить результат", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (participationIndex == 1 && !string.IsNullOrEmpty(task.SupervisorComment))
            {
                dialog.CommentBox.Text = task.SupervisorComment;
            }
            else if (!string.IsNullOrEmpty(task.SupervisorComment))
            {
                dialog.CommentBox.Text = task.EmployeeComment;
            }

            if (dialog.ShowDialog() == true)
            {
                if (participationIndex == 1)
                {
                    task.SupervisorComment = dialog.CommentBox.Text;

                }
                else
                {
                    task.EmployeeComment = dialog.CommentBox.Text;
                }
                dialog.CommentBox.Text = string.Empty;
                _tasks.Commit();
            }
        }

        private void ShowMyTasks()
        {
            _mainTask = new EmployeeTask();
            string fio = $"{_currentAnalytic.LastName} {_currentAnalytic.FirstName} {_currentAnalytic.FatherName}";
            List<EmployeeTask> myTasks = new List<EmployeeTask>(_tasks.GetTasks().Where(i => i.Reporter.FIO != null && i.Reporter.FIO.Equals(fio)));
            for (int i = 0; i < myTasks.Count; i++)
            {
                if(myTasks[i].ParentTask!=null && myTasks[i].ParentTask.Reporter.FIO.Equals(fio))
                {
                    myTasks.Remove(myTasks[i--]);
                }
            }
            _mainTask.ChildTasks = myTasks;
            _currentTask = _mainTask;
            TaskStack.Clear();
            TaskStack.Push(_mainTask);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
            UpdateTasks();
        }

        private void ShowTasksWhereIAssigned()
        {
            _mainTask = new EmployeeTask();
            string fio = $"{_currentAnalytic.LastName} {_currentAnalytic.FirstName} {_currentAnalytic.FatherName}";
            List<EmployeeTask> myTasks = new List<EmployeeTask>(_tasks.GetTasks().Where(i => i.Assignee.FIO != null && i.Assignee.FIO.Equals(fio)));
            for (int i = 0; i < myTasks.Count; i++)
            {
                if (myTasks[i].ParentTask != null && myTasks[i].ParentTask.Reporter.FIO.Equals(fio))
                {
                    myTasks.Remove(myTasks[i--]);
                }
            }
            _mainTask.ChildTasks = myTasks;
            _currentTask = _mainTask;
            TaskStack.Clear();
            TaskStack.Push(_mainTask);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
            UpdateTasks();
        }

        private void ShowAllTAsks()
        {
            _mainTask = new EmployeeTask();
            _mainTask.ChildTasks = new List<EmployeeTask>(_tasks.GetTasks(null));
            _currentTask = _mainTask;
            TaskStack.Clear();
            TaskStack.Push(_mainTask);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
            UpdateTasks();
        }

        private void StoreProcesses(ICollection<object> obj)
        {
            List<ProcessProxy> _processes = new List<ProcessProxy>();
            foreach (object item in obj)
            {
                if (item is Process)
                {
                    _processes.Add(new ProcessProxy
                    {
                        ProcessId = (item as Process).Id
                    });
                }
            }
            newTask.Processes = _processes;

        }

        private void DeleteSubTasks(EmployeeTask task)
        {
            while (task.ChildTasks.Count > 0)
            {
                DeleteSubTasks(task.ChildTasks[0]);
            }
            CurrentTasks.Remove(task);
            _tasks.Remove(task);
        }

        private void DeleteTaskMethod(EmployeeTask task)
        {
            EmployeeTask deletedTask = null;
            foreach (EmployeeTask _task in CurrentTasks)
            {
                if (_task.Equals(task))
                {
                    deletedTask = _task;
                    break;
                }
            }
            if (deletedTask != null && deletedTask.ChildTasks != null)
            {
                if (deletedTask.ChildTasks.Count > 0)
                {
                    if (MessageBox.Show("Удаляемая задача содержит подзадачи, которые также будут удалены.\r\n Вы уверены?", "Удаление задачи", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        DeleteSubTasks(deletedTask);
                        _currentTask.ChildTasks.Remove(deletedTask);
                        if (_currentTask.ChildTasks.Count < 1)
                        {
                            Back();
                        }
                        UpdateTasks();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    DeleteSubTasks(deletedTask);
                    _currentTask.ChildTasks.Remove(deletedTask);
                    if (_currentTask.ChildTasks.Count < 1)
                    {
                        Back();
                    }
                    UpdateTasks();
                }

            }
        }

        private void SelectTask(EmployeeTask task)
        {
            SelectedTask = task;
            RaisePropertyChanged("SelectedTask");
            SelectedProcesses.Clear();
            foreach (ProcessProxy processProxy in SelectedTask.Processes)
            {
                SelectedProcesses.Add(Processes.FirstOrDefault(i => i.Id == processProxy.ProcessId));
            }
        }

        private void Back()
        {
            if (TaskStack.Count > 1)
            {
                TaskStack.Pop();
                SelectParent(TaskStack.Peek());
                TaskStack.Pop();
            }
        }

        private void SelectParent(EmployeeTask task)
        {
            if (task != null && task.ChildTasks.Count > 0)
            {
                TaskStack.Push(task);
                _currentTask = task;
                SelectTask(task.ChildTasks[0]);
                UpdateTasks();
            }
        }

        private void UpdateTasks()
        {
            CurrentTasks.Clear();

            foreach (EmployeeTask childTask in _currentTask.ChildTasks)
            {
                CurrentTasks.Add(childTask);
            }
        }

        private void GenerateNodes()
        {

            foreach (AnalyticOrdered analytic in SubordinatedOrdered)
            {
                #region initialize

                if (NodesCollection.Count < 1)
                    NodesCollection.Add(new Node(analytic.FirstStructure));

                #endregion

                #region Generate Ierarhial
                foreach (Node node in NodesCollection)
                {

                    #region 1stGen

                    if (string.IsNullOrEmpty(analytic.FirstStructure)) break;
                    Node firstGen = Node.FindNode(analytic.FirstStructure, NodesCollection);
                    if (firstGen == null)
                    {
                        firstGen = new Node(analytic.FirstStructure);
                        NodesCollection.Add(firstGen);
                    }

                    #endregion

                    #region 2ndGen

                    if (string.IsNullOrEmpty(analytic.SecondStructure))
                    {
                        firstGen.Analytics.Add(analytic);
                        break;
                    }

                    Node secondGen = Node.FindNode(analytic.SecondStructure, NodesCollection);
                    if (secondGen == null)
                    {
                        firstGen.AddChild(new Node(analytic.SecondStructure));
                        secondGen = Node.FindNode(analytic.SecondStructure, NodesCollection);
                    }

                    #endregion

                    #region 3ndGen

                    if (string.IsNullOrEmpty(analytic.ThirdStructure))
                    {
                        secondGen.Analytics.Add(analytic);
                        break;
                    }

                    Node thirdGen = Node.FindNode(analytic.ThirdStructure, NodesCollection);
                    if (thirdGen == null)
                    {
                        secondGen.AddChild(new Node(analytic.ThirdStructure));
                        thirdGen = Node.FindNode(analytic.ThirdStructure, NodesCollection);
                    }

                    #endregion

                    #region 4thGen

                    if (string.IsNullOrEmpty(analytic.FourStructure))
                    {
                        thirdGen.Analytics.Add(analytic);
                        break;
                    }

                    Node fourGen = Node.FindNode(analytic.FourStructure, NodesCollection);
                    if (fourGen == null)
                    {
                        thirdGen.AddChild(new Node(analytic.FourStructure));
                        fourGen = Node.FindNode(analytic.FourStructure, NodesCollection);
                    }
                    fourGen.Analytics.Add(analytic);
                    #endregion


                }
                #endregion


            }
            foreach (Node node1 in NodesCollection)
            {
                node1.CountAnalytics(node1);
            }
        }

        private void EditTask(EmployeeTask task)
        {
            EmployeeTask tempTask = task.AsCopy();
            newTask = task;
            EditedOwnerFIO = newTask.Owner.FIO;
            EditedAssigneeFIO = newTask.Assignee.FIO;
            AddTask addTaskWindow = new AddTask();

            addTaskWindow.ProcessList.SelectedItemsOverride = SelectedTask.Processes.Select(i => Processes.FirstOrDefault(n => n.Id == i.ProcessId)).ToList();
            if (addTaskWindow.ShowDialog() == false)
            {
                task.CopyFields(tempTask);
            }
            else
            {
                DeleteTaskMethod(task);
                AddTaskMethod();
                _tasks.Commit();
                UpdateTasks();
            }
            newTask = new EmployeeTask();
        }
    }
}