using System.Collections.Generic;

namespace EntFrm.Framework.Utility
{
    public class FilterChain : InterFilter
    {
        private List<InterFilter> filterList = new List<InterFilter>();

        public FilterChain addFilter(InterFilter filter)
        {
            filterList.Add(filter);
            return this;
        }


        public InterFilter filter(string[] source)
        {
            foreach (InterFilter filter in this.filterList)
            {
                filter.filter(source);
            }
            return this;
        }
    }
}
