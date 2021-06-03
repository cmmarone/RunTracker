using Microsoft.AspNet.Identity;
using RunTracker.Models.DistanceModels;
using RunTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RunTracker.WebAPI.Controllers
{
    [Authorize]
    public class DistanceController : ApiController
    {
        private DistanceService CreateDistanceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var distanceService = new DistanceService(userId);
            return distanceService;
        }

        public IHttpActionResult Post(DistanceCreate distance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateDistanceService();

            if (!service.CreateDistance(distance))
                return InternalServerError();

            return Ok("Distance created successfully.");
        }

        public IHttpActionResult Get()
        {
            var service = CreateDistanceService();
            var distances = service.GetDistances();
            return Ok(distances);
        }

        public IHttpActionResult Get(int id)
        {
            var service = CreateDistanceService();
            var distance = service.GetDistanceById(id);
            if (distance == null)
                return NotFound();
            return Ok(distance);
        }

        public IHttpActionResult Put(DistanceEdit distance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateDistanceService();

            if (!service.UpdateDistance(distance))
                return InternalServerError();

            return Ok("Distance updated successfully.");
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateDistanceService();

            if (!service.DeleteDistance(id))
                return InternalServerError();

            return Ok("Distance deleted successfully.");
        }
    }
}
