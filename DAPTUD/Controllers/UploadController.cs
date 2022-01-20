using DAPTUD.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAPTUD.Controllers
{
    class FileModel
    {
        public string link { get; set; }

        public FileModel(string _link)
        {
            this.link = _link;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private static string ApiKey = "";
        private static string Bucket = "ptud-94f91.appspot.com";
        private static string AuthEmail = "ptud@gmail.com";
        private static string AuthPassword = "abcd1234";

       [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> file)
        {
            long size = file.Sum(f => f.Length);
            string link = "";
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    using var stream = System.IO.File.Create(formFile.FileName + ".jpg");
                    await formFile.CopyToAsync(stream);

                    link = await UploadFile(stream, formFile.FileName);
                }
            }
            
            return Ok(new { link = link });
        }

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child("images").Child(fileName).PutAsync(fileStream, cancellation.Token);

            task.Progress.ProgressChanged += (s, e) =>
            {
                Console.WriteLine($"Process: { e.Percentage }%");
            };
            try
            {
                string link = await task;
                return link;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
