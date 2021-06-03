using Microsoft.AspNet.Identity;
using RunTracker.Models;
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
    public class RunController : ApiController
    {
        private RunService CreateRunService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var runService = new RunService(userId);
            return runService;
        }

        public IHttpActionResult Post(RunCreate run)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRunService();

            if (!service.CreateRun(run))
                return InternalServerError();

            return Ok("Run created successfully.");
        }

        public IHttpActionResult Get()
        {
            RunService service = CreateRunService();
            var notes = service.GetRuns();
            return Ok(notes);
        }

        public IHttpActionResult Get(int id)
        {
            RunService service = CreateRunService();
            var run = service.GetRunById(id);
            if (run == null)
                return NotFound();
            return Ok(run);
        }

        public IHttpActionResult Put(RunEdit run)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRunService();

            if (!service.UpdateRun(run))
                return InternalServerError();

            return Ok("Run updated successfully.");
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateRunService();

            if (!service.DeleteRun(id))
                return InternalServerError();

            return Ok("Run deleted successfully.");
        }
    }
}
