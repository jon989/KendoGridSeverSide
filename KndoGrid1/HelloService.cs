using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
namespace KndoGrid1
{
    [Route("/Orders")]
    public class Orders : QueryBase<SalesOrderDetail>
    {
        public string[] SalesOrderID { get; set; }
    }


    public class HelloService : Service
    {
        public IAutoQuery AutoQuery { get; set; }
        public object Any(Orders request)
        {
             var q = AutoQuery.CreateQuery(request, Request.GetRequestParams());
             var FirstFilterLogic = Request.QueryString["filter[logic]"];
             string Logic;
             if (FirstFilterLogic != null)
             {
                 for (var i = 0; Request.QueryString["filter[filters][" + i + "][filters][0][field]"] != null || Request.QueryString["filter[filters][" + i + "][field]"] != null; i++)
                 {
                     //we have only 2 simple cases!
                     Boolean IsSingle = true;
                     if (Request.QueryString["filter[filters][" + i + "][filters][0][field]"] != null)
                         IsSingle = false;
                     else if (Request.QueryString["filter[filters]["+i+"][field]"] != null)
                                                   
                         IsSingle = true;


                     if(IsSingle)
                     {
                         var Field = Request.QueryString["filter[filters][" + i + "][field]"];
                         var Value = Request.QueryString["filter[filters][" + i + "][value]"];
                         var Operator = Request.QueryString["filter[filters][" + i + "][operator]"];
                         Logic = "";
                         string sql = null;
                         string FirstSqlFilter = null;

                         if (Operator == "eq")
                             sql = (Field + " = " + Value);
                         else if (Operator == "neq")
                             sql = (Field + " != " + Value);
                         else if (Operator == "startswith")
                         {
                             FirstSqlFilter = Value + "%";
                             sql = (Field + " LIKE {0}");
                         }
                         else if (Operator == "contains")
                         {
                             sql = (Field + " LIKE {0}");
                             FirstSqlFilter = "%" + Value + "%";
                         }
                         else if (Operator == "doesnotcontain")
                         {
                             sql = (Field + " NOT LIKE {0}");
                             FirstSqlFilter = "%" + Value + "%";
                         }
                         else if (Operator == "endswith")
                         {
                             sql = (Field + " LIKE {0}");
                             FirstSqlFilter = "%" + Value;
                         }

                         if (FirstFilterLogic == "and")
                             q.And("(" + sql + ")", FirstSqlFilter);
                         else
                             q.Or("(" + sql + ")", FirstSqlFilter);
                         
                     }
                     else
                     {
                         string FirstSqlFilter = null;
                         string sql = null;
                         Logic = Request.QueryString["filter[filters][" + i + "][logic]"];

                         // ok he put 2 stuff
                         for (var j = 0; j < 2; j++)
                         {
                             var Field = Request.QueryString["filter[filters][" + i + "][filters][" + j + "][field]"];
                             var Value = Request.QueryString["filter[filters][" + i + "][filters][" + j + "][value]"];
                             var Operator = Request.QueryString["filter[filters][" + i + "][filters][" + j + "][operator]"];
                             if (Value != null && Operator != null)
                             {
                                 if (j == 0)
                                 {
                                     if (Operator == "eq")
                                         sql += (Field + " = " + Value);
                                     else if (Operator == "neq")
                                         sql += (Field + " != " + Value);
                                     else if (Operator == "startswith")
                                     {
                                         FirstSqlFilter = Value + "%";
                                         sql += (Field + " LIKE {0}");
                                     }
                                     else if (Operator == "contains")
                                     {
                                         sql += (Field + " LIKE {0}");
                                         FirstSqlFilter = "%" + Value + "%";
                                     }
                                     else if (Operator == "doesnotcontain")
                                     {
                                         sql += (Field + " NOT LIKE {0}");
                                         FirstSqlFilter = "%" + Value + "%";
                                     }
                                     else if (Operator == "endswith")
                                     {
                                         sql += (Field + " LIKE {0}");
                                         FirstSqlFilter = "%" + Value;
                                     }
                                 }
                                 else
                                 {
                                     if (Logic == "and")
                                     {
                                         Console.WriteLine(sql);
                                         if (Operator == "eq")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " AND " + (Field + " = " + Value) + ")", FirstSqlFilter);
                                             else
                                                 q.Where("(" + sql + " AND " + (Field + " = " + Value) + ")");
                                         }
                                         else if (Operator != "neq")
                                         {
                                             if (FirstSqlFilter == null)
                                                 q.Where("(" + sql + " AND " + (Field + " != " + Value) + ")", FirstSqlFilter);
                                             else
                                                 q.Where("(" + sql + " AND " + (Field + " != " + Value) + ")");
                                         }
                                         else if (Operator == "startswith")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " AND " + (Field + " LIKE {1}") + ")", FirstSqlFilter, Value + "%");
                                             else
                                                 q.Where("(" + sql + " AND " + (Field + " LIKE {0}") + ")", Value + "%");

                                         }
                                         else if (Operator == "contains")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " AND " + (Field + " LIKE {1}") + ")", FirstSqlFilter, "%" + Value + "%");
                                             else
                                                 q.Where("(" + sql + " AND " + (Field + " LIKE {0}") + ")", "%" + Value + "%");

                                         }
                                         else if (Operator == "doesnotcontain")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " AND " + (Field + " NOT LIKE {1}") + ")", FirstSqlFilter, "%" + Value + "%");
                                             else
                                                 q.Where("(" + sql + " AND " + (Field + " NOT LIKE {0}") + ")", "%" + Value + "%");

                                         }
                                         else if (Operator == "endswith")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " AND " + (Field + " LIKE {1}") + ")", FirstSqlFilter, "%" + Value);
                                             else
                                                 q.Where("(" + sql + " AND " + (Field + " LIKE {0}") + ")", "%" + Value);
                                         }
                                     }
                                     else
                                     {
                                         if (Operator == "eq")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " OR " + (Field + " = " + Value) + ")", FirstSqlFilter);
                                             else
                                                 q.Where("(" + sql + " OR " + (Field + " = " + Value) + ")");
                                         }
                                         else if (Operator != "neq")
                                         {
                                             if (FirstSqlFilter == null)
                                                 q.Where("(" + sql + " OR " + (Field + " != " + Value) + ")", FirstSqlFilter);
                                             else
                                                 q.Where("(" + sql + " OR " + (Field + " != " + Value) + ")");
                                         }
                                         else if (Operator == "startswith")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " OR " + (Field + " LIKE {1}") + ")", FirstSqlFilter, Value + "%");
                                             else
                                                 q.Where("(" + sql + " OR " + (Field + " LIKE {0}") + ")", Value + "%");

                                         }
                                         else if (Operator == "contains")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " OR " + (Field + " LIKE {1}") + ")", FirstSqlFilter, "%" + Value + "%");
                                             else
                                                 q.Where("(" + sql + " OR " + (Field + " LIKE {0}") + ")", "%" + Value + "%");

                                         }
                                         else if (Operator == "doesnotcontain")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " OR " + (Field + " NOT LIKE {1}") + ")", FirstSqlFilter, "%" + Value + "%");
                                             else
                                                 q.Where("(" + sql + " OR " + (Field + " NOT LIKE {0}") + ")", "%" + Value + "%");

                                         }
                                         else if (Operator == "endswith")
                                         {
                                             if (FirstSqlFilter != null)
                                                 q.Where("(" + sql + " OR " + (Field + " LIKE {1}") + ")", FirstSqlFilter, "%" + Value);
                                             else
                                                 q.Where("(" + sql + " OR " + (Field + " LIKE {0}") + ")", "%" + Value);
                                         }
                                     }
                                 }
                             }
                         }

                     }

                 }
             }
            // --- End Of Filtering --- 
           

            var result = AutoQuery.Execute(request, q);
            //--- Sorting ---
            List<SalesOrderDetail> SalesList = null;
            var field = Request.QueryString["sort[0][field]"];
            var dir = Request.QueryString["sort[0][dir]"];
            if (field != null && dir != null)
            {
                SalesList = (dir == "desc") ? result.Results.OrderBy(field + " DESC").ToList() : result.Results.OrderBy(field).ToList();
                result.Results = SalesList;
            }
            //--- End of Sorting ----


            return result;
        }
    } 
}