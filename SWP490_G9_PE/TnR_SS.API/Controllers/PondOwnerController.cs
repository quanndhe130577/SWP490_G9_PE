using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Authorize]
    [Route("api/pondOwner")]
    [ApiController]
    public class PondOwnerController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;
        private readonly IMapper _mapper;

        public PondOwnerController(ITnR_SSSupervisor tnrssSupervisor, IMapper mapper)
        {
            _tnrssSupervisor = tnrssSupervisor;
            _mapper = mapper;
        }

        /*[HttpGet]
        [Route("getAll")]
        public ResponseModel GetAll(int id)
        {
            var rs = _tnrssSupervisor.PondOwner.GetAll().Select(po => _mapper.Map<PondOwnerResModel>(po)).ToList();
            return new ResponseBuilder<List<PondOwnerResModel>>().Success("Get Info Success").WithData(rs).ResponseModel;
        }*/
        [HttpGet]
        [Route("getByTraderId/{id}")]
        public ResponseModel GetByTraderId(int id)
        {
            var rs = _tnrssSupervisor.GetPondOwnerByTraderId(id);
            return new ResponseBuilder<List<PondOwnerAPIModel>>().Success("Get Info Success").WithData(rs).ResponseModel;
        }

        [HttpPost]
        [Route("addNewPondOwner")]
        public async Task<ResponseModel> AddNewPondOwner(PondOwnerAPIModel pondOwner)
        {
            var valid = Valid(pondOwner);
            if (valid.IsValid)
            {
                await _tnrssSupervisor.AddPondOwner(pondOwner);
                return new ResponseBuilder<List<PondOwnerAPIModel>>().Success("Thêm thành công").ResponseModel;
            }
            else
            {
                return new ResponseBuilder<List<PondOwnerAPIModel>>().Error(valid.Message).ResponseModel;
            }
        }

        public static PondOwnerValidModel Valid(PondOwnerAPIModel pondOwner)
        {
            if (pondOwner.Name == null)
            {
                return new PondOwnerValidModel() { IsValid = false, Message = "Tên không được để trống" };
            }
            if (pondOwner.Address == null)
            {
                return new PondOwnerValidModel() { IsValid = false, Message = "Địa chỉ không được để trống" };

            }
            if (pondOwner.PhoneNumber == null)
            {

                return new PondOwnerValidModel() { IsValid = false, Message = "Điện thoại không được để trống" };
            }
            return new PondOwnerValidModel() { IsValid = true, Message = "Cập nhật thành công" };
        }

    }
}