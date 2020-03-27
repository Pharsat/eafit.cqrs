using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Queries.GetDueDate;
using Eafit.MarcosYPatrones.Cqrs.Application.Projects.Queries;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.CreateTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.DeleteTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.CheckTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.GetTeam;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories;
using Eafit.MarcosYPatrones.Cqrs.Common;
using Eafit.MarcosYPatrones.Cqrs.Domain.Projects;
using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
using Eafit.MarcosYPatrones.Cqrs.Infrastructure;
using Eafit.MarcosYPatrones.Cqrs.Persistence;
using Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.EntityMapping;
using Eafit.MarcosYPatrones.Cqrs.Persistence.Projects;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using Eafit.MarcosYPatrones.Cqrs.Persistence.Teams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.DeleteDueDate;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxPayerTypes;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.UpdateDueDate;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Quarters;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxPayerTypes.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes.Queries;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions.Queries;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms.Queries;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears.Queries;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Quarters.Queries;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.QuarterDueDates;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi
{
    public class Startup
    {
        private readonly string _appName = "Eafit.MarcosYPatrones.Cqrs.Application";
        private readonly string _allowedOriginsPolicyName = "_allowedOriginsPolicyName";
        private readonly IEnumerable<string> _allowedOrigins;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<List<string>>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_allowedOrigins.Any())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(_allowedOriginsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins(_allowedOrigins.ToArray());
                    });
                });
            }

            services.AddControllers();

            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped<IDbConnection>(_ => new SqlConnection(Configuration["connectionString"]));

            services.AddScoped<ICommandHandler<CreateTeamCommand, Team>, CreateTeamCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteTeamCommand, bool>, DeleteTeamCommandHandler>();
            services.AddScoped<IVoidCommandHandler<DeleteDueDateCommand>, DeleteDueDateCommandHandler>();
            services.AddScoped<IVoidCommandHandler<UpdateDueDateCommand>, UpdateDueDateCommandHandler>();
            services.AddScoped<IQueryHandler<GetSingleTeamQuery, Team>, GetSingleTeamQueryHandler>();
            services.AddScoped<IQueryHandler<CheckIfExistsQuery, bool>, CheckIfExistsQueryHandler>();
            services.AddScoped<IQueryHandler<CheckIfExitsByNameQuery, bool>, CheckIfExitsByNameQueryHandler>();
            services.AddScoped<IQueryHandler<GetSingleDueDateQuery, DueDateViewModel>, GetSingleDueDateQueryHandler>();
            services.AddScoped<IQueryHandler<GetProjectsByTeamIdQuery, IEnumerable<Project>>, GetProjectsByTeamIdHandler>();
            services.AddScoped<IQueryHandler<GetAllTaxPayerTypeQuery, IEnumerable<TaxPayerType>>, GetAllTaxPayerTypeQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllDeliverableTypesQuery, IEnumerable<DeliverableType>>, GetAllDeliverableTypesQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllJurisdictionsQuery, IEnumerable<Jurisdiction>>, GetAllJurisdictionsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllFormsQuery, IEnumerable<Form>>, GetAllFormsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllTaxYearsQuery, IEnumerable<TaxYear>>, GetAllTaxYearsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllQuartersQuery, IEnumerable<Quarter>>, GetAllQuartersQueryHandler>();

            services.AddScoped<IDataComparer, DataComparer>();
            services.AddScoped<ISqlGenerator, BaseSqlGenerator>();

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IDueDateRepository, DueDateRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IDueDateRepository, DueDateRepository>();
            services.AddScoped<ITaxPayerTypeRepository, TaxPayerTypeRepository>();
            services.AddScoped<IDeliverableTypeRepository, DeliverableTypeRepository>();
            services.AddScoped<IJurisdictionRepository, JurisdictionRepository>();
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<ITaxYearRepository, TaxYearRepository>();
            services.AddScoped<IQuarterRepository, QuarterRepository>();
            services.AddScoped<IQuarterDueDateRepository, QuarterDueDateRepository>();

            services.AddScoped<ITeamSqlGenerator, TeamSqlGenerator>();
            services.AddScoped<IDueDateSqlGenerator, DueDateSqlGenerator>();
            services.AddScoped<IProjectSqlGenerator, ProjectSqlGenerator>();
            services.AddScoped<IFormSqlGenerator, FormSqlGenerator>();
            services.AddScoped<IListItemSqlGenerator, ListItemSqlGenerator>();
            services.AddScoped<ITaxYearSqlGenerator, TaxYearSqlGenerator>();

            services.AddSingleton<IEntityPropertiesProvider, EntityPropertiesProvider>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = _appName, Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (_allowedOrigins.Any())
            {
                app.UseCors(_allowedOriginsPolicyName);
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", _appName);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
