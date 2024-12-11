using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;
using PeoplesPartnership.ApiRefactor.Handlers;
using PeoplesPartnership.ApiRefactor.Handlers.AddStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.DeleteStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioHeaderItems;
using PeoplesPartnership.ApiRefactor.Handlers.GetStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.UpdateStudioItem;

namespace PeoplesPartnership.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class StudioItemsController : ControllerBase
    {
        private readonly IDeleteStudioItemHandler _deleteStudioItemHandler;
        private readonly IAddStudioItemHandler _addStudioItemHandler;
        private readonly IGetAllStudioHeaderItemsHandler _getAllStudioHeaderItemsHandler;
        private readonly IGetStudioItemByIdHandler _getStudioItemByIdHandler;
        private readonly IUpdateStudioItemHandler _updateStudioItemHandler;

        public StudioItemsController(
            IDeleteStudioItemHandler deleteStudioItemHandler, 
            IAddStudioItemHandler addStudioItemHandler,
            IGetAllStudioHeaderItemsHandler getAllStudioHeaderItemsHandler,
            IGetStudioItemByIdHandler getStudioItemByIdHandler,
            IUpdateStudioItemHandler updateStudioItemHandler)
        {
            _deleteStudioItemHandler = deleteStudioItemHandler;
            _addStudioItemHandler = addStudioItemHandler;
            _getAllStudioHeaderItemsHandler = getAllStudioHeaderItemsHandler;
            _getStudioItemByIdHandler = getStudioItemByIdHandler;
            _updateStudioItemHandler = updateStudioItemHandler;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetStudioItemHeaderDto>>> Get()
        {
            var handlerResponse = await _getAllStudioHeaderItemsHandler.GetAllStudioHeaderItems();
            return Ok(handlerResponse.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetStudioItemDto>> GetById(int id)
        {
            var handlerResponse = await _getStudioItemByIdHandler.GetStudioItemById(id);
            
            return handlerResponse.Type switch
            {
                ServiceResponseType.Success => Ok(handlerResponse.Data),
                ServiceResponseType.NotFound => NotFound(handlerResponse.Message),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetStudioItemDto>> Add(AddStudioItemDto studioItem)
        {
            var handlerResponse = await _addStudioItemHandler.AddStudioItem(studioItem);

            return handlerResponse.Type switch
            {
                ServiceResponseType.Success => Ok(handlerResponse.Data),
                ServiceResponseType.NotFound => NotFound(handlerResponse.Message),
                ServiceResponseType.BadRequest => BadRequest(handlerResponse.Message),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetStudioItemDto>> Update(int id, UpdateStudioItemDto studioItem)
        {
            var handlerResponse = await _updateStudioItemHandler.UpdateStudioItem(id, studioItem);

            return handlerResponse.Type switch
            {
                ServiceResponseType.Success => Ok(handlerResponse.Data),
                ServiceResponseType.NotFound => NotFound(handlerResponse.Message),
                ServiceResponseType.BadRequest => BadRequest(handlerResponse.Message),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var handlerResponse = await _deleteStudioItemHandler.DeleteStudioItem(id);
    
            return handlerResponse.Type switch
            {
                ServiceResponseType.Success => Ok(),
                ServiceResponseType.NotFound => NotFound(handlerResponse.Message),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}