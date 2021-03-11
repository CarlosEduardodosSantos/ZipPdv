using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class FabricaRelatorioRepository : RepositoryBase, IFabricaRelatorioRepository
    {
        public FabricaRelatorio GetById(int id)
        {
            var sql = "Select * From FabricaRelatorios Where IdGuid = @id";
            using (var cn = Connection)
            {
                cn.Open();
                var relatorios = cn.Query<FabricaRelatorio>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return relatorios;
            }
        }

        public IEnumerable<FabricaRelatorio> GetAll()
        {
            var sql = "Select * From FabricaRelatorios";
            using (var cn = Connection)
            {
                cn.Open();
                var relatorios = cn.Query<FabricaRelatorio>(sql);
                cn.Close();

                return relatorios;
            }
        }

        public void Adcionar(FabricaRelatorio fabricaRelatorio)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into FabricaRelatorios(IdGuid, UduarioId, DataHora, [FileName], [File], Tipo)");
            sql.AppendLine("Values(@IdGuid, @UduarioId, @DataHora, @FileName, @File, @Tipo)");

            var parms = new DynamicParameters();
            parms.Add("@IdGuid", fabricaRelatorio.IdGuid);
            parms.Add("@UduarioId", fabricaRelatorio.UduarioId);
            parms.Add("@DataHora", fabricaRelatorio.DataHora);
            parms.Add("@FileName", fabricaRelatorio.FileName);
            parms.Add("@File", fabricaRelatorio.File);
            parms.Add("@Tipo", fabricaRelatorio.Tipo);

            using (var cn = Connection)
            {
                cn.Open();
                cn.Query(sql.ToString(), parms);
                cn.Close();
            }
        }

        public void Editar(FabricaRelatorio fabricaRelatorio)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Update FabricaRelatorios Set ");
            sql.AppendLine("UduarioId = @UduarioId,");
            sql.AppendLine("DataHora = @DataHora,");
            sql.AppendLine("[FileName] = @FileName,");
            sql.AppendLine("[File] = @File,");
            sql.AppendLine("Tipo = @Tipo");
            sql.AppendLine("Where IdGuid = @IdGuid");

            var parms = new DynamicParameters();
            parms.Add("@IdGuid", fabricaRelatorio.IdGuid);
            parms.Add("@UduarioId", fabricaRelatorio.UduarioId);
            parms.Add("@DataHora", fabricaRelatorio.DataHora);
            parms.Add("@FileName", fabricaRelatorio.FileName);
            parms.Add("@File", fabricaRelatorio.File);
            parms.Add("@Tipo", fabricaRelatorio.Tipo);

            using (var cn = Connection)
            {
                cn.Open();
                cn.Query(sql.ToString(), parms);
                cn.Close();
            }
        }

        public void Remover(FabricaRelatorio fabricaRelatorio)
        {
            var sql = "Delete FabricaRelatorios Where IdGuid = @IdGuid";
            using (var cn = Connection)
            {
                cn.Open();
                cn.Query(sql, new { fabricaRelatorio.IdGuid });
                cn.Close();
            }
        }

        public FabricaRelatorio GetByGuid(Guid id)
        {
            var sql = "Select * From FabricaRelatorios Where IdGuid = @id";
            using (var cn = Connection)
            {
                cn.Open();
                var relatorios = cn.Query<FabricaRelatorio>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return relatorios;
            }
        }

        public IEnumerable<FabricaRelatorio> ObterPorPesquisa(string pesquisa)
        {
            var sql = "Select * From FabricaRelatorios Where FileName Like '%' + @pesquisa + '%'";
            using (var cn = Connection)
            {
                cn.Open();
                var relatorios = cn.Query<FabricaRelatorio>(sql, new { pesquisa });
                cn.Close();

                return relatorios;
            }
        }
    }
}