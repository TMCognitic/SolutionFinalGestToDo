using GestToDo.Models.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GestToDo.Api.Models.Mappers
{
    internal static class IDataRecordExtensions
    {
        internal static User ToUser(this IDataRecord dataRecord)
        {
            return new User() { Id = (int)dataRecord["Id"], LastName = (string)dataRecord["LastName"], FirstName = (string)dataRecord["FirstName"], Email = (string)dataRecord["Email"] };
        }

        internal static ToDo ToToDo(this IDataRecord dataRecord)
        {
            return new ToDo() { Id = (int)dataRecord["Id"], Title = (string)dataRecord["Title"], Description = (string)dataRecord["Description"], Done = (bool)dataRecord["Done"], ValidationDate = (dataRecord["ValidationDate"] is DBNull) ? null : (DateTime?)dataRecord["ValidationDate"], UserId = (int)dataRecord["UserId"] };
        }
    }
}