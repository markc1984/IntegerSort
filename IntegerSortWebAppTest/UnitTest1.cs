using Xunit;
using IntegerSortWebApp;
using IntegerSortWebApp.Controllers;
using IntegerSortWebApp.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

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
            // Assert - If the pages return 200 successful, error handling where no ID provided is valid
            Assert.Equal(200, code);
        }
}


}