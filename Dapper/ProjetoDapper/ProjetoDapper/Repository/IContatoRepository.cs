using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoDapper.Entities;

namespace ProjetoDapper.Repository
{
    public interface IContatoRepository
    {
        int Add(Contato contato);
        List<Contato> GetContatos();
        Contato Get(int id);
        int Edit(Contato contato);
        int Delete(int id);
    }
}
