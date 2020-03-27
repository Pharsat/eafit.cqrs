using Eafit.MarcosYPatrones.Cqrs.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Projects.Queries
{
    public class GetProjectsByTeamIdHandler : IQueryHandler<GetProjectsByTeamIdQuery, IEnumerable<Project>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectsByTeamIdHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Project>> HandleAsync(GetProjectsByTeamIdQuery query)
        {
            return await _unitOfWork.Projects.GetProjectsByTeamId(query.TeamId).ConfigureAwait(false);
        }
    }
}
