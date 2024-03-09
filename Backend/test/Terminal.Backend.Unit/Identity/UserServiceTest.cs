// using System.Text;
// using JetBrains.Annotations;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Routing;
// using Microsoft.AspNetCore.WebUtilities;
// using Microsoft.Extensions.DependencyInjection;
// using NSubstitute;
// using Terminal.Backend.Infrastructure.Identity;
// using Xunit;
//
// namespace Terminal.Backend.Unit.Identity;
//
// [TestSubject(typeof(UserService))]
// public class UserServiceTest
// {
//     private readonly UserManager<ApplicationUser> _userManagerMock;
//     private readonly IEmailSender<ApplicationUser> _emailSenderMock;
//     private readonly LinkGenerator _linkGeneratorMock;
//
//     public UserServiceTest()
//     {
//         _userManagerMock = Substitute.For<UserManager<ApplicationUser>>();
//         _emailSenderMock = Substitute.For<IEmailSender<ApplicationUser>>();
//         _linkGeneratorMock = Substitute.For<LinkGenerator>();
//     }
//     
//     [Fact]
//     public async void RegisterAsync_Should_NotThrow_WhenEmailIsUniqueAndPasswordMatchesPolicy()
//     {
//         var id = Guid.NewGuid();
//         const string email = "test@gmail.com";
//         const string password = "StrongPassword1#";
//         const string code = "code";
//         var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
//         
//         _userManagerMock.FindByEmailAsync(email).Returns((ApplicationUser?)null);
//         _userManagerMock.CreateAsync(new ApplicationUser()).ReturnsForAnyArgs(IdentityResult.Success);
//         _userManagerMock.GetUserIdAsync(new ApplicationUser()).ReturnsForAnyArgs(id.ToString());
//         _userManagerMock.GenerateEmailConfirmationTokenAsync(new ApplicationUser()).ReturnsForAnyArgs(code);
//         _linkGeneratorMock.GetUriByName(new DefaultHttpContext(), "Email confirmation endpoint", )
//         
//         var userService = new UserService(_userManagerMock, _emailSenderMock, _linkGeneratorMock);
//         
//         await userService.RegisterAsync(email, password);
//     }
// }