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
    public class OpMesa2AppService: IOpMesa2AppService
    {
        private readonly IOpMesa2Repository _mesaRepository;
        public OpMesa2AppService(IOpMesa2Repository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public void Abrir(OpMesa2ViewModel opmesa)
        {
            var mesa = TypeAdapter.Adapt<OpMesa2ViewModel, OpMesa2>(opmesa);
            _mesaRepository.Abrir(mesa);
        }

        public void Dispose()
        {
            _mesaRepository.Dispose();
        }


        public IEnumerable<OpMesa2ViewModel> PegarItens(int id)
        {

            return
                TypeAdapter.Adapt<IEnumerable<OpMesa2>, IEnumerable<OpMesa2ViewModel>>(
                    _mesaRepository.PegarItens(id));
        }

       public  void DeletarItem(int id)
        {
            _mesaRepository.DeletarItem(id);
        }

        public void Bonificar(int id)
        {
            _mesaRepository.Bonificar(id);
        }

        public void BonificarMesa(int id)
        {
            _mesaRepository.BonificarMesa(id);
        }

        public void PagaItem(int id, string metodo)
        {
            _mesaRepository.PagaItem(id, metodo);
        }

        public void EstornaItem(OpMesa2ViewModel opmesa)
        {
            var mesa = TypeAdapter.Adapt<OpMesa2ViewModel, OpMesa2>(opmesa);
            _mesaRepository.EstornaItem(mesa);
        }
    }
}
