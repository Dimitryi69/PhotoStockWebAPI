using AutoMapper;
using BuisnessLogicLayer.DataTransferObjects;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Parameters;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoStockWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace PhotoStockWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;
        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        [Route("api/[controller]/all")]
        [HttpGet]
        public IActionResult GetAllEntities(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range");
                }
                if (pageSize < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size is out of range");
                }
                _logger.LogInformation("Get method GetAllEntities() called");
                return Ok(_stockService.GetAllEntities(new ListParameters() { PageNumber = pageNumber, PageSize = pageSize }));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on parameter: {ex.ParamName}");
                return BadRequest(ex.Message);
            }
        }

        [Route("api/[controller]/photos")]
        [HttpGet]
        public IActionResult GetAllPhotos(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if(pageNumber<1)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range");
                }
                if (pageSize < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size is out of range");
                }
                _logger.LogInformation("Get method GetAllPhotos() called");
                IEnumerable<PhotoDto> photoDtos = _stockService.GetPhotos(new ListParameters() { PageNumber = pageNumber, PageSize = pageSize });
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PhotoDto, PhotoViewModel>()
                .ForMember("AuthorName", opt => opt.MapFrom(src => src.Author.Name))
                .ForMember("AuthorNickname", opt => opt.MapFrom(src => src.Author.Nickname))).CreateMapper();
                var photos = mapper.Map<IEnumerable<PhotoDto>, List<PhotoViewModel>>(photoDtos);
                return Ok(photos);
            }
            catch (ValidationDtoException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on property: {ex.Property}");
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on parameter: {ex.ParamName}");
                return BadRequest(ex.Message);
            }
        }

        [Route("api/[controller]/texts")]
        [HttpGet]
        public IActionResult GetAllTextsCSV()
        {
            _logger.LogInformation("Get method GetAllTextsCSV() called");
            IEnumerable<TextDto> photoDtos = _stockService.GetTexts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TextDto, TextViewModel>()
            .ForMember("AuthorName", opt => opt.MapFrom(src => src.Author.Name))
            .ForMember("AuthorNickname", opt => opt.MapFrom(src => src.Author.Nickname))).CreateMapper();
            var texts = mapper.Map<IEnumerable<TextDto>, List<TextViewModel>>(photoDtos);
            StringBuilder csv = new();
            using (StringWriter streamReader = new(csv))
            {
                using CsvWriter csvReader = new(streamReader, CultureInfo.InvariantCulture);
                csvReader.WriteRecords(texts);
            }
            return Ok(csv.ToString());
        }

        [Route("api/[controller]/photos/{id}")]
        [HttpGet]
        public IActionResult GetPhoto(int id)
        {
            try
            {
                _logger.LogInformation("Get method GetPhoto(id) called");
                var photo = _stockService.GetPhoto(id);
                return Ok(photo);
            }
            catch (ValidationDtoException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on property: {ex.Property}");
                return NotFound(ex.Message);
            }
        }


        [Route("api/[controller]/photos/{id}")]
        [HttpPut]
        public IActionResult UpdatePhoto(int id, [FromBody] PhotoViewModel value)
        {
            try
            {
                _logger.LogInformation("Put method UpdatePhoto(id, photo) called");
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PhotoViewModel, PhotoDto>()).CreateMapper();
                var photo = mapper.Map<PhotoViewModel, PhotoDto>(value);
                _stockService.UpdatePhoto(id, photo);
                return Ok(id);
            }
            catch (ValidationDtoException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on property: {ex.Property}");
                return BadRequest(ex.Message);
            }
        }

        [Route("api/[controller]/photos/rating/{id}")]
        [HttpPut]
        public IActionResult PutPhotoRating(int id, int rating)
        {
            try
            {
                _logger.LogInformation("Put method PutPhotoRating(id, rating) called");
                _stockService.SetRating(id, rating);
                return Ok();
            }
            catch (ValidationDtoException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on property: {ex.Property}");
                return BadRequest(ex.Message);
            }
        }

        [Route("api/[controller]/texts")]
        [HttpPost]
        public IActionResult AddText([FromBody] TextViewModel text)
        {
            try
            {
                _logger.LogInformation("Post method AddText(text) called");
                var author = _stockService.GetAuthor(text.AuthorNickname, text.AuthorName);
                var newText = new TextDto()
                {
                    Name = text.Name,
                    Content = text.Content,
                    CreationDate = text.CreationDate,
                    Price = text.Price,
                    Length = text.Length,
                    PurchaseCount = text.PurchaseCount,
                    Rating = text.Rating,
                    Author = author,
                };
                _stockService.AddText(newText);
                return Ok();
            }
            catch (ValidationDtoException ex)
            {
                _logger.LogError($"Error: {ex.Message}, on property: {ex.Property}");
                return BadRequest(ex.Message);
            }
        }
    }
}
