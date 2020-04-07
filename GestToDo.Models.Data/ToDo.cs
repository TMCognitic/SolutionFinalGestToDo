using System;

namespace GestToDo.Models.Data
{
    public class ToDo
    {
        private int _id, _userId;
        private string _title, _description;
        private bool _done;
        private DateTime? _validationDate;

        public int Id
        {
            get
            {
                return _id;
            }

            private set
            {
                _id = value;
            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }

            private set
            {
                _userId = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public bool Done
        {
            get
            {
                return _done;
            }

            set
            {
                _done = value;
            }
        }

        public DateTime? ValidationDate
        {
            get
            {
                return _validationDate;
            }

            private set
            {
                _validationDate = value;
            }
        }

        public ToDo(string title, string description, int userId)
        {
            Title = title;
            Description = description;
            UserId = userId;
        }

        public ToDo(int id, string title, string description, bool done, DateTime? validationDate, int userId)
            : this(title, description, userId)
        {
            Id = id;
            Done = done;
            ValidationDate = validationDate;
        }
    }
}
