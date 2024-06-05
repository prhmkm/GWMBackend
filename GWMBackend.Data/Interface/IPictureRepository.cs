using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.PhotoDTO;

namespace GWMBackend.Data.Interface
{
    public interface IPictureRepository
    {
        PictureResponse FindById(long? id);
        long Add(Picture picture);
        void DeleteById(long id);
        List<Picture> FindByFolderId(long id);
        Picture GetByAddress(string address);
    }
}
