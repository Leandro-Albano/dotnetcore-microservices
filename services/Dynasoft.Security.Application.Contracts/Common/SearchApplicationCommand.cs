namespace Dynasoft.Security.Application.Contracts.Common
{
    public class SearchApplicationCommand
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int OrderBy { get; set; }
        public OrderDirection OrderDirection { get; set; }
    }

    public enum OrderDirection
    {
        Asc,
        Desc
    }
}
