using System.Security.Claims;
using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Order> _orderRepository;
        private readonly IRepository<int, OrderDetail> _orderDetailRepository;
        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, Product> _productRepostiory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IRepository<int, Order> orderRepository,
                            IRepository<int, OrderDetail> orderDetailRepository,
                            IRepository<int, User> userRepository,
                            IRepository<int, Product> productRepostiory,
                            IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userRepository = userRepository;
            _productRepostiory = productRepostiory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Order> PlaceOrder(OrderRequestDto orderDto)
        {
            var cartItems = orderDto.CartItems;

            var productNames = new List<string>();
            foreach (var item in cartItems)
            {
                var product = await _productRepostiory.Get(item.ProductId)
                              ?? throw new Exception($"Product with ID {item.ProductId} not found");
                productNames.Add(product.ProductName);
            }

            string orderName = productNames.Count switch
            {
                > 1 => string.Join(", ", productNames[..^1]) + " and " + productNames[^1],
                1 => productNames[0],
                _ => "No items"
            };

            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll()).SingleOrDefault(u => u.Username == username)
                       ?? throw new Exception("No User Logged in");

            var newOrder = new Order
            {
                OrderName = orderName,
                UserId = user.UserId,
                OrderDate = DateTime.UtcNow,
                PaymentType = orderDto.PaymentType,
                Status = "Created",
                CustomerName = orderDto.CustomerName,
                CustomerPhone = orderDto.CustomerPhone,
                CustomerAddress = orderDto.CustomerAddress,
                CustomerEmail = orderDto.CustomerEmail,
                OrderDetails = new List<OrderDetail>()
            };

            newOrder = await _orderRepository.Add(newOrder);

            foreach (var item in cartItems)
            {
                var product = await _productRepostiory.Get(item.ProductId)
                              ?? throw new Exception($"Product with ID {item.ProductId} not found");

                var orderDetail = new OrderDetail
                {
                    OrderID = newOrder.OrderID, 
                    ProductID = product.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Quantity * (product.Price ?? 0),
                    Product = product,
                    Order = newOrder
                };

                await _orderDetailRepository.Add(orderDetail);
            }

            return newOrder;
        }




        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll()).SingleOrDefault(u => u.Username == username)
                       ?? throw new Exception("No User Logged in");
            var orders = await _orderRepository.GetAll();
            orders = orders.Where(o => o.UserId == user.UserId);
            if (orders == null)
            {
                throw new Exception("No orders in the db");
            }
            return orders;
        }
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _orderRepository.Get(id);
            if (order == null)
            {
                throw new Exception($"No order foudn by the id: {id}");
            }
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll()).SingleOrDefault(u => u.Username == username)
                    ?? throw new Exception("No User Logged in");

            if (order.UserId != user.UserId)
                throw new UnauthorizedAccessException("You cannot get someone else's order.");
            return order;
        }
        public async Task<bool> DeleteOrder(int orderId)
        {
            var order = await _orderRepository.Get(orderId)
                        ?? throw new Exception("Order not found");

            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = (await _userRepository.GetAll()).SingleOrDefault(u => u.Username == username)
                    ?? throw new Exception("No User Logged in");

            if (order.UserId != user.UserId)
                throw new UnauthorizedAccessException("You cannot delete someone else's order.");

            var orderDetails = (await _orderDetailRepository.GetAll())
                            .Where(od => od.OrderID == orderId)
                            .ToList();

            foreach (var detail in orderDetails)
            {
                await _orderDetailRepository.Delete(detail.OrderDetailID);
            }

            await _orderRepository.Delete(orderId);

            return true;
        }
    }
}