using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using st10269378.Models;
using st10269378.Services;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace st10269378.Controllers
{
    // home controller for handling customer profile, image, order, and file uploads
    public class HomeController : Controller
    {
        // blob service for uploading images and files to azure blob storage
        private readonly BlobService _blobService;
        // table service for adding customer profiles to azure table
        private readonly TableService _tableService;
        // queue service for sending messages to azure queue
        private readonly QueueService _queueService;
        // file service for uploading files to azure file share
        private readonly FileService _fileService;

        // initializes home controller with required services
        public HomeController(BlobService blobService, TableService tableService, QueueService queueService, FileService fileService)
        {
            _blobService = blobService;
            _tableService = tableService;
            _queueService = queueService;
            _fileService = fileService;
        }

        // displays the index view with a new customer profile
        public IActionResult Index()
        {
            return View(new CustomerProfile());
        }

        // adds customer profile to azure table
        [HttpPost]
        public async Task<IActionResult> AddCustomerProfile(CustomerProfile profile)
        {
            // checks if model state is valid
            if (ModelState.IsValid)
            {
                // adds customer profile to azure table
                await _tableService.AddEntityAsync(profile);
                // displays success message
                ViewBag.Message = "customer profile added successfully!";
            }
            // returns view with customer profile
            return View("Index", profile);
        }

        // uploads image to azure blob storage
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // checks if file is valid
            if (file != null && file.Length > 0)
            {
                // opens stream for file
                using var stream = file.OpenReadStream();
                // uploads file to azure blob storage
                await _blobService.UploadBlobAsync("product-images", file.FileName, stream);
                // displays success message
                ViewBag.Message = "image uploaded successfully!";
            }
            // returns view
            return View("Index");
        }

        // sends a message to the azure queue
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            // checks if order id is not empty
            if (!string.IsNullOrEmpty(orderId))
            {
                // sends message to azure queue
                await _queueService.SendMessageAsync("order-processing", $"processing order {orderId}");
                // displays success message
                ViewBag.Message = "order processed successfully!";
            }
            // returns view
            return View("Index");
        }

        // uploads file to azure file share
        [HttpPost]
        public async Task<IActionResult> UploadFileToAzure(IFormFile file)
        {
            // checks if file is valid
            if (file != null && file.Length > 0)
            {
                // opens stream for file
                using var stream = file.OpenReadStream();
                // uploads file to azure file share
                await _fileService.UploadFileAsync("fileshare", file.FileName, stream);
                // displays success message
                ViewBag.Message = "file uploaded successfully!";
            }
            // returns view
            return View("Index");
        }
    }
}