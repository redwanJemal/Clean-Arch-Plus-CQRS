using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Entites;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApp.Application.Commands
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IUnitOfWork _uow;

        public DeleteDepartmentCommandHandler(IUnitOfWork Uow)
        {
            _repo = Uow.DepartmentRepositroy;
            _uow = Uow;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _repo.GetById(request.Id);
            _repo.Delete(department);

            var result = await _uow.Commit();

            return result > 0;
        }
    }

    public class DeleteDepartmentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteDepartmentCommand(Guid id)
        {
            Id = id;
        }
        public DeleteDepartmentCommand()
        {

        }
    }
}
