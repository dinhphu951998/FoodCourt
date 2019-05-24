using Doitsu.Service.Core.IdentitiesExtension;
using Doitsu.Service.Core.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

    }
    public partial class Food : BaseEntity, ICreateTimeEnable, IActivable
    {

    }

    public partial class MyIdentity : DoitsuUserInt, IActivable
    {
    }

    public partial class Order : BaseEntity, ICreateTimeEnable
    {
    }

    public partial class Store : BaseEntity, ICreateTimeEnable, IActivable
    {
    }

    public partial class Transaction : BaseEntity, ICreateTimeEnable
    {

    }
    public partial class Account : BaseEntity
    {

    }

    public partial class Category : BaseEntity
    {

    }
    
    public partial class OrderDetail : BaseEntity
    {

    }
    
    public partial class Payment : BaseEntity
    {

    }

}