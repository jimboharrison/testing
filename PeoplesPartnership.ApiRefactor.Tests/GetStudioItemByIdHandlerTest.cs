using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.Database.Models;
using PeoplesPartnership.ApiRefactor.DTOs;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.Handlers;
using PeoplesPartnership.ApiRefactor.Handlers.AddStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.GetStudioItem;

namespace PeoplesPartnership.ApiRefactor.Tests;

public class GetStudioItemByIdHandlerTest
{
    private readonly GetStudioItemByIdHandler _sut;
    private readonly Mock<StudioContext> _mockContext;
    
    public GetStudioItemByIdHandlerTest()
    {
        var mapper = CreateAutoMapperWithProfile();
        _mockContext = new Mock<StudioContext>();

        _sut = new GetStudioItemByIdHandler(mapper, _mockContext.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenItemDoesNotExist()
    {
        //Arrange
        var mockStudioItems = new List<StudioItem>
        {
            new StudioItem { StudioItemId = 2, Name = "ExistingItem", Description = "Description 1" , SerialNumber = "123" }
        }.AsQueryable();

        _mockContext.Setup<DbSet<StudioItem>>(x => x.StudioItems).ReturnsDbSet(mockStudioItems);

        //Act
        var response = await _sut.GetStudioItemById(1);

        //Assert
        response.Success.Should().BeFalse();
        response.Type.Should().Be(ServiceResponseType.NotFound);
        response.Message.Should().Be("Studio Item not found");
        response.Data.Should().BeNull();
    }
    
    [Fact]
    public async Task GetStudioItem_ShouldReturnSuccess_WhenItemExists()
    {
        //Arrange
        var mockStudioItems = new List<StudioItem>
        {
            new StudioItem { StudioItemId = 2, Name = "ExistingItem", Description = "Description 1" , SerialNumber = "123" }
        }.AsQueryable();
    
        _mockContext.Setup<DbSet<StudioItem>>(x => x.StudioItems).ReturnsDbSet(mockStudioItems);
    
        //Act
        var response = await _sut.GetStudioItemById(2);

        //Assert
        response.Success.Should().BeTrue();
        response.Type.Should().Be(ServiceResponseType.Success);
        response.Message.Should().Be("Here's your selected studio item");
        response.Data.StudioItemId.Should().Be(2);
        response.Data.Name.Should().Be("ExistingItem");
        response.Data.Description.Should().Be("Description 1");
        response.Data.SerialNumber.Should().Be("123");
    }

    private static Mapper CreateAutoMapperWithProfile()
    {
        var autoMapperProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        return new Mapper(configuration);
    }
}