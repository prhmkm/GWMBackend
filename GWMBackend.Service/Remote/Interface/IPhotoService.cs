using GWMBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.PhotoDTO;

namespace GWMBackend.Service.Remote.Interface
{
    public interface IPhotoService
    {
        Task<string> Upload(IFormFile image);
        Task<string> Delete(string image);
        UploadPic Upload(string objectId, string picture, bool thumbnail, int? id);
        PictureResponse FindById(long? id);
        void DeleteById(long id);
        List<Picture> FindByFolderId(long id);
        public Picture GetByAddress(string address);

    }
}
