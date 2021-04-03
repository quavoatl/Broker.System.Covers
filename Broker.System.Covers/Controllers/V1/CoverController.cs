using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Broker.System.Covers.Contracts.V1;
using Broker.System.Controllers.V1.Requests;
using Broker.System.Controllers.V1.Responses;
using Broker.System.Covers.Services;
using Broker.System.Domain;
using Broker.System.Covers.Extensions;
using Broker.System.Covers.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Broker.System.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Broker")]
    [ApiController]
    public class CoverController : Controller
    {
        private readonly ICoverService _coverService;
        private readonly IMapper _mapper;

        public CoverController(ICoverService coverService, IMapper mapper)
        {
            _coverService = coverService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Cover.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var limits = await _coverService.GetCoversAsync(HttpContext.GetUserId());

            if (limits.Count == 0) return NoContent();

            return Ok(_mapper.Map<List<CoverResponse>>(limits));
        }

        [HttpPost(ApiRoutes.Cover.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCoverRequest coverRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {Messages = ModelState.Values.SelectMany(v => v.Errors)});
            }
            
            Cover limit = new Cover()
            {
                BrokerId = HttpContext.GetUserId(),
                Type = coverRequest.Type,
                LimitMultiplier = coverRequest.LimitMultiplier
            };

            var createdLimit = await _coverService.CreateAsync(limit);

            if (createdLimit == null) return BadRequest(new {Message = "Cover already exists"});
          
            var baseUrl = $"{HttpContext.Request.Scheme}://" + $"{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" +
                              ApiRoutes.Cover.Get.Replace("{coverId}", createdLimit.Entity.CoverId.ToString());
     
            return Created(locationUri, _mapper.Map<CoverResponse>(limit));
        }

        [HttpGet(ApiRoutes.Cover.Get)]
        public async Task<IActionResult> Get([FromRoute] int coverId)
        {
            var limit = await _coverService.GetByIdAsync(coverId);
            if (limit == null) return NoContent();

            return Ok(_mapper.Map<CoverResponse>(limit));
        }

        [HttpPut(ApiRoutes.Cover.Update)]
        public async Task<IActionResult> Update(
            [FromRoute] int coverId,
            [FromBody] UpdateLimitRequest updateLimitRequest)
        {
            var userOwnsPost = await _coverService.UserOwnsCover(coverId, HttpContext.GetUserId());

            if (userOwnsPost)
            {
                var limitFromDb = await _coverService.GetByIdAsync(coverId);
                limitFromDb.LimitMultiplier = updateLimitRequest.LimitMultiplier;

                var updated = await _coverService.UpdateAsync(limitFromDb);

                if (updated) return Ok(_mapper.Map<CoverResponse>(limitFromDb));
            }

            return BadRequest(new {error = "You do not own this cover !"});
        }

        [HttpDelete(ApiRoutes.Cover.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int coverId)
        {
            var userOwnsPost = await _coverService.UserOwnsCover(coverId, HttpContext.GetUserId());

            if (userOwnsPost)
            {
                var deleted = await _coverService.DeleteAsync(coverId);
                if (deleted) return NoContent();
            }

            return NotFound();
        }
    }
}