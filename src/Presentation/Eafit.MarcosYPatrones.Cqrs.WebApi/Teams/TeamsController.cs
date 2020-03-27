using Eafit.MarcosYPatrones.Cqrs.Domain.Projects;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.CreateTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.DeleteTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.CheckTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.GetTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Projects.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.Teams
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ICommandHandler<CreateTeamCommand, Team> _createTeamCommandHandler;
        private readonly ICommandHandler<DeleteTeamCommand, bool> _deleteTeamCommandHandler;
        private readonly IQueryHandler<GetSingleTeamQuery, Team> _getSingleTeamQueryHandler;
        private readonly IQueryHandler<GetProjectsByTeamIdQuery, IEnumerable<Project>> _getProjectsByTeamIdQueryHandler;

        public TeamsController(
            ICommandHandler<CreateTeamCommand, Team> createTeamCommandHandler,
            ICommandHandler<DeleteTeamCommand, bool> deleteTeamCommandHandler,
            IQueryHandler<GetSingleTeamQuery, Team> getSingleTeamQueryHandler,
            IQueryHandler<GetProjectsByTeamIdQuery, IEnumerable<Project>> getProjectsByTeamId)
        {
            _createTeamCommandHandler = createTeamCommandHandler;
            _deleteTeamCommandHandler = deleteTeamCommandHandler;

            _getSingleTeamQueryHandler = getSingleTeamQueryHandler;
            _getProjectsByTeamIdQueryHandler = getProjectsByTeamId;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] GetSingleTeamQuery getSingleTeamQuery)
        {
            try
            {
                return Ok(await _getSingleTeamQueryHandler.HandleAsync(getSingleTeamQuery).ConfigureAwait(false));
            }
            catch (EntityDoesNotExistsException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateTeamCommand createTeamCommand)
        {
            try
            {
                Team createdTeam = await _createTeamCommandHandler
                    .HandleAsync(createTeamCommand)
                    .ConfigureAwait(false);
                return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
            }
            catch (EntityAlreadyExistsException exception)
            {
                return Conflict(exception.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] DeleteTeamCommand deleteTeamCommand)
        {
            try
            {
                await _deleteTeamCommandHandler
                    .HandleAsync(deleteTeamCommand)
                    .ConfigureAwait(false);
                return NoContent();
            }
            catch (EntityDoesNotExistsException)
            {
                return NotFound("Team doesn't exist");
            }
        }

        [HttpGet]
        [Route("{teamId}/projects")]
        public async Task<ActionResult> GetProjectsByTeamId(int teamId)
        {
            try
            {
                var getProjectsByTeamId = new GetProjectsByTeamIdQuery
                {
                    TeamId = teamId
                };
                return Ok(await _getProjectsByTeamIdQueryHandler.HandleAsync(getProjectsByTeamId).ConfigureAwait(false));
            }
            catch (EntityDoesNotExistsException)
            {
                return NotFound("No projects found");
            }
        }
    }
}
