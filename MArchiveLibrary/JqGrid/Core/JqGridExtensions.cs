using MArchiveLibrary.JqGrid.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;

namespace MArchiveLibrary.JqGrid.Core
{
    public static class JqGridExtensions
    {
        /// <summary>
        /// Converts a queryable expression into a format suitable for the JqGrid component, when
        /// serialized to JSON. Use this method when returning data that the JqGrid component will
        /// fetch via AJAX. Take the actionResponse of this method, and then serialize to JSON. This method
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object
        /// graph. The default JSON serializer will throw an exception if the object graph it is
        /// serializing contains cycles.</typeparam>
        /// <param name="baseList">The list of records to display in the grid.</param>
        /// <param name="currentPage">A 1-based index indicating which page the grid is about to display.</param>
        /// <param name="rowsPerPage">The maximum number of rows which the grid can display at the moment.</param>
        /// <param name="orderBy">A string expression containing a column name and an optional ASC or
        /// DESC. You can, separate multiple column names as with SQL.</param>
        /// <param name="searchQuery">The value which the user typed into the search box, if any. Can be
        /// null/empty.</param>
        /// <param name="searchColumns">An array of strings containing the names of properties in the
        /// element type of baseList which should be considered when searching the data for searchQuery.</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call
        /// a JSON serializer, passing this actionResponse.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this IQueryable<T> baseList,
            int currentPage,
            int rowsPerPage,
            string orderBy,
            Dictionary<string, string> searchQuery,
            IEnumerable<string> searchColumns)
        {
            return ToJqGridData(baseList, currentPage, rowsPerPage, orderBy, searchQuery, searchColumns, null);
        }

