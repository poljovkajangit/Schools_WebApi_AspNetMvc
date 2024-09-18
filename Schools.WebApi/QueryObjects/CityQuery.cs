namespace SchoolWebApi.QueryObjects
{
    public class CityQuery
    {
        public string? Name { get; set; } = null;
        public string? Code { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int? PageNumber { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
