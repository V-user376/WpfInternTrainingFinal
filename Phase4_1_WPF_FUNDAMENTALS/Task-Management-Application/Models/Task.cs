//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace Task_Management_Application.Models
//{
//    public class Task
//    {
//        public Guid Id { get; set; } = Guid.NewGuid();

//        public string Title { get; set; }

//        public string Description { get; set; }

//        public DateTime DueDate { get; set; }

//        public TaskPriority Priority { get; set; }

//        public bool IsCompleted { get; set; }

//        public DateTime CreatedDate { get; set; } = DateTime.Now;

//        public DateTime? CompletedDate { get; set; }
//    }
//}



using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_Management_Application.Models
{
    public class Task : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                dueDate = value;
                OnPropertyChanged();
            }
        }

        private TaskPriority priority;
        public TaskPriority Priority
        {
            get => priority;
            set
            {
                priority = value;
                OnPropertyChanged();
            }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        private DateTime? completedDate;
        public DateTime? CompletedDate
        {
            get => completedDate;
            set
            {
                completedDate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
