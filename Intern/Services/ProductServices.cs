//using Microsoft.EntityFrameworkCore;
using Intern.EF;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.PagingCommon;

namespace Intern.Services
{
    public class ProductServices : IProductServices
    {
        private readonly MyDbContext _context;

        public ProductServices(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> BuyProduct(ProductDetailRequest request)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                if (!_context.Products.Any(x => x.ProductId == request.ProductId))
                    throw new Exception($"Can not find product ID = {request.ProductId}");

                var query = from pd in _context.PropertyDetails
                            join pdpd in _context.ProductDetailPropertyDetails on pd.PropertyDetailId equals pdpd.PropertyDetailId
                            where pdpd.ProductId == request.ProductId
                            group new
                            {
                                pd.PropertyDetailId,
                                pdpd.ProductDetailId
                            } by pdpd.ProductDetailId;
                query = query.AsQueryable();
                int dem = 0;
                int productDetailId = 0;

                foreach (var item in query)
                {
                    dem = 0;

                    foreach (var subitem in item)
                    {
                        foreach (var propertyDetailId in request.PropertyDetailIds)
                        {
                            if (subitem.PropertyDetailId == propertyDetailId && request.PropertyDetailIds.Count == item.Count())
                                dem++;
                        }
                        if (dem == request.PropertyDetailIds.Count)
                            productDetailId = subitem.ProductDetailId;
                    }
                }
                if (productDetailId == 0)
                    throw new Exception("Cannot find productDetailId. Check list of PropertyDetailId!");

                var productDetail = await _context.ProductDetails.FindAsync(productDetailId);

                if (productDetail == null)
                    throw new Exception($"Can not find ProductDetail with ID = {productDetailId}");

                if (productDetail.Quantity == 0)
                    throw new Exception($"ProductDetail with ID = {productDetailId} out of stock!");

                if (productDetail.Quantity < request.Quantity)
                    throw new Exception($"ProductDetail with ID = {productDetailId} not enough quantity to sell!");

                productDetail.Quantity -= request.Quantity;
                _context.ProductDetails.Update(productDetail);

                //var parentProductDetail = new ProductDetail();
                do
                {
                    productDetail = await _context.ProductDetails.FindAsync(productDetail.ParentId);
                    productDetail.Quantity -= request.Quantity;
                    _context.ProductDetails.Update(productDetail);

                } while (productDetail.ParentId != null);

                var result = await _context.SaveChangesAsync();
                //await trans.CommitAsync();
                return result;
            }

        }

        public async Task<int> UpdateQuantity(ProductDetailRequest request)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                if (!_context.Products.Any(x => x.ProductId == request.ProductId))
                    throw new Exception($"Can not find product ID = {request.ProductId}");

                var query = from pd in _context.PropertyDetails
                            join pdpd in _context.ProductDetailPropertyDetails on pd.PropertyDetailId equals pdpd.PropertyDetailId
                            where pdpd.ProductId == request.ProductId
                            group new
                            {
                                pd.PropertyDetailId,
                                pdpd.ProductDetailId
                            } by pdpd.ProductDetailId;

                int dem = 0;
                int productDetailId = 0;

                foreach (var item in query)
                {
                    dem = 0;

                    foreach (var subitem in item)
                    {
                        foreach (var propertyDetailId in request.PropertyDetailIds)
                        {
                            if (subitem.PropertyDetailId == propertyDetailId && request.PropertyDetailIds.Count == item.Count())
                                dem++;
                        }
                        if (dem == request.PropertyDetailIds.Count)
                            productDetailId = subitem.ProductDetailId;
                    }
                }
                if (productDetailId == 0)
                    throw new Exception("Cannot find productDetailId. Check list of PropertyDetailId!");

                var productDetail = await _context.ProductDetails.FindAsync(productDetailId);

                if (productDetail == null)
                    throw new Exception($"Can not find ProductDetail with ID = {productDetailId}");

                int range = request.Quantity - productDetail.Quantity;
                productDetail.Quantity = request.Quantity;
                _context.ProductDetails.Update(productDetail);

                do
                {
                    productDetail = await _context.ProductDetails.FindAsync(productDetail.ParentId);
                    productDetail.Quantity += range;
                    _context.ProductDetails.Update(productDetail);

                } while (productDetail.ParentId != null);

                var result = await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return result;
            }

        }

        public async Task<PageResult<ProductDetail>> GetProductDetails(ProductPagingRequest request)
        {
            var productDetails = new List<ProductDetail>();

            var parentProductDetails = _context.ProductDetails.Where(x => x.ParentId != null);

            if(!String.IsNullOrEmpty(request.keyWord))
                parentProductDetails = parentProductDetails.Where(x=>x.ProductDetailName.Contains(request.keyWord));

            if(request.MinPrice.HasValue)
                parentProductDetails = parentProductDetails.Where(x => x.Price>= request.MinPrice);

            if (request.MaxPrice.HasValue)
                parentProductDetails = parentProductDetails.Where(x => x.Price <= request.MaxPrice);

            var lst = parentProductDetails.ToList();

            if (lst.Count() > 0)
                foreach (var item in lst)
                {
                    var pd = lst.Find(x => x.ParentId == item.ProductDetailId);

                    if (pd == null)
                    {
                        productDetails.Add(item);
                    }
                }

            int totalRecord = productDetails.Count();

            int totalPage = (productDetails.Count() % request.PageSize > 0) ? productDetails.Count() / request.PageSize + 1 : productDetails.Count() / request.PageSize;

            productDetails = productDetails.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize).ToList();

            var pageResult = new PageResult<ProductDetail>()
            {
                Data = productDetails,
                TotalPage = totalPage,
                TotalRecord = totalRecord,
            };
            
            return pageResult;
        }
    }
}
