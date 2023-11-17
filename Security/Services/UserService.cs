using AutoMapper;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Security.Authorization.Handlers.Interfaces;
using BackendXComponent.Security.Domain.Models;
using BackendXComponent.Security.Domain.Repositories;
using BackendXComponent.Security.Domain.Services;
using BackendXComponent.Security.Domain.Services.Communication;
using BackendXComponent.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BackendXComponent.Security.Services;

public class UserService: ImplUserService
{
    private readonly ImplUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ImplUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    

    
   
    public UserService(ImplUserRepository userRepository, IMapper mapper, ImplUnitOfWork unitOfWork, IJwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
    }
    
    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);
        Console.WriteLine($"Request: {request.Email},{request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName},{user.LastName}, {user.Email}, {user.Password}");

        // validate
        if (user == null || !BCryptNet.Verify(request.Password,
                user.Password))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }

        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.FirstName},{response.LastName}, {response.Email}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async  Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user==null) throw new KeyNotFoundException("User not found");
        return user;
    }
    
    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user==null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task<IEnumerable<User>> GetUserByEmailAndPassword(string email, string password)
    {
        return await _userRepository.GetUserByEmailAndPassword(email, password);
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // validate
        if (_userRepository.ExistsByEmail(request.Email)) 
            throw new AppException("Username '" + request.Email + "' is already taken");
        // map model to new user object
        var user = _mapper.Map<User>(request);
        // hash password
        user.Password = BCryptNet.HashPassword(request.Password);
        // save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }

    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);
        // Validate
        if (_userRepository.ExistsByEmail(request.Email))
            throw new AppException("Username '" + request.Email + "' is already taken");
        // Hash password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.Password = BCryptNet.HashPassword(request.Password);
        // Copy model to user and save
        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
}