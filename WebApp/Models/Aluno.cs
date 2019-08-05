using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobreNome { get; set; }
        public int ra{ get; set; }

        public List<Aluno> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            //Ler todo arquivo Json
            var json = File.ReadAllText(caminhoArquivo);
            //Converter os dados do Json para um objeto
            var listarAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);            

            return listarAlunos;
        }

        public bool ReescreverAluno(List<Aluno> listaAluno)
        {
            var caminho = HostingEnvironment.MapPath(@"~/App_Data\Bse.json");
            //Converte o objeto para um arquivo Json
            var json = JsonConvert.SerializeObject(listaAluno, Formatting.Indented);

            return true;
        }

        public Aluno Salvar(Aluno aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var maxId = listaAlunos.Max(a => a.id);
            aluno.id = maxId;
            listaAlunos.Add(aluno);

            ReescreverAluno(listaAlunos);
            return aluno;            
        }

        public Aluno Atualizar(int id, Aluno aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(a => a.id == id);

            if (itemIndex >= 0)
            {
                aluno.id = id;
                listaAlunos[itemIndex] = aluno;
            }
            else
            {
                return null;
            }

            ReescreverAluno(listaAlunos);
            return aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(a => a.id == id);

            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverAluno(listaAlunos);
            return true;
        }

    }
}