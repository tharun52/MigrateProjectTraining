using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> PlaceOrder(OrderRequestDto orderDto);
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<Order> GetOrderById(int id);
        public Task<bool> DeleteOrder(int orderId);
    }
}