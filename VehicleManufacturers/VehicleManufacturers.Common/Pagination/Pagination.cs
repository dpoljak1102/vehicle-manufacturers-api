namespace VehicleManufacturers.Common.Pagination
{
    public class Pagination : IPagination
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
