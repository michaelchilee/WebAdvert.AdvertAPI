﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvertAPI.Controllers
{
    [ApiController]
    [Route("api/v1/adverts")]  // implement versioning so don't break clients --- e.g. v2, v3 next rev
   
    public class Advert : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public Advert(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type=typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            
            };

            return StatusCode(201, new CreateAdvertResponse { Id = recordId });

        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);

            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);

            };

            return new OkResult();
        }
    }
}
