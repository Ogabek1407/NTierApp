using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTierApplication.Servece.Service.Interface;
using NTierAppliction.Domain.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService ItemService;

        public ItemController(IItemService itemService)
        {
            ItemService = itemService;
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation(OperationId = "GetAll")]
        public async ValueTask<ICollection<ItemViewModel>> GetAll()
        {
            return await ItemService.GetItemsAsync();
        }

        [HttpGet]
        [Route("{offset}&{limit}")]
        [SwaggerOperation(OperationId = "GetItemsPag")]
        public async ValueTask<ItemPaginationViewModel> GetPag(int offset, int limit)
        {
            return await ItemService.GetItemsPag(offset, limit);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(OperationId = "create")]
        public async ValueTask<ItemViewModel> CreateNew(ItemViewModel itemViewModel)
        {
            await ItemService.CreateNewAsync(itemViewModel);
            return itemViewModel;
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(OperationId = "GetById")]
        public async ValueTask<ItemViewModel> GetById(long id)
        {
            return await ItemService.GetByIdAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(OperationId = "Delete")]
        public async ValueTask<ItemViewModel> Delete(long id)
        {
            var item = ItemService.GetByIdAsync(id);
            await ItemService.DeleteAsync(id);
            return item.Result;
        }

        [HttpPut]
        [Route("")]
        [SwaggerOperation(OperationId = "Update")]
        public async ValueTask<ItemViewModel> Update(ItemViewModel item)
        {
            await ItemService.UpdateAsync(item);
            return item;
        }
    }
}
