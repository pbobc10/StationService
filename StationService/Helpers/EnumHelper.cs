using Microsoft.AspNetCore.Mvc.Rendering;

namespace StationService.Helpers
{
    public static class EnumHelper
    {
        public static List<SelectListItem> GetEnumSelectList<T>() where T : Enum
        { 
            return Enum.GetValues(typeof(T)).Cast<T>().Select( e => new SelectListItem{ 
            Value =  e.ToString(),
            Text = e.ToString()
            }).ToList();
        }
    }
}
