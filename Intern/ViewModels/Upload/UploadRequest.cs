using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Intern.ViewModels.Upload
{
    public class UploadRequest
    {
        public IFormFile file1 { get; set; }
        public IFormFile file2 { get; set;}
        public string data { get; set; }
    }
}
