using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Helpers
{
    //public static partial class ClaimTypes
    //{
    //    public const string UserId = "UserId";
    //    public const string Username = "Username";
    //    public const string Roles = "Roles";
    //}

    public static partial class DateTimeHelpers
    {
        public static bool IsValid(this DateTime current, DateTime start, DateTime end)
        {
            if(DateTime.Compare(start, current) <= 0 && DateTime.Compare(end, current) >= 0)
            {
                return true;
            }
            return false;
        }

    }

}

//namespace ThiHuong.Framework
//{
//    public static class EntitiesToViewModelsMapping
//    {
//        public static List<TDest> ToListViewModel<TSource, TDest>(this IEnumerable<TSource> entities)
//            where TDest : BaseViewModel
//            where TSource : BaseEntity
//        {
//            return entities.Select(e => e.ToViewModel<TDest>()).ToList();
//        }

//        public static List<TDest> ToListEntity<TSource, TDest>(this IEnumerable<TSource> entities)
//            where TSource : BaseViewModel
//            where TDest : BaseEntity
//        {
//            return entities.Select(e => e.ToEntity<TDest>()).ToList();
//        }
//    }
//}
