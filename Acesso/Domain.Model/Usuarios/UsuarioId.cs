using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.Common.Domain.Model;

namespace AlphaNet.Acesso.Domain.Model.Usuarios
{
    public class UsuarioId:Identity
    {
        public UsuarioId(string id){
            this.Id  = id;
        }
    }
}
