using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

namespace BackendXComponent.ComponentX.Services;

public class OrderService : ImplOrderService
{
    public readonly ImplOrderRepository _orderRepository;
    public readonly ImplUnitOfWork _unitOfWork;
    
    public OrderService(ImplOrderRepository orderRepository, ImplUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _orderRepository.ListAsync();
    }
    
    public async Task<OrderResponse> SaveAsync(Order order)
    {
        var existingOrder = await _orderRepository.FindByDateAsync(order.Date);
        if (existingOrder != null)
            return new OrderResponse("The order in the date: "+ order.Date + " already exists.");
        try
        {
            await _orderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return new OrderResponse(order);
        }
        catch (Exception e)
        {
            return new OrderResponse("$\"An error occurred when saving the category: {e.Message}\"");
        }
    }

    public async Task<OrderResponse> UpdateAsync(int id, Order order)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);
        if(existingOrder==null)
            return new OrderResponse("Order not found.");
        existingOrder.Date= order.Date;
        existingOrder.Status= order.Status;
        try
        {
            _orderRepository.Update(existingOrder);
            await _unitOfWork.CompleteAsync();
            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred when saving the category: {e.Message}");
        }
    }

    public async Task<OrderResponse> DeleteAsync(int id)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);
        if (existingOrder == null)
            return new OrderResponse("order not found");
        try
        {
            _orderRepository.Delete(existingOrder);
            await _unitOfWork.CompleteAsync();
            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse("$\"An error occurred when deleting the order: {e.Message}\"");
        }
    }
}