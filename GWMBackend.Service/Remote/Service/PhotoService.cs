using Amazon.S3;
using Amazon.S3.Model;
using GWMBackend.Core.Model.Base;
using GWMBackend.Service.Remote.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Service.Remote.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly AppSettings _appSettings;
        public PhotoService(IOptions<AppSettings> appSettings)
        {
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
    }
}
