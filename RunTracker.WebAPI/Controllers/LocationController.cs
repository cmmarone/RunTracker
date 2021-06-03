using Microsoft.AspNet.Identity;
using RunTracker.Models.LocationModels;
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
    public class LocationController : ApiController
    {
        private LocationService CreateLocationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var locationService = new LocationService(userId);
            return locationService;
        }

        public IHttpActionResult Post(LocationCreate location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLocationService();

            if (!service.CreateLocation(location))
                return InternalServerError();

            return Ok("Location created successfully.");
        }

        public IHttpActionResult Get()
        {
            var service = CreateLocationService();
            var locations = service.GetLocations();
            return Ok(locations);
        }

        public IHttpActionResult Get(int id)
        {
            var service = CreateLocationService();
            var location = service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return Ok(location);
        }

        public IHttpActionResult Put(LocationEdit location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLocationService();

            if (!service.UpdateLocation(location))
                return InternalServerError();

            return Ok("Location updated successfully.");
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateLocationService();

            if (!service.DeleteLocation(id))
                return InternalServerError();

            return Ok("Location deleted successfully.");
        }
    }
}
