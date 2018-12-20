using BTMEntity;
using BTMService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTMApi.Controllers
{
    [RoutePrefix("api/Bus")]
    public class BusApiController : ApiController
    {
        BusService service = new BusService() ;

        [Route("")]
        public IHttpActionResult Get()
        {
            List<Bus> result = service.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            else
                return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{id}", Name = "FindBus")]
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri]int id)
        {
            Bus bus = service.Get(id);
            if (bus == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(bus);
        }
        [Route("")]
        public IHttpActionResult Post([FromBody]Bus bus)
        {
            var insert = service.Insert(bus);
            return Created(Url.Link("FindBus", new { id = bus.Id }), bus);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromBody]Bus bus, [FromUri] int id)
        {
            bus.Id = id;
            var update = service.Update(bus);
            return Ok(bus);
        }
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            Bus bus = service.Get(id);
            if (bus !=null)
            {
                service.Delete(bus);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}