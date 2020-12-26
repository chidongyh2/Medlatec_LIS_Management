using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LIS.Infrastructure.Domain.SystemAggregate;

namespace LIS.Core.Domain.IRepository
{
    public interface IProvinceRepository
    {
        Task<List<National>> GetAllNational();

        Task<National> GetNationById(Guid nationalId);

        Task<List<Province>> GetProvinceByNational(Guid nationalId);

        Task<List<Province>> GetAllProvince();

        Task<List<District>> GetAllDistrict();

        Task<List<District>> GetDistrictByProvinceId(Guid provinceId);

        Task<Province> GetProvinceInfo(Guid provinceId);

        Task<District> GetDistrictInfo(Guid districtId);
    }
}
