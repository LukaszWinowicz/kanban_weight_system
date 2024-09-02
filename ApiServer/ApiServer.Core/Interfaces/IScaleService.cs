using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleService
    {
        IEnumerable<ScaleDto> GetAll();
    }
}
