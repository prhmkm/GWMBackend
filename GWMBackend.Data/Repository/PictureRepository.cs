using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.PhotoDTO;

namespace GWMBackend.Data.Repository
{
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        GWM_DBContext _repositoryContext;
        public PictureRepository(GWM_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }


        public long Add(Picture picture)
        {
            Create(picture);
            Save();
            return picture.Id;
        }

        public void DeleteById(long id)
        {
            _repositoryContext.Remove(_repositoryContext.Pictures.FirstOrDefault(a => a.Id == id));
            _repositoryContext.SaveChanges();
        }
        public List<Picture> FindByFolderId(long id)
        {
            return _repositoryContext.Pictures.ToList();
        }

        public PictureResponse FindById(long? id)
        {
            //PictureResponse picture = new PictureResponse
            //{
            //    Address = "",
            //    FolderId = 0,
            //    Id = 0,
            //    ImageName = "",
            //    Thumbnail = ""
            //};
            //var q = (from a in _repositoryContext.Pictures
            //         join b in _repositoryContext.Folders on a.FolderId equals b.Id
            //         where a.Id == id
            //         select new PictureResponse
            //         {
            //             Id = a.Id,
            //             Address = a.Address,
            //             FolderId = a.FolderId,
            //             ImageName = a.ImageName,
            //             Thumbnail = a.Thumbnail
            //         }).FirstOrDefault();

            //if (q != null)
            //{
            //    return q;
            //}
            //else
            //{
            //    return picture;
            //}
            PictureResponse picture = new PictureResponse();
            return picture;
        }

        public Picture GetByAddress(string address)
        {
            return FindByCondition(w => w.Address == address || w.Thumbnail == address).FirstOrDefault();
        }
    }
}
