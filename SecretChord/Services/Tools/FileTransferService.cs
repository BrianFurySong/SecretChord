using Amazon.S3;
using Amazon.S3.Model;
using SecretChord.Models.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SecretChord.Services.Tools
{
    public class FileTransferService : IFileTransferService
    {
        public string FileUpload(Stream stream, string fileName, string fileRepositoryIdRequest = "false")
        {
            string statusMessage = "File successfully uploaded.";

            try
            {
                string awsAccessKey = AppConfiguration.Instance.AppGetByKey("AWSAccessKey");
                string awsSecretKey = AppConfiguration.Instance.AppGetByKey("AWSSecretKey");
                string bucketName = AppConfiguration.Instance.AppGetByKey("AWSBucketName");

                Guid guid = Guid.NewGuid();
                string s3FileName = guid + "_" + fileName;

                using (IAmazonS3 client = new AmazonS3Client(awsAccessKey, awsSecretKey, Amazon.RegionEndpoint.USWest1))
                {

                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = s3FileName,
                        InputStream = stream
                    };

                    PutObjectResponse response = client.PutObject(request);

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {

                        if (fileRepositoryIdRequest == "false")
                        {
                            statusMessage = s3FileName;
                            FileRepositoryAddRequest model = new FileRepositoryAddRequest();
                            model.FilePathName = "/" + s3FileName;

                            FileRepositoryService fileRepositoryService = new FileRepositoryService();
                            int fileRepositoryId = fileRepositoryService.Insert(model);
                            statusMessage = fileRepositoryId.ToString();
                        }
                    }
                }
            }
            catch (AmazonS3Exception s3Exception)
            {
                statusMessage = s3Exception.Message;
            }

            return statusMessage;
        }
    }
}
