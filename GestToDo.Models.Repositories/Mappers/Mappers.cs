using GestToDo.Models.Data;
using G = GestToDo.Models.Global;
using System;
using System.Collections.Generic;
using System.Text;


namespace GestToDo.Models.Repositories.Mappers
{
    internal static class Mappers
    {
        internal static ToDo ToClient(this G.ToDo entity)
        {
            return new ToDo(entity.Id, entity.Title, entity.Description, entity.Done, entity.ValidationDate, entity.UserId);
        }
    }
}
