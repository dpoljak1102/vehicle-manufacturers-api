namespace VehicleManufacturers.Model.Common
{
    /// <remarks>
    /// Represents a vehicle model.
    /// Includes the following properties:
    /// <list type="bullet">
    ///     <item><term>Id</term> <description><see cref="System.Guid"/></description></item>
    ///     <item><term>MakeId</term> <description><see cref="System.Guid"/></description></item>
    ///     <item><term>Name</term> <description><see cref="System.String"/></description></item>
    ///     <item><term>Abrv</term> <description><see cref="System.String"/></description></item>
    /// </list>
    /// </remarks>
    public interface IVehicleModel
    {
        Guid Id { get; set; }
        Guid MakeId { get; set; }
        string? Name { get; set; }
        string? Abrv { get; set; }
    }
}
