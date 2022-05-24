using Xunit;
using IntegerSortWebApp;
using IntegerSortWebApp.Controllers;
using IntegerSortWebApp.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace IntegerSortWebAppTest
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {         
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        
        [Theory]
        [InlineData("/")]
        [InlineData("/Sorts/Index")]
        [InlineData("/Sorts/CreateSort")]
        [InlineData("/Sorts/RemoveSort")]
        [InlineData("/Sorts/ExportJSON")]
        [InlineData("/Number/Index")]
        [InlineData("/Number/AddNumbers")]
        [InlineData("/Number/EditNumber")]

        public async void Test_That_All_Pages_Load(string URL)
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;
            // Assert - Return 200 successful for all pages
            Assert.Equal(200, code);
        }

        [Fact]
        public async void Test_Page_Content()
        {
            //IFormCollection form = new IFormCollection();
        }
}


}