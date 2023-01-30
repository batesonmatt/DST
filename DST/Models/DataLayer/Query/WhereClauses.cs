using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DST.Models.DataLayer.Query
{
    public class WhereClauses<T> : List<Expression<Func<T, bool>>> { }
}