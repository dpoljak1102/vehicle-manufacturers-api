namespace VehicleManufacturers.Model.Common
{
    /// <remarks>
    /// Represents a vehicle make.
    /// Includes the following properties:
    /// <list type="bullet">
    ///     <item><term>Id</term> <description><see cref="System.Guid"/></description></item>
    ///     <item><term>Name</term> <description><see cref="System.String"/></description></item>
    ///     <item><term>Abrv</term> <description><see cref="System.String"/></description></item>
    /// </list>
    /// </remarks>
    public interface IVehicleMake
    {
        Guid Id { get; set; }
        string? Name { get; set; }
        string? Abrv { get; set; }
    }
}
