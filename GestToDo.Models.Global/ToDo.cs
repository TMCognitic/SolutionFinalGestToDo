using System;
using System.Collections.Generic;
using System.Text;

namespace GestToDo.Models.Global
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime? ValidationDate { get; set; }
        public int UserId { get; set; }
    }
}
