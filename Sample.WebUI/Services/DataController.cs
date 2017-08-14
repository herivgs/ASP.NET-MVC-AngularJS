using Coqueta.BusinessInterfaces;
using Coqueta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Coqueta.Mvc.Services
{
    public class DataController : ApiController
    {
        private readonly IDataProcessor _dataProcessor;
        public DataController(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        [HttpGet]
        [Route("api/Data/All")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var data = await _dataProcessor.GetAll();
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(BusinessLayerException ex)
            {
                var errors = default(string);
                foreach (var item in ex.Errors)
                {
                    errors += item;
                }
                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("api/Data/Create")]
        public async Task<IHttpActionResult> Create([FromBody] User user)
        {
            try
            {
                await _dataProcessor.AddUser(user);
                return Ok();
            }
            catch (BusinessLayerException ex)
            {
                var errors = default(string);
                foreach (var item in ex.Errors)
                {
                    errors += item;
                }
                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("api/Data/Edit")]
        public async Task<IHttpActionResult> Edit([FromBody] User user)
        {
            try
            {
                await _dataProcessor.UpdateUser(user);
                return Ok();
            }
            catch (BusinessLayerException ex)
            {
                var errors = default(string);
                foreach (var item in ex.Errors)
                {
                    errors += item;
                }
                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("api/Data/Delete/{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            try
            {
                await _dataProcessor.RemoveUser(id);
                return Ok();
            }
            catch (BusinessLayerException ex)
            {
                var errors = default(string);
                foreach (var item in ex.Errors)
                {
                    errors += item;
                }
                return BadRequest(errors);
            }
        }

        [HttpGet]
        [Route("api/Data/{id}")]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            try
            {
                var data = await _dataProcessor.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (BusinessLayerException ex)
            {
                var errors = default(string);
                foreach (var item in ex.Errors)
                {
                    errors += item;
                }
                return BadRequest(errors);
            }
        }
    }
}