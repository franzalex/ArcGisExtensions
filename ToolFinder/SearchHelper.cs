using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFinder
{
    class SearchHelper
    {
        /// <summary>Calculates the relevance of a search by comparing the search terms with the terms in the result.</summary>
        /// <typeparam name="T">Generic type of search and result terms</typeparam>
        /// <param name="searchTerms">Terms that were searched for.</param>
        /// <param name="resultTerms">Terms of an item returned by the search.</param>
        /// <returns>A single-precision number from 1.0 to 0.0 indicating the relevance of the search result item.</returns>
        public static float Relevance<T>(ICollection<T> searchTerms, IEnumerable<T> resultTerms)
        {
            return SearchHelper.Relevance(searchTerms, resultTerms, EqualityComparer<T>.Default);
        }

        /// <summary>Calculates the relevance of a search by comparing the search terms with the terms in the result.</summary>
        /// <typeparam name="T">Generic type of search and result terms</typeparam>
        /// <param name="searchTerms">Terms that were searched for.</param>
        /// <param name="resultTerms">Terms of an item returned by the search.</param>
        /// <param name="comparer">An <see cref="System.Collections.Generic.IEqualityComparer{T}"/> to use in comparing terms.</param>
        /// <returns>A single-precision number from 1.0 to 0.0 indicating the relevance of the search result item.</returns>
        public static float Relevance<T>(ICollection<T> searchTerms, IEnumerable<T> resultTerms,
                                         IEqualityComparer<T> comparer)
        {
            var intersection = searchTerms.Intersect(resultTerms, comparer);
            return (float)intersection.Count() / searchTerms.Count;
        }
    }
}
