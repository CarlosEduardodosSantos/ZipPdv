using System.Collections.Generic;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class DeliveryRepository : RepositoryBase, IDeliveryRepository
    {
        public Delivery GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Delivery> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Entregar(Delivery delivery)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Update Televenda_1 Set ");
            sql.AppendLine("sai_data = @sai_data,");
            sql.AppendLine("sai_hora = @sai_hora,");
            sql.AppendLine("sai_func = @sai_func,");
            sql.AppendLine("troco = @troco,");
            sql.AppendLine("Moto = @Moto,");
            sql.AppendLine("Taxa_Adicional = @Taxa_Adicional,");
            sql.AppendLine("valor = @valor");
            sql.AppendLine("Where Nro = @Nro");

            var param = new DynamicParameters();
            param.Add("@Nro", delivery.DeliveryId);
            param.Add("@sai_data", delivery.DataHoraSaida.Date);
            param.Add("@sai_hora", delivery.DataHoraSaida);
            param.Add("@sai_func", delivery.UsuarioSaida);
            param.Add("@troco", delivery.Troco);
            param.Add("@Moto", delivery.EntregadorId);
            param.Add("@Taxa_Adicional", delivery.TaxaEntrega);
            param.Add("@valor", delivery.Valor);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), param);
                conn.Close();
            }

        }

        public void Retornar(Delivery delivery)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Update Televenda_1 Set ");
            sql.AppendLine("Ret_Data = @Ret_Data,");
            sql.AppendLine("Ret_Hora = @Ret_Hora,");
            sql.AppendLine("Ret_Func = @Ret_Func");
            sql.AppendLine("Where Nro = @Nro");

            var param = new DynamicParameters();
            param.Add("@Nro", delivery.DeliveryId);
            param.Add("@Ret_Data", delivery.DataHoraRetorno.Date);
            param.Add("@Ret_Hora", delivery.DataHoraRetorno);
            param.Add("@Ret_Func", delivery.UsuarioRetorno);
           

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), param);
                conn.Close();
            }
        }
    }
}