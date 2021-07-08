using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class FornecedorRepository : RepositoryBase, IFornecedorRepository
    {
        public Fornecedor GetById(int id)
        {
            var sql = $"{getSelectbase()} Where Codigo = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var fornecedor = conn.Query<Fornecedor>(sql, new {id}).FirstOrDefault();
                conn.Close();

                return fornecedor;
            }
        }

        public IEnumerable<Fornecedor> GetAll()
        {
            var sql = $"{getSelectbase()}";
            using (var conn = Connection)
            {
                conn.Open();
                var fornecedores = conn.Query<Fornecedor>(sql);
                conn.Close();

                return fornecedores;
            }
        }

        public void Adicionar(Fornecedor fornecedor)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into Fornec(Nome, Endereco, END_NUM, END_COMP, Cep, Cidade, Bairro, Uf,");
            sql.AppendLine("FONE1, FONE2, CGC, IE, email, Ativo)");
            sql.AppendLine("Values (@Nome, @Endereco, @END_NUM, @END_COMP, @Cep, @Cidade, @Bairro, @Uf,");
            sql.AppendLine("@FONE1, @FONE2, @CGC, @IE, @email, @Ativo)");

            var parms = new DynamicParameters();
            parms.Add("@Nome", fornecedor.Nome);
            parms.Add("@Endereco", fornecedor.Endereco);
            parms.Add("@END_NUM", fornecedor.Numero);
            parms.Add("@END_COMP", fornecedor.Complemento);
            parms.Add("@Cep", fornecedor.Cep);
            parms.Add("@Cidade", fornecedor.Cidade);
            parms.Add("@Bairro", fornecedor.Bairro);
            parms.Add("@Uf", fornecedor.Uf);
            parms.Add("@FONE1", fornecedor.Telefone);
            parms.Add("@FONE2", fornecedor.Celular);
            parms.Add("@CGC", fornecedor.Cnpj);
            parms.Add("@IE", fornecedor.Ie);
            parms.Add("@email", fornecedor.Email);
            parms.Add("@Ativo", fornecedor.Situacao == 1 ? "S": "N");
        }

        public void Editar(Fornecedor fornecedor)
        {
            var sql = new StringBuilder();
            sql.AppendLine("update Fornec Set");
            sql.AppendLine("Nome = @Nome,");
            sql.AppendLine("Endereco = @Endereco,");
            sql.AppendLine("END_NUM = @END_NUM,");
            sql.AppendLine("END_COMP = @END_COMP,");
            sql.AppendLine("Cep = @Cep,");
            sql.AppendLine("Cidade = @Cidade,");
            sql.AppendLine("Bairro = @Bairro,");
            sql.AppendLine("Uf = @Uf,");
            sql.AppendLine("FONE1 = @FONE1,");
            sql.AppendLine("FONE2 = @FONE2");
            sql.AppendLine("CGC = @CGC,");
            sql.AppendLine("IE = @IE,");
            sql.AppendLine("email = @email,");
            sql.AppendLine("Ativo = @Ativo");
            sql.AppendLine("Where Codigo = @Codigo");

            var parms = new DynamicParameters();
            parms.Add("@Codigo", fornecedor.FornecedorId);
            parms.Add("@Nome", fornecedor.Nome);
            parms.Add("@Endereco", fornecedor.Endereco);
            parms.Add("@END_NUM", fornecedor.Numero);
            parms.Add("@END_COMP", fornecedor.Complemento);
            parms.Add("@Cep", fornecedor.Cep);
            parms.Add("@Cidade", fornecedor.Cidade);
            parms.Add("@Bairro", fornecedor.Bairro);
            parms.Add("@Uf", fornecedor.Uf);
            parms.Add("@FONE1", fornecedor.Telefone);
            parms.Add("@FONE2", fornecedor.Celular);
            parms.Add("@CGC", fornecedor.Cnpj);
            parms.Add("@IE", fornecedor.Ie);
            parms.Add("@email", fornecedor.Email);
            parms.Add("@Ativo", fornecedor.Situacao == 1 ? "S" : "N");

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parms);
                conn.Close();
            }
        }

        public void Excluir(Fornecedor fornecedor)
        {
            var sql = "Delete From Fornec Where Codigo = @codigo";
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, new { codigo = fornecedor.FornecedorId});
                conn.Close();
            }
        }

        private string getSelectbase()
        {
            return @"select 
	                    Codigo as FornecedorId,
	                    Nome as Nome,
	                    Endereco,
	                    Isnull(END_NUM,'') as Numero,
	                    Isnull(END_COMP,'') as Complemento,
	                    Cep,
	                    Cidade,
	                    Bairro,
	                    Uf,
	                    FONE1 as Telefone,
	                    Isnull(FONE2,'') as Celular,
	                    Isnull(CGC, '') as Cnpj,
	                    Isnull(IE, '') as Ie,
	                    Isnull(email,'') as Email,
	                    Situacao = case when Ativo = 'S' then 1 else 2 end
                    from Fornec";
        }
    }
}