        /// <summary>
        /// Converts a queryable expression into a format suitable for the JqGrid component, when
        /// serialized to JSON. Use this method when returning data that the JqGrid component will
        /// fetch via AJAX. Take the actionResponse of this method, and then serialize to JSON. This method
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object
        /// graph. The default JSON serializer will throw an exception if the object graph it is
        /// serializing contains cycles.</typeparam>
        /// <param name="baseList">The list of records to display in the grid.</param>
        /// <param name="currentPage">A 1-based index indicating which page the grid is about to display.</param>
        /// <param name="rowsPerPage">The maximum number of rows which the grid can display at the moment.</param>
        /// <param name="orderBy">A string expression containing a column name and an optional ASC or
        /// DESC. You can, separate multiple column names as with SQL.</param>
        /// <param name="searchQuery">The value which the user typed into the search box, if any. Can be
        /// null/empty.</param>
        /// <param name="searchColumns">An array of strings containing the names of properties in the
        /// element type of baseList which should be considered when searching the data for searchQuery.</param>
        /// <param name="userData">Arbitrary data to be returned to the grid along with the row data. Leave
        /// null if not using. Must be serializable to JSON!</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call
        /// a JSON serializer, passing this actionResponse.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this IQueryable<T> baseList,
            int currentPage,
            int rowsPerPage,
            string orderBy,
            Dictionary<string, string> searchQuery,
            IEnumerable<string> searchColumns,
            object userData)
        {
            var filteredList = ListAddSearchQuery(baseList, searchQuery, searchColumns);
            var orderedList = (orderBy != null) ? filteredList.OrderBy<T>(orderBy) : filteredList;
            var pagedModel = new PagedList<T>(orderedList, currentPage - 1, rowsPerPage);
            return pagedModel.ToJqGridData(userData);
        }

        /// <summary>
        /// Converts a queryable expression into a format suitable for the JqGrid component, when
        /// serialized to JSON. Use this method when returning data that the JqGrid component will
        /// fetch via AJAX. Take the actionResponse of this method, and then serialize to JSON. This method
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object
        /// graph. The default JSON serializer will throw an exception if the object graph it is
        /// serializing contains cycles.</typeparam>
        /// <param name="baseList">The list of records to display in the grid.</param>
        /// <param name="currentPage">A 1-based index indicating which page the grid is about to display.</param>
        /// <param name="rowsPerPage">The maximum number of rows which the grid can display at the moment.</param>
        /// <param name="orderBy">A string expression containing a column name and an optional ASC or
        /// DESC. You can, separate multiple column names as with SQL.</param>
        /// <param name="searchQuery">The value which the user typed into the search box, if any. Can be
        /// null/empty.</param>
        /// <param name="searchColumns">An array of strings containing the names of properties in the
        /// element type of baseList which should be considered when searching the data for searchQuery.</param>
        /// <param name="userData">Arbitrary data to be returned to the grid along with the row data. Leave
        /// null if not using. Must be serializable to JSON!</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call
        /// a JSON serializer, passing this actionResponse.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this IQueryable<T> baseList,
            int currentPage,
            int rowsPerPage)
        {
            var pagedModel = new PagedList<T>(baseList, currentPage - 1, rowsPerPage);
            return pagedModel.ToJqGridData();
        }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
    Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this IEnumerable<T> baseList,
            int currentPage,
            int rowsPerPage)
        {
            var pagedModel = new PagedList<T>(baseList, currentPage - 1, rowsPerPage);
            return pagedModel.ToJqGridData();
        }

        /// <summary>
        /// Converts a paged list into a format suitable for the JqGrid component, when
        /// serialized to JSON. Use this method when returning data that the JqGrid component will
        /// fetch via AJAX. Take the actionResponse of this method, and then serialize to JSON. This method
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object
        /// graph. The default JSON serializer will throw an exception if the object graph it is
        /// serializing contains cycles.</typeparam>
        /// <param name="list">The list of records to display in the grid.</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call
        /// a JSON serializer, passing this actionResponse.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this PagedList<T> list)
        {
            return ToJqGridData(list, null);
        }

        /// <summary>
        /// Converts a paged list into a format suitable for the JqGrid component, when
        /// serialized to JSON. Use this method when returning data that the JqGrid component will
        /// fetch via AJAX. Take the actionResponse of this method, and then serialize to JSON. This method
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object
        /// graph. The default JSON serializer will throw an exception if the object graph it is
        /// serializing contains cycles.</typeparam>
        /// <param name="list">The list of records to display in the grid.</param>
        /// <param name="userData">Arbitrary data to be returned to the grid along with the row data.
        /// Leave null if not using. Must be serializable to JSON!</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call
        /// a JSON serializer, passing this actionResponse.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this PagedList<T> list, object userData)
        {
            return new JqGridData()
            {
                page = list.PageIndex + 1,
                records = list.TotalItemCount,
                rows = from record in list
                       select record,
                total = list.PageCount,
                userData = userData
            };
        }

        /// <summary>
        /// Adds a Where to a Queryable list of entity instances.  In other words, filter the list
        /// based on the search parameters passed.
        /// </summary>
        /// <typeparam name="T">Entity type contained within the list</typeparam>
        /// <param name="baseList">Unfiltered list</param>
        /// <param name="searchQuery">Whatever the user typed into the search box</param>
        /// <param name="searchColumns">List of entity properties which should be included in the
        /// search.  If any property in an entity instance begins with the search query, it will
        /// be included in the actionResponse.</param>
        /// <returns>Filtered list.  Note that the query will not actually be executed until the
        /// IQueryable is enumerated.</returns>
        public static IQueryable<T> ListAddSearchQuery<T>(IQueryable<T> baseList, Dictionary<string, string> searchQuery, IEnumerable<string> searchColumns)
        {
            if (searchQuery == null || searchQuery.Count() == 0 | (searchColumns == null))
                return baseList;

            const string strpredicateFormat = "{0}.ToString().StartsWith(@0)";
            var searchExpression = new System.Text.StringBuilder();
            string orPart = String.Empty;

            foreach (var item in searchQuery.Where(q => searchColumns.Contains(q.Key)))
            {
                searchExpression.Append(orPart);
                searchExpression.AppendFormat(strpredicateFormat, item.Key, item.Value);
                orPart = " AND ";
            }

            if (string.IsNullOrEmpty(searchExpression.ToString()))
                return baseList;

            var filteredList = baseList.Where(searchExpression.ToString(), searchQuery.Where(q => searchColumns.Contains(q.Key)).Select(q => q.Value).ToArray());
            return filteredList;
        }

        public static IQueryable<T> ListAddSearchQuery<T>(IQueryable<T> baseList, JqGridRequest jqGridRequest)
        {
            if (jqGridRequest.filters == null)
                return baseList;

            if (jqGridRequest.filters != null)
            {
                jqGridRequest.filters.rules.RemoveAll(q => q.data == "-1");
            }

            var searchExpression = GetSearchExpression(jqGridRequest);

            if (string.IsNullOrEmpty(searchExpression.ToString()))
                return baseList;
            var filteredList = baseList.Where(searchExpression.ToString());

            return filteredList;
        }

        public static IQueryable<T> MakePagination<T>(IQueryable<T> filteredList, JqGridRequest jqGridRequest)
        {
            filteredList = filteredList.OrderBy(jqGridRequest.sidx.Replace("_", ".") + " " + jqGridRequest.sord)
                .Skip((jqGridRequest.page - 1) * jqGridRequest.rows)
                .Take(jqGridRequest.rows)
                ;

            return filteredList;
        }

        private static StringBuilder GetSearchExpression(JqGridRequest jqGridRequest)
        {
            const string strPredicateFormatBeginsWith = "{0}.ToString().StartsWith(\"{1}\")";
            const string strPredicateFormatContains = "{0} != null && {0}.ToString() != \"\" && {0}.ToString().ToLower().Contains(\"{1}\".ToLowerInvariant())";
            const string strPredicateFormatEqual = "{0}.ToString() = \"{1}\"";
            const string strPredicateFormatEqualForChar = "{0}.ToString() = {1}.ToString()";
            const string strPredicateFormatDateGreaterOrEqual = "{0} >= DateTime.Parse(\"{1}\")";
            const string strPredicateFormatDateGreaterThan = "{0} > DateTime.Parse(\"{1}\")";
            const string strPredicateFormatDateLessThan = "{0} <= DateTime.Parse(\"{1}\")";
            const string strPredicateFormatDateEquals = "{0}.Date = DateTime.Parse(\"{1}\")";
            const string strPredicateFormatNullIntEquals = "{0}.HasValue && {0}.Value.ToString() = \"{1}\"";
            const string strPredicateFormatNumberEquals = "{0} = Decimal.Parse(\"{1}\")";
            const string strPredicateFormatNumberGreaterThan = "{0} >= Decimal.Parse(\"{1}\")";
            const string strPredicateFormatNumberGreaterOrEqualThan = "{0} >= Decimal.Parse(\"{1}\")";
            const string strPredicateFormatNumberLessThan = "{0} <= Decimal.Parse(\"{1}\")";

            var searchExpression = new System.Text.StringBuilder();
            string groupOperation = String.Empty;

            foreach (JqGridFilterRule item in jqGridRequest.filters.rules)
            {
                if (string.IsNullOrEmpty(item.data) == false)
                {
                    item.data = item.data.Trim();
                }
                if (item.op == "bw")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatBeginsWith, item.field, item.data);
                }
                else if (item.op == "eq")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatEqual, item.field, item.data);
                }
                else if (item.op == "cn")
                {
                    searchExpression.Append(groupOperation);
                    if (item.data.Contains("\""))
                        searchExpression.AppendFormat(strPredicateFormatContains, item.field, item.data.Replace("\"", "\"\""));
                    else
                        searchExpression.AppendFormat(strPredicateFormatContains, item.field, item.data);
                }
                else if (item.op == "dgt")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatDateGreaterThan, item.field, item.data);
                }
                else if (item.op == "dge")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatDateGreaterOrEqual, item.field, item.data);
                }
                else if (item.op == "dlt")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatDateLessThan, item.field, item.data);
                }
                else if (item.op == "deq")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatDateEquals, item.field, item.data);
                }
                else if (item.op == "gt")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatNumberGreaterThan, item.field, item.data);
                }
                else if (item.op == "ge")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatNumberGreaterOrEqualThan, item.field, item.data);
                }
                else if (item.op == "lt")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatNumberLessThan, item.field, item.data);
                }
                else if (item.op == "chr" && item.data != "-1")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatEqualForChar, item.field, item.data);
                }
                else if (item.op == "neq" && item.data != "-1")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatNullIntEquals, item.field, item.data);
                }
                else if (item.op == "dec" && item.data != "-1")
                {
                    searchExpression.Append(groupOperation);
                    searchExpression.AppendFormat(strPredicateFormatNumberEquals, item.field, item.data);
                }

                groupOperation = String.Format(" {0} ", jqGridRequest.filters.groupOp);
            }
            return searchExpression;
        }
    }
}
