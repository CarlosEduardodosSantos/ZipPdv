using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;
using System;
using System.Collections.Generic;

namespace Eticket.Application
{
    public class CadMesasAppService : ICadMesasAppService
    {
        private readonly ICadMesasRepository _mesaRepository;

        public CadMesasAppService(ICadMesasRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public void Dispose()
        {
            _mesaRepository.Dispose();
        }

        public IEnumerable<CadMesasViewModel> ObterMesas()
        {
            return
                TypeAdapter.Adapt<IEnumerable<CadMesas>, IEnumerable<CadMesasViewModel>>(
                    _mesaRepository.ObterMesas());
        }

        public IEnumerable<CadMesasViewModel> ObterMesasDisponiveis()
        {
            return
                TypeAdapter.Adapt<IEnumerable<CadMesas>, IEnumerable<CadMesasViewModel>>(
                    _mesaRepository.ObterMesasDisponiveis());
        }

        public CadMesasViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<CadMesas, CadMesasViewModel>(_mesaRepository.GetById(id));
        }

        public void AlterarStatusMesa(CadMesasViewModel mesaView)
        {
            var mesa = TypeAdapter.Adapt<CadMesasViewModel, CadMesas>(mesaView);
            _mesaRepository.AlterarStatusMesa(mesa);
        }


        public void IncluirOpMesa1(CadMesasViewModel mesaView)
        {
            var mesa = TypeAdapter.Adapt<CadMesasViewModel, CadMesas>(mesaView);
            _mesaRepository.IncluirOpMesa1(mesa);
        }

    }
}
