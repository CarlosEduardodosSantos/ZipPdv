﻿using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class CaixaAppService : ICaixaAppService
    {
        private readonly ICaixaRepository _caixaRepository;

        public CaixaAppService(ICaixaRepository caixaRepository)
        {
            _caixaRepository = caixaRepository;
        }

        public void Dispose()
        {
            _caixaRepository.Dispose();
        }

        public void Abrir(CaixaViewModel caixaView)
        {
            var caixa = TypeAdapter.Adapt<CaixaViewModel, Caixa>(caixaView);
            _caixaRepository.Abrir(caixa);
        }

        public void Fechar(CaixaViewModel caixaView)
        {
            var caixa = TypeAdapter.Adapt<CaixaViewModel, Caixa>(caixaView);
            _caixaRepository.Fechar(caixa);
        }

        public CaixaViewModel ObterCaixaAberto(int pdv)
        {
            return TypeAdapter.Adapt<Caixa, CaixaViewModel>(_caixaRepository.ObterCaixaAberto(pdv));
        }
    }
}