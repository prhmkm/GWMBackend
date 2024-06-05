using Amazon.S3;
using Amazon.S3.Model;
using GWMBackend.Core.Helpers;
using GWMBackend.Core.Model.Base;
using GWMBackend.Data.Base;
using GWMBackend.Domain.DTOs;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Remote.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.PhotoDTO;

namespace GWMBackend.Service.Remote.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly AppSettings _appSettings;
        IRepositoryWrapper _repository;
        public PhotoService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
        }

        public async Task<string> Delete(string image)
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _appSettings.LIARA_ENDPOINT,
                ForcePathStyle = true,
                SignatureVersion = "4"
            };
            var credentials = new Amazon.Runtime.BasicAWSCredentials(_appSettings.LIARA_ACCESS_KEY, _appSettings.LIARA_SECRET_KEY);
            using var client = new AmazonS3Client(credentials, config);
            try
            {

                DeleteObjectRequest deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _appSettings.LIARA_BUCKET_NAME,
                    Key = image.Split("gwm-backend/")[1]
                };

                // deleting image in bucket
                await client.DeleteObjectAsync(deleteRequest);
                return ("Image deleted.");
            }

            catch (AmazonS3Exception e)
            {
                return ($"Error: {e.Message}");
            }

        }

        public async Task<string> Upload(IFormFile image)
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _appSettings.LIARA_ENDPOINT,
                ForcePathStyle = true,
                SignatureVersion = "4"
            };
            var credentials = new Amazon.Runtime.BasicAWSCredentials(_appSettings.LIARA_ACCESS_KEY, _appSettings.LIARA_SECRET_KEY);
            using var client = new AmazonS3Client(credentials, config);
            string objectKey = "Products/" + image.FileName;
            try
            {

                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream).ConfigureAwait(false);

                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = _appSettings.LIARA_BUCKET_NAME,
                    Key = objectKey,
                    InputStream = memoryStream,
                };

                // uploading image in bucket
                await client.PutObjectAsync(request);
                return ($"URL: {_appSettings.LIARA_ENDPOINT}/{_appSettings.LIARA_BUCKET_NAME}/{objectKey}");
            }

            catch (AmazonS3Exception e)
            {
                return ($"Error: {e.Message}");
            }

        }
        public void DeleteById(long id)
        {
            _repository.picture.DeleteById(id);
        }

        public List<Picture> FindByFolderId(long id)
        {
            return _repository.picture.FindByFolderId(id);
        }
        public PictureResponse FindById(long? id)
        {
            return _repository.picture.FindById(id);
        }

        public Picture GetByAddress(string address)
        {
            return _repository.picture.GetByAddress(address);
        }

        public UploadPic Upload(string objectId, string picture, bool thumbnail, int? id)
        {
            List<string> _imagName = objectId.Split(".").ToList();
            string imgName = null;
            for (int i = 0; i < _imagName.Count - 1; i++)
            {
                imgName = imgName + _imagName[i] + ".";
            }
            string path = Path.Combine(_appSettings.SaveImagePath + "\\");

            if (id == 1)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\Products\\");
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string Address = null;
            string thumbnailAddress = null;
            string imageName = null;
            string imageNameThumb = null;
            if (thumbnail == false)
            {
                imageName = Convertor.Base64ToImage(picture, path, objectId.Split(".")[0]);
                Address = null;
            }
            if (thumbnail == true)
            {
                imageNameThumb = Convertor.Base64ToThumbnail(picture, path, objectId.Split(".")[0] + "thumb");
                thumbnailAddress = null;
            }
            if (thumbnail == false)
            {

                Address = _appSettings.PublishImagePath + "//" + imageName;

                if (id == 1)
                {
                    Address = _appSettings.PublishImagePath + "//Products//" + imageName;
                }

            }
            if (thumbnail == true)
            {

                thumbnailAddress = _appSettings.PublishImagePath + "//" + imageNameThumb;

                if (id == 1)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//Products//" + imageNameThumb;
                }
            }
            if (imageName == "crash" || imageNameThumb == "crash")
            {
                return new UploadPic
                {
                    Address = null,
                    Id = 0
                };
            }
            return new UploadPic
            {
                Address = Address,
                ThumpAddress = thumbnailAddress,
                Id = _repository.picture.Add(new Picture
                {
                    Address = Address,
                    ImageName = imageName,
                    Thumbnail = thumbnailAddress
                })
            };

        }
    }
}
