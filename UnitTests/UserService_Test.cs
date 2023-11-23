using Exchanger.Context;
using Exchanger.Models;
using Exchanger.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

// Добавьте необходимые директивы using

public class UserService_Test : IDisposable
{
    private readonly DbContextOptions<AppDbContext> _options;
    private readonly AppDbContext _dbContext;
    public UserService_Test()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
            .Options;

        _dbContext = new AppDbContext(_options);
    }

    [Fact]
    public async Task GetUsers_Test()
    {
        var userService = new UserService(_dbContext);
        var testUser = new User("TestUser");
        _dbContext.Users.Add(testUser);
        var testUser2 = new User("TestUser2");
        _dbContext.Users.Add(testUser2);
        var testUser3 = new User("TestUser3");
        _dbContext.Users.Add(testUser3);
        await _dbContext.SaveChangesAsync();


        var result = await userService.GetUsers();


        Assert.NotNull(result);
        Assert.IsType<List<User>>(result);
        Assert.True(result.Any());
    }

    [Fact]
    public async Task GetUser_Test()
    {
        var userService = new UserService(_dbContext);

        var testUser = new User("TestUser");
        _dbContext.Users.Add(testUser);
        await _dbContext.SaveChangesAsync();

        var result = await userService.GetUser(testUser.Id);

        Assert.NotNull(result);
        Assert.IsType<User>(result);
        Assert.Equal(testUser.Id, result.Id);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_Test()
    {
        var userService = new UserService(_dbContext);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await userService.GetUser(Guid.NewGuid()); 
        });
        
    }

    [Fact]
    public async Task Create_Test()
    {
        var userService = new UserService(_dbContext);

        await userService.Create(new User("NewUser"));

        var newUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == "NewUser");
        Assert.NotNull(newUser);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
