using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Interfaces
{
    public interface IOpleidingService
    {
        Task<List<Opleiding>> GetAllOpleidingen(string jwtToken);
    }
}
