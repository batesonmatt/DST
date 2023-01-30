using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DST.Models.DataLayer.Query
{
    public class OrderClauses<T> : List<Expression<Func<T, object>>> { }
}