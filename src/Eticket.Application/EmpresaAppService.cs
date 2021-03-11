using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class EmpresaAppService : IEmpresaAppService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaAppService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public void Dispose()
        {
            _empresaRepository.Dispose();
        }

        public EmpresaViewModel ObterPorLoja(int lojaId)
        {
            return TypeAdapter.Adapt<Empresa, EmpresaViewModel>(_empresaRepository.GetById(lojaId));
        }
    }
}