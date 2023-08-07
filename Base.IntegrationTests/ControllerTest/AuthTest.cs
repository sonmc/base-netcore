using Base.IntegrationTests;

namespace Kidsenglish.IntergrationTests
{
    public class AuthTest : BaseTest
    {

        //[Fact]
        //public async void GetToken_WithCorrectAccount_ResultToken()
        //{
        //    // Arrange 
        //    Authenticate model = md.GetAdminAccount();
        //    // Act 
        //    string token = await GetToken(model);
        //    // Assert
        //    Assert.NotNull(token);
        //}

        //[Fact]
        //public async void Login_WithCorrectToken_ResultUser()
        //{
        //    // Arrange 
        //    Authenticate model = md.GetAdminAccount();
        //    string token = await GetToken(model);

        //    // Act
        //    var response = await GetAsync("api/auth/login", token);
        //    response.EnsureSuccessStatusCode();
        //    string jsonString = await response.Content.ReadAsStringAsync();
        //    AuthenticateDto user = JsonSerializer.Deserialize<AuthenticateDto>(jsonString);
        //    // Assert
        //    Assert.NotNull(user);
        //}

        //[Fact]
        //public async void CheckToken_WithCorrectToken_ResultBoolean()
        //{
        //    // Arrange 
        //    Authenticate model = md.GetAdminAccount();
        //    string token = await GetToken(model);

        //    // Act
        //    var response = await PostAsync("api/auth/checkToken", new { token = token });
        //    response.EnsureSuccessStatusCode();
        //    string jsonString = await response.Content.ReadAsStringAsync();
        //    Dto.TokenValid tv = JsonSerializer.Deserialize<Dto.TokenValid>(jsonString);

        //    // Assert
        //    Assert.True(tv.tokenValid);
        //}

        //[Fact]
        //public async void Logout_WithCorrectData_ResultBoolean()
        //{
        //    // Arrange  
        //    Authenticate auth = md.GetAdminAccount();
        //    string token = await GetToken(auth);
        //    object model = new
        //    {
        //        userId = 1,
        //        deviceTokenString = token,
        //        isAndroidDevice = false,
        //        domain = "CMS"
        //    };
        //    // Act
        //    var response = await PostAsync("api/auth/logout", model);
        //    response.EnsureSuccessStatusCode();
        //    string jsonString = await response.Content.ReadAsStringAsync();
        //    bool isLogouted = JsonSerializer.Deserialize<bool>(jsonString);

        //    // Assert
        //    Assert.True(isLogouted);
        //}

        //[Fact]
        //public async void ChangePassword_WithCorrectParentId_ResultBoolean()
        //{
        //    // Arrange  
        //    Authenticate auth = md.GetAdminAccount();
        //    string token = await GetToken(auth);
        //    object model = new
        //    {
        //        parentId = 1,
        //        password = "abc123"
        //    };

        //    // Act
        //    var response = await PostAsync("api/auth/changePassword", model, token);
        //    response.EnsureSuccessStatusCode();
        //    string jsonString = await response.Content.ReadAsStringAsync();
        //    StatusResponse sp = JsonSerializer.Deserialize<StatusResponse>(jsonString);

        //    // Assert
        //    Assert.True(sp.status);
        //}


    }
}