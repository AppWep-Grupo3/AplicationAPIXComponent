using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;


namespace BackendXComponent.ComponentX.Services;

public class OrderDetailService : ImpOrderDetailService
{
    private readonly ImplOrderDetailRepository _orderDetailRepository;
    private readonly ImplUnitOfWork _unitOfWork;

    public OrderDetailService(ImplOrderDetailRepository orderDetailRepository, ImplUnitOfWork unitOfWork)
    {
        _orderDetailRepository = orderDetailRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderDetail>> ListAsync()
    {
        return await _orderDetailRepository.ListAsync();
    }

    public async Task<OrderDetailResponse> SaveAsync(OrderDetail orderDetail)
    {
        try
        {
            await _orderDetailRepository.AddAsync(orderDetail);
            await _unitOfWork.CompleteAsync();
            return new OrderDetailResponse(orderDetail);
        }
        catch (Exception e)
        {
            return new OrderDetailResponse($"An error occurred when saving the order detail: {e.Message}");
        }
    }

    public async Task<OrderDetailResponse> UpdateAsync(int id, OrderDetail orderDetail)
    {
        var existingOrderDetail = await _orderDetailRepository.FindByIdAsync(id);

        if (existingOrderDetail == null)
            return new OrderDetailResponse("Order detail not found.");

        // Update order detail properties as needed
        existingOrderDetail.Quantity = orderDetail.Quantity;
        existingOrderDetail.UnitPrice = orderDetail.UnitPrice;

        try
        {
            _orderDetailRepository.Update(existingOrderDetail);
            await _unitOfWork.CompleteAsync();
            return new OrderDetailResponse(existingOrderDetail);
        }
        catch (Exception e)
        {
            return new OrderDetailResponse($"An error occurred when updating the order detail: {e.Message}");
        }
    }

    public async Task<OrderDetailResponse> DeleteAsync(int id)
    {
        var existingOrderDetail = await _orderDetailRepository.FindByIdAsync(id);

        if (existingOrderDetail == null)
            return new OrderDetailResponse("Order detail not found.");

        try
        {
            _orderDetailRepository.Remove(existingOrderDetail);
            await _unitOfWork.CompleteAsync();
            return new OrderDetailResponse(existingOrderDetail);
        }
        catch (Exception e)
        {
            return new OrderDetailResponse($"An error occurred when deleting the order detail: {e.Message}");
        }
    }
}