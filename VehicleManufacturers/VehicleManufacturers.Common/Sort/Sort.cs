namespace VehicleManufacturers.Common.Sort
{
    public  class Sort : ISort
    {
        public string? SortBy { get; set; }
        public string? Order { get; set; } = SortOrder.Asc;
    }
}
