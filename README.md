# !NOTE:
It is recommended now to utilize the MSAL .net library : https://github.com/AzureAD/microsoft-authentication-library-for-dotnet

This console app may work with the packages :
```
<packages>
  <package id="BouncyCastle.Crypto.dll" version="1.8.1" targetFramework="net461" />
  <package id="Microsoft.IdentityModel" version="6.1.7600.16394" targetFramework="net461" />
  <package id="Microsoft.IdentityModel.Clients.ActiveDirectory" version="4.3.0" targetFramework="net461" />
  <package id="Microsoft.IdentityModel.JsonWebTokens" version="5.3.0" targetFramework="net461" />
  <package id="Microsoft.IdentityModel.Logging" version="5.3.0" targetFramework="net461" />
  <package id="Microsoft.IdentityModel.Tokens" version="5.3.0" targetFramework="net461" />
  <package id="Newtonsoft.Json" version="11.0.2" targetFramework="net461" />
  <package id="System.IdentityModel.Tokens.Jwt" version="5.3.0" targetFramework="net461" />
</packages>
```

But your mileage may vary. See updates for the ADAL library here: 
https://github.com/AzureAD/azure-activedirectory-library-for-dotnet


# ADAL-.NET-Console-Application
This is an ADAL .net Console application that will get 5 users from the Microsoft Graph in the tenant that you define in the variables for the program.cs file.

These variables are :
```
        private static string resourceUri = "https://graph.microsoft.com";
        private static string clientId = "<Replace With Your Client ID> ";
        private static string redirectUri = "<Replace with your Redirect URI> ";
        private static string authority = "https://login.microsoftonline.com/";
        private static string tenantID = "<Replace with your TenantID>";
```

They are at the top of the Program.CS file in the repo. https://github.com/frankhu1234/ADAL-.NET-Console-Application/blob/master/MyTestADALConsoleApp/Program.cs

The creation of this Application is explained in the AAD Dev Support blogs : https://blogs.msdn.microsoft.com/aaddevsup/2018/11/07/how-to-use-the-adal-net-library-to-call-the-microsoft-graph-api-in-a-console-application-using-authorization-code-flow/

This will get an access token using ADAL V4.3 and then get 5 users from the Microsoft Graph API 

In addition to that, if you're interested in the Microsoft Graph SDK, feel free to follow this blog post to learn how to do that to extend off of this github repo:  
https://blogs.msdn.microsoft.com/aaddevsup/2018/11/07/how-to-use-the-net-microsoft-graph-sdk-to-get-users-and-get-next-page-of-results/

# Support

If there are any issues in regards to this sample, please file a GitHub issue. This sample is not supported by Azure Support or Microsoft. 

