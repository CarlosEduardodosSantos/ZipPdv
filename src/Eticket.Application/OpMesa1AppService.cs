using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application
{
    public class OpMesa1AppService: IOpMesa1AppService
    {
        private readonly IOpMesa1Repository _mesaRepository;

        public OpMesa1AppService(IOpMesa1Repository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public void Dispose()
        {
            _mesaRepository.Dispose();
        }

        public int Abrir(OpMesa1ViewModel mesaView)
        {
            var mesa = TypeAdapter.Adapt<OpMesa1ViewModel, OpMesa1>(mesaView);
            return _mesaRepository.Abrir(mesa);
        }

        public void Atualizar(OpMesa1ViewModel opmesa)
        {
            var mesa = TypeAdapter.Adapt<OpMesa1ViewModel, OpMesa1>(opmesa);
           _mesaRepository.Atualizar(mesa);
        }

        public void Transferir(OpMesa1ViewModel opmesa)
        {
            var mesa = TypeAdapter.Adapt<OpMesa1ViewModel, OpMesa1>(opmesa);
            _mesaRepository.Transferir(mesa);
        }

        public OpMesa1ViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<OpMesa1, OpMesa1ViewModel>(_mesaRepository.GetById(id));
        }

        public void Pagamento(OpMesa1ViewModel opmesa)
        {
            var mesa = TypeAdapter.Adapt<OpMesa1ViewModel, OpMesa1>(opmesa);
            _mesaRepository.Pagamento(mesa);
        }
    }
}
