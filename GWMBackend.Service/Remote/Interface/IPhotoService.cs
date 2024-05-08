using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Service.Remote.Interface
{
    public interface IPhotoService
    {
        Task<string> Upload(IFormFile image);
        Task<string> Delete(string image);

    }
}
