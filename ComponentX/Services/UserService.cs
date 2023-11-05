using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;

namespace BackendXComponent.ComponentX.Services;

public class UserService : ImplUserService
{

    public readonly ImplUserRespository _userRespository;
    public readonly ImplUnitOfWork _unitOfWork;
    
    public UserService(ImplUserRespository UserRespository, ImplUnitOfWork unitOfWork)
    {
        _userRespository = UserRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRespository.ListAsync() ;
    }

    public async Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password)
    {
        return await _userRespository.GetUserByEmailAndPassword(email, password);
    }
    
    
    public async Task<UserResponse> SaveAsync(User user)
    {
        var existingUser = await _userRespository.FindByEmailAsync(user.Email);
        if (existingUser != null)
            return new UserResponse("User with this email: "+ user.Email + " already exists.");
        try
        {
            await _userRespository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(user);

        }
        catch (Exception e)
        {
            return new UserResponse("$\"An error occurred when saving the category: {e.Message}\"");

        }
        
    }
    

    
    public async Task<UserResponse> UpdateAsync(int id, User user)
    {
        var existingUser = await _userRespository.FindByIdAsync(id);
        if (existingUser == null)
            return new UserResponse("User not found.");
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.Password= user.Password;
        try
        {
            _userRespository.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred when updating the category: {e.Message}");
        }
    }
    
    public async Task<UserResponse> DeleteAsync(int id)
    {
        var existingUser = await _userRespository.FindByIdAsync(id);
        if (existingUser == null)
            return new UserResponse("User not found.");
        try
        {
            _userRespository.Delete(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse(" $\"An error occurred when deleting the user: {e.Message}\"");
        }
    }
}