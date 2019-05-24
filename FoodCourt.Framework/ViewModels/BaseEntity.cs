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
    }
    public partial class Food : ICreateTimeEnable, IActivable
    {

    }

    public partial class MyIdentity : DoitsuUserInt, IActivable
    {
    }

    public partial class Order : ICreateTimeEnable
    {
    }

    public partial class Store : ICreateTimeEnable, IActivable
    {
    }

    public partial class Transaction : ICreateTimeEnable
    {

    }
